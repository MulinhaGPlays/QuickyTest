using System.Text;

namespace QuickyTest.Infra.Services;

public class JsonValidator
{
    public static string CloseJson(string json)
    {
        var variants = new Dictionary<char, char>
        {
            { '"', '"' },
            { '{', '}' },
            { '[', ']' },
        };

        string based = json;
        Dictionary<char, bool> results = new();
        while (results.Count == 0 || results.Any(x => x.Value)) 
        {
            Dictionary<char, int> indexes = new();

            bool verify(char c1, char c2) 
                => ((based.Count(x => x == c1) + based.Count(x => x == c2 && c2 != '"')) % 2) is 1 
                || based.Count(x => x == c1) != based.Count(x => x == c2);

            foreach (var c in results.Where(x => x.Value is not false))
            {
                var dic = variants.FirstOrDefault(x => x.Key == c.Key);
                int calc = c.Key == '"'
                    ? based.Count(x => x == dic.Key) % 2
                    : based.Count(x => x == dic.Key) 
                    - based.Count(x => x == dic.Value);

                if (calc >= 1)
                {
                    int lastIndex = based.LastIndexOf(c.Key);
                    indexes[c.Key] = lastIndex;
                }
            }

            based = indexes.Count > 0 ? based[..indexes.Values.Max()] : based;

            json = indexes.OrderByDescending(x => x.Value).FirstOrDefault().Key switch
            {
                '{' => String.Join(String.Empty, json.Append('}')),
                '[' => String.Join(String.Empty, json.Append(']')),
                '"' => String.Join(String.Empty, json.Append('"')),
                _ => json,
            };

            foreach (var variant in variants)
                results[variant.Key] = verify(variant.Key, variant.Value);
        }
        return json;
    }
}

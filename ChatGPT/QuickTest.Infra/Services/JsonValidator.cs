using System.Text.RegularExpressions;

namespace QuickyTest.Infra.Services;

public partial class JsonValidator
{
    public static string CloseJson(string json)
    {
        var variants = new Dictionary<char, char> { { '"', '"' }, { '{', '}' }, { '[', ']' } };
        Dictionary<char, bool> results = new();
        string based = json;

        while (results.Count == 0 || results.Any(x => x.Value)) 
        {
            Dictionary<char, int> indexes = new();

            bool verify(char c1, char c2) 
                => (json.Count(x => x == c1) 
                + json.Count(x => x == c2 && c2 != '"')) % 2 is 1 
                || json.Count(x => x == c1) 
                != json.Count(x => x == c2);

            foreach (var c in results.Where(x => x.Value is not false))
            {
                var dic = variants.FirstOrDefault(x => x.Key == c.Key);
                int calc = c.Key == '"'
                    ? json.Count(x => x == dic.Key) % 2
                    : json.Count(x => x == dic.Key) 
                    - json.Count(x => x == dic.Value);

                if (calc >= 1)
                {
                    while (based.LastIndexOf(dic.Key) < based.LastIndexOf(dic.Value))
                        based = based[..based.LastIndexOf(dic.Key)];
                    int lastIndex = based.LastIndexOf(c.Key);
                    indexes[c.Key] = lastIndex;
                }
            }

            based = indexes.Count > 0 ? based[..indexes.Values.Max()] : based;

            json = indexes.OrderByDescending(x => x.Value).FirstOrDefault().Key switch
            {
                '{' => string.Join(string.Empty, json.Append('}')),
                '[' => string.Join(string.Empty, json.Append(']')),
                '"' => string.Join(string.Empty, json.Append('"')),
                _ => json,
            };

            foreach (var variant in variants)
                results[variant.Key] = verify(variant.Key, variant.Value);
        }
        return json;
    }

    public static string RemoveSpacesOutsideQuotes(string input)
    { 
        Regex regex = RemoveSpacesOutsideQuotes();
        string result = regex.Replace(input, "");
        return result;
    }

    [GeneratedRegex("\\s+(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)")]
    private static partial Regex RemoveSpacesOutsideQuotes();
}

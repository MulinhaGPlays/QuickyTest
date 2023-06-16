using System.Text.RegularExpressions;

namespace QuickyTest.Infra.Services;

public partial class JsonValidator
{
    public static string CloseJson(string json)
    {
        bool verify(char c1, char c2) => (json.Count(x => x == c1) + json.Count(x => x == c2 && c2 != '"')) % 2 is 1 
            || json.Count(x => x == c1) != json.Count(x => x == c2);
        var variants = new Dictionary<char, char> { { '"', '"' }, { '{', '}' }, { '[', ']' } };
        Dictionary<char, bool> results = new();
        json = json.Replace("\r\n", String.Empty);
        string based = json;
        while (results.Count == 0 || results.Any(x => x.Value))
        {
            if (!(results.Count == 0 || results.Any(x => x.Value))) break;
            Dictionary<char, int> indexes = new();
            foreach (KeyValuePair<char, bool> c in results.Where(x => x.Value is not false))
            {
                KeyValuePair<char, char> dic = variants.FirstOrDefault(x => x.Key == c.Key);
                int calc = c.Key == '"' ? json.Count(x => x == dic.Key) % 2
                    : json.Count(x => x == dic.Key) - json.Count(x => x == dic.Value);
                if (calc >= 1)
                {
                    while (based.LastIndexOf(dic.Key) < based.LastIndexOf(dic.Value))
                        based = based[..based.LastIndexOf(dic.Key)];
                    int lastIndex = based.LastIndexOf(c.Key);
                    indexes[c.Key] = lastIndex;
                }
            }
            based = indexes.Count > 0 ? json[..indexes.Values.Max()] : based;
            json = indexes.OrderByDescending(x => x.Value).FirstOrDefault().Key switch
            {
                '{' => string.Join(string.Empty, json.Append('}')),
                '[' => string.Join(string.Empty, json.Append(']')),
                '"' => string.Join(string.Empty, json.Append('"')),
                _ => json,
            };
            foreach (KeyValuePair<char, char> variant in variants)
                results[variant.Key] = verify(variant.Key, variant.Value);
        }
        return json;
    }
}

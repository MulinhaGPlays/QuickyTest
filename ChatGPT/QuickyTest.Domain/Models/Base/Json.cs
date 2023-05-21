using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace QuickyTest.Domain.Models.Base
{
    public class Json<TModel> where TModel : class
    {
        protected TModel? _model;

        protected JsonSerializerOptions GetJsonSerializeOptions()
        {
            var options = new JsonSerializerOptions();
            options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            options.PropertyNameCaseInsensitive = true;
            return options;
        }

        public override string ToString() => JsonSerializer.Serialize(_model, GetJsonSerializeOptions());
    }
}

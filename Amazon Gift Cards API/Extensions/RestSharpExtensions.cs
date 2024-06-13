using Newtonsoft.Json;
using RestSharp;
using System.Text;


namespace Amazon_Gift_Cards_API.Extensions
{
    public static class RestSharpExtensions
    {
        public static IEnumerable<Parameter> Headers(this RestRequest request)
        {
            return request.Parameters.Where(p => p.Type == ParameterType.HttpHeader).OrderBy(p => p.Name.ToLower());
        }

        public static String RequestBody(this RestRequest request)
        {
            var parameter = request.Parameters.First(p => p.Type == ParameterType.RequestBody);

            if (parameter != null)
            {
                if (parameter.Value is string)
                {
                    // If the value is already a string (e.g., raw JSON), return it
                    return (string)parameter.Value;
                }
                else
                {
                    // Serialize the object to JSON
                    var jsonSerializer = new JsonSerializer();
                    var stringBuilder = new StringBuilder();
                    using var stringWriter = new StringWriter(stringBuilder);
                    jsonSerializer.Serialize(stringWriter, parameter.Value);
                    return stringBuilder.ToString();
                }
            }

            return String.Empty;
        }

        public static String AbsoluteResource(this RestRequest request)
        {
            if (request.Resource.StartsWith("/"))
                return request.Resource;

            return String.Format("/{0}", request.Resource);
        }
    }
}

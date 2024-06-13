using Amazon.Runtime;
using Amazon.Util;
using Amazon_Gift_Cards_API.Extensions;
using RestSharp;
using System.Text;

namespace Amazon_Gift_Cards_API.Authentication
{
    public class AWSAuthenticator(IConfiguration configuration, string awsAccessKey, string awsAccessSecret, string region, string serviceName)
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly string _region = region;
        private readonly string _serviceName = serviceName;
        private readonly string _serviceNamespace = "com.amazonaws.agcod";
        private readonly ImmutableCredentials _credentials = new(awsAccessKey, awsAccessSecret, null);

        public void Authenticate(RestRequest request)
        {
            var date = DateTime.UtcNow;

            request.AddHeader("Host", new Uri(_configuration.GetValue<string>("Amazon:AGCODUrl")).Host);
            request.AddHeader("X-Amz-Date", date.ToString(AWSSDKUtils.ISO8601BasicDateTimeFormat));

            var signedHeaders = BuildSignedHeadersString(request).Trim();
            var canonicalRequest = BuildCanonicalRequest(request).Trim();
            var hashedCanonicalRequest = canonicalRequest.Sha256().ToHex().Trim();
            var scopeString = BuildScopeString(date).Trim();
            var stringToSign = BuildStringToSign(date, scopeString, hashedCanonicalRequest).Trim();
            var signingKey = BuildSigningKey(date);
            var signature = stringToSign.HmacSha256(signingKey).ToHex().Trim();
            var authString = BuildAuthorizationString(signedHeaders, scopeString, signature).Trim();

            request.AddHeader("Authorization", authString);
        }

        private static string BuildSignedHeadersString(RestRequest request)
        {
            var headers = request.Headers().OrderBy(x => x.Name.ToLower()).Select(x => x.Name.ToLower());

            return String.Join(";", headers);
        }

        private static string BuildCanonicalRequest(RestRequest request)
        {
            var builder = new StringBuilder();

            builder.AppendFormat("{0}\n", request.Method.ToString().ToUpper());
            builder.AppendFormat("{0}\n", request.AbsoluteResource());
            builder.Append('\n');

            request.Headers()
                .OrderBy(x => x.Name.ToLower())
                .ToList()
                .ForEach(x => builder.AppendFormat("{0}:{1}\n", x.Name.ToLower(), x.Value.ToString()));

            builder.Append('\n');
            builder.AppendFormat("{0}\n", BuildSignedHeadersString(request));

            builder.Append(request.RequestBody().Sha256().ToHex());

            return builder.ToString();
        }

        private string BuildScopeString(DateTime date)
        {
            var format = "{0}/{1}/{2}/{3}";

            return String.Format(format, date.ToString("yyyyMMdd"), _region, _serviceName, AwsConstants.TERMINATOR);
        }

        private static string BuildStringToSign(DateTime date, string scopeString, string hashedCanonicalRequest)
        {
            var format = "{0}-{1}\n{2}\n{3}\n{4}";
            var dateString = date.ToString(AWSSDKUtils.ISO8601BasicDateTimeFormat);

            return String.Format(
                    format, AwsConstants.SCHEME, AwsConstants.ALGORITHM, dateString, scopeString, hashedCanonicalRequest
                );
        }

        private byte[] BuildSigningKey(DateTime date)
        {
            var dateString = date.ToString("yyyyMMdd");

            var kSecret = Encoding.UTF8.GetBytes(AwsConstants.SCHEME + _credentials.SecretKey);
            var kDate = dateString.HmacSha256(kSecret);
            var kRegion = _region.HmacSha256(kDate);
            var kService = _serviceName.HmacSha256(kRegion);

            return AwsConstants.TERMINATOR.HmacSha256(kService);
        }

        private string BuildAuthorizationString(string signedHeaders, string scopeString, string signature)
        {
            var format = "{0}-{1} Credential={2}/{3}, SignedHeaders={4}, Signature={5}";

            return String.Format(
                    format, AwsConstants.SCHEME, AwsConstants.ALGORITHM, _credentials.AccessKey, scopeString, signedHeaders, signature
                );
        }
    }
}

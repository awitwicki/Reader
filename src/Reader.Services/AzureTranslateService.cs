using System.Net;
using System.Text;
using Newtonsoft.Json;
using Reader.Domain.Enums;
using Reader.Domain.Interfaces;

namespace Reader.Services;

public class AzureTranslateService : ITranslateService
{
    private const string Key = "key";
    private const string Endpoint = "https://api.cognitive.microsofttranslator.com";
    private const string Location = "region";
    
    /// <summary>
    /// The C# classes that represents the JSON returned by the Translator Text API.
    /// </summary>
    public class TranslationResult
    {
        public DetectedLanguage DetectedLanguage { get; set; }
        public TextResult SourceText { get; set; }
        public Translation[] Translations { get; set; }
    }

    public class DetectedLanguage
    {
        public string Language { get; set; }
        public float Score { get; set; }
    }

    public class TextResult
    {
        public string Text { get; set; }
        public string Script { get; set; }
    }

    public class Translation
    {
        public string Text { get; set; }
        public TextResult Transliteration { get; set; }
        public string To { get; set; }
        public Alignment Alignment { get; set; }
        public SentenceLength SentLen { get; set; }
    }

    public class Alignment
    {
        public string Proj { get; set; }
    }

    public class SentenceLength
    {
        public int[] SrcSentLen { get; set; }
        public int[] TransSentLen { get; set; }
    }
    
    public async Task<string> Translate(string input, Language language)
    {
        try
        {
            // TODO add lang selector
            // Input and output languages are defined as parameters.
            var route = "/translate?api-version=3.0&from=en&to=uk";
            var body = new object[] { new { Text = input } };
            var requestBody = JsonConvert.SerializeObject(body);

            using var client = new HttpClient();
            using var request = new HttpRequestMessage();
            
            // Build the request.
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(Endpoint + route);
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            request.Headers.Add("Ocp-Apim-Subscription-Key", Key);
            request.Headers.Add("Ocp-Apim-Subscription-Region", Location);

            // Send the request and get response.
            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return "Unauthorized (Wrong azure cognitive services api key)";
            }
            
            // Read response as a string.
            var result = await response.Content.ReadAsStringAsync();

            var deserializedResult = JsonConvert.DeserializeObject<TranslationResult[]>(result);

            return deserializedResult[0].Translations[0].Text;
        }
        catch (Exception e)
        {
            return e.ToString();
        }
    }
}

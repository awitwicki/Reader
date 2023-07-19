using System.Collections;
using Newtonsoft.Json;
using Reader.Domain.Enums;
using Reader.Domain.Interfaces;

namespace Reader.Services;

public class TranslateService : ITranslateService
{
    public async Task<string> Translate(string input, Language language)
    {
        // TODO refact
        // Set the language from/to in the url (or pass it into this function)
        var url = String.Format
        ("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
            "en", "uk", Uri.EscapeUriString(input));
        
        var httpClient = new HttpClient();
        var response = await httpClient.GetStringAsync(url);

        var deserialized = JsonConvert.DeserializeObject<List<dynamic>>(response);
        
        // Extract just the first array element (This is the only data we are interested in)
        var translationItems = deserialized[0];

        // Translation Data
        var translation = "";

        // Loop through the collection extracting the translated objects
        foreach (object item in translationItems)
        {
            // Convert the item array to IEnumerable
            var translationLineObject = item as IEnumerable;

            // Convert the IEnumerable translationLineObject to a IEnumerator
            var translationLineString = translationLineObject.GetEnumerator();

            // Get first object in IEnumerator
            translationLineString.MoveNext();

            // Save its value (translated text)
            translation += string.Format(" {0}", Convert.ToString(translationLineString.Current));
        }

        // Remove first blank character
        if (translation.Length > 1)
        {
            translation = translation.Substring(1);
        }

        // Return translation
        return translation;
    }
}

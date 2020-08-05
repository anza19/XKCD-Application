using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Diagnostics;

namespace XKCDComics
{

    class Comic
    {
        [JsonPropertyName("img")]
        public string ImageURL { get; set; }
    }

    class Program
    {

        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the comic you want to read:");
            var comicNumber = Console.ReadLine();

            //simple application and returns an XKCD based on the comic number the user enters
            HttpClient client = new HttpClient();

            //clear default headers
            client.DefaultRequestHeaders.Clear();

            //add JSON headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //get the url that the user will enter in the browser to see comic
            var req = await client.GetStreamAsync($"http://xkcd.com/{comicNumber}/info.0.json");

            //deserialize to get the comic link
            var comic = await JsonSerializer.DeserializeAsync<Comic>(req);

            Console.WriteLine("Here is the link to paste in the browser to view the comic:");
            Console.WriteLine(comic.ImageURL);

            //we will launch the comic in the default browser the user uses
            var psi = new ProcessStartInfo
            {
                FileName = comic.ImageURL,
                UseShellExecute = true
            };
            Process.Start(psi);

        }
    }
}

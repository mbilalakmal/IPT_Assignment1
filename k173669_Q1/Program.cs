using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace k173669_Q1
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {

            if (args == null || args.Length != 2)
            {
                throw new ApplicationException("Please specify the URL of the website to retreive and a valid output directory.");
            }

            // Validate the first argument as a URL. If a valid URL, it is returned as uriResult.
            bool result = Uri.TryCreate(args[0], UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (result == false)
                throw new ApplicationException($"{args[0]} is not a valid URL.");

            /// Validate the second argument as an exisiting directory path.
            string directoryPath = args[1];
            if (Directory.Exists(directoryPath) == false)
                throw new ApplicationException($"{args[1]} is not an existing directory.");

            try
            {
                string responseBody = await client.GetStringAsync(uriResult);

                string fileName = "Summary " +  DateTime.Now.ToLongDateString();

                using (StreamWriter outputFile = new StreamWriter(
                    Path.Combine(directoryPath, fileName)
                    ))
                {
                    outputFile.Write(responseBody);
                }
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}

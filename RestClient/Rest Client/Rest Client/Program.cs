using System;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Nancy.Json;
using Newtonsoft.Json;

namespace Rest_Client
{
    class Program
    {
        /* static Task<HttpResponseMessage> responseMessage = null;*/
        static HttpClient client = new HttpClient();
        static RandomNumber random;
        static void Main(string[] args)
        {
           
            while (true)
            {
                Console.WriteLine("Give a Lower bound number");
                string low = Console.ReadLine();

                Console.WriteLine("Give a Higher bound number");
                string high = Console.ReadLine();
                Console.WriteLine("Start");
                string input = Console.ReadLine();
                if (input == "Start")
                {
                    requestandhandle(low, high);
                    //Console.WriteLine("Answer is:" + random);
                    if (random != null)
                    {
                        Console.WriteLine("Lower Number is:" + random.LowerNumber);
                        Console.WriteLine("Higher Number is:" + random.HigherNumber);
                        Console.WriteLine("Random Number is:" + random.Random);
                    }
                }
            }
        }

        static async void requestandhandle(string low, string high)
        {
            string adres = "http://127.0.0.1:8080/RandomValueInBound?";
            string lower = "Low=" + low;
            string Higher = "High=" + high;
            string complete = adres + lower +"&"+ Higher;
            
            HttpResponseMessage responseMessage = await client.GetAsync(complete);
            //RandomNumber random = JsonConvert.DeserializeObject<RandomNumber>(responseMessage.Result);
            //Task<String> random = responseMessage.Result.Content.ReadAsStringAsync();
            JavaScriptSerializer j = new JavaScriptSerializer();
            if (responseMessage.IsSuccessStatusCode)
            {
                random = new RandomNumber(responseMessage.Content.ReadAsStringAsync());
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using WebServiceDemo;
using Newtonsoft.Json;


namespace WSClientConsoleCore
{
    class Program
    {
        static void Main(string[] args)
        {
            const string serverUrl = "http://localhost:61172/";
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var response = client.GetAsync("api/demohotels").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string res = response.Content.ReadAsStringAsync().Result;
                        var hotels = JsonConvert.DeserializeObject<List<DemoHotel>>(res);
                        foreach (var hotel in hotels)
                        {
                            Console.WriteLine(hotel);
                        }
                    }

                    response = client.GetAsync("api/demohotels/5").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string res = response.Content.ReadAsStringAsync().Result;
                        var hotel = JsonConvert.DeserializeObject<DemoHotel>(res);
                        Console.WriteLine("");
                        Console.WriteLine("Get single hotel:");
                        Console.WriteLine(hotel);
                        Console.WriteLine("Rooms:");
                        foreach(DemoRoom dr in hotel.DemoRooms)
                        {
                            Console.WriteLine(dr);
                        }
                    }

                    response = client.DeleteAsync("api/demohotels/42").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var res = response.Content.ReadAsStringAsync().Result;
                        var hotel = JsonConvert.DeserializeObject<DemoHotel>(res);
                        Console.WriteLine("");
                        Console.WriteLine("Delete single hotel:");
                        Console.WriteLine(hotel);
                    }

                    DemoHotel dh = new DemoHotel() { Hotel_No = 42, Address = "Address 42", Name = "42nd hotel" };
                    String jsonStr = JsonConvert.SerializeObject(dh);
                    StringContent content = new StringContent(jsonStr, Encoding.ASCII, "application/json");
                    response = client.PostAsync("api/demohotels", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var res = response.Content.ReadAsStringAsync().Result;
                        var hotel = JsonConvert.DeserializeObject<DemoHotel>(res);
                        Console.WriteLine("");
                        Console.WriteLine("Create single hotel:");
                        Console.WriteLine(hotel);
                    }
                    else
                    {
                        Console.WriteLine("Failed to create single hotel");
                    }

                    dh = new DemoHotel() { Hotel_No = 42, Address = "Address 42 (update)", Name = "42nd hotel (update)" };
                    jsonStr = JsonConvert.SerializeObject(dh);
                    content = new StringContent(jsonStr, Encoding.ASCII, "application/json");
                    response = client.PutAsync("api/demohotels/42", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Updated single hotel");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update single hotel");
                    }
                }
                catch (Exception)
                {

                }
            }

            Console.ReadLine();

        }
    }
}

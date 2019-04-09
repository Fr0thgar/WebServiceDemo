using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using WebServiceDemo;
using Newtonsoft.Json;



namespace WSClientConsole
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
                        var hotels = response.Content.ReadAsAsync<IEnumerable<DemoHotel>>().Result;
                      
                        foreach (var hotel in hotels)
                        {
                            Console.WriteLine(hotel);               
                        }       
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Get single hotel");

                    response = client.GetAsync("api/demohotels/5").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var hotel = response.Content.ReadAsAsync<DemoHotel>().Result;

                        Console.WriteLine(hotel);

                        foreach (DemoRoom item in hotel.DemoRooms)
                        {
                            Console.WriteLine(item);
                        }
                     
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Single hotel");

                    response = client.DeleteAsync("api/demohotels/42").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var hotel = response.Content.ReadAsAsync<DemoHotel>().Result;

                        Console.WriteLine(hotel);

                    }


                    DemoHotel dh = new DemoHotel() { Hotel_No = 42, Address = "Address 42", Name = "42nd hotel" };

                    String jsonStr = JsonConvert.SerializeObject(dh);
                    StringContent content = new StringContent(jsonStr, Encoding.ASCII, "application/json");
                    HttpResponseMessage resp = client.PostAsync("api/demohotels", content).Result;
                   
                    /*if (resp.IsSuccessStatusCode)
                    {
                        
                        ok = true;
                    }
                    else
                    {
                        ok = false;
                    }*/




                    //DemoHotel dh =new DemoHotel() { Hotel_No = 42, Address = "Address 42", Name = "42nd hotel" };
                    //var responsex = client.PostAsync<DemoHotel>("api/DemoHotels", dh);

                    /*Console.WriteLine("");
                    Console.WriteLine("Single hotel");

                    response = client.DeleteAsync("api/demohotels/5").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var hotel = response.Content.ReadAsAsync<DemoHotel>().Result;

                        Console.WriteLine(hotel);

                    }*/

                }
                catch (Exception)
                {

                }
            }

            Console.ReadLine();
        }
    }
}

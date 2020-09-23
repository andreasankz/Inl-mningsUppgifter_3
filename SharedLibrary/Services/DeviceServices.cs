using AD = Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Services
{
    public static class DeviceServices
    {


        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string url = $"http://api.openweathermap.org/data/2.5/weather?q=%C3%96rebro&appid=b9f2df37033f5febf912e841800f475a&units=metric&cnt=6";

        public static async Task SendMessageAsync(DeviceClient deviceClient)
        {
            while (true)
            {
                try
                {
                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = JsonConvert.DeserializeObject<TemperaturModel.Root>(await response.Content.ReadAsStringAsync());

                        var data = JsonConvert.SerializeObject(json);


                        var payload = new Message(Encoding.UTF8.GetBytes(data));
                        await deviceClient.SendEventAsync(payload);

                        Console.WriteLine(data);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Something went wrong! {ex}");
                    
                }
                
                await Task.Delay(60 * 1000);
            }


        }

        public static async Task ReceiveMessageAsync(DeviceClient deviceClient)
        {
            while (true)
            {
                var payload = await deviceClient.ReceiveAsync();

                if (payload == null)
                    continue;

                Console.WriteLine($"Message Received: { Encoding.UTF8.GetString(payload.GetBytes())}");

                await deviceClient.CompleteAsync(payload);
            }

        }

        public static async Task SendMessageToDeviceAsync(AD.ServiceClient serviceClient, string targetdeviceId, string message)
        {
            var payload = new AD.Message(Encoding.UTF8.GetBytes(message));
            await serviceClient.SendAsync(targetdeviceId, payload);



        }
    }
}

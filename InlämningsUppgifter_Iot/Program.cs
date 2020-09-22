using System;
using Microsoft.Azure.Devices.Client;
using SharedLibrary.Models;
using SharedLibrary.Services;

namespace InlämningsUppgifter_Iot
{
    class Program
    {
        
        private static readonly DeviceClient deviceClient = DeviceClient.CreateFromConnectionString("HostName=ec-win20-iothuben.azure-devices.net;DeviceId=uppgifter3;SharedAccessKey=IxAGcpGfeE10TpIN0QfOXZkcc6ibWEdfomAmuKU2WLM=", TransportType.Mqtt);

        
        static void Main(string[] args)
        {
            
            DeviceServices.SendMessageAsync(deviceClient).GetAwaiter();
            DeviceServices.ReceiveMessageAsync(deviceClient).GetAwaiter();


            Console.ReadLine();
        }
    }
}

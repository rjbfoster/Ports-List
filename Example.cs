using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ports_List
{
    internal partial class Port
    {
        /*
         * This is an example method which deserializes ports.json into the 'Port' object.
         */
        public static void consumePortsList()
        {
            Console.WriteLine("Deserializing from local file ports.json...");
            List<Port> Ports = new();

            using (StreamReader sr = new StreamReader("ports.json"))
            {
                string jsonString = sr.ReadToEnd();
                Ports = JsonConvert.DeserializeObject<List<Port>>(jsonString);
            }

            foreach (var port in Ports)
            {
                // Output all retrieved objects to the console for demonstration purposes
                Console.WriteLine($"{port.PortNumber}/{port.Protocol}: {port.Description}");
            }

            Console.WriteLine("Deserialization complete. Press any key to exit.");
            Console.ReadKey();
        }
    }
}

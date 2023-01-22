using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ports_List
{
    internal class Port
    {
        /*
         * This is an example 'Port' class which uses the data extracted from the Registry.
         * This can easily be improved; a basic example of this would be transforming the
         * PortNumber property to an integer, or introducing an Enum for protocols. All
         * properties have been kept as strings with default values for ease of demonstration.
         */
        public string Service { get; set; } = "";
        public string Protocol { get; set; } = "";
        public string PortNumber { get; set; } = "";
        public string Description { get; set; } = "";
    }
}

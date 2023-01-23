namespace Ports_List
{
    internal partial class Port
    {
        /*
         * This is an example 'Port' class which uses the data extracted from the Registry.
         * This can easily be improved or adapted to suit your needs. For example, you may wish
         * to change the PortNumber property to an integer, or introduce an Enum for the available
         * Protocols. All properties have been kept as strings with default values for ease of
         * demonstration.
         */
        public string Service { get; set; } = "";
        public string Protocol { get; set; } = "";
        public string PortNumber { get; set; } = "";
        public string Description { get; set; } = "";
    }
}

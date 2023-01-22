using Newtonsoft.Json;
using System.Net.Http.Headers;
using Ports_List;

/* Attribution:
 * 
 * The Internet Assigned Numbers Authority (IANA) helpfully makes their Service Name and
 * Transport Protocol Port Number Registry available for download to the public. The efforts of
 * IANA and the individual contributors who assist in generating this Registry are acknowledged.
 * 
 * https://www.iana.org/assignments/service-names-port-numbers/service-names-port-numbers.xhtml
 */

// Initialise list of Ports
List<Port> Ports = new();
// Set desired output filename
string output = "ports.json";
// Set target URL
string target = "https://www.iana.org/assignments/service-names-port-numbers/service-names-port-numbers.csv";

Console.WriteLine("Downloading list of ports from IANA...");

try
{
    // Summary:
    // Open new HttpClient and give it the web-agent headers that IANA requires.
    // Get the ports CSV file as a stream, parse it, and let the user know if there is an error.
    using HttpClient client = new();
    var productValue = new ProductInfoHeaderValue("CSharp_PortsList", "1.0");
    var commentValue = new ProductInfoHeaderValue("(+https://github.com/rjbfoster/)");
    client.DefaultRequestHeaders.UserAgent.Add(productValue);
    client.DefaultRequestHeaders.UserAgent.Add(commentValue);
    Stream response = await client.GetStreamAsync(target);
    Console.WriteLine("File downloaded. Parsing list of ports...");
    using (StreamReader sr = new(response))
    {
        // Parse the stream into the 'Port' object
        bool isFirstLine = true;
        while (sr.Peek() >= 0)
        {
            var line = sr.ReadLine();
            if (line == null)
            {
                // Skip null entries
                continue;
            }
            if (isFirstLine)
            {
                // Skip the headers
                isFirstLine = false;
                continue;
            }
            var columns = line.Split(',');
            if (columns.Length < 4)
            {
                // If the row does not have at least 4 columns, it is a comment or malformed. This will throw an exception if not skipped.
                // A common issue here is that the comments from a previous row will sometimes spill over to multiple lines below the entry.
                continue;
            }
            var newPort = new Port
            {
                Service = columns[0],
                PortNumber = columns[1],
                Protocol = columns[2],
                Description = columns[3]
                /*
                 * Note: expected columns are:
                 *  [0] Service                         'telnet'
                 *  [1] Port Number                     '23'
                 *  [2] Protocol                        'tcp'
                 *  [3] Description                     'Telnet'
                 *  [4] Asigneee                        '[Jon_Postel]'
                 *  [5] Contact                         '[Jon_Postel]'
                 *  [6] Registration Date               '2022-02-07'
                 *  [7] Modification Date               '2022-02-07'
                 *  [8] Reference                       'RFC854'
                 *  [9] Service Code                    '1145656131'
                 *  [10] Unauthorised Use Reported      null/empty string
                 *  [11] Assignment Notes               'Defined TXT keys: u=<username> p=<password>'
                 */
            };
            Ports.Add(newPort);
        }
    }
    Console.WriteLine($"Generated list of {Ports.Count} ports. Serializing to JSON...");
    if (Ports.Count < 10000)
    {
        Console.WriteLine("Warning: expected > 10,000 ports. Output file may be corrupt or incomplete.");
    }
}
catch (Exception e)
{
    Console.WriteLine($"Unable to download and parse file: {e.Message} {e.InnerException}");
    Console.WriteLine("Terminating program. Please try again.");
    Environment.Exit(1);
}

// Set output options to make the output look nice, then write JSON to file.
JsonSerializerSettings options = new() { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented };
var jsonString = JsonConvert.SerializeObject(Ports, options);
File.WriteAllText(output, jsonString);
Console.WriteLine($"Success! List of ports output to file '{output}'.");

// The below section is not required to retrieve and generate the ports list.
//  It is a proof of concept for interrogating and returning values from the data obtained.
bool isViewMode = true;
while (isViewMode)
{
    Console.WriteLine("\nSelect option: " +
        "\n\t[number]\tView available port information for entered number (e.g. 23, 80, 3535)" +
        "\n\t[any other]\tExit program");
    Console.Write("\nInput: ");
    var entry = Console.ReadLine();
    var isInt = int.TryParse(entry, out int result);

    /* This method uses TryParse to work out whether or not an integer (i.e. port number) was entered, but the
    *  string itself is used for comparison purposes. The entered string either matches an entry or it doesn't.
    *  Note that this is a deliberately basic implementation; it does not account for port ranges (e.g. '24555-24576')
    *  or services without a port number, which would need to be discovered by name or description (e.g. 'Reserved' ports).
    */
    if (!isInt)
    {
        isViewMode = false;
    }
    else
    {
        List<Port> matchedPorts = Ports.Where(x => x.PortNumber == entry).ToList();
        if (matchedPorts.Count > 0)
        {
            int x = 1;
            Console.WriteLine("The following ports matched your query:");
            foreach (var match in matchedPorts)
            {
                Console.WriteLine($"\n{x})\tPort: {match.PortNumber}/{match.Protocol}\n\tService: {match.Service}\n\tDescription: {match.Description}");
                x++;
            }
        }
        else
        {
            Console.WriteLine("No matches found for your entry; please try again.");
        }
    }
}
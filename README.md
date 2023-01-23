# C# TCP/UDP Ports List
This is a program which retrieves an up-to-date list of ports (and associated service names/descriptions), for use in applications which require this information. The list is retrieved directly from IANA.

## Purpose

The purpose of this program is to retireve information regarding various network services, and provide a means by which this information can be presented to an end user.

It also demonstrates the following concepts in C#:
* Use of HttpClient to retrieve data
* JSON serialization
* Error handling

## Usage

Simple usage:
* Download and import the file 'ports.json' into your project.
* Consume it using Newtonsoft.Json or an equivalent method. See the below section for an example of this.

Consumption:
* You can retrieve and deserialize the file using the following code:
```cs
using (StreamReader sr = new StreamReader("ports.json"))
{
    string jsonString = sr.ReadToEnd();
    Ports = JsonConvert.DeserializeObject<List<Port>>(jsonString);

}
```
* An example of the above method is included in Example.cs.

Dynamic usage:
* To ensure your copy of ports.json is always up to date, build and run the program. You may wish to incorporate the method into your application if it requires up-to-date port/service references.
* Clone and modify this project to ensure that the output object (Port.cs) matches your use case.
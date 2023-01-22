# C# TCP/UDP Ports List
This is a program which dynamically retrieves a list of ports (and associated service names/descriptions), for use in applications which require this information.

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
* TBD

Dyanmic usage:
* To ensure your copy of ports.json is always up to date, build and run the program.
* Clone and modify this project to ensure that the output object (Port.cs) matches your use case.
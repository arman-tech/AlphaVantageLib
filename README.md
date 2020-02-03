
# Alpha Vantage Library

An Alpha Vantage .NET library that allows capturing of historical, and technical stock prices.  


## **Disclaimer**
This is a third-party library, we have no affiliation with Alpha Vantage company.


## **Prerequisites**

You have installed .NET Core 3.0 or higher on your machine.   It should be noted that this library was developed and tested on Windows 10, using Visual Studio 2019.


## **Getting Started**

You can either clone or download the AlphaVantageLibrary and use it based on the MIT license.  The two main projects that will most likely be important to you will be AlphaVantage.Common, and AlphaVantage.Core.  Under &#39;What You Need To Know&#39; we will go through the purpose of each project.

If you wish to run this solution as is, then you will need to do the following:

1. You will need to have installed the latest MongoDB
2. If you are using Visual Studio, right-click on &#39;AlphaVantage.TimedTask.Runner&#39; project and select &#39;Set as StartUp project&#39;.
3. Update &#39;appsettings.json&#39; found under AlphaVantage.TimedTask.Runner project by changing the value for &#39;AvMongoConnection&#39; to your MongoDB location.
4. You will need to update &#39;nlog.config&#39; file by changing the location of internal log files to one you prefer.  The element you will need to update is called &#39;internalLogFile&#39;.  &#39;nlog.Config&#39; can be found under AlphaVantage.TimedTask.Runner project.
5. This step is optional, if you have an API key for Alpha Vantage then you can update &#39;timedtask-runner-config.json&#39; by replacing &#39;&amp;apikey=demo&#39; with &#39;&amp;apikey={your Alpha Vantage API key here}&#39;


## **What You Need To Know**

**AlphaVantage.Common** – holds all the possible models that will be used in AlphaVantage.Core or DataAccess projects.  Common project also holds all the possible enumerations required as part of the project.

**AlphaVantage.Core.Test** – unit tests for AlphaVantage.Core.

**AlphaVantage.Core** – holds all the processes needed to capture and convert JSON response into a POCO class that can be consumed by other projects.

**AlphaVantage.DataAccess.Test** – unit tests for AlphaVantage.DataAccess

**AlphaVantage.DataAccess** – an experimental project to update or save data into local Mongo DB database.  Please note, &#39;AlphaVantage.DataAccess&#39; project should not be used for anything other than a learning process.

**AlphaVantage.TimedTask.Runner** – A console application that will grab data without hammering the AlphaVantage web service.  It should be pointed out that AlphaVantage webservice will allow only certain number of calls per minute.  Based on the free version of web service calls this is 30 calls per minute.  All this information can be found in timedtask-runner-config.json file, which can be found here: [https://github.com/arman-tech/AlphaVantageLibrary/blob/master/AlphaVantage.TimedTask.Runner/timedtask-runner-config.json](https://github.com/arman-tech/AlphaVantageLibrary/blob/master/AlphaVantage.TimedTask.Runner/timedtask-runner-config.json)

Information on premium plan of Alpha Vantage can be found here: [https://www.alphavantage.co/premium/](https://www.alphavantage.co/premium/)

Although the console application entry point is Program.cs, the file that starts the &#39;chain-reaction&#39; is called _Helm.cs_.  In this file we go through all the URIs found in &#39;timedtask-runner-config.json&#39;.  _We use Visitor design pattern to make the correct calls needed to convert the JSON response into the desired POCO class and then call the desired repository_.  To make the code more readable, we have separated time series with technical indicators into HelmTimeSeries.cs and HelmTechnicalSeries.cs, respectively.

**AlphaVantage.TimedTask** – project will make a determined number of calls per minute to Alpha Vantage web service.  The number of calls to Alpha Vantage end point is configurable.  Under AlphaVantage.TimedTask.Runner project, a file called &#39;timedtask-runner-config.json&#39; is used to configure the number of calls per minute with the element called &#39;api-calls-per-minute-allowed&#39;.

**AlphaVantage.Utilities** – AlphaVantage.TimedTask.Runner project uses AutoFac as an IoC container.  AlphaVantage.Utilities holds all the necessary AutoFac components.


## **Built With**

- [.NET Core 3.0](https://dotnet.microsoft.com/download/dotnet-core/3.0) - .NET framework used to build all projects.
- [AutoFac](https://autofac.org/)– IoC container
- [NewtonSoft](https://www.newtonsoft.com/json)- Used to convert JSON into .NET POCO.


## **Versioning**

For the versions available, see the [tags on this repository](https://github.com/arman-tech/AlphaVantageLib/tags).


## **Authors**

- **Arman**  - _Initial work_


## **License**

This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/arman-tech/AlphaVantageLibrary/blob/master/LICENSE) file for details
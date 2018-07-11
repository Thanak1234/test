# K2 Workflow

## Release node
NAGA WORKFLOW 2.0 is a Smartform replacement purpose, Single page application to improve user experience – Increase performance, new custom worklist,   data transfer between server and client reduction.
By using Extjs along with .Net technology, it gives developers some benefits like customization, maintenance, rescuable component and quick to be picked up by new developers.  

## Features
###	Modules: 
1. Workflows: dictate your processes, and every time a step, system, person or rule gets added or changed, the lines of code pile up. Allow you can build workflows visually — saving significant time and creating opportunities to improve.
2. Forms: quickly deliver a user interface for every need, using a single technology stack. Allow you can create reusable views and forms (called Formdroid), and use them like building blocks to assemble even more forms. It’s this reusability that makes it possible to create a form, customize the design, set up integrations and quickly take it live.
3. Integration: integrate systems, machines and people are as different as the processes that interact with them. However, getting them all to play nicely together is difficult and time-consuming. Allow your processes can consume and surface line-of-business data easily and contextually.
4. Report: once the automate, opportunities to improve will be much easier to identify. From free out-of-the-box process reports and integration with third-party tools like Excel, Pdf gives you the real-time insights you need to make better, data-driven decisions.

## Getting Started
### Prerequisites

- [C# .Framework](https://www.microsoft.com/en-us/download/details.aspx?id=53344) (>4.5)
- [K2 Blackpeal](https://www.k2.com) Workflow engine
- [Sencha](http://forms.nagaworld.com/sencha/) Sencha CLI and UI Library (6.2.0)
- [Node.js](https://nodejs.org/) (>8.3) 

### Third Party

- [iClock]() Finger Print SDK: you can find in (/lib/Finger Print SDK/32bit_register_SDK.bat)
- [RabbitMQ](https://www.rabbitmq.com/) is lightweight and easy to deploy for message broker.

### Build

- [Visual Studio](http://visualstudio.com) (>2015) IDE for development.
- [SQL Server Management Studio Tool](https://go.microsoft.com/fwlink/?linkid=875802) Easy access SQL database.
- [Report Builder](https://www.microsoft.com/en-us/download/details.aspx?id=6116) (3.0)
- [Sencha CMD]() Build > sencha app build | Watch > - > sencha app watch

### Run

- [Dev DNS](https://forms.nagaworld.local)  Go to [Win + R] type (notepad c:\Windows\System32\drivers\etc\hosts) add (127.0.0.1		forms.nagaworld.local)
- [IIS]() (>7.5) map vitual path to (%PROJECT_PATH%\Workflow\src\Workflow.Web.Service)

![Alt text](https://forms.nagaworld.com/sencha/asset/iis_feature_on.jpg)
> [Microsoft URL Rewrite Module 2.0 for IIS (x64)](https://www.microsoft.com/en-us/download/details.aspx?id=47337)

> Register .NET 4 into IIS application pool (If not yet register by default). Run > (C:\Windows\Microsoft.NET\Framework\v4.0.30319\aspnet_regiis.exe -ir)

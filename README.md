# NexPay.BankApp

### Prerequisites
* Visual Studio Visual Studio 2017 Community
* The latest Node.JS web server

** nuget and npm packages will be recovered during the first solution build.

### Description
The application developed by using Asp .Net Core 2 and Angular 5 in Visual Studio 2017 Community version 15.5.7.
It demostrates how SPA Angular 5 application communicates with restful Asp .Net Core Web Api service.
Client and server side validations added along with logging feature.
Unit tests for front and back end code are also implemented

** in order to run jasmine front end code unit tests please run "npm test"

### Configuration settings
The web application writes the bank transactions into the default c:\NexPay.TransactionFile.txt file. If you want to change it, please do that by editing appsettings.json.
The log files are written into the default C:\Temp folder. If you want to change it, please do that by editing nlog.config file.

The following commands should be executed in command prompt (cmd.exe) in NexPay.BankApp.Repository folder

How to add a new migration:
	dotnet ef migrations add InitialCreate --startup-project ..\NexPay.BankApp.Web

How to apply a new migration into database:
	dotnet ef database update --startup-project ..\NexPay.BankApp.Web
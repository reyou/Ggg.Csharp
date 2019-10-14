```
$ dotnet new console -o EFGetStarted
$ cd EFGetStarted
$ dotnet tool install --global dotnet-ef
$ dotnet add package Microsoft.EntityFrameworkCore.Design
$ dotnet ef migrations add InitialCreate
$ dotnet ef database update
```

```
$ dotnet run
```

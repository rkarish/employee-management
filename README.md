# Employee Management API

## Instructions

### Run SQL Server in Docker

```shell
docker run --name sql_server -e ACCEPT_EULA=Y -e SA_PASSWORD=StrongPassword! -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
docker exec -it sql_server /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P StrongPassword!
```

### Create the Database

```shell
dotnet ef migrations add InitialCreate
dotnet ef database update 20211207003936_InitialCreate --connection "Server=127.0.0.1,1433\\mssqllocaldb; Database=employee_management; User=SA; Password=StrongPassword!;"
```

### Run for Development

```shell
dotnet watch run
```

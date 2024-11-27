# Lab3

## Swagger 
http://localhost:5249/swagger/index.html
![image](https://github.com/user-attachments/assets/6d14f76d-7c13-4dab-80e2-b1b83838730e)


## Run
```
dotnet run --project API
```

## HTTP testings
`API/API.http`
![image](https://github.com/user-attachments/assets/b3c88342-28d6-406d-af81-ec00ff1c1772)


## PostgreSQL 
### Up
```
docker compose up
```

### Migrations
```
dotnet ef database update -v
```

### appsettings
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "DBA": "db",
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5555;Database=postgres;Username=postgres;Password=postgres"
  }
}
```
### Scheme
![image](https://github.com/user-attachments/assets/b2bdf35d-b3fb-4fa3-95b8-541341aa75e2)
(+ Shop.Address)

## Files
### appsettings
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "DBA": "file",
}
```
### Files
Hard-coded files are:
- product.json
- shop.json
- shopproducts.json
![image](https://github.com/user-attachments/assets/0cf0315c-a9ad-452f-9633-5e56c912ec06)


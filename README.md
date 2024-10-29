# Lab3

## Swagger 
http://localhost:5249/swagger/index.html

## HTTP testings
`Shops/shops.http`

## PostgreSQL 
appsettings:
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

## Files
appsettings:
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
Hard-coded files are:
- product.json
- shop.json
- shopproducts.json

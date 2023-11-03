### Restore project
```shell
dotnet restore
```

### Config `appsettings.json`
```json
{
  "ConnectionStrings": {
    "SqlServer": ""
  }
}
```

### Create tables
```shell
dotnet ef database update --project Api 
```

### Drop tables
```shell
dotnet ef database drop --project Api
```

### Build project
```shell
dotnet build --no-restore
```

### Run project tests
```shell
dotnet test --no-build --verbosity normal 
```

### Publish project
```shell
publish -c Release --output ./Release
```
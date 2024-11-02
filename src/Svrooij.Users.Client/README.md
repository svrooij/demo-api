# User API client

The user API client is a simple client to interact with the user API. It's a simple client that can be used to interact with the user API.

## Regenerate the client

To regenerate the client you can run the following command in the same folder as this readme:

```bash
dotnet kiota generate -l CSharp -c MyApiClient -n Svrooij.Users.Client -d https://demo-api.svrooij.io/swagger/v1/swagger.json -o Generated
```

Or regenerate the client from the local api:

```bash
dotnet kiota generate -l CSharp -c MyApiClient -n Svrooij.Users.Client -d https://localhost:32777/swagger/v1/swagger.json -o Generated
```

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


// Some variables
//Uri baseUri = new Uri("https://localhost:32777/");
Uri baseUri = new Uri("https://demo-api.svrooij.io/");


// Load the users with an HttpClient
HttpClient httpClient = new HttpClient();
httpClient.BaseAddress = baseUri;

var usersResponse = await httpClient.GetStringAsync("users");
Console.WriteLine(usersResponse);
// Parse the response to a list of users ...



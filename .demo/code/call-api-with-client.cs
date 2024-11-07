// New API client
var apiClient = new MyApiClient(
  new HttpClientRequestAdapter(
    new AnonymousAuthenticationProvider(),
    httpClient: httpClient));

// Load the users with the API client
var users = await apiClient.Users.GetAsync();

// Users are parsed into a list of User objects automatically
foreach (var user in users!)
{
  Console.WriteLine($"{user.Name} favorite animal is {user.FavoriteAnimal}");
}
Console.WriteLine(users.Count);
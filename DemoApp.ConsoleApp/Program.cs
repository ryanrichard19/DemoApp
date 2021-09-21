
// See https://aka.ms/new-console-template for more information
using IdentityModel.Client;

TokenResponse tokenResponse;
using ( var discoveryDocumentHttpClient = new HttpClient())
{
    var discoveryDocument = await discoveryDocumentHttpClient.GetDiscoveryDocumentAsync("https://localhost:5001");
    Console.WriteLine(discoveryDocument.TokenEndpoint);

    tokenResponse = await discoveryDocumentHttpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
    {
        Address = discoveryDocument.TokenEndpoint,
        ClientId = "console",
        ClientSecret = "secret",
        Scope = "api"
    });
    Console.WriteLine(tokenResponse.AccessToken);
}

using (var apiHttpClient = new HttpClient())
{
    apiHttpClient.SetBearerToken(tokenResponse.AccessToken);

    var response = await apiHttpClient.GetStringAsync("https://localhost:7179/WeatherForecast");
    Console.WriteLine(response);
}

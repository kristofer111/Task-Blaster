namespace Gateway.Services.Interfaces;

public interface IM2MAuthenticationService
{
    /// <summary>
    /// Retrieves a Bearer token from Auth0 for the use of M2M authentication. The Bearer token is retrieved
    /// using a HttpClient with the setup as shown in the Auth0 M2M documentation (https://auth0.com/blog/using-m2m-authorization/) under
    /// the Client Credentials Grant chapter.
    /// </summary>
    /// <param name="audience">The API which is suppose to receive the token</param>

    /// <returns>A Bearer token</returns> 
    Task<string> RetrieveAccessToken(string audience);
}
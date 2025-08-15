namespace API.Interfaces
{
    public interface ICookieService
    {
        void SetAuthCookies(string accessToken, string refreshToken);
    }
}

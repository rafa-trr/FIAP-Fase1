namespace FIAP.TECH.TESTS.Setup;

public static class TestsExtensions
{
    public static void AssignToken(this HttpClient httpClient, string token)
    {
        httpClient.AssignJsonMediaType();
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }

    public static void AssignJsonMediaType(this HttpClient httpClient)
    {
        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    }
}
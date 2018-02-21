using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TuesPechkin.Service
{
    public enum Uri
    {
        Local
    }

    public static class TuesPechkinService
    {
        public static byte[] ConvertToPdf(Uri uri, string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(string.Format(GetUri(uri), url)).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsByteArrayAsync().Result;
                }
                return null;
            }
        }

        public static async Task<byte[]> ConvertToPdfAsync(Uri uri, string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(string.Format(GetUri(uri), url));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
                return null;
            }
        }

        private static string GetUri(Uri uri)
        {
            switch (uri)
            {
                case Uri.Local:
                    return "http://localhost:52576/api/tuespechkin?url={0}";
                default:
                    throw new ArgumentOutOfRangeException(nameof(uri), uri, null);
            }
        }
    }
}

using CoffeeHouse.Core.Misc;
using Polly;
using Polly.Contrib.WaitAndRetry;
using System.Text;
using System.Text.Json;

namespace CoffeeHouse.Core.Extensions
{
    public static class HttpClientExtensions
    {
        private readonly static IAsyncPolicy<HttpResponseMessage> _retryPolicy = Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(x => x.StatusCode >= System.Net.HttpStatusCode.InternalServerError || x.StatusCode == System.Net.HttpStatusCode.RequestTimeout)
            .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 3));

        public static async Task<GeneralResponse<T>> Get<T>(this HttpClient httpClient, string apiUrl)
        {
            try
            {
                var response = await _retryPolicy.ExecuteAsync(async () => await httpClient.GetAsync(apiUrl, HttpCompletionOption.ResponseHeadersRead));
                
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    
                    return await JsonSerializer.DeserializeAsync<GeneralResponse<T>>(stream, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }
            }
            catch (Exception ex)
            {
                
            }

            return new GeneralResponse<T>();
        }

        public static async Task<GeneralResponse<T>> Post<T>(this HttpClient httpClient, string apiUrl, object postObject)
        {
            string json = JsonSerializer.Serialize(postObject);

            StringContent content = new(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _retryPolicy.ExecuteAsync(async () => await httpClient.PostAsync(apiUrl, content));

                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    return await DeserializeData<T>(stream);
                }
            }
            catch (Exception ex)
            {
                
            }

            return new GeneralResponse<T>();
        }

        public static async Task<GeneralResponse<T>> PostIdentityServer<T>(this HttpClient httpClient, string apiUrl, Dictionary<string, string> postObject)
        {
            var formContent = new FormUrlEncodedContent(postObject);

            try
            {
                var response = await _retryPolicy.ExecuteAsync(async () => await httpClient.PostAsync(apiUrl, formContent));
                
                if (response.IsSuccessStatusCode)
                {
                    var stringContent = await response.Content.ReadAsStringAsync();

                    var result = JsonSerializer.Deserialize<T>(stringContent, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    return new GeneralResponse<T>()
                    {
                        IsSuccess = true,
                        Content = result
                    };
                }
            }
            catch (Exception ex)
            {
                
            }

            return new GeneralResponse<T>();
        }

        public static async Task<GeneralResponse<T>> Delete<T>(this HttpClient httpClient, string apiUrl)
        {
            try
            {
                var response = await _retryPolicy.ExecuteAsync(async () => await httpClient.DeleteAsync(apiUrl));

                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    return await DeserializeData<T>(stream);
                }
            }
            catch (Exception ex)
            {
                
            }

            return new GeneralResponse<T>();
        }

        public static async Task<GeneralResponse<T>> Put<T>(this HttpClient httpClient, string apiUrl, object postObject)
        {
            string json = JsonSerializer.Serialize(postObject);

            StringContent content = new(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _retryPolicy.ExecuteAsync(async () => await httpClient.PutAsync(apiUrl, content));

                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    return await DeserializeData<T>(stream);
                }
            }
            catch (Exception ex)
            {
                
            }

            return new GeneralResponse<T>();
        }

        private static async Task<GeneralResponse<T>> DeserializeData<T>(Stream stream)
        {
            return await JsonSerializer.DeserializeAsync<GeneralResponse<T>>(stream, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}

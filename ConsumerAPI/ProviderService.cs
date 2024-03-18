using Newtonsoft.Json;
using ProducerAPI;
using System.Text;
using System.Text.Json;

namespace ConsumerAPI
{
    // DTO
    public class ProviderResponse
    {
        public IEnumerable<string> Data { get; set; }
    }
    public class ProviderService
    {
        private readonly HttpClient _client;

        public ProviderService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            if (_client.BaseAddress is null)
            {
                throw new ArgumentNullException(nameof(_client.BaseAddress));
            }
        }

        public Order GetEmptyOrder()
        {
            return new Order
            {
                Id = 0,
                Name = string.Empty,
                Price = 0,
            };
        }

        public async Task<Order> GetData()
        {

            var httpResponse = await _client.GetAsync("/api/Order?id=10");
            if (httpResponse.IsSuccessStatusCode)
            {
                return System.Text.Json.JsonSerializer.Deserialize<Order>(
                        await httpResponse.Content.ReadAsStringAsync(),
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return GetEmptyOrder();
        }


        public async Task<Order> PostData()
        {

            var newOrder = new Order
            {
                Id = 20,
                Name = "Test",
                Price = 24,
            };
            var json = JsonConvert.SerializeObject(newOrder);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+

            var httpResponse = await _client.PostAsync("/api/Order/post", stringContent);
            if (httpResponse.IsSuccessStatusCode)
            {
                return System.Text.Json.JsonSerializer.Deserialize<Order>(
                        await httpResponse.Content.ReadAsStringAsync(),
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return GetEmptyOrder();
        }
    }
}

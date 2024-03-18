using PactNet;
using System.Net;

namespace ConsumerAPI
{
    public class Test_CreateConsumerPactContract
    {

        private IPactBuilderV3 pactBuilder;

        private IPactV3 pact;


        [SetUp]
        public void Setup()
        {
            pact = Pact.V3("Consumer API", "Provider API", new PactConfig());
            pactBuilder = pact.WithHttpInteractions();
        }

        [Test]
        public async Task EnsureProducerHonorsContract()
        {
            pactBuilder
          .UponReceiving("A GET request to /api/Order")
              .Given("There is available data")
              .WithQuery("id", "10")
              .WithRequest(HttpMethod.Get, "/api/Order")
              .WithHeader("Content-Type", "application/json")
               .WillRespond()
              .WithStatus(HttpStatusCode.OK)

              .WithJsonBody(new
              {
                  Id = 10,
                  Name = "Test",
                  Price = 24,
              });

            await pactBuilder.VerifyAsync(async ctx =>
            {
                var httpClient = new HttpClient();
                httpClient.BaseAddress = ctx.MockServerUri;
                var provider = new ProviderService(httpClient);
                var response = await provider.GetData();
                Assert.IsNotEmpty(response.Name);
            });
        }

        [Test]
        public async Task EnsureProducerHonorsContractPost()
        {
            pactBuilder
          .UponReceiving("A post request to /api/Order/post")
              .Given("There is available data")
              .WithJsonBody(new
              {
                  Id = 20,
                  Name = "Test",
                  Price = 24,
              })
              .WithRequest(HttpMethod.Post, "/api/Order/post")
              .WillRespond()
              .WithStatus(HttpStatusCode.OK)
              .WithJsonBody(new
              {
                  Id = 20,
                  Name = "Test",
                  Price = 24,
              });

            await pactBuilder.VerifyAsync(async ctx =>
            {
                var httpClient = new HttpClient();
                httpClient.BaseAddress = ctx.MockServerUri;
                var provider = new ProviderService(httpClient);
                var response = await provider.PostData();
                Assert.IsNotEmpty(response.Name);
            });
        }
    }
}
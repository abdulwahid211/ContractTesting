using PactNet;
using PactNet.Verifier;

namespace ConsumerAPI
{
    public class VerifyPactWithConsumer
    {

        private readonly string url_server = "https://localhost:44374";

        private PactVerifierConfig config;

        string pactPath = Path.Join("..",
                                "..",
                                "..",
                                "pacts",
                                "Consumer API-Provider API.json");

        [SetUp]
        public void Setup()
        {
            config = new PactVerifierConfig
            {
                LogLevel = PactLogLevel.Information,
            };
        }

        [Test]
        public async Task VerifyContractPact()
        {

            IPactVerifier verifier = new PactVerifier(config);

            verifier
               .ServiceProvider("Order API", new Uri(url_server))
               .WithFileSource(new FileInfo(pactPath))
               .WithRequestTimeout(TimeSpan.FromMinutes(20))
               .Verify();
        }
    }
}

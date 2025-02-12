using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient.Models;
using GrpcService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Web;

namespace GrpcClient.Controllers
{
    public partial class HomeController
    {
        public IActionResult Payment()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Payment(string amount)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new Greeter.GreeterClient(channel);

            string? token = null;
            PayResponse response = new PayResponse();

            try
            {
                token = await Authenticate();
                Metadata? headers = null;
                if (token != null)
                {
                    headers = new Metadata();
                    headers.Add("Authorization", $"Bearer {token}");
                }

                response = client.ProcessPayment(new PayRequest { Amount = amount }, headers);
            }
            catch (RpcException ex)
            {
                return View("Payment", (object)ex.Message);
            }

            return View("Payment", (object)response.Message);
        }

        static async Task<string> Authenticate()
        {
            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"https://localhost:7021/generateJwtToken?name={HttpUtility.UrlEncode(Environment.UserName)}"),
                Method = HttpMethod.Get,
                Version = new Version(2, 0)
            };
            using var tokenResponse = await httpClient.SendAsync(request);
            tokenResponse.EnsureSuccessStatusCode();

            var token = await tokenResponse.Content.ReadAsStringAsync();

            return token;
        }
    }

    public partial class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Unary()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SendJokeAsync(new JRequest { No = 3 });
            return View("ShowJoke", (object)ChangetoDictionary(reply));
        }
        public async Task<IActionResult> ServerStreaming()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new Greeter.GreeterClient(channel);

            Dictionary<string, string> jokeDict = new Dictionary<string, string>();
            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(10));

            using (var call = client.SendJokeSS(new JRequest { No = 1 }, cancellationToken: cts.Token))
            {
                try
                {
                    await foreach(var message in call.ResponseStream.ReadAllAsync())
                    {
                        foreach(var joke in message.Joke)
                        {
                            if (jokeDict.ContainsKey(joke.Author) == false)
                                jokeDict.Add(joke.Author, joke.Description);
                        }
                        
                    }
                }
                catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.Cancelled)
                {                    
                    _logger.LogError(ex, "An error occurred on the server streaming call");                    
                }
            }
            return View("ShowJoke", (object)jokeDict);
        }
        public async Task<IActionResult> ClientStreaming()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new Greeter.GreeterClient(channel);
            Dictionary<string, string> jokeDict = new Dictionary<string, string>();
            int[] jokes = { 3, 2, 4 };

            using (var call = client.SendJokesCS())
            {
                foreach (var jt in jokes)
                {
                    await call.RequestStream.WriteAsync(new JRequest { No = jt });
                }
                await call.RequestStream.CompleteAsync();

                JResponse jRes = await call.ResponseAsync;
                foreach(var joke in jRes.Joke)
                {
                    if (jokeDict.ContainsKey(joke.Author) == false)
                        jokeDict.Add(joke.Author, joke.Description);
                }
            }
            return View("ShowJoke", (object)jokeDict);
        }
        public async Task<IActionResult> BiDirectionalStreaming()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7199");
            var client = new Greeter.GreeterClient(channel);

            Dictionary<string, string> jokeDict = new Dictionary<string, string>();

            using (var call = client.SendJokesBD())
            {
                var responseReaderTask = Task.Run(async () =>
                {
                    while (await call.ResponseStream.MoveNext())
                    {
                        var response = call.ResponseStream.Current;
                        foreach (Joke joke in response.Joke)
                            jokeDict.Add(joke.Author, joke.Description);
                    }
                });

                int[] jokeNo = { 3, 2, 4 };
                foreach (var jT in jokeNo)
                {
                    await call.RequestStream.WriteAsync(new JRequest { No = jT });
                }

                await call.RequestStream.CompleteAsync();
                await responseReaderTask;
            }
            return View("ShowJoke", (object)jokeDict);
        }
        private Dictionary<string, string> ChangetoDictionary(JResponse response)
        {
            Dictionary<string, string> jokeDict = new Dictionary<string, string>();
            foreach (Joke joke in response.Joke)
                jokeDict.Add(joke.Author, joke.Description);
            return jokeDict;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

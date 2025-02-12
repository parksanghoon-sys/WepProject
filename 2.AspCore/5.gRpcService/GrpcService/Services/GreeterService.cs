using Grpc.Core;
using GrpcService;
using Microsoft.AspNetCore.Authorization;

namespace GrpcService.Services
{
    public partial class GreeterService
    {
        [Authorize]
        public override Task<PayResponse> ProcessPayment(PayRequest request, ServerCallContext context)
        {
            var user = context.GetHttpContext().User;

            return Task.FromResult(new PayResponse
            {
                Message = $"Payment of {request.Amount} processed successfully"
            });
        }
    }
    public partial class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
        public override Task<JResponse> SendJoke(JRequest request, ServerCallContext context)
        {
            List<Joke> jokeList = JokeRepo();

            JResponse response = new JResponse();
            response.Joke.AddRange(jokeList.Skip(request.No - 1).Take(1));

            return Task.FromResult(response);
        }
        public override async Task SendJokeSS(JRequest request, IServerStreamWriter<JResponse> responseStream, ServerCallContext context)
        {
            List<Joke> jokeList = JokeRepo();
            JResponse response = new JResponse();
            var i = 0;
            while (context.CancellationToken.IsCancellationRequested == false)
            {
                response.Joke.Add(jokeList.Skip(i).Take(request.No));
                await responseStream.WriteAsync(response);
                i++;
                await Task.Delay(1000);
            }

        }
        public override async Task<JResponse> SendJokesCS(IAsyncStreamReader<JRequest> requestStream, ServerCallContext context)
        {
            List<Joke> jokeList = JokeRepo();
            JResponse response = new JResponse();
            await foreach(var message in requestStream.ReadAllAsync())
            {
                response.Joke.AddRange(jokeList.Skip(message.No - 1).Take(1));
            }
            return response;
        }
        public override async Task SendJokesBD(IAsyncStreamReader<JRequest> requestStream, IServerStreamWriter<JResponse> responseStream, ServerCallContext context)
        {
            List<Joke> jokeList = JokeRepo();
            JResponse jRes;

            await foreach (var message in requestStream.ReadAllAsync())
            {
                jRes = new JResponse();
                jRes.Joke.Add(jokeList.Skip(message.No - 1).Take(1));
                await responseStream.WriteAsync(jRes);
            }
        }
        
        public List<Joke> JokeRepo()
        {
            List<Joke> jokeList = new List<Joke> {
                new Joke { Author = "Random", Description = "I ate a clock yesterday, it was very time-consuming"},
                new Joke { Author = "Xeno", Description = "Have you played the updated kids' game? I Spy With My Little Eye ... Phone"},
                new Joke { Author = "Jak", Description = "A perfectionist walked into a bar...apparently, the bar wasn¡¯t set high enough"},
                new Joke { Author = "Peta", Description = "To be or not to be a horse rider, that is equestrian"},
                new Joke { Author = "Katnis", Description = "What does a clam do on his birthday? He shellabrates"}
            };
            return jokeList;
        }
    }
}

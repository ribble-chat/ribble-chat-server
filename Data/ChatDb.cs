using System.Threading.Tasks;
using Cassandra;
using Microsoft.Extensions.Logging;
using RibbleChatServer.Models;

namespace RibbleChatServer.Data

{
    public class ChatDb
    {
        private ISession? session;

        private readonly ILogger<ChatDb> logger;

        public ChatDb(ILogger<ChatDb> logger)
        {
            this.logger = logger;
            Task.Run(Connect);
        }

        public async Task Connect()
        {
            // TODO retry connections periodically
            try
            {
                var cluster = Cluster.Builder()
                    .AddContactPoint("ribble-scylla")
                    .WithPort(9042)
                    .Build();
                this.session = await cluster.ConnectAsync();
                // await session.UserDefinedTypes.DefineAsync(UdtMap.For<Message>());
                System.Console.WriteLine("connected?");
            }
            catch (NoHostAvailableException e)
            {
                logger.LogError(e.Message);
                await Task.Delay(5000);
                await Connect();
            }
        }
    }
}


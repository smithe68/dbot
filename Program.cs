
namespace CsDiscordBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var CSBot = new Bot();
            CSBot.RunAsync().GetAwaiter().GetResult();
        }
    }
}


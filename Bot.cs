using System.Threading.Tasks;
using DSharpPlus.EventArgs;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using CsDiscordBot.Commands;
using System;

namespace CsDiscordBot
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public InteractivityModule Interactivity { get; private set; }
        public CommandsNextModule Commands { get; private set; }
        
        public async Task RunAsync()
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                    json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJason>(json);

            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };

            Client = new DiscordClient(config);
            Client.Ready += OnClientReady;

            Client.UseInteractivity(new InteractivityConfiguration{

            });

            doMessageReadingChecks(Client);

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefix = configJson.Prefix,
                EnableDms = false,
                EnableMentionPrefix = true
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<UtilityCommands>();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }
        

        private Task OnClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
        

        DateTime StartDate = DateTime.Now;
        int vimCount = 0;
        private void doMessageReadingChecks(DiscordClient client){
           client.MessageCreated += async e =>{
               int numDays = (int)Math.Round((DateTime.Now - StartDate).TotalDays);
               if (e.Message.Content.ToLower().Contains("vim") && !e.Author.IsCurrent && !e.Author.IsBot){
                   String[] message = e.Message.Content.ToLower().Split(" ");
                   foreach(String word in message){
                       if(word == "vim"){
                           vimCount++;
                       }
                   }
                   if(numDays == 1){
                        await e.Message.RespondAsync($"it has been {numDays} day since vim has been mentiond vim has been said {vimCount} times");
                   }else{
                       await e.Message.RespondAsync($"it has been {numDays} days since vim has been mentiond vim has been said {vimCount} times");
                   }
                   StartDate = DateTime.Now;
               }
           };

        }
    }
}
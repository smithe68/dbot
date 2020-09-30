using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System.Linq;
using System.Text.RegularExpressions;

namespace CsDiscordBot.Commands
{
    public class UtilityCommands : BaseModule
    {
        private Random random = new Random();
        private List<string> Movies { get; set; } = new List<string>();
        
        private async Task<DiscordMessage> Message(CommandContext ctx, string message) {
            return await ctx.Channel.SendMessageAsync(message);
        }

        protected override void Setup(DiscordClient client) {}

        [Command("roll")]
        [Description("Rolls a random number between 0 and your number.")]
        public async Task Roll(CommandContext ctx, int diceSize)
        {
            int result = random.Next(0, diceSize);
            await Message(ctx, $"{result}");
        }

        [Command("coinflip")]
        [Description("Flips a coin.")]
        public async Task Flip(CommandContext ctx) 
        {
            bool flip = random.Next(0, 1) == 1;
            string result = flip ? "Heads" : "Tails";
            await Message(ctx, $"{result}");
        }

        [Command("shame")]
        [Description("Shames someone.")]
        public async Task Shame(CommandContext ctx)
        {
            int numUsers = ctx.Guild.MemberCount;
            int shameNumber = random.Next(0,numUsers-1);
            var membersIter = ctx.Guild.Members;
            var members = membersIter.ToList();
            await Message(ctx, $"Shame: {members[shameNumber].DisplayName} ");
        }

        [Command("big")]
        [Description("Make word big")]
        public async Task Big(CommandContext ctx, string word)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var ch in word.ToLower()) 
            {
                if (ch != ' ') {
                    builder.Append($":regional_indicator_{ch}: ");
                } else {
                    builder.Append(" ");
                }
            }

            await Message(ctx, builder.ToString());
        }

    }
}
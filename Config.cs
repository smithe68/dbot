using Newtonsoft.Json;

namespace CsDiscordBot
{
    public struct ConfigJason
    {
        [JsonProperty("token")]
        public string Token { get; private set; }

        [JsonProperty("prefix")]
        public string Prefix { get; private set; }
    }
}
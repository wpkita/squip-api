using Newtonsoft.Json;

namespace Squip.Rest.Domain
{
    public class Tile : DomainModelBase
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}

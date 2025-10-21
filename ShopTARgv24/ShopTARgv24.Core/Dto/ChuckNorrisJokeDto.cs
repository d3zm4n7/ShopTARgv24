using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopTARgv24.Core.Dto
{
    public class ChuckNorrisJokeDto
    {
        [JsonPropertyName("icon_url")]
        public string IconUrl { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        // Это свойство будет содержать саму шутку
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}

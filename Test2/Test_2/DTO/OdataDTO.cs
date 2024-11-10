using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Test_2.DTO
{
    public class OdataDTO<T>
    {
        [JsonProperty("value")]
        public List<T> Value { get; set; }

        [JsonProperty("@odata.count")]
        public int Count { get; set; }

    }
}

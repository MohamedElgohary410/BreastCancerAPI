using System.Text.Json.Serialization;

namespace BreastCancerAPI.Models
{
    public class ClientDetails
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public byte[]? CTscan { get; set; }
        public byte[]? Mammogram { get; set; }
        public byte[]? MRLTest { get; set; }
    }
}

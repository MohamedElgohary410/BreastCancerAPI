using System.Text.Json.Serialization;

namespace BreastCancerAPI.Models
{
    public class PatientProfile
    {
        public byte[]? PatientImage { get; set; }

        [JsonIgnore]
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public int Age { get; set; }
        public string Stage { get; set; }
        public string Treatment { get; set; }
        public string DrFollow { get; set; }
    }
}

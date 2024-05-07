using System.Text.Json.Serialization;

namespace BreastCancerAPI.Models
{
    public class FileUpload
    {
        [JsonIgnore]
        public int Id { get; set; }
        public byte[]? ImgName { get; set; }
    }
}

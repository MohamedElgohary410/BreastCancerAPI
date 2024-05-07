namespace BreastCancerAPI.DTOs
{
    public class ClientDetailsDto
    {
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public IFormFile CTscan { get; set; }
        public IFormFile Mammogram { get; set; }
        public IFormFile MRLTest { get; set; }
    }
}

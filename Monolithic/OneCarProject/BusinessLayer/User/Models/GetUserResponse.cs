using Newtonsoft.Json;

namespace OneCarProject.BusinessLayer.User.Models
{
    public class GetUserResponse
    {
        [JsonProperty("user")]
        public UserDTO User { get; set; }
    }
}

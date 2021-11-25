using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneCarProject.BusinessLayer.User.Models
{
    public class GetUsersResponse
    {
        [JsonProperty("userList")]
        public IList<UserDTO> UserList { get; set; }
    }
}

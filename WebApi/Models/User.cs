using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Models
{
public class User
    {
        [Required]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string Token { get; set; }
    }
}

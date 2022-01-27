using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Dell.Lead.WeApi.Data.VO
{
    public class UserVO
    {
        [Required]
        [XmlElement("login")]
        [JsonPropertyName("login")]
        public string Login { get; set; }
        [Required]
        [XmlElement("password")]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}

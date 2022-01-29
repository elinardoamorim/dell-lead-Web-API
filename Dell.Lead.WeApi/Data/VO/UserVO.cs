using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Dell.Lead.WeApi.Data.VO
{
    public class UserVO
    {
        /// <summary>
        /// ID do usuário
        /// </summary>
        [XmlElement("code")]
        [JsonPropertyName("code")]
        public long Id { get; set; }
        /// <summary>
        /// Login de acesso do usuário
        /// </summary>
        [Required]
        [XmlElement("login")]
        [JsonPropertyName("login")]
        public string Login { get; set; }
        /// <summary>
        /// Senha de acesso do usuário
        /// </summary>
        [Required]
        [XmlElement("password")]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}

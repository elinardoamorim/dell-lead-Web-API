using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Dell.Lead.WeApi.Data.VO
{
    public class AddressVO
    {
        /// <summary>
        /// ID do endereço
        /// </summary>
        [XmlElement("code")]
        [JsonPropertyName("code")]
        public long Id { get; set; }
        /// <summary>
        /// Nome da rua
        /// </summary>
        [Required]
        [XmlElement("street")]
        [JsonPropertyName("street")]
        public string Street { get; set; }
        /// <summary>
        /// Número da casa/apartamento
        /// </summary>
        [Required]
        [XmlElement("number")]
        [JsonPropertyName("number")]
        public int Number { get; set; }
        /// <summary>
        /// Nome do bairro
        /// </summary>
        [Required]
        [XmlElement("district")]
        [JsonPropertyName("district")]
        public string District { get; set; }
        /// <summary>
        /// Nome da Cidade
        /// </summary>
        [Required]
        [XmlElement("city")]
        [JsonPropertyName("city")]
        public string City { get; set; }
        /// <summary>
        /// Nome do Estado
        /// </summary>
        [Required]
        [XmlElement("state")]
        [JsonPropertyName("state")]
        public string State { get; set; }
        /// <summary>
        /// CEP do Endereço
        /// </summary>
        [Required]
        [XmlElement("cep")]
        [JsonPropertyName("cep")]
        public long Cep { get; set; }
    }
}

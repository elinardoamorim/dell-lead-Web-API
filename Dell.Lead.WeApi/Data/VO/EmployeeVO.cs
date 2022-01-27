using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Dell.Lead.WeApi.Data.VO
{
    public class EmployeeVO
    {
        /// <summary>
        /// ID do cadastro
        /// </summary>
        [XmlElement("code")]
        [JsonPropertyName("code")]
        public long Id { get; set; }
        /// <summary>
        /// Nome completo
        /// </summary>
        [Required]
        [XmlElement("name_full")]
        [JsonPropertyName("name_full")]
        public string NameFull { get; set; }
        /// <summary>
        /// Número de CPF
        /// </summary>
        [Required]
        [XmlElement("cpf")]
        [JsonPropertyName("cpf")]
        public long Cpf { get; set; }
        /// <summary>
        /// Data de nascimento
        /// </summary>
        [Required]
        [XmlElement("birth_date")]
        [JsonPropertyName("birth_date")]
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Número de telefone
        /// </summary>
        [Required]
        [XmlElement("phone")]
        [JsonPropertyName("phone")]
        public long Phone { get; set; }
        /// <summary>
        /// Gênero
        /// </summary>
        [Required]
        [XmlElement("gender")]
        [JsonPropertyName("gender")]
        public string Gender { get; set; }
        /// <summary>
        /// Informações de endereço
        /// </summary>
        [Required]
        [XmlElement("address")]
        [JsonPropertyName("address")]
        public AddressVO Address { get; set; }
    }
}

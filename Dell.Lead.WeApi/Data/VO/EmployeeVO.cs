using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Dell.Lead.WeApi.Data.VO
{
    public class EmployeeVO
    {
        [XmlElement("code")]
        [JsonPropertyName("code")]
        public long Id { get; set; }
        [XmlElement("name_full")]
        [JsonPropertyName("name_full")]
        public string NameFull { get; set; }
        [XmlElement("cpf")]
        [JsonPropertyName("cpf")]
        public long Cpf { get; set; }
        [XmlElement("birth_date")]
        [JsonPropertyName("birth_date")]
        public DateTime BirthDate { get; set; }
        [XmlElement("phone")]
        [JsonPropertyName("phone")]
        public long Phone { get; set; }
        [XmlElement("gender")]
        [JsonPropertyName("gender")]
        public string Gender { get; set; }
        [XmlElement("address")]
        [JsonPropertyName("address")]
        public AddressVO Address { get; set; }
    }
}

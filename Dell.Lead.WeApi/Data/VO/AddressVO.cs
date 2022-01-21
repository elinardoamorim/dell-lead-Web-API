using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Dell.Lead.WeApi.Data.VO
{
    public class AddressVO
    {
        [XmlElement("code")]
        [JsonPropertyName("code")]
        public long Id { get; set; }
        [XmlElement("street")]
        [JsonPropertyName("street")]
        public string Street { get; set; }
        [XmlElement("number")]
        [JsonPropertyName("number")]
        public int Number { get; set; }
        [XmlElement("district")]
        [JsonPropertyName("district")]
        public string District { get; set; }
        [XmlElement("city")]
        [JsonPropertyName("city")]
        public string City { get; set; }
        [XmlElement("state")]
        [JsonPropertyName("state")]
        public string State { get; set; }
        [XmlElement("cep")]
        [JsonPropertyName("cep")]
        public string Cep { get; set; }
    }
}

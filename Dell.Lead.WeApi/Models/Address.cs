using Dell.Lead.WeApi.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dell.Lead.WeApi.Models
{
    [Table("address")]
    public class Address : BaseEntity
    {
        [Column("street")]
        public string Street { get; set; }
        [Column("number")]
        public int Number { get; set; }
        [Column("district")]
        public string District { get; set; }
        [Column("city")]
        public string City { get; set; }
        [Column("state")]
        public string State { get; set; }
        [Column("cep")]
        public long Cep { get; set; }
        [Column("employee")]
        public Employee Employee { get; set; }
    }
}

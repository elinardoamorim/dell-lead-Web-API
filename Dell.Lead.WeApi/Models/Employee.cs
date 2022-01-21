using Dell.Lead.WeApi.Models.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dell.Lead.WeApi.Models
{
    [Table("employees")]
    public class Employee : BaseEntity
    {
        [Column("name_full")]
        public string NameFull { get; set; }
        [Column("cpf")]
        public long Cpf { get; set; }
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }
        [Column("phone")]
        public long Phone { get; set; }
        [Column("gender")]
        public string Gender { get; set; }
        [Column("address_id")]
        public long AddressId { get; set; }
        [Column("address")]
        public Address Address { get; set; }


    }
}

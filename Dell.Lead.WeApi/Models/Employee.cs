using Dell.Lead.WeApi.Models.Base;
using System;

namespace Dell.Lead.WeApi.Models
{
    public class Employee : BaseEntity
    {
        public string NameFull { get; set; }
        public int Cpf { get; set; }
        public DateTime BirthDate { get; set; }
        public int Phone { get; set; }
        public string Gender { get; set; }
        public long AddressId { get; set; }
        public Address Address { get; set; }


    }
}

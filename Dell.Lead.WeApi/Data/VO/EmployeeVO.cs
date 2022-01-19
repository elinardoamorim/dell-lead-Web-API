using System;

namespace Dell.Lead.WeApi.Data.VO
{
    public class EmployeeVO
    {
        public long Id { get; set; }
        public string NameFull { get; set; }
        public int Cpf { get; set; }
        public DateTime BirthDate { get; set; }
        public int Phone { get; set; }
        public string Gender { get; set; }
        public AddressVO Address { get; set; }
    }
}

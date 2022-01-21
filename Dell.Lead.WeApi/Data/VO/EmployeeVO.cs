using System;

namespace Dell.Lead.WeApi.Data.VO
{
    public class EmployeeVO
    {
        public long Id { get; set; }
        public string NameFull { get; set; }
        public long Cpf { get; set; }
        public DateTime BirthDate { get; set; }
        public long Phone { get; set; }
        public string Gender { get; set; }
        public AddressVO Address { get; set; }
    }
}

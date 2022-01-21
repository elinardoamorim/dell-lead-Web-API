namespace Dell.Lead.WeApi.Data.VO
{
    public class AddressVO
    {
        public long Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Cep { get; set; }
    }
}

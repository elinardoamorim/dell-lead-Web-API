using Dell.Lead.WeApi.Data.Converter.Contract;
using Dell.Lead.WeApi.Data.VO;
using Dell.Lead.WeApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace Dell.Lead.WeApi.Data.Converter.Converter
{
    public class AddressConverter : IParse<AddressVO, Address>, IParse<Address, AddressVO>
    {
        public AddressVO Parse(Address origin)
        {
            if (origin == null) return null;
            return new AddressVO
            {
                Id = origin.Id,
                Street = origin.Street,
                Number = origin.Number,
                District = origin.District,
                City = origin.City,
                State = origin.State
            };
        }

        public List<AddressVO> Parse(List<Address> origins)
        {
            if(origins == null) return null;
            return origins.Select(item => Parse(item)).ToList();
        }

        public Address Parse(AddressVO origin)
        {
            if(origin == null) return null;
            return new Address
            {
                Id = origin.Id,
                Street = origin.Street,
                Number = origin.Number,
                District = origin.District,
                City = origin.City,
                State = origin.State
            };
        }

        public List<Address> Parse(List<AddressVO> origins)
        {
            if (origins == null) return null;
            return origins.Select(item => Parse(item)).ToList();
        }
    }
}

using Dell.Lead.WeApi.Data.Converter.Contract;
using Dell.Lead.WeApi.Data.VO;
using Dell.Lead.WeApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace Dell.Lead.WeApi.Data.Converter.Converter
{
    public class UserConverter : IParse<User, UserVO>, IParse<UserVO, User>
    {
        public User Parse(UserVO origin)
        {
            if (origin == null) return null;
            return new User
            {
                Id = origin.Id,
                Login = origin.Login,
                Password = origin.Password
            };
        }

        public List<User> Parse(List<UserVO> origins)
        {
            if(origins == null) return null;
            return origins.Select(item => Parse(item)).ToList();
        }

        public UserVO Parse(User origin)
        {
            if (origin == null) return null;
            return new UserVO
            {
                Id = origin.Id,
                Login = origin.Login,
                Password = origin.Password
            };
        }

        public List<UserVO> Parse(List<User> origins)
        {
            if(origins == null) return null;
            return origins.Select(item => Parse(item)).ToList();
        }
    }
}

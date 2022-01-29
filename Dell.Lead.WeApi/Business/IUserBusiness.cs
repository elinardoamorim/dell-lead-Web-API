using Dell.Lead.WeApi.Data.VO;

namespace Dell.Lead.WeApi.Business
{
    public interface IUserBusiness
    {
        UserVO Create(UserVO user);
        UserVO FindById(long id);
    }
}

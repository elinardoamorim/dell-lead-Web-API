using Dell.Lead.WeApi.Data.VO;

namespace Dell.Lead.WeApi.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO user);
        TokenVO ValidateCredentials(TokenRefreshVO token);
        bool RevokeToken(string login);

    }
}

namespace Dell.Lead.WeApi.Data.VO
{
    public class TokenRefreshVO
    {
        /// <summary>
        /// Token de acesso
        /// </summary>
        public string AcessToken { get; set; }
        /// <summary>
        /// Token para realizar renovação do token de acesso
        /// </summary>
        public string RefreshToken { get; set; }
    }
}

namespace Dell.Lead.WeApi.Data.VO
{
    public class TokenVO
    {
        /// <summary>
        /// Informa se esta autenticado
        /// </summary>
        public bool Authenticated { get; set; }
        /// <summary>
        /// Data e hora de criação do token
        /// </summary>
        public string Created { get; set; }
        /// <summary>
        /// Data e hora que vai expirar o token
        /// </summary>
        public string Expiration { get; set; }
        /// <summary>
        /// Token de acesso
        /// </summary>
        public string AcessToken { get; set; }
        /// <summary>
        /// Token de renovação do token de acesso
        /// </summary>
        public string RefreshToken { get; set; }

        public TokenVO(bool authenticated, string created, string expiration, string acessToken, string refreshToken)
        {
            Authenticated = authenticated;
            Created = created;
            Expiration = expiration;
            AcessToken = acessToken;
            RefreshToken = refreshToken;
        }
    }
}

namespace Employee_Portal.Models.ResponseViewModels
{
    public class TokenViewModel
    {
        public string EncryptionKey { get; set; }
        public string SecretKeyEncrypted { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}

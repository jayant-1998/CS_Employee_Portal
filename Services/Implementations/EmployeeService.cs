using Employee_Portal.Models.RequestViewModels;
using Employee_Portal.Services.Interfaces;
using System.Text.RegularExpressions;
using System.Text;
using Employee_Portal.Extensions;
using Employee_Portal.DAL.Entities;
using System.Security.Cryptography;
using Employee_Portal.DAL.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Employee_Portal.Models.ResponseViewModels;

namespace Employee_Portal.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private const string Pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetRequiredService<IEmployeeRepository>();
        }
        public async Task<RegistrationViewModel?> RegistrationAsync(RegistrationViewModel reg)
        {
            reg.Password = HashPassword(reg.Password);
            var temp = reg.ToViewModel<RegistrationViewModel, Employee>();
            if (!Regex.Match(reg.Email, Pattern).Success)
            {
                return null;
            }
            var entity = await _repository.RegistrationAsync(temp);
            if (entity != null)
            {
                return entity.ToViewModel<Employee, RegistrationViewModel>();
            }
            return null;
        }
        static string HashPassword(string plainPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public async Task<string> LoginAsync(LoginViewModel reg)
        {
            Token? temp = await _repository.GetSecretKey();
            var key = temp.ToViewModel<Token, TokenViewModel>();
            if (key == null)
            {
                return "unauthorized";
            }
            string SecretsKey = DecryptString(key.EncryptionKey, key.SecretKeyEncrypted);
            Employee? details = await _repository.LoginAsync(reg);
            if (details != null)
            {
                if (SecretsKey != null)
                {
                    if (ComparePassword(reg.Password, details.Password))
                    {
                        return GenerateJwt(details, SecretsKey ,key);
                    }
                    return "unauthorized";
                }
                return "unauthorized";
            }
            return "unauthorized";
        }
        private static string GenerateJwt(Employee emp, string secrete,TokenViewModel key)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrete));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, emp.EmpId.ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new Claim(JwtHeaderParameterNames.Typ, emp.Role.ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                issuer: key.Issuer,
                audience: key.Issuer,
                signingCredentials: credentials
            );

            //var jwtTokenHandler = new JwtSecurityTokenHandler();
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private static bool ComparePassword(string plainPassword, string hashedPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
                string hashedInputPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hashedInputPassword == hashedPassword;
            }
        }
        //public static string EncryptString(string key, string plainText)
        //{
        //    byte[] iv = new byte[16];
        //    byte[] array;
        //    using (Aes aes = Aes.Create())
        //    {
        //        aes.Key = Encoding.UTF8.GetBytes(key);
        //        aes.IV = iv;
        //        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        //        using (MemoryStream memoryStream = new MemoryStream())
        //        {
        //            using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
        //            {
        //                using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
        //                {
        //                    streamWriter.Write(plainText);
        //                }
        //                array = memoryStream.ToArray();
        //            }
        //        }
        //    }
        //    return Convert.ToBase64String(array);
        //}
        private static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decrypt = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decrypt, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        //public string Encryption()
        //{
        //    string key = "b14ca5898a4e4133bbce2ea2315a1916";
        //    string tokened = "ABCDqyrgs7s4JHd4sbdje54poukyr4sfS74DDS5S8D44SZ55D";
        //    return EncryptString(key, tokened);
        //}
    }
}

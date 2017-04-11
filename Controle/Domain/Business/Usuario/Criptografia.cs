namespace Domain.Business.Usuario
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class Criptografia
    {
        private readonly MD5 _md5;

        public Criptografia() { _md5 = MD5.Create(); }

        public string Criptografar(string unCrypt)
        {
            byte[] data = _md5.ComputeHash(Encoding.UTF8.GetBytes(unCrypt));
            var stringBuilder = new StringBuilder();

            foreach (var item in data)
                stringBuilder.Append(item.ToString("x2"));

            return stringBuilder.ToString();
        }

        public bool AutenticarHashMd5(string unCrypt, string crypt)
        {
            var hashOfInput = Criptografar(unCrypt);
            var stringComparer = StringComparer.OrdinalIgnoreCase;

            return 0 == stringComparer.Compare(hashOfInput, crypt);
        }
    }
}

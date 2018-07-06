using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WordpressPasswordHasher
{
    public class WpPasswordHasher : IWpPasswordHasher, IDisposable
    {
        private Encoder _encoder = new Encoder();
        private MD5 _md5 = MD5.Create();
        private const int HASH_ROUNDS = 8192;

        public bool Verify(string hash, string password)
        {
            if (hash == null)
                throw new ArgumentNullException(nameof(hash), "Hash cannot be null");

            if (password == null)
                throw new ArgumentNullException(nameof(password), "Password cannot be null");

            if (hash.Length < 12)
                throw new ArgumentException("Hash cannot be shorter than 12 characters", nameof(hash));

            string salt = hash.Substring(0, 12);
            return Hash(password, salt) == hash;
        }
        
        public string Hash(string password, string salt)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password), "Password cannot be null");

            if (salt == null)
                throw new ArgumentNullException(nameof(salt), "Salt cannot be null");

            if (salt.Length != 12)
                throw new ArgumentException("Salt has to be a 12 character tring", nameof(salt));

            byte[] saltBytes = Encoding.UTF8.GetBytes(salt.Substring(4, 8));
            byte[] pwBytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = _md5.ComputeHash(saltBytes.Concat(pwBytes).ToArray());
            byte[] previousHash = hash.Concat(pwBytes).ToArray();

            for (int i = 0; i < HASH_ROUNDS; i++)
            {
                hash = _md5.ComputeHash(previousHash);
                Array.Copy(hash, previousHash, hash.Length);
            }

            return salt + _encoder.Encode64(hash, hash.Length);
        }

        public void Dispose()
        {
            if (_md5 != null)
            {
                _md5.Dispose();
                _md5 = null;
            }
        }
    }
}

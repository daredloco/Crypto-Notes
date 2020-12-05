using CryptoNotes;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Notes
{
	public class Crypta
	{
		private IBlockCipherPadding _padding = new Pkcs7Padding();
        private Encoding _encoding = Encoding.UTF8;

		public string AESEncryption(string plain, string key)
        {
            BCEngine bcEngine = new BCEngine(new AesEngine(), _encoding);
            bcEngine.SetPadding(_padding);
            return bcEngine.Encrypt(plain, key);
        }

        public string AESDecryption(string cipher, string key)
        {
            BCEngine bcEngine = new BCEngine(new AesEngine(), _encoding);
            bcEngine.SetPadding(_padding);
            return bcEngine.Decrypt(cipher, key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length">32 = 256 bit</param>
        /// <returns></returns>
        public string RandomKey(int length = 32)
		{
            string k = "";
            Random rnd = new Random();
            for(int i = 0; i < length; i++)
			{
                System.Threading.Thread.Sleep(15);
                char c = (char)rnd.Next(49, 122);
                k += c;
			}
            return k;
        }
    }
}

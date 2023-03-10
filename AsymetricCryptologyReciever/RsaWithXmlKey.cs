using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AssymetricCyptologyReciever
{
	public class RsaWithXmlKey
	{
		public void AssignKey(string publicKeyPath, string privateKeyPath)
		{
			using (var rsa = new RSACryptoServiceProvider(2048))
			{
				rsa.PersistKeyInCsp = false;

				var publicKeyfolder = Path.GetDirectoryName(publicKeyPath);
				var privateKeyfolder = Path.GetDirectoryName(privateKeyPath);

				if (!Directory.Exists(publicKeyfolder))
				{
					Directory.CreateDirectory(publicKeyfolder);
				}

				if (!Directory.Exists(privateKeyfolder))
				{
					Directory.CreateDirectory(privateKeyfolder);
				}

				if (!File.Exists(privateKeyPath))
				{
					File.WriteAllText(privateKeyPath, rsa.ToXmlString(true));
				}

				if (!File.Exists(publicKeyPath))
				{
					File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));

				}
			}
		}

		public byte[] Decrypt(string privateKeyPath, string dataToDecrypt)
		{
			byte[] dataAsBytesArray = new byte[dataToDecrypt.Length];
			dataAsBytesArray = Convert.FromBase64String(dataToDecrypt);

			byte[] plain;

			using (var rsa = new RSACryptoServiceProvider(2048))
			{
				rsa.PersistKeyInCsp = false;
				rsa.FromXmlString(File.ReadAllText(privateKeyPath));
				plain = rsa.Decrypt(dataAsBytesArray, false);
				return plain;
			}

		}
	}
}

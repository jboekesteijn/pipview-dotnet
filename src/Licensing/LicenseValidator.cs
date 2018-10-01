using System.Security.Cryptography;
using System.Text;

namespace PipView.Licensing
{
	public static class LicenseValidator
	{
		private static RSACryptoServiceProvider rsa = InitializeRSACryptoServiceProvider();
		private static SHA1Managed sha = new SHA1Managed();

		private static RSACryptoServiceProvider InitializeRSACryptoServiceProvider()
		{
			RSAParameters publicKey = new RSAParameters();

			publicKey.Modulus = new byte[] { 190, 192, 4, 144, 173, 132, 240, 134, 32, 151, 111, 219, 222, 81, 74, 240, 116, 6, 110, 115, 248, 231, 118, 217, 22, 245, 128, 99, 31, 104, 216, 228, 195, 18, 108, 34, 189, 12, 208, 68, 217, 243, 9, 73, 50, 51, 137, 26, 109, 112, 87, 154, 54, 190, 133, 148, 173, 126, 21, 115, 155, 72, 145, 105, 9, 127, 17, 175, 242, 136, 165, 58, 2, 102, 168, 154, 9, 234, 255, 198, 62, 1, 88, 211, 28, 25, 201, 251, 112, 122, 142, 157, 177, 157, 222, 143, 75, 241, 86, 8, 211, 24, 115, 115, 190, 161, 159, 39, 135, 13, 61, 124, 167, 169, 43, 81, 252, 103, 173, 132, 41, 164, 226, 149, 121, 91, 119, 81 };
			publicKey.Exponent = new byte[] { 1, 0, 1 };

			RSACryptoServiceProvider rsaCSP = new RSACryptoServiceProvider();

			rsaCSP.ImportParameters(publicKey);

			return rsaCSP;
		}

		public static bool VerifySignature(string data, byte[] signature)
		{
			return rsa.VerifyData(Encoding.UTF8.GetBytes(data), sha, signature);
		}
	}
}

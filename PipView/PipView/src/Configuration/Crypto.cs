using System.Security.Cryptography;
using System.Text;

namespace PipView.Configuration
{
	public static class Crypto
	{
		private static RijndaelManaged RM = InitializeRijndaelManaged();
		private static ICryptoTransform CTE = RM.CreateEncryptor();
		private static ICryptoTransform CTD = RM.CreateDecryptor();

		private static RijndaelManaged InitializeRijndaelManaged()
		{
			RijndaelManaged rijndael = new RijndaelManaged();

			rijndael.IV = new byte[] { 137, 137, 36, 113, 15, 63, 61, 104, 160, 197, 134, 182, 62, 65, 211, 189 };
			rijndael.Key = new byte[] { 11, 4, 95, 102, 155, 13, 37, 226, 187, 205, 226, 53, 43, 207, 42, 159, 146, 189, 127, 158, 6, 95, 106, 216, 232, 166, 21, 197, 111, 125, 17, 44 };

			return rijndael;
		}

		internal static byte[] Encrypt(string data)
		{
			byte[] input = Encoding.UTF8.GetBytes(data);

			return CTE.TransformFinalBlock(input, 0, input.Length);
		}

		internal static string Decrypt(byte[] data)
		{
			return Encoding.UTF8.GetString(CTD.TransformFinalBlock(data, 0, data.Length));
		}
	}
}

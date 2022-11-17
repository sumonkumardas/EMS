using System.Security.Cryptography;
using System.Text;

namespace SinePulse.EMS.Utility
{
  public class StringGenerator
  {
    public static string GenerateUniqueNumberKey(int keylen)
    {
      char[] chars = new char[62];
      string a;
      a = "123456789";
      chars = a.ToCharArray();
      byte[] data = new byte[1];
      RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
      crypto.GetNonZeroBytes(data);
      data = new byte[keylen];
      crypto.GetNonZeroBytes(data);
      StringBuilder result = new StringBuilder(keylen);
      foreach (byte b in data)
      {
        result.Append(chars[b % (chars.Length - 1)]);
      }

      return result.ToString().ToUpper();
    }

    public static string GenerateUniqueStringKey(int keylen)
    {
      char[] chars = new char[62];
      string a;
      a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
      chars = a.ToCharArray();
      byte[] data = new byte[1];
      RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
      crypto.GetNonZeroBytes(data);
      data = new byte[keylen];
      crypto.GetNonZeroBytes(data);
      StringBuilder result = new StringBuilder(keylen);
      foreach (byte b in data)
      {
        result.Append(chars[b % (chars.Length - 1)]);
      }

      return result.ToString().ToUpper();
    }
  }
}
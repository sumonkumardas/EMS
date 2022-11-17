using System;
using System.Security.Cryptography;
using System.Text;

namespace SinePulse.EMS.Utility
{
  public class Md5EncryptDecryptor
  {
    public static string EncryptorDecrypt(string key, bool encrypt, string salt)
    {
      byte[] toEncryptorDecryptArray;
      ICryptoTransform cTransform;
      // Transform the specified region of bytes array to resultArray
      MD5CryptoServiceProvider md5Hasing = new MD5CryptoServiceProvider();
      byte[] keyArrays = md5Hasing.ComputeHash(UTF8Encoding.UTF8.GetBytes(salt));
      md5Hasing.Clear();
      TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider()
        {Key = keyArrays, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7};
      if (encrypt == true)
      {
        toEncryptorDecryptArray = UTF8Encoding.UTF8.GetBytes(key);
        cTransform = tdes.CreateEncryptor();
      }
      else
      {
        toEncryptorDecryptArray = Convert.FromBase64String(key.Replace(' ', '+'));
        cTransform = tdes.CreateDecryptor();
      }

      byte[] resultsArray = cTransform.TransformFinalBlock(toEncryptorDecryptArray, 0, toEncryptorDecryptArray.Length);
      tdes.Clear();
      if (encrypt == true)
      {
        //if encrypt we need to return encrypted string
        return Convert.ToBase64String(resultsArray, 0, resultsArray.Length);
      }

      //else we need to return decrypted string
      return UTF8Encoding.UTF8.GetString(resultsArray);
    }
  }
}
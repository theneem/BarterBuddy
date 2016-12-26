using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BarterBuddy.Common.Helper
{
    public class Helper
    {
        //List<SearchParameters> listParam = new List<SearchParameters>();

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        /// <summary>
        /// Gets the current page.
        /// </summary>
        /// <param name="pageNo">The page no.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public static int GetCurrentPage(int pageNo, int size)
        {
            if (pageNo * size == size)
                return pageNo - 1;
            return (pageNo * size) - size;
        }

        public static string ConvertToBase64String(string strInput)
        {
            byte[] bytesInput = UTF8Encoding.UTF8.GetBytes(strInput);
            string output = Convert.ToBase64String(bytesInput);
            return output;
        }

        public static string ConvertFromBase64String(string strInput)
        {
            byte[] bytesInput = Convert.FromBase64String(strInput);
            string output = UTF8Encoding.UTF8.GetString(bytesInput);
            return output;
        }

        public static string ConvertHexToString(string hexValue)
        {
            string strValue = "";
            while (hexValue.Length > 0)
            {
                strValue += System.Convert.ToChar(System.Convert.ToUInt32(hexValue.Substring(0, 2), 16)).ToString();
                hexValue = hexValue.Substring(2, hexValue.Length - 2);
            }
            return strValue;
        }

        //public static X509Certificate GetSSLCertificateOfURL(string url)
        //{
        //    Uri u = new Uri(url);
        //    ServicePoint sp = ServicePointManager.FindServicePoint(u);

        //    string groupName = Guid.NewGuid().ToString();
        //    HttpWebRequest req = HttpWebRequest.Create(u) as HttpWebRequest;
        //    req.Accept = "*/*";
        //    req.ConnectionGroupName = groupName;

        //    try
        //    {
        //        using (WebResponse resp = req.GetResponse())
        //        {
        //            // Ignore response
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        //Log.WriteLog(LogLevelL4N.ERROR, "GetSSLCertificateOfURL : " + ex.ToString());
        //        // TODO: This indicates SSL is not valid 
        //        // Check how which CA list it uses to verify
        //    }
        //    finally
        //    {
        //        // Close the response
        //        sp.CloseConnectionGroup(groupName);
        //    }

        //    return sp.Certificate;
        //}

        //private static Uri CheckAndUpdateURL(string url)
        //{
        //    if (url.Contains("http://"))
        //        throw new Exception("URL must have https:// suffix. (It will be added if not provided");
        //    else if (!url.Contains("http"))
        //        url = "https://" + url;

        //    return new Uri(url);
        //}

        public static byte[] StringToByteArray(String hex)
        {
            int numberChars = hex.Length / 2;
            byte[] bytes = new byte[numberChars];
            using (var sr = new StringReader(hex))
            {
                for (int i = 0; i < numberChars; i++)
                    bytes[i] =
                      Convert.ToByte(new string(new char[2] { (char)sr.Read(), (char)sr.Read() }), 16);
            }
            return bytes;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static byte[] ConvertStreamToByte(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        /// <summary>
        /// Converts to hash string.
        /// </summary>
        /// <param name="strInput">The STR input.</param>
        /// <returns></returns>
        public static string ConvertToHashString(string strInput)
        {
            if (!string.IsNullOrEmpty(strInput))
            {
                HashAlgorithm sha = new SHA1CryptoServiceProvider();
                byte[] input = UTF8Encoding.UTF8.GetBytes(strInput);
                byte[] bytesInput = sha.ComputeHash(input);
                string output = Convert.ToBase64String(bytesInput);
                return output;
            }
            return strInput;
        }
    }
}

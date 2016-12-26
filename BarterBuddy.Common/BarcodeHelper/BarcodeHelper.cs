using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;

namespace BarterBuddy.Common.BarcodeHelper
{
    public class BarcodeHelper
    {
        /// <summary>
        /// Gets the barcode128.
        /// </summary>
        public static byte[] GetBarcode128(string input)
        {
            Barcode128 code128 = new Barcode128();
            code128.CodeType = Barcode.CODE128;
            code128.Code = input;

            MemoryStream ms = new MemoryStream();
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White));
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] imgByte = ms.ToArray();

            return imgByte;
        }

        public static byte[] GetPrintableBarcode128(string input, string text)
        {
            Barcode128 code128 = new Barcode128();
            code128.CodeType = Barcode.CODE128;
            code128.Code = input;
            code128.ChecksumText = true;
            code128.GenerateChecksum = true;
            code128.AltText = input;

            MemoryStream ms = new MemoryStream();

            Bitmap bmp = new Bitmap(300, 60);
            Graphics grp = Graphics.FromImage(bmp);
            grp.Clear(Color.White);
            grp.DrawString(text, new System.Drawing.Font("Segoe UI", 10), SystemBrushes.WindowText, new Point(0, 0));
            grp.DrawImage(code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White), new Point(0, 18));
            grp.DrawString(input, new System.Drawing.Font("Segoe UI", 10), SystemBrushes.WindowText, new Point(0, 40));

            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] imgByte = ms.ToArray();

            return imgByte;
        }

        public static iTextSharp.text.Image GetQRCode(string input)
        {
            return new BarcodeQRCode(input, 80, 80, null).GetImage();
        }
    }
}

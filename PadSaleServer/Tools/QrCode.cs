using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using ThoughtWorks.QRCode.Codec;

namespace ShopSaleForPad.Tools
{
    public class QrCode
    {
        private static QRCodeEncoder encoder = new QRCodeEncoder();

        public static Bitmap BitmapTo2Bpp(Bitmap img)
        {
            int width = img.Width;
            int height = img.Height;
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format1bppIndexed);
            BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format1bppIndexed);
            for (int i = 0; i < height; i++)
            {
                byte[] source = new byte[(width + 7) / 8];
                for (int j = 0; j < width; j++)
                {
                    if (img.GetPixel(j, i).GetBrightness() >= 0.5)
                    {
                        source[j / 8] = (byte)(source[j / 8] | ((byte)(((int)0x80) >> (j % 8))));
                    }
                }
                Marshal.Copy(source, 0, (IntPtr)(((int)bitmapdata.Scan0) + (bitmapdata.Stride * i)), source.Length);
            }
            bitmap.UnlockBits(bitmapdata);
            return bitmap;
        }

        public static Bitmap CreateCode(string WantStringCoed, QRCodeEncoder.ENCODE_MODE CT, QRCodeEncoder.ERROR_CORRECTION CC, int VersionNum, int CreateSize)
        {
            try
            {
                encoder.QRCodeEncodeMode = CT;
                encoder.QRCodeErrorCorrect = CC;
                encoder.QRCodeVersion = VersionNum;
                encoder.QRCodeScale = CreateSize;
                return encoder.Encode(WantStringCoed);
            }
            catch 
            {
                return null;
            }
        }
    }
}
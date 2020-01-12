using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace PhacoxsInjector
{
    public class RomNDS : RomFile, IDisposable
    {
        private bool disposed = false;

        public string TitleLine1 { private set; get; }
        public string TitleLine2 { private set; get; }
        public Bitmap Icon
        { private set; get; }

        public RomNDS(string filename)
            : base()
        {
            TitleLine1 = "";
            TitleLine2 = "";
            Icon = null;

            byte[] header = new byte[0x200];
            FileStream fs = File.OpenRead(filename);
            fs.Read(header, 0, 0x200);
            fs.Close();

            if (Validate(header))
            {
                byte uniqueCode;
                byte[] shortTitle = new byte[2];
                byte region;

                uniqueCode = header[0x0C];
                shortTitle[0] = header[0x0D];
                shortTitle[1] = header[0x0E];
                region = header[0x0F];
                Version = header[0x1E];
                FormatCode = (char)uniqueCode;
                ShortId = Encoding.ASCII.GetString(shortTitle);
                RegionCode = (char)region;

                byte[] offsetBytes = new byte[4];
                byte[] bitmapBytes = new byte[0x200];
                byte[] paletteBytes = new byte[0x20];
                byte[] titleBytes = new byte[0x100];

                fs = File.OpenRead(filename);
                Size = (int)fs.Length;
                fs.Seek(0x68, SeekOrigin.Begin);
                fs.Read(offsetBytes, 0, 4);
                int offset = (offsetBytes[3] << 24) + (offsetBytes[2] << 16) + (offsetBytes[1] << 8) + offsetBytes[0];
                fs.Seek(offset + 0x20, SeekOrigin.Begin);
                fs.Read(bitmapBytes, 0, 0x200);
                fs.Read(paletteBytes, 0, 0x20);
                fs.Read(titleBytes, 0, 0x100);
                fs.Position = 0;
                HashCRC16 = Cll.Security.ComputeCRC16(fs);
                fs.Close();

                string title = Encoding.Unicode.GetString(titleBytes);
                string[] lines = title.Split(new char[] { '\n' });

                if (lines.Length == 2)
                {
                    Title = lines[0];
                    TitleLine1 = lines[0];
                }
                else if (lines.Length >= 3)
                {
                    Title = lines[0] + " " + lines[1];
                    TitleLine1 = lines[0];
                    TitleLine2 = lines[1];
                }

                Color[] palette = new Color[16];
                int j = -1;
                for (int i = 0; i < 16; i++)
                    palette[i] = Color.FromArgb(
                        (paletteBytes[++j] & 0x1F) << 3,   //0000 0000 0001 1111
                        ((paletteBytes[j] & 0xE0) >> 2) +  //0000 0000 1110 0000
                        ((paletteBytes[++j] & 0x03) << 6), //0000 0011 0000 0000
                        (paletteBytes[j] & 0x7C) << 1);    //0111 1100 0000 0000
                palette[0] = Color.FromArgb(0, palette[0].R, palette[0].G, palette[0].B);

                byte[] pix = new byte[1024];

                int pixIndex;
                int bytesIndex;
                for (int tileY = 0; tileY < 4; tileY++)
                    for (int tileX = 0; tileX < 4; tileX++)
                        for (int i = 0; i < 8; i++)
                        {
                            pixIndex = i * 32 + tileX * 8 + tileY * 256;
                            bytesIndex = i * 4 + tileX * 32 + tileY * 128;
                            pix[pixIndex] = (byte)(bitmapBytes[bytesIndex] & 0x0F);
                            pix[pixIndex + 1] = (byte)((bitmapBytes[bytesIndex] & 0xF0) >> 4);
                            pix[pixIndex + 2] = (byte)(bitmapBytes[bytesIndex + 1] & 0x0F);
                            pix[pixIndex + 3] = (byte)((bitmapBytes[bytesIndex + 1] & 0xF0) >> 4);
                            pix[pixIndex + 4] = (byte)(bitmapBytes[bytesIndex + 2] & 0x0F);
                            pix[pixIndex + 5] = (byte)((bitmapBytes[bytesIndex + 2] & 0xF0) >> 4);
                            pix[pixIndex + 6] = (byte)(bitmapBytes[bytesIndex + 3] & 0x0F);
                            pix[pixIndex + 7] = (byte)((bitmapBytes[bytesIndex + 3] & 0xF0) >> 4);
                        }

                Bitmap icon = new Bitmap(32, 32);
                Rectangle rect = new Rectangle(0, 0, icon.Width, icon.Height);
                System.Drawing.Imaging.BitmapData data =
                    icon.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    icon.PixelFormat);
                int length = data.Width * data.Height * 4;
                byte[] iconBytes = new byte[length];
                IntPtr ptr = data.Scan0;
                System.Runtime.InteropServices.Marshal.Copy(ptr, iconBytes, 0, length);
                Color color;
                for (int i = 0; i < 1024; i++)
                {
                    color = palette[pix[i]];
                    iconBytes[i * 4] = color.B;
                    iconBytes[i * 4 + 1] = color.G;
                    iconBytes[i * 4 + 2] = color.R;
                    iconBytes[i * 4 + 3] = color.A;
                }
                System.Runtime.InteropServices.Marshal.Copy(iconBytes, 0, ptr, length);
                icon.UnlockBits(data);

                Icon = icon;
                IsValid = true;
                Console = Format.NDS;
            }
            else                
                throw new FormatException("Checksums in the NDS ROM header are invalid.");
        }

        private static bool Validate(byte[] header)
        {
            ushort logoCrc = (ushort)((header[0x15D] << 8) + header[0x15C]);
            ushort headerCrc = (ushort)((header[0x15F] << 8) + header[0x15E]);
            ushort logoCrcComp = Cll.Security.ComputeCRC16_MODBUS(header, 0xC0, 0x9C);
            ushort headerCrcComp = Cll.Security.ComputeCRC16_MODBUS(header, 0, 0x15E);

            if (logoCrc == logoCrcComp && headerCrc == headerCrcComp)
                return true;

            return false;
        }

        public static bool Validate(string filename)
        {
            byte[] header = new byte[0x200];
            FileStream fs = File.OpenRead(filename);
            fs.Read(header, 0, 0x200);
            fs.Close();
            return Validate(header);
        }

        ~RomNDS()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (Icon != null)
                    {
                        Icon.Dispose();
                        Icon = null;
                    }
                }
                disposed = true;
            }
        }
    }
}

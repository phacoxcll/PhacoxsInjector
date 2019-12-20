using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace PhacoxsInjector
{
    public static class NusContent
    {
        public const uint CommonKeyHashCRC32 = 0xB92B703D;

        public enum Format
        {
            Encrypted,
            Decrypted,
            Indeterminate
        }

        public static string GetTitleID(string path)
        {
            if (File.Exists(path + "\\meta\\meta.xml"))
            {
                XmlDocument xmlMeta = new XmlDocument();
                xmlMeta.Load(path + "\\meta\\meta.xml");
                XmlNode meta_title_id = xmlMeta.SelectSingleNode("menu/title_id");
                return meta_title_id.InnerText;
            }
            else
                throw new Exception("The \"" + path + "\\meta\\meta.xml\" file not exist.");
        }

        public static void Check(string path)
        {
            if (!Directory.Exists(path + "\\code"))
                throw new Exception("The \"" + path + "\\code\" folder not exist.");
            if (!Directory.Exists(path + "\\content"))
                throw new Exception("The \"" + path + "\\content\" folder not exist.");
            if (!Directory.Exists(path + "\\meta"))
                throw new Exception("The \"" + path + "\\meta\" folder not exist.");
        }

        public static void CheckEncrypted(string path)
        {
            if (!File.Exists(path + "\\title.tmd"))
                throw new Exception("The \"" + path + "\\title.tmd\" file not exist.");
            if (!File.Exists(path + "\\title.tik"))
                throw new Exception("The \"" + path + "\\title.tik\" file not exist.");
            if (!File.Exists(path + "\\title.cert"))
                throw new Exception("The \"" + path + "\\title.cert\" file not exist.");
        }

        public static Format GetFormat(string path)
        {
            Format format = Format.Indeterminate;
            try
            {
                CheckEncrypted(path);
                format = Format.Encrypted;
            }
            catch
            {
                try
                {
                    Check(path);
                    format = Format.Decrypted;
                }
                catch
                {
                }
            }

            return format;
        }

        public static void CheckBatchFiles()
        {
            if (!Directory.Exists(Environment.CurrentDirectory + "\\resources"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\resources");
            if (!Directory.Exists(Environment.CurrentDirectory + "\\resources\\pack"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\resources\\pack");
            if (!Directory.Exists(Environment.CurrentDirectory + "\\resources\\unpack"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\resources\\unpack");

            if (!File.Exists(Environment.CurrentDirectory + "\\resources\\pack\\run.bat"))
            {
                StreamWriter sw = File.CreateText(Environment.CurrentDirectory + "\\resources\\pack\\run.bat");
                sw.WriteLine("@echo off");
                sw.WriteLine("cd resources\\pack");
                sw.Write("CNUSPACKER.exe -in %1 -out %2");
                sw.Close();
            }

            if (!File.Exists(Environment.CurrentDirectory + "\\resources\\unpack\\run.bat"))
            {
                StreamWriter sw = File.CreateText(Environment.CurrentDirectory + "\\resources\\unpack\\run.bat");
                sw.WriteLine("@echo off");
                sw.WriteLine("cd resources\\unpack");
                sw.Write("CDecrypt.exe %1 %2");
                sw.Close();
            }

            if (!File.Exists(Environment.CurrentDirectory + "\\resources\\unpack\\getfile.bat"))
            {
                StreamWriter sw = File.CreateText(Environment.CurrentDirectory + "\\resources\\unpack\\getfile.bat");
                sw.WriteLine("@echo off");
                sw.WriteLine("cd resources\\unpack");
                sw.Write("CDecrypt.exe %1 %2 %3");
                sw.Close();
            }
        }

        private static bool IsValidCommonKey(string key)
        {
            byte[] array = Encoding.ASCII.GetBytes(key.ToUpper());
            uint hash = Cll.Security.ComputeCRC32(array, 0, array.Length);
            if (hash == CommonKeyHashCRC32)
                return true;
            return false;
        }

        public static bool LoadKey(string key)
        {
            if (IsValidCommonKey(key))
            {
                try
                {
                    if (!Directory.Exists(Environment.CurrentDirectory + "\\resources"))
                        Directory.CreateDirectory(Environment.CurrentDirectory + "\\resources");
                    if (!Directory.Exists(Environment.CurrentDirectory + "\\resources\\pack"))
                        Directory.CreateDirectory(Environment.CurrentDirectory + "\\resources\\pack");
                    if (!Directory.Exists(Environment.CurrentDirectory + "\\resources\\unpack"))
                        Directory.CreateDirectory(Environment.CurrentDirectory + "\\resources\\unpack");

                    StreamWriter sw = File.CreateText(Environment.CurrentDirectory + "\\resources\\pack\\encryptKeyWith");
                    sw.WriteLine(key.ToUpper());
                    sw.Close();
                    sw = File.CreateText(Environment.CurrentDirectory + "\\resources\\unpack\\keys.txt");
                    sw.WriteLine(key.ToUpper());
                    sw.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public static bool CheckCommonKeyFiles()
        {
            StreamReader sr;
            StreamWriter sw;
            string cdecryptKey = "";
            string nuspackerKey = "";
            bool cdecryptKeyValid = false;
            bool nuspackerKeyValid = false;

            if (File.Exists(Environment.CurrentDirectory + "\\resources\\unpack\\keys.txt"))
            {
                sr = File.OpenText(Environment.CurrentDirectory + "\\resources\\unpack\\keys.txt");
                cdecryptKey = sr.ReadLine();
                sr.Close();
                cdecryptKeyValid = IsValidCommonKey(cdecryptKey);
            }

            if (File.Exists(Environment.CurrentDirectory + "\\resources\\pack\\encryptKeyWith"))
            {
                sr = File.OpenText(Environment.CurrentDirectory + "\\resources\\pack\\encryptKeyWith");
                nuspackerKey = sr.ReadLine();
                sr.Close();
                nuspackerKeyValid = IsValidCommonKey(nuspackerKey);
            }

            if (cdecryptKeyValid && nuspackerKeyValid)
                return true;
            else if (cdecryptKeyValid && !nuspackerKeyValid)
            {
                try
                {
                    sw = File.CreateText(Environment.CurrentDirectory + "\\resources\\pack\\encryptKeyWith");
                    sw.WriteLine(cdecryptKey);
                    sw.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else if (!cdecryptKeyValid && nuspackerKeyValid)
            {
                try
                {
                    sw = File.CreateText(Environment.CurrentDirectory + "\\resources\\unpack\\keys.txt");
                    sw.WriteLine(nuspackerKey);
                    sw.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }

        public static void Encrypt(string inputPath, string outputPath)
        {
            Check(inputPath);
            if (!CheckCommonKeyFiles())
                throw new Exception("Common Key Files error.");
            if (!File.Exists(Environment.CurrentDirectory + "\\resources\\pack\\CNUSPACKER.exe"))
                throw new Exception("The \"" + Environment.CurrentDirectory + "\\resources\\pack\\CNUSPACKER.exe\" file not exist.");
            
            CheckBatchFiles();
            Process encrypt = Process.Start(Environment.CurrentDirectory + "\\resources\\pack\\run.bat", "\"" + inputPath + "\" \"" + outputPath + "\"");
            encrypt.WaitForExit();


            if (encrypt.ExitCode == 0)            
                encrypt.Dispose();
            else
            {
                encrypt.Dispose();
                throw new Exception("Encrypt fail.");
            }
        }

        public static void Decrypt(string inputPath, string outputPath)
        {
            CheckEncrypted(inputPath);
            if (!CheckCommonKeyFiles())
                throw new Exception("Common Key Files error.");
            if (!File.Exists(Environment.CurrentDirectory + "\\resources\\unpack\\libeay32.dll"))
                throw new Exception("The \"" + Environment.CurrentDirectory + "\\resources\\unpack\\libeay32.dll\" file not exist.");
            if (!File.Exists(Environment.CurrentDirectory + "\\resources\\unpack\\CDecrypt.exe"))
                throw new Exception("The \"" + Environment.CurrentDirectory + "\\resources\\unpack\\CDecrypt.exe\" file not exist.");

            CheckBatchFiles();
            Process decrypt = Process.Start(Environment.CurrentDirectory + "\\resources\\unpack\\run.bat", "\"" + inputPath + "\" \"" + outputPath + "\"");
            decrypt.WaitForExit();
            decrypt.Dispose();

            if (decrypt.ExitCode == 0)
                decrypt.Dispose();
            else
            {
                decrypt.Dispose();
                throw new Exception("Decrypt fail.");
            }
        }

        public static void Decrypt(string inputPath, string filename, string outputFilename)
        {
            CheckEncrypted(inputPath);
            if (!CheckCommonKeyFiles())
                throw new Exception("Common Key Files error.");
            if (!File.Exists(Environment.CurrentDirectory + "\\resources\\unpack\\libeay32.dll"))
                throw new Exception("The \"" + Environment.CurrentDirectory + "\\resources\\unpack\\libeay32.dll\" file not exist.");
            if (!File.Exists(Environment.CurrentDirectory + "\\resources\\unpack\\CDecrypt.exe"))
                throw new Exception("The \"" + Environment.CurrentDirectory + "\\resources\\unpack\\CDecrypt.exe\" file not exist.");

            CheckBatchFiles();
            Process decrypt = Process.Start(Environment.CurrentDirectory + "\\resources\\unpack\\getfile.bat", "\"" + inputPath + "\" \"" + filename + "\" \"" + outputFilename + "\"");
            decrypt.WaitForExit();
            decrypt.Dispose();

            if (decrypt.ExitCode == 0)
                decrypt.Dispose();
            else
            {
                decrypt.Dispose();
                throw new Exception("Decrypt file fail.");
            }
        }

        public static bool SaveTGA(Bitmap image, string outputFilename)
        {
            if (image.Width > 0xFFFF || image.Height > 0xFFFF)
                return false;

            byte[] header = {
                    0x00, 0x00, 0x02, 0x00,
                    0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00,
                    (byte)(image.Width & 0xFF),
                    (byte)((image.Width >> 8) & 0xFF),
                    (byte)(image.Height & 0xFF),
                    (byte)((image.Height >> 8) & 0xFF),
                    0x00, 0x00 };

            byte[] footer = {
                    0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00,
                    0x54, 0x52, 0x55, 0x45,
                    0x56, 0x49, 0x53, 0x49,
                    0x4F, 0x4E, 0x2D, 0x58,
                    0x46, 0x49, 0x4C, 0x45,
                    0x2E, 0x00 };

            int bytesPerPixel = 0;
            if (image.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            {
                header[0x10] = 0x20;
                header[0x11] = 0x08;
                bytesPerPixel = 4;
            }
            else if (image.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            {
                header[0x10] = 0x18;
                bytesPerPixel = 3;
            }
            else
                return false;

            Bitmap bmp = new Bitmap(image.Width, image.Height, image.PixelFormat);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height));
            g.Dispose();

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData data =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);
            int length = data.Width * data.Height * bytesPerPixel;
            byte[] bmpBytes = new byte[length];
            for (int y = 0; y < data.Height; y++)
            {
                IntPtr ptr = (IntPtr)((long)data.Scan0 + y * data.Stride);
                System.Runtime.InteropServices.Marshal.Copy(
                    ptr, bmpBytes, (data.Height - 1 - y) * data.Width * bytesPerPixel, data.Width * bytesPerPixel);
            }
            bmp.UnlockBits(data);
            bmp.Dispose();

            FileStream fs = File.Open(outputFilename, FileMode.Create);
            fs.Write(header, 0, header.Length);
            fs.Write(bmpBytes, 0, bmpBytes.Length);
            fs.Write(footer, 0, footer.Length);
            fs.Close();

            return true;
        }

        public static Bitmap TGAToBitmap(string filename)
        {
            FileStream fs = File.Open(filename, FileMode.Open);
            byte[] header = new byte[0x12];
            fs.Read(header, 0, 0x12);
            fs.Close();

            if (header[0x00] != 0 ||
                header[0x01] != 0 ||
                header[0x02] != 0x02 ||
                header[0x03] != 0 ||
                header[0x04] != 0 ||
                header[0x05] != 0 ||
                header[0x06] != 0 ||
                header[0x07] != 0 ||
                header[0x08] != 0 ||
                header[0x09] != 0 ||
                header[0x0A] != 0 ||
                header[0x0B] != 0)
                throw new FormatException("Unsupported TGA type.");

            int bytesPerPixel = 0;
            System.Drawing.Imaging.PixelFormat pixelFormat;
            if (header[0x10] == 0x20 && header[0x11] == 0x08)
            {
                bytesPerPixel = 4;
                pixelFormat = System.Drawing.Imaging.PixelFormat.Format32bppArgb;
            }
            else if (header[0x10] == 0x18 && header[0x11] == 0)
            {
                bytesPerPixel = 3;
                pixelFormat = System.Drawing.Imaging.PixelFormat.Format24bppRgb;
            }
            else
                throw new FormatException("Unsupported TGA format.");

            int width = (header[0x0D] << 8) + header[0x0C];
            int height = (header[0x0F] << 8) + header[0x0E];

            fs = File.Open(filename, FileMode.Open);
            byte[] tgaBytes = new byte[width * height * bytesPerPixel];
            fs.Position = 0x12;
            fs.Read(tgaBytes, 0, tgaBytes.Length);
            fs.Close();

            Bitmap bmp = new Bitmap(width, height, pixelFormat);
            
            /*Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height));
            g.Dispose();
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);*/

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData data =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly, bmp.PixelFormat);

            for (int y = 0; y < data.Height; y++)
            {
                IntPtr ptr = (IntPtr)((long)data.Scan0 + y * data.Stride);
                System.Runtime.InteropServices.Marshal.Copy(
                     tgaBytes, (data.Height - 1 - y) * data.Width * bytesPerPixel, ptr, data.Width * bytesPerPixel);
            }
            bmp.UnlockBits(data);
            //bmp.Dispose();

            /*FileStream fs = File.Open(outputFileName, FileMode.Create);
            fs.Write(header, 0, header.Length);
            fs.Write(bmpBytes, 0, bmpBytes.Length);
            fs.Write(footer, 0, footer.Length);
            fs.Close();*/

            return bmp;
        }
    }
}

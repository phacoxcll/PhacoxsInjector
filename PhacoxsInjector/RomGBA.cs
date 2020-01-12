using System;
using System.IO;
using System.Text;

namespace PhacoxsInjector
{
    public class RomGBA : RomFile
    {
        public RomGBA(string filename)
            : base()
        {
            byte[] header = new byte[0xC0];
            FileStream fs = File.OpenRead(filename);

            fs.Read(header, 0, 0xC0);
            fs.Close();

            if (Validate(header))
            {
                byte uniqueCode;
                byte[] shortTitle = new byte[2];
                byte region;

                uniqueCode = header[0xAC];
                shortTitle[0] = header[0xAD];
                shortTitle[1] = header[0xAE];
                region = header[0xAF];
                Version = header[0xBC];
                FormatCode = (char)uniqueCode;
                ShortId = Encoding.ASCII.GetString(shortTitle);
                RegionCode = (char)region;

                byte[] titleBytes = new byte[0x0C];
                Array.Copy(header, 0xA0, titleBytes, 0, 0x0C);
                int count = 0x0C;
                while (titleBytes[--count] == 0 && count > 0) ;                
                Title = Encoding.ASCII.GetString(titleBytes, 0, count + 1);

                fs = File.Open(filename, FileMode.Open);
                Size = (int)fs.Length;
                HashCRC16 = Cll.Security.ComputeCRC16(fs);
                fs.Close();

                IsValid = true;
                Console = Format.GBA;
            }
            else
                throw new FormatException("Check bytes in the GBA ROM header are invalid.");
        }

        private static bool Validate(byte[] header)
        {
            int chk = 0;
            for (int i = 0xA0; i < 0xBD; i++)
                chk -= header[i];
            chk = (chk - 0x19) & 0xFF;

            if (header[0xB2] == 0x96 && header[0xBD] == chk)
                return true;

            return false;
        }

        public static bool Validate(string filename)
        {
            byte[] header = new byte[0xC0];
            FileStream fs = File.OpenRead(filename);
            fs.Read(header, 0, 0xC0);
            fs.Close();
            return Validate(header);
        }
    }
}

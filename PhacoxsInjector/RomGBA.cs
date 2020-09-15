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

                if (Useful.IsUpperLetterOrDigit(uniqueCode))
                    FormatCode = (char)uniqueCode;
                if (Useful.IsUpperLetterOrDigit(shortTitle[0]) &&
                    Useful.IsUpperLetterOrDigit(shortTitle[1]))
                    ShortId = Encoding.ASCII.GetString(shortTitle);
                if (Useful.IsUpperLetterOrDigit(region))
                    RegionCode = (char)region;
                if (Useful.IsUpperLetterOrDigit(header[0xBC]))
                    Version = header[0xBC];

                byte[] titleBytes = new byte[0x0C];
                Array.Copy(header, 0xA0, titleBytes, 0, 0x0C);
                int count = 0x0C;
                while (--count >= 0 && titleBytes[count] == 0) ;                
                Title = Encoding.ASCII.GetString(titleBytes, 0, count + 1);

                fs = File.Open(filename, FileMode.Open);
                Size = (int)fs.Length;
                HashCRC16 = Cll.Security.ComputeCRC16(fs);
                fs.Close();

                if (Size > 33554432)
                    throw new FormatException("The GBA ROM has more than 32 MiB.");

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
            if (File.Exists(filename))
            {
                byte[] header = new byte[0xC0];
                FileStream fs = File.OpenRead(filename);
                fs.Read(header, 0, 0xC0);
                fs.Close();
                return Validate(header);
            }
            return false;
        }
    }
}

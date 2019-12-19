using System;
using System.IO;
using System.Text;

namespace PhacoxsInjector
{
    public class RomNES : RomFile
    {
        public enum Subformat
        {
            iNES,  //Archaic iNES, iNES 0.7 and iNES 1.0
            NES20, //NES 2.0
            FDS,   //Famicom Disk System
            Indeterminate
        }

        public Subformat Header
        { private set; get; }

        public RomNES(string filename)
            : base()
        {
            Header = Subformat.Indeterminate;

            byte[] header = new byte[0x10];
            FileStream fs = File.Open(filename, FileMode.Open);
            fs.Read(header, 0, 0x10);
            fs.Close();

            Header = GetFormat(header);

            if (Header != Subformat.Indeterminate)
            {
                fs = File.Open(filename, FileMode.Open);
                Size = (int)fs.Length - (Header == Subformat.FDS ? 0 : 16);
                HashCRC16 = Cll.Security.ComputeCRC16(fs);
                fs.Close();

                IsValid = true;
                Console = Format.NES;

            }
            else                
                throw new FormatException("It was not possible to determine the NES ROM format.");                
            
        }

        private static Subformat GetFormat(byte[] header)
        {
            Subformat format = Subformat.Indeterminate;

            if (header[0] == 0x4E && header[1] == 0x45 && header[2] == 0x53 && header[3] == 0x1A)
            {
                if ((header[7] & 0x0C) == 0x08)
                    format = Subformat.NES20;
                else
                    format = Subformat.iNES;
            }
            else if(header[0] == 0x01 &&
                header[1] == 0x2A &&
                header[2] == 0x4E &&
                header[3] == 0x49 &&
                header[4] == 0x4E &&
                header[5] == 0x54 &&
                header[6] == 0x45 &&
                header[7] == 0x4E &&
                header[8] == 0x44 &&
                header[9] == 0x4F &&
                header[10] == 0x2D &&
                header[11] == 0x48 &&
                header[12] == 0x56 &&
                header[13] == 0x43 &&
                header[14] == 0x2A)
            {
                format = Subformat.FDS;
            }

            return format;
        }

        public static bool Validate(string filename)
        {
            byte[] header = new byte[0x10];
            FileStream fs = File.Open(filename, FileMode.Open);
            fs.Read(header, 0, 0x10);
            fs.Close();
            Subformat format = GetFormat(header);
            return format != Subformat.Indeterminate;
        }
    }
}

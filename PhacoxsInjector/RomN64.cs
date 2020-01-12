using System;
using System.IO;
using System.Text;

namespace PhacoxsInjector
{
    public class RomN64 : RomFile
    {
        public enum Subformat
        {
            BigEndian,
            LittleEndian,
            ByteSwapped,
            Indeterminate
        }

        public Subformat Endianness
        { private set; get; }       

        public RomN64(string filename)
            : base()
        {
            Endianness = Subformat.Indeterminate;

            byte[] header = new byte[0x40];
            FileStream fs = File.Open(filename, FileMode.Open);
            Size = (int)fs.Length;
            fs.Read(header, 0, 0x40);
            fs.Close();

            Endianness = GetFormat(header);

            if (Endianness == Subformat.BigEndian || 
               (Endianness != Subformat.Indeterminate && Size % 4 == 0))
            {
                byte uniqueCode;
                byte[] shortTitle = new byte[2];
                byte region;

                header = ToBigEndian(header, Endianness);

                uniqueCode = header[0x3B];
                shortTitle[0] = header[0x3C];
                shortTitle[1] = header[0x3D];
                region = header[0x3E];
                Version = header[0x3F];
                FormatCode = (char)uniqueCode;
                ShortId = Encoding.ASCII.GetString(shortTitle);
                RegionCode = (char)region;

                byte[] titleBytes = new byte[20];
                Array.Copy(header, 0x20, titleBytes, 0, 20);
                int count = 20;
                while (titleBytes[--count] == 0x20 && count > 0) ;
                Title = Encoding.ASCII.GetString(titleBytes, 0, count + 1);

                fs = File.Open(filename, FileMode.Open);
                HashCRC16 = Cll.Security.ComputeCRC16(fs);
                fs.Close();

                IsValid = true;
                Console = Format.N64;
            }
            else
            {
                Size = 0;
                throw new FormatException("It was not possible to determine the N64 ROM format.");
            }
        }

        private static Subformat GetFormat(byte[] header)
        {
            Subformat format = Subformat.Indeterminate;

            if (header[0] == 0x80 &&
                header[1] == 0x37 &&
                header[2] == 0x12 &&
                header[3] == 0x40)
                format = Subformat.BigEndian;
            else if (header[0] == 0x37 &&
                header[1] == 0x80 &&
                header[2] == 0x40 &&
                header[3] == 0x12)
                format = Subformat.ByteSwapped;
            else if (header[0] == 0x40 &&
                header[1] == 0x12 &&
                header[2] == 0x37 &&
                header[3] == 0x80)
                format = Subformat.LittleEndian;

            return format;
        }

        public static bool Validate(string filename)
        {
            byte[] header = new byte[0x40];
            FileStream fs = File.OpenRead(filename);
            int size = (int)fs.Length;
            fs.Read(header, 0, 0x40);
            fs.Close();
            Subformat format = GetFormat(header);
            return format == Subformat.BigEndian ||
               (format != Subformat.Indeterminate && size % 4 == 0);
        }

        public static void ToBigEndian(string source, string destination)
        {
            byte[] header = new byte[0x40];
            FileStream fs = File.OpenRead(source);
            int size = (int)fs.Length;
            fs.Read(header, 0, 0x40);
            fs.Close();
            Subformat format = GetFormat(header);

            if (format == Subformat.BigEndian ||
               (format != Subformat.Indeterminate && size % 4 == 0))
            {
                fs = File.OpenRead(source);
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                fs.Close();

                data = ToBigEndian(data, format);
                fs = File.Open(destination, FileMode.Create);
                fs.Write(data, 0, data.Length);
                fs.Close();
            }
            else
                throw new Exception("The source file is not an N64 ROM.");
        }

        private static byte[] ToBigEndian(byte[] array, Subformat endianness)
        {
            byte[] bigEndian = new byte[4];

            if (endianness == Subformat.ByteSwapped)
            {
                for (int i = 0; i < array.Length / 4; i++)
                {
                    bigEndian[0] = array[(i * 4) + 1];
                    bigEndian[1] = array[(i * 4) + 0];
                    bigEndian[2] = array[(i * 4) + 3];
                    bigEndian[3] = array[(i * 4) + 2];

                    array[(i * 4) + 0] = bigEndian[0];
                    array[(i * 4) + 1] = bigEndian[1];
                    array[(i * 4) + 2] = bigEndian[2];
                    array[(i * 4) + 3] = bigEndian[3];
                }
            }
            else if (endianness == Subformat.LittleEndian)
            {
                for (int i = 0; i < array.Length / 4; i++)
                {
                    bigEndian[0] = array[(i * 4) + 3];
                    bigEndian[1] = array[(i * 4) + 2];
                    bigEndian[2] = array[(i * 4) + 1];
                    bigEndian[3] = array[(i * 4) + 0];

                    array[(i * 4) + 0] = bigEndian[0];
                    array[(i * 4) + 1] = bigEndian[1];
                    array[(i * 4) + 2] = bigEndian[2];
                    array[(i * 4) + 3] = bigEndian[3];
                }
            }

            return array;
        }
    }
}

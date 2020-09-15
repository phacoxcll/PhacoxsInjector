using System;
using System.IO;
using System.Text;

namespace PhacoxsInjector
{
    public class RomSNES : RomFile
    {
        public enum Subformat
        {
            LoROM,
            HiROM,
            Indeterminate
        }

        public bool IsSMC
        { private set; get; }
        public Subformat Mode
        { private set; get; }

        public RomSNES(string filename)
            : base()
        {
            IsSMC = false;
            Mode = Subformat.Indeterminate;

            FileStream fs = File.OpenRead(filename);
            int smcHeaderSize = SMCHeaderSize((int)fs.Length);
            byte[] data = GetData(fs, smcHeaderSize);
            fs.Close();

            if (smcHeaderSize == 0x200)
                IsSMC = true;

            int headerOffset = -1;
            Mode = GetFormat(data, ref headerOffset);

            if (Mode != Subformat.Indeterminate)
            {
                if (data[headerOffset + 0x2A] == 0x33)
                {
                    byte uniqueCode;
                    byte[] shortTitle = new byte[2];
                    byte region;

                    uniqueCode = data[headerOffset + 0x02];
                    shortTitle[0] = data[headerOffset + 0x03];
                    shortTitle[1] = data[headerOffset + 0x04];
                    region = data[headerOffset + 0x05];
                    
                    if (Useful.IsUpperLetterOrDigit(uniqueCode))
                        FormatCode = (char)uniqueCode;
                    if (Useful.IsUpperLetterOrDigit(shortTitle[0]) &&
                        Useful.IsUpperLetterOrDigit(shortTitle[1]))
                        ShortId = Encoding.ASCII.GetString(shortTitle);
                    if (Useful.IsUpperLetterOrDigit(region))
                        RegionCode = (char)region;
                }

                Version = data[headerOffset + 0x2B];

                byte[] titleBytes = new byte[21];
                Array.Copy(data, headerOffset + 0x10, titleBytes, 0, 21);
                int count = 21;
                while (--count >= 0 && titleBytes[count] == 0x20) ;
                Title = Encoding.ASCII.GetString(titleBytes, 0, count + 1);
                Size = data.Length;
                HashCRC16 = Cll.Security.ComputeCRC16_ARC(data, 0, data.Length);

                IsValid = true;
                Console = Format.SNES_USA;
            }
            else
            {
                Size = 0;
                throw new FormatException("It was not possible to determine the SNES ROM format.");
            }
        }

        public static int SMCHeaderSize(int filesize)
        {
            if (filesize % 1024 == 0)
                return 0;
            else if (filesize % 1024 == 0x200)
                return 0x200;
            else
                return -1;
        }

        private static byte[] GetData(FileStream fs, int smcHeader)
        {
            byte[] data = null;

            if (smcHeader != -1)
            {
                data = new byte[(int)fs.Length - smcHeader];
                fs.Seek(smcHeader, SeekOrigin.Begin);
                fs.Read(data, 0, data.Length);
            }
            else
                throw new FormatException("The SNES ROM has an invalid size.");            

            return data;
        }

        private static Subformat GetFormat(byte[] data, ref int headerOffset)
        {
            Subformat format = Subformat.Indeterminate;

            if (data != null)
            {
                ushort checksum16 = Cll.Security.Checksum16(data, 0, data.Length);
                ushort checksumCL = (ushort)(data[0x7FDC] + (data[0x7FDD] << 8));
                ushort checksumL = (ushort)(data[0x7FDE] + (data[0x7FDF] << 8));
                ushort checksumCH = (ushort)(data[0xFFDC] + (data[0xFFDD] << 8));
                ushort checksumH = (ushort)(data[0xFFDE] + (data[0xFFDF] << 8));

                if ((checksumCL ^ 0xFFFF) == checksumL && checksumL == checksum16)
                {
                    format = Subformat.LoROM;
                    headerOffset = 0x7FB0;
                }
                else if ((checksumCH ^ 0xFFFF) == checksumH && checksumH == checksum16)
                {
                    format = Subformat.HiROM;
                    headerOffset = 0xFFB0;
                }
                else
                {
                    headerOffset = 0x7FB0;
                    format = Subformat.LoROM;
                    for (int i = 0; i < 21; i++)
                    {
                        if (data[headerOffset + 0x10 + i] < 0x20 || data[headerOffset + 0x10 + i] > 0x7E)
                        {
                            headerOffset = 0xFFB0;
                            break;
                        }
                    }

                    if (headerOffset == 0xFFB0)
                    {
                        format = Subformat.HiROM;
                        for (int i = 0; i < 21; i++)
                        {
                            if (data[headerOffset + 0x10 + i] < 0x20 || data[headerOffset + 0x10 + i] > 0x7E)
                            {
                                headerOffset = -1;
                                format = Subformat.Indeterminate;
                                break;
                            }
                        }
                    }

                    if (
                            format != Subformat.Indeterminate &&
                            (
                                (data[headerOffset + 0x27] > 0x0D || data[headerOffset + 0x28] > 0x08) ||
                                (Math.Pow(2.0, 3.0 + data[headerOffset + 0x27]) / 8.0 > data.Length) ||
                                (data[headerOffset + 0x25] != 0x20 &&//Slow + LoROM
                                data[headerOffset + 0x25] != 0x21 && //Slow + HiROM
                                data[headerOffset + 0x25] != 0x22 && //Slow + LoROM (S-DD1) [?]
                                data[headerOffset + 0x25] != 0x23 && //Slow + LoROM (SA-1)
                                data[headerOffset + 0x25] != 0x25 && //Slow + ExHiROM [?]
                                data[headerOffset + 0x25] != 0x2A && //Slow + ExHiROM (SPC7110) [?]
                                data[headerOffset + 0x25] != 0x30 && //Fast + LoROM
                                data[headerOffset + 0x25] != 0x31 && //Fast + HiROM
                                data[headerOffset + 0x25] != 0x32 && //Fast + ExLoROM (or LoROM (S-DD1) [?])
                                data[headerOffset + 0x25] != 0x33 && //Fast + LoROM (SA-1) [?]
                                data[headerOffset + 0x25] != 0x35 && //Fast + ExHiROM
                                data[headerOffset + 0x25] != 0x3A)   //Fast + ExHiROM (SPC7110)
                            )
                        )
                    {
                        format = Subformat.Indeterminate;
                    }
                }
            }

            return format;
        }

        public static bool Validate(string filename)
        {
            if (File.Exists(filename))
            {
                FileStream fs = File.OpenRead(filename);
                int smcHeaderSize = SMCHeaderSize((int)fs.Length);
                byte[] data = GetData(fs, smcHeaderSize);
                fs.Close();
                int headerOffset = -1;
                Subformat format = GetFormat(data, ref headerOffset);
                return format != Subformat.Indeterminate;
            }
            return false;
        }
    }
}

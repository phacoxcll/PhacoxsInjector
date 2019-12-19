using System;
using System.IO;
using System.Text;

namespace PhacoxsInjector
{
    public abstract class RomFile
    {
        public enum Format
        {
            Famicom,
            NES,
            SuperFamicom,
            SNES_EUR,
            SNES_USA,
            N64,
            GBA,
            NDS,
            Indeterminate
        }

        public Format Console { protected set; get; }

        public int Size
        { protected set; get; }
        public string Title
        { protected set; get; }
        public char FormatCode
        { protected set; get; }
        public string ShortId
        { protected set; get; }
        public char RegionCode
        { protected set; get; }
        public byte Version
        { protected set; get; }
        public char Revision
        { get { return Version == 0 ? ' ' : (char)(Version + 0x40); } }
        public string ProductCode
        { get { return (FormatCode + ShortId + RegionCode).ToUpper(); } }
        public string ProductCodeVersion
        { get { return ProductCode + Version.ToString("X1"); } }
        public bool IsValid
        { protected set; get; }
        public ushort HashCRC16
        { protected set; get; }

        public RomFile()
        {
            Console = Format.Indeterminate;
            Size = 0;
            Title = "";
            FormatCode = '?';
            ShortId = "??";
            RegionCode = '?';
            Version = 0;
            IsValid = false;
            HashCRC16 = 0;
        }

        public static Format GetFormat(string filename)
        {
            if (RomNES.Validate(filename))
                return Format.NES;
            else if (RomN64.Validate(filename))
                return Format.N64;
            else if (RomGBA.Validate(filename))
                return Format.GBA;
            else if (RomNDS.Validate(filename))
                return Format.NDS;            
            else if (RomSNES.Validate(filename))
                return Format.SNES_USA;            

            return Format.Indeterminate;
        }
    }
}

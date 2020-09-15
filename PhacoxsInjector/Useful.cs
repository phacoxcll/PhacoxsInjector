using System;
using System.IO;
using System.Text;

namespace PhacoxsInjector
{
    public static class Useful
    {
        public static bool DirectoryCopy(string source, string destination, bool overwrite)
        {
            if (destination != source && (!Directory.Exists(destination) || overwrite))
            {
                Directory.CreateDirectory(destination);

                string[] sources = Directory.GetFiles(source);
                string[] destinations = new string[sources.Length];
                int i, last;

                for (i = 0; i < sources.Length; i++)
                    destinations[i] = destination + "\\" + Path.GetFileName(sources[i]);

                for (i = 0; i < sources.Length; i++)
                    File.Copy(sources[i], destinations[i], overwrite);

                string[] sourcesDir = Directory.GetDirectories(source);
                string[] destinationsDir = new string[sourcesDir.Length];

                for (i = 0; i < sourcesDir.Length; i++)
                {
                    last = sourcesDir[i].LastIndexOf('\\');
                    destinationsDir[i] = destination + sourcesDir[i].Substring(last, sourcesDir[i].Length - last);
                }

                for (i = 0; i < sourcesDir.Length; i++)
                {
                    if (!DirectoryCopy(sourcesDir[i], destinationsDir[i], overwrite))
                        return false;
                }

                return true;
            }
            return false;
        }

        public static byte[] StrHexToByteArray(string hex, string separator)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits.");

            if (separator.Length > 0)
                hex = hex.Replace(separator, "");

            if (!IsHexadecimal(hex))
                throw new Exception("The binary key does not contain only hexadecimal values.");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));

            return arr;
        }

        public static bool IsHexadecimal(string hex)
        {
            for (int i = 0; i < hex.Length; i++)
            {
                if ((hex[i] < '0' || hex[i] > '9')
                    && (hex[i] < 'A' || hex[i] > 'F')
                    && (hex[i] < 'a' || hex[i] > 'f'))
                    return false;
            }
            return true;
        }

        public static bool IsUpperLetterOrDigit(byte c)
        {
            if (c >= 0x30 && c <= 0x39 || c >= 0x41 && c <= 0x5A)
                return true;
            else
                return false;
        }

        private static int GetHexVal(char hex)
        {
            int val = (int)hex;
            //For uppercase A-F letters:
            //return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        public static bool IsUTF8(byte[] bytes)
        {
            try
            {
                (new UTF8Encoding(false, true)).GetString(bytes);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static string Windows1252ToASCII(string windows1252, char replacement)
        {
            if (replacement < 0x20 || replacement > 0x7E)
                throw new Exception("Replacement character is not a valid ASCII character.");

            windows1252 = windows1252.Replace("Š", "Sh");
            windows1252 = windows1252.Replace("Œ", "OE");
            windows1252 = windows1252.Replace("Ž", "Zh");
            windows1252 = windows1252.Replace("š", "sh");
            windows1252 = windows1252.Replace("œ", "oe");
            windows1252 = windows1252.Replace("ž", "zh");
            windows1252 = windows1252.Replace("Ÿ", "Ij");
            windows1252 = windows1252.Replace("Æ", "AE");
            windows1252 = windows1252.Replace("Ð", "Dh");
            windows1252 = windows1252.Replace("Ñ", "Nh");
            windows1252 = windows1252.Replace("Ø", "OE");
            windows1252 = windows1252.Replace("Þ", "Th");
            windows1252 = windows1252.Replace("ß", "ss");
            windows1252 = windows1252.Replace("æ", "ae");
            windows1252 = windows1252.Replace("ð", "dh");
            windows1252 = windows1252.Replace("ñ", "nh");
            windows1252 = windows1252.Replace("ø", "oe");
            windows1252 = windows1252.Replace("þ", "th");
            windows1252 = windows1252.Replace("ÿ", "ij");

            char[] array = windows1252.ToCharArray();

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] >= 0xC0 && array[i] <= 0xC5)
                    array[i] = (char)0x41; //A
                else if (array[i] >= 0xC8 && array[i] <= 0xCB)
                    array[i] = (char)0x45; //E
                else if (array[i] >= 0xCC && array[i] <= 0xCF)
                    array[i] = (char)0x49; //I
                else if (array[i] >= 0xD2 && array[i] <= 0xD6)
                    array[i] = (char)0x4F; //O
                else if (array[i] >= 0xD9 && array[i] <= 0xDC)
                    array[i] = (char)0x55; //U
                else if (array[i] >= 0xE0 && array[i] <= 0xE5)
                    array[i] = (char)0x61; //a
                else if (array[i] >= 0xE8 && array[i] <= 0xEB)
                    array[i] = (char)0x65; //e
                else if (array[i] >= 0xEC && array[i] <= 0xEF)
                    array[i] = (char)0x69; //i
                else if (array[i] >= 0xF2 && array[i] <= 0xF6)
                    array[i] = (char)0x6F; //o
                else if (array[i] >= 0xF9 && array[i] <= 0xFC)
                    array[i] = (char)0x75; //u
                else if (array[i] == 0xC7)
                    array[i] = (char)0x53; //S
                else if (array[i] == 0xE7)
                    array[i] = (char)0x73; //s
                else if (array[i] == 0xDD)
                    array[i] = (char)0x59; //Y
                else if (array[i] == 0xFD)
                    array[i] = (char)0x79; //y
                else if (array[i] == 0x91)
                    array[i] = (char)0x27; //'
                else if (array[i] == 0x92)
                    array[i] = (char)0x27; //'
                else if (array[i] == 0x93)
                    array[i] = (char)0x22; //"
                else if (array[i] == 0x94)
                    array[i] = (char)0x22; //"
                else if (array[i] < 0x20 || array[i] > 0x7E)
                    array[i] = replacement;
            }

            return new string(array);
        }

        public static string ToFileSize(int filesize)
        {
            if (filesize < 0)
                throw new Exception("File size cannot be negative.");

            if (filesize == 1)
                return "1 byte";

            double value = filesize;
            int multiple = 0;
            string unit = "";

            while (value >= 1024 && multiple < 8)
            {
                value /= 1024;
                multiple++;
            }

            switch (multiple)
            {
                case 0: unit = "bytes"; break;
                case 1: unit = "KiB"; break; // Kibibyte
                case 2: unit = "MiB"; break; // Mebibyte
                case 3: unit = "GiB"; break; // Gibibyte
                case 4: unit = "TiB"; break; // Tebibyte
                case 5: unit = "PiB"; break; // Pebibyte
                case 6: unit = "EiB"; break; // Exbibyte
                case 7: unit = "ZiB"; break; // Zebibyte
                case 8: unit = "YiB"; break; // Yobibyte
            }

            return Math.Round(value, 2).ToString(System.Globalization.CultureInfo.CurrentCulture) + " " + unit;
        }
    }
}

using System;
using System.IO;

namespace PhacoxsInjector
{
    public class VCN64ConfigFile
    {
        public bool IsValid
        { private set; get; }
        public ushort HashCRC16
        { private set; get; }

        public VCN64ConfigFile(string filename)
        {
            IsValid = false;
            HashCRC16 = 0;

            try
            {
                IsValid = Validate(filename);
            }
            catch
            {
                IsValid = UTF8Validator(filename);
            }

            if (IsValid)
            {
                FileStream fs = File.Open(filename, FileMode.Open);
                HashCRC16 = Cll.Security.ComputeCRC16(fs);
                fs.Close();
            }
            //else
                //throw new FormatException("N64 config file \"" + filename + "\" is invalid.");
        }

        public static void Copy(string source, string destination)
        {
            bool valid = false;
            try
            {
                valid = Validate(source);
            }
            catch
            {
                valid = UTF8Validator(source);
            }

            if (valid)            
                File.Copy(source, destination);
            //else
                //throw new Exception("N64 config file \"" + source + "\" copy failed.");
        }

        private static bool Validate(string filename)
        {
            return VCN64Config.Validator.Evaluate(filename);
        }

        //The UTF8 validator is not 100% reliable.
        public static bool UTF8Validator(string filename)
        {
            if (File.Exists(filename))
            {
                FileStream fs = File.Open(filename, FileMode.Open);
                    byte[] file = new byte[fs.Length];
                    fs.Read(file, 0, file.Length);
                    fs.Close();
                return Useful.IsUTF8(file);
            }
            else
                throw new Exception("N64 config file \"" + filename + "\" not exists.");
        }
    }
}

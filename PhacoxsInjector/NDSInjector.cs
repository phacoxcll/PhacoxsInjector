using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;

namespace PhacoxsInjector
{
    public class NDSInjector : WiiUInjector
    {
        public Bitmap RomIcon
        {
            get
            {
                if (Rom != null && Rom.IsValid)
                    return (Rom as RomNDS).Icon;
                else
                    return null;
            }
        }

        public override string TitleId
        {
            get
            {
                if (BaseIsLoaded && RomIsValid)
                    return "00050000D5" + Rom.HashCRC16.ToString("X4") + Base.Index.ToString("X2");
                else
                    return "";
            }            
        }
        
        public NDSInjector()
            : base()
        {
            BasePath = Environment.CurrentDirectory + "\\base_nds";
            Base = GetLoadedBase();
        }

        public override void SetRom(string romPath)
        {
            RomPath = romPath;
            Rom = new RomNDS(romPath);
        }

        public override void Inject(bool encrypt, string outputPath, string shortName, string longName,
            Bitmap menuIconImg, Bitmap bootTvImg, Bitmap bootDrcImg)
        {
            string outPath = GetValidOutputPath(outputPath, shortName);
            if (Directory.Exists(outPath) &&
               (Directory.GetDirectories(outPath).Length != 0 || Directory.GetFiles(outPath).Length != 0))
                throw new Exception("The output path \"" + outPath + "\"exists and is not empty.");

            Base = GetLoadedBase();
            if (!BaseIsLoaded)
                throw new Exception("The base is not ready.");

            InjectImages(menuIconImg, bootTvImg, bootDrcImg);
            if (RomIsValid)
            {
                InjectMeta(shortName, longName);
                InjectRom();
            }

            if (encrypt)
                NusContent.Encrypt(BasePath, outPath);
            else if (!Useful.DirectoryCopy(BasePath, outPath, true))
                throw new Exception("\"" + BasePath + "\" copy failed.");
        }

        protected override void InjectRom()
        {
            if (!Directory.Exists(Environment.CurrentDirectory + "\\resources"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\resources");

            if (Directory.Exists(Environment.CurrentDirectory + "\\resources\\nds"))
                Directory.Delete(Environment.CurrentDirectory + "\\resources\\nds", true);
            Directory.CreateDirectory(Environment.CurrentDirectory + "\\resources\\nds");

            if (File.Exists(BasePath + "\\content\\0010\\rom.zip"))
                File.Delete(BasePath + "\\content\\0010\\rom.zip");

            File.Copy(RomPath, Environment.CurrentDirectory + "\\resources\\nds\\U" + Rom.ProductCodeVersion + ".nds");
            ZipFile.CreateFromDirectory(Environment.CurrentDirectory + "\\resources\\nds", BasePath + "\\content\\0010\\rom.zip");

            Directory.Delete(Environment.CurrentDirectory + "\\resources\\nds", true);
        }

        protected override WiiUVC GetLoadedBase()
        {
            ValidateBase(BasePath);
            
            FileStream fs = File.Open(BasePath + "\\code\\hachihachi_ntr.rpx", FileMode.Open);
            uint hash = Cll.Security.ComputeCRC32(fs);
            fs.Close();
            return VCNDS.GetVC(hash);
        }

        protected override void ValidateBase(string path)
        {
            string[] folders = {
                path + "\\content\\0010\\assets",
                path + "\\content\\0010\\data"
            };

            string[] files = {
                path + "\\code\\app.xml",
                path + "\\code\\cos.xml",
                path + "\\code\\hachihachi_ntr.rpx",
                path + "\\content\\0010\\configuration_cafe.json",
                path + "\\content\\0010\\rom.zip",
                path + "\\meta\\iconTex.tga",
                path + "\\meta\\bootTvTex.tga",
                path + "\\meta\\bootDrcTex.tga",
                path + "\\meta\\meta.xml"
            };

            ValidateBase(folders, files);
        }
       
        protected override void ValidateEncryptedBase(string path)
        {
            ValidateEncryptedBase(path, "hachihachi_ntr.rpx");
        }
    }
}

using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;

namespace PhacoxsInjector
{
    public class NDSInjector : WiiUInjector
    {
        public bool DarkFilter;

        public string LayoutFilePath
        { private set; get; }

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
                    return "00050002D5" + Rom.HashCRC16.ToString("X4") + Base.Index.ToString("X2");
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

        public void SetLayoutFile(string layoutFilePath)
        {
            LayoutFilePath = layoutFilePath;
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

            if (!InjectGameLayout())
                throw new Exception("Failed.");

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

        private bool InjectGameLayout()
        {
            StreamReader sr = null;
            try
            {
                sr = File.OpenText(Path.Combine(BasePath, "content", "0010", "configuration_cafe.json"));
                Cll.JSON.SyntacticAnalyzer syn = new Cll.JSON.SyntacticAnalyzer(sr);
                Cll.JSON.Element json = syn.Run();
                sr.Close();

                Cll.JSON.Object config = (Cll.JSON.Object)json.Value.GetValue("configuration");

                if (DarkFilter)
                    config.GetValue("Display").SetValue("Brightness", new Cll.JSON.Number(80));
                else
                    config.GetValue("Display").SetValue("Brightness", new Cll.JSON.Number(100));

                string text = json.ToString("");
                File.WriteAllText(Path.Combine(BasePath, "content", "0010", "configuration_cafe.json"), text);

                return true;
            }
            catch { }
            finally { if (sr != null) sr.Close(); }

            return false;
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

            string dest = Environment.CurrentDirectory + "\\resources\\nds\\U" + Rom.ProductCodeVersion + ".nds";
            File.Copy(RomPath, dest);
            FileAttributes attributes = File.GetAttributes(dest);
            if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                File.SetAttributes(dest, attributes & ~FileAttributes.ReadOnly);            
            ZipFile.CreateFromDirectory(Environment.CurrentDirectory + "\\resources\\nds", BasePath + "\\content\\0010\\rom.zip");

            Directory.Delete(Environment.CurrentDirectory + "\\resources\\nds", true);
        }

        protected override WiiUVC GetLoadedBase()
        {
            return GetBase(BasePath);
        }

        public VCNDS GetBase(string path)
        {
            try
            {
                ValidateBase(path);
                FileStream fs = File.Open(path + "\\code\\hachihachi_ntr.rpx", FileMode.Open);
                uint hash = Cll.Security.ComputeCRC32(fs);
                fs.Close();
                return VCNDS.GetVC(hash);
            }
            catch
            {
                return null;
            }
        }

        public override void ValidateBase(string path)
        {
            string[] folders = {
                path + "\\content\\0010\\assets",
                path + "\\content\\0010\\assets\\textures",
                path + "\\content\\0010\\data",
                path + "\\content\\0010\\data\\strings"
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

        public override void ValidateEncryptedBase(string path)
        {
            ValidateEncryptedBase(path, "hachihachi_ntr.rpx");
        }
    }
}

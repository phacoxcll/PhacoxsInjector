using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace PhacoxsInjector
{
    public class N64Injector : WiiUInjector
    {
        public bool DarkFilter;
        public bool Widescreen;
        public float ScaleX;
        public float ScaleY;
        public float TranslationX;
        public float TranslationY;

        public string ConfigFilePath
        { private set; get; }
        private VCN64ConfigFile ConfigFile;

        public bool ConfigFileIsValid
        {
            get { return ConfigFile != null && ConfigFile.IsValid; }
        }
        public ushort ConfigFileHashCRC16
        {
            get
            {
                if (ConfigFile != null)
                    return ConfigFile.HashCRC16;
                else
                    return 0;
            }
        }

        public override string TitleId
        {
            get
            {
                if (BaseIsLoaded && RomIsValid)
                {
                    uint crc = Rom.HashCRC16;

                    if (ConfigFileIsValid)
                        crc += ConfigFile.HashCRC16;
                    else
                        crc += Cll.Security.ComputeCRC16_ARC(new byte[] { }, 0, 0);
                    crc >>= 1;

                    int flags = Base.Index;
                    flags |= DarkFilter ? 0x80 : 0;
                    flags |= Widescreen ? 0x40 : 0;
                    flags |= ScaleX != 1.0F ? 0x20 : (ScaleY != 1.0F ? 0x20 : (TranslationX != 0.0F ? 0x20 : (TranslationY != 0.0F ? 0x20 : 0)));

                    return "0005000264" + crc.ToString("X4") + ((byte)(flags)).ToString("X2");
                }
                else
                    return "";
            }
        }
        
        public N64Injector()
            : base()
        {
            BasePath = Environment.CurrentDirectory + "\\base_n64";
            Base = GetLoadedBase();

            ConfigFilePath = null;
            ConfigFile = null;

            DarkFilter = true;
            Widescreen = false;
            ScaleX = 1.0F;
            ScaleY = 1.0F;
            TranslationX = 0.0F;
            TranslationY = 0.0F;
        }

        public override void SetRom(string romPath)
        {
            RomPath = romPath;
            Rom = new RomN64(romPath);
        }

        public void SetConfigFile(string configFilePath)
        {
            ConfigFilePath = configFilePath;
            ConfigFile = new VCN64ConfigFile(configFilePath);
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
            InjectConfigFile();
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
            FileStream fs = null;
            try
            {
                byte darkFilterB = (byte)(DarkFilter ? 1 : 0);
                byte[] widescreenB = Widescreen ?
                    new byte[] { 0x44, 0xF0, 0, 0 } :
                    new byte[] { 0x44, 0xB4, 0, 0 };
                byte[] scaleXB = BitConverter.GetBytes(ScaleX);
                byte[] scaleYB = BitConverter.GetBytes(ScaleY);
                byte[] translationXB = BitConverter.GetBytes(TranslationX);
                byte[] translationYB = BitConverter.GetBytes(TranslationY);

                byte[] magic = new byte[4];
                uint offset = 0;
                uint size = 0;
                byte[] offsetB = new byte[4];
                byte[] sizeB = new byte[4];
                byte[] nameB = new byte[0x18];

                fs = File.Open(BasePath + "\\content\\FrameLayout.arc", FileMode.Open);
                fs.Read(magic, 0, 4);

                if (magic[0] == 'S' &&
                    magic[1] == 'A' &&
                    magic[2] == 'R' &&
                    magic[3] == 'C')
                {
                    fs.Position = 0x0C;
                    fs.Read(offsetB, 0, 4);
                    offset = (uint)(offsetB[0] << 24 |
                        offsetB[1] << 16 |
                        offsetB[2] << 8 |
                        offsetB[3]);
                    fs.Position = 0x38;
                    fs.Read(offsetB, 0, 4);
                    offset += (uint)(offsetB[0] << 24 |
                        offsetB[1] << 16 |
                        offsetB[2] << 8 |
                        offsetB[3]);

                    fs.Position = offset;
                    fs.Read(magic, 0, 4);

                    if (magic[0] == 'F' &&
                        magic[1] == 'L' &&
                        magic[2] == 'Y' &&
                        magic[3] == 'T')
                    {
                        fs.Position = offset + 0x04;
                        fs.Read(offsetB, 0, 4);
                        offsetB[0] = 0;
                        offsetB[1] = 0;
                        offset += (uint)(offsetB[0] << 24 |
                            offsetB[1] << 16 |
                            offsetB[2] << 8 |
                            offsetB[3]);
                        fs.Position = offset;

                        while (true)
                        {
                            fs.Read(magic, 0, 4);
                            fs.Read(sizeB, 0, 4);
                            size = (uint)(sizeB[0] << 24 |
                                sizeB[1] << 16 |
                                sizeB[2] << 8 |
                                sizeB[3]);

                            if (magic[0] == 'p' &&
                                magic[1] == 'i' &&
                                magic[2] == 'c' &&
                                magic[3] == '1')
                            {
                                fs.Position = offset + 0x0C;
                                fs.Read(nameB, 0, 0x18);
                                int count = Array.IndexOf(nameB, (byte)0);
                                string name = Encoding.ASCII.GetString(nameB, 0, count);

                                if (name == "frame")
                                {
                                    fs.Position = offset + 0x2C;//TranslationX
                                    fs.WriteByte(translationXB[3]);
                                    fs.WriteByte(translationXB[2]);
                                    fs.WriteByte(translationXB[1]);
                                    fs.WriteByte(translationXB[0]);
                                    fs.Position = offset + 0x30;//TranslationX
                                    fs.WriteByte(translationYB[3]);
                                    fs.WriteByte(translationYB[2]);
                                    fs.WriteByte(translationYB[1]);
                                    fs.WriteByte(translationYB[0]);
                                    fs.Position = offset + 0x44;//ScaleX
                                    fs.WriteByte(scaleXB[3]);
                                    fs.WriteByte(scaleXB[2]);
                                    fs.WriteByte(scaleXB[1]);
                                    fs.WriteByte(scaleXB[0]);
                                    fs.Position = offset + 0x48;//ScaleY
                                    fs.WriteByte(scaleYB[3]);
                                    fs.WriteByte(scaleYB[2]);
                                    fs.WriteByte(scaleYB[1]);
                                    fs.WriteByte(scaleYB[0]);
                                    fs.Position = offset + 0x4C;//Widescreen
                                    fs.Write(widescreenB, 0, 4);
                                }
                                else if (name == "frame_mask")
                                {
                                    fs.Position = offset + 0x08;//Dark filter
                                    fs.WriteByte(darkFilterB);
                                }
                                else if (name == "power_save_bg")
                                {
                                    return true;
                                }

                                offset += size;
                                fs.Position = offset;
                            }
                            else if (offset + size >= fs.Length)
                            {
                            }
                            else
                            {
                                offset += size;
                                fs.Position = offset;
                            }
                        }
                    }
                }
                fs.Close();
            }
            catch { }
            finally { if (fs != null) fs.Close(); }

            return false;
        }

        private void InjectConfigFile()
        {
            if (RomIsValid)
            {
                if (Directory.Exists(BasePath + "\\content\\config"))
                    Directory.Delete(BasePath + "\\content\\config", true);
                Directory.CreateDirectory(BasePath + "\\content\\config");

                if (!ConfigFileIsValid)
                    File.Create(BasePath + "\\content\\config\\U" + Rom.ProductCodeVersion + ".z64.ini").Close();
                else
                    VCN64ConfigFile.Copy(ConfigFilePath, BasePath + "\\content\\config\\U" + Rom.ProductCodeVersion + ".z64.ini");
            }
            else if (ConfigFileIsValid)
            {
                string[] files = Directory.GetFiles(BasePath + "\\content\\rom");

                if (files.Length > 1)
                    throw new Exception("The folder \"" + BasePath + "\\content\\rom\" contains more than one file.");

                string filename = Path.GetFileName(files[0]);

                if (Directory.Exists(BasePath + "\\content\\config"))
                    Directory.Delete(BasePath + "\\content\\config", true);
                Directory.CreateDirectory(BasePath + "\\content\\config");

                VCN64ConfigFile.Copy(ConfigFilePath, BasePath + "\\content\\config\\" + filename + ".ini");
            }
        }

        protected override void InjectRom()
        {
            if (Directory.Exists(BasePath + "\\content\\rom"))
                Directory.Delete(BasePath + "\\content\\rom", true);
            Directory.CreateDirectory(BasePath + "\\content\\rom");

            RomN64.ToBigEndian(RomPath, BasePath + "\\content\\rom\\U" + Rom.ProductCodeVersion + ".z64");
        }

        protected override WiiUVC GetLoadedBase()
        {
            return GetBase(BasePath);
        }

        public VCN64 GetBase(string path)
        {
            try
            {
                ValidateBase(path);
                FileStream fs = File.Open(path + "\\code\\VESSEL.rpx", FileMode.Open);
                uint hash = Cll.Security.ComputeCRC32(fs);
                fs.Close();
                return VCN64.GetVC(hash);
            }
            catch
            {
                return null;
            }
        }

        public override void ValidateBase(string path)
        {
            string[] folders = {
                path + "\\content\\config",
                path + "\\content\\rom"
            };

            string[] files = {
                path + "\\code\\app.xml",
                path + "\\code\\cos.xml",
                path + "\\code\\VESSEL.rpx",
                path + "\\content\\BuildInfo.txt",
                path + "\\content\\config.ini",
                path + "\\content\\FrameLayout.arc",
                path + "\\meta\\iconTex.tga",
                path + "\\meta\\bootTvTex.tga",
                path + "\\meta\\bootDrcTex.tga",
                path + "\\meta\\meta.xml"
            };

            ValidateBase(folders, files);
        }

        public override void ValidateEncryptedBase(string path)
        {
            ValidateEncryptedBase(path, "VESSEL.rpx");
        }
    }
}

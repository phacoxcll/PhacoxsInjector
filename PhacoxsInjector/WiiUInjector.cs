using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Xml;

namespace PhacoxsInjector
{
    public abstract class WiiUInjector
    {
        public const string Release = "1.0.9"; //CllVersionReplace "major.minor.revision"

        protected string RomPath;
        protected RomFile Rom;
        public string BasePath
        { protected set; get; }
        protected WiiUVC Base;

        public RomFile.Format Console
        {
            get
            {
                if (Rom != null)
                    return Rom.Console;
                else
                    return RomFile.Format.Indeterminate;
            }
        }

        public int RomSize
        {
            get
            {
                if (Rom != null)
                    return Rom.Size;
                else
                    return 0;
            }
        }
        public string RomTitle
        {
            get
            {
                if (Rom != null)
                    return Rom.Title;
                else
                    return "";
            }
        }
        public string RomProductCode
        {
            get
            {
                if (Rom != null)
                    return Rom.ProductCode;
                else
                    return "";
            }
        }
        public string RomProductCodeVersion
        {
            get
            {
                if (Rom != null)
                    return Rom.ProductCodeVersion;
                else
                    return "";
            }
        }
        public byte RomVersion 
        {
            get
            {
                if (Rom != null)
                    return Rom.Version;
                else
                    return 0;
            }
        }
        public char RomRevision
        {
            get
            {
                if (Rom != null)
                    return Rom.Revision;
                else
                    return ' ';
            }
        }
        public bool RomIsValid
        {
            get { return Rom != null && Rom.IsValid; }
        }
        public ushort RomHashCRC16
        {
            get
            {
                if (Rom != null)
                    return Rom.HashCRC16;
                else
                    return 0;
            }
        }

        public bool BaseIsLoaded
        {
            get { return Base != null; }
        }
        public string LoadedBase
        {
            get
            {
                if (Base != null)
                    return Base.ToString();
                else
                    return "";
            }
        }
        public bool BaseSupportsRomSize
        {
            get
            {
                if ((Console == RomFile.Format.Famicom ||
                    Console == RomFile.Format.NES) &&
                    Base != null && Rom != null &&
                    (Base as VCNES).ROMSize < Rom.Size)
                    return false;
                else if ((Console == RomFile.Format.SuperFamicom ||
                    Console == RomFile.Format.SNES_EUR ||
                    Console == RomFile.Format.SNES_USA) &&
                    Base != null && Rom != null &&
                    (Base as VCSNES).ROMSize < Rom.Size)
                    return false;
                else
                    return true;
            }
        }

        public abstract string TitleId { get; }

        public WiiUInjector()
        {
            RomPath = null;
            Rom = null;
            BasePath = null;
            Base = null;
        }

        public abstract void SetRom(string romPath);

        protected abstract WiiUVC GetLoadedBase();

        public abstract void Inject(bool encrypt, string outputPath, string shortName, string longName,
            Bitmap menuIconImg, Bitmap bootTvImg, Bitmap bootDrcImg);

        protected void InjectImages(Bitmap menuIconImg, Bitmap bootTvImg, Bitmap bootDrcImg)
        {
            Graphics g;

            if (menuIconImg != null)
            {
                Bitmap tmpMenuIconImg;
                tmpMenuIconImg = new Bitmap(128, 128, PixelFormat.Format32bppArgb);
                g = Graphics.FromImage(tmpMenuIconImg);
                g.DrawImage(menuIconImg, new Rectangle(0, 0, 128, 128));
                g.Dispose();
                if (!NusContent.SaveTGA(tmpMenuIconImg, BasePath + "\\meta\\iconTex.tga"))
                    throw new Exception("Error creating \"iconTex.tga\" file.");
            }

            if (bootTvImg != null)
            {
                Bitmap tmpBootTvImg;
                tmpBootTvImg = new Bitmap(1280, 720, PixelFormat.Format24bppRgb);
                g = Graphics.FromImage(tmpBootTvImg);
                g.DrawImage(bootTvImg, new Rectangle(0, 0, 1280, 720));
                g.Dispose();
                if (!NusContent.SaveTGA(tmpBootTvImg, BasePath + "\\meta\\bootTvTex.tga"))
                    throw new Exception("Error creating \"bootTvTex.tga\" file.");
            }

            if (bootDrcImg != null)
            {
                Bitmap tmpBootDrcImg;
                tmpBootDrcImg = new Bitmap(854, 480, PixelFormat.Format24bppRgb);
                g = Graphics.FromImage(tmpBootDrcImg);
                g.DrawImage(bootDrcImg, new Rectangle(0, 0, 854, 480));
                g.Dispose();
                if (!NusContent.SaveTGA(tmpBootDrcImg, BasePath + "\\meta\\bootDrcTex.tga"))
                    throw new Exception("Error creating \"bootDrcTex.tga\" file.");
            }
        }

        protected void InjectMeta(string shortName, string longName)
        {
            string titleId = TitleId;
            byte[] id = Useful.StrHexToByteArray(titleId, "");

            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false),
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\n",
                NewLineHandling = NewLineHandling.Replace
            };

            XmlDocument xmlApp = new XmlDocument();
            XmlDocument xmlMeta = new XmlDocument();

            xmlApp.Load(BasePath + "\\code\\app.xml");
            xmlMeta.Load(BasePath + "\\meta\\meta.xml");

            XmlNode app_title_id = xmlApp.SelectSingleNode("app/title_id");
            XmlNode app_group_id = xmlApp.SelectSingleNode("app/group_id");

            XmlNode meta_product_code = xmlMeta.SelectSingleNode("menu/product_code");
            XmlNode meta_title_id = xmlMeta.SelectSingleNode("menu/title_id");
            XmlNode meta_group_id = xmlMeta.SelectSingleNode("menu/group_id");
            XmlNode meta_longname_ja = xmlMeta.SelectSingleNode("menu/longname_ja");
            XmlNode meta_longname_en = xmlMeta.SelectSingleNode("menu/longname_en");
            XmlNode meta_longname_fr = xmlMeta.SelectSingleNode("menu/longname_fr");
            XmlNode meta_longname_de = xmlMeta.SelectSingleNode("menu/longname_de");
            XmlNode meta_longname_it = xmlMeta.SelectSingleNode("menu/longname_it");
            XmlNode meta_longname_es = xmlMeta.SelectSingleNode("menu/longname_es");
            XmlNode meta_longname_zhs = xmlMeta.SelectSingleNode("menu/longname_zhs");
            XmlNode meta_longname_ko = xmlMeta.SelectSingleNode("menu/longname_ko");
            XmlNode meta_longname_nl = xmlMeta.SelectSingleNode("menu/longname_nl");
            XmlNode meta_longname_pt = xmlMeta.SelectSingleNode("menu/longname_pt");
            XmlNode meta_longname_ru = xmlMeta.SelectSingleNode("menu/longname_ru");
            XmlNode meta_longname_zht = xmlMeta.SelectSingleNode("menu/longname_zht");
            XmlNode meta_shortname_ja = xmlMeta.SelectSingleNode("menu/shortname_ja");
            XmlNode meta_shortname_en = xmlMeta.SelectSingleNode("menu/shortname_en");
            XmlNode meta_shortname_fr = xmlMeta.SelectSingleNode("menu/shortname_fr");
            XmlNode meta_shortname_de = xmlMeta.SelectSingleNode("menu/shortname_de");
            XmlNode meta_shortname_it = xmlMeta.SelectSingleNode("menu/shortname_it");
            XmlNode meta_shortname_es = xmlMeta.SelectSingleNode("menu/shortname_es");
            XmlNode meta_shortname_zhs = xmlMeta.SelectSingleNode("menu/shortname_zhs");
            XmlNode meta_shortname_ko = xmlMeta.SelectSingleNode("menu/shortname_ko");
            XmlNode meta_shortname_nl = xmlMeta.SelectSingleNode("menu/shortname_nl");
            XmlNode meta_shortname_pt = xmlMeta.SelectSingleNode("menu/shortname_pt");
            XmlNode meta_shortname_ru = xmlMeta.SelectSingleNode("menu/shortname_ru");
            XmlNode meta_shortname_zht = xmlMeta.SelectSingleNode("menu/shortname_zht");

            app_title_id.InnerText = titleId;
            app_group_id.InnerText = "0000" + id[5].ToString("X2") + id[6].ToString("X2");

            if (Console != RomFile.Format.Famicom &&
                Console != RomFile.Format.NES &&
                Console != RomFile.Format.SuperFamicom &&
                Console != RomFile.Format.SNES_EUR &&
                Console != RomFile.Format.SNES_USA)
                meta_product_code.InnerText = "WUP-N-" + Rom.ProductCode;

            meta_title_id.InnerText = titleId;
            meta_group_id.InnerText = "0000" + id[5].ToString("X2") + id[6].ToString("X2");
            meta_longname_ja.InnerText = longName;
            meta_longname_en.InnerText = longName;
            meta_longname_fr.InnerText = longName;
            meta_longname_de.InnerText = longName;
            meta_longname_it.InnerText = longName;
            meta_longname_es.InnerText = longName;
            meta_longname_zhs.InnerText = longName;
            meta_longname_ko.InnerText = longName;
            meta_longname_nl.InnerText = longName;
            meta_longname_pt.InnerText = longName;
            meta_longname_ru.InnerText = longName;
            meta_longname_zht.InnerText = longName;
            meta_shortname_ja.InnerText = shortName;
            meta_shortname_en.InnerText = shortName;
            meta_shortname_fr.InnerText = shortName;
            meta_shortname_de.InnerText = shortName;
            meta_shortname_it.InnerText = shortName;
            meta_shortname_es.InnerText = shortName;
            meta_shortname_zhs.InnerText = shortName;
            meta_shortname_ko.InnerText = shortName;
            meta_shortname_nl.InnerText = shortName;
            meta_shortname_pt.InnerText = shortName;
            meta_shortname_ru.InnerText = shortName;
            meta_shortname_zht.InnerText = shortName;

            XmlWriter app = XmlWriter.Create(BasePath + "\\code\\app.xml", xmlSettings);
            XmlWriter meta = XmlWriter.Create(BasePath + "\\meta\\meta.xml", xmlSettings);

            xmlApp.Save(app);
            xmlMeta.Save(meta);

            app.Close();
            meta.Close();
        }

        protected abstract void InjectRom();

        public void LoadBase(string path)
        {
            NusContent.Format format = NusContent.GetFormat(path);

            if (format == NusContent.Format.Decrypted)
            {
                ValidateBase(path);

                if (Directory.Exists(BasePath))
                {
                    Directory.Delete(BasePath, true);
                    Base = null;
                }

                if (Useful.DirectoryCopy(path, BasePath, true))
                    Base = GetLoadedBase();
                else
                    throw new Exception("Could not load base \"" + path + "\".");   
            }
            else if (format == NusContent.Format.Encrypted)
            {
                ValidateEncryptedBase(path);

                if (Directory.Exists(BasePath))
                {
                    Directory.Delete(BasePath, true);
                    Base = null;
                }

                Directory.CreateDirectory(BasePath);
                NusContent.Decrypt(path, BasePath);
                Base = GetLoadedBase();
            }
            else
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendLine("The folder not contains a valid NUS content.");
                strBuilder.AppendLine("If it is an unpackaged (decrypted) NUS content, then:");
                strBuilder.AppendLine("The \"" + path + "\\code\" folder not exist.");
                strBuilder.AppendLine("Or \"" + path + "\\content\" folder not exist.");
                strBuilder.AppendLine("Or \"" + path + "\\meta\" folder not exist.");
                strBuilder.AppendLine("If it is an packaged (encrypted) NUS content, then:");
                strBuilder.AppendLine("The \"" + path + "\\title.tmd\" file not exist.");
                strBuilder.AppendLine("Or \"" + path + "\\title.tik\" file not exist.");
                strBuilder.AppendLine("Or \"" + path + "\\title.cert\" file not exist.");
                throw new Exception(strBuilder.ToString());
            }
        }

        protected abstract void ValidateBase(string path);

        protected abstract void ValidateEncryptedBase(string path);

        protected void ValidateBase(string[] folders, string[] files)
        {
            StringBuilder strBuilder = new StringBuilder();

            bool valid = true;
            foreach (string folder in folders)
            {
                if (!Directory.Exists(folder))
                {
                    strBuilder.AppendLine("This folder is missing: \"" + folder + "\"");
                    valid = false;
                }
            }

            foreach (string file in files)
            {
                if (!File.Exists(file))
                {
                    strBuilder.AppendLine("This file is missing: \"" + file + "\"");
                    valid = false;
                }
            }

            if(!valid)
                throw new Exception(strBuilder.ToString());
        }

        protected void ValidateEncryptedBase(string path, string cvFileName)
        {
            string appFileName = GetAppFileName(path);
            if (appFileName != cvFileName)
                throw new Exception("The \"" + appFileName + "\" not match the requested \"" + cvFileName + "\".");
        }

        protected string GetAppFileName(string path)
        {
            if (File.Exists(Environment.CurrentDirectory + "\\resources\\unpack\\cos.xml"))
                File.Delete(Environment.CurrentDirectory + "\\resources\\unpack\\cos.xml");

            NusContent.Decrypt(path, "code\\cos.xml", Environment.CurrentDirectory + "\\resources\\unpack\\cos.xml");

            if (!File.Exists(Environment.CurrentDirectory + "\\resources\\unpack\\cos.xml"))            
                throw new Exception("The NUS content does not contains \"code\\cos.xml\" file.");

            XmlDocument xmlCos = new XmlDocument();
            xmlCos.Load(Environment.CurrentDirectory + "\\resources\\unpack\\cos.xml");
            XmlNode cos_argstr = xmlCos.SelectSingleNode("app/argstr");
            return cos_argstr.InnerText;
        }

        protected string GetValidOutputPath(string outputPath, string shortName)
        {
            if (!Directory.Exists(outputPath))
                throw new Exception("The output path \"" + outputPath + "\" not exist.");

            if (shortName.Length == 0)
                throw new Exception("The short name is empty.");

            char[] array = Useful.Windows1252ToASCII(shortName, '_').ToCharArray();
            char[] invalid = Path.GetInvalidFileNameChars();
            for (int i = 0; i < array.Length; i++)
            {
                foreach (char c in invalid)
                {
                    if (array[i] == c)
                        array[i] = '_';
                }
            }
            string folderName =  new string(array);

            StringBuilder strBuilder = new StringBuilder(outputPath.Length + folderName.Length + 20);
            strBuilder.Append(outputPath);
            strBuilder.Append("\\");
            strBuilder.Append(folderName);
            strBuilder.Append(" [");
            if (RomIsValid)
            {
                strBuilder.Append(TitleId);
                strBuilder.Append("]");
            }
            else
            {
                strBuilder.Append(NusContent.GetTitleID(BasePath));
                strBuilder.Append("] (Edited)");
            }

            return strBuilder.ToString();
        }
    }
}

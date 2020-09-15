using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace PhacoxsInjector
{
    public class SNESInjector : WiiUInjector
    {
        public enum AspectRatio
        {
            Default,//H4V3
            H8V7,
            H16V9
        };

        public AspectRatio AspectRatioValue;
        public byte Speed;
        public byte Players;
        public byte SoundVolume;

        public override string TitleId
        {
            get
            {
                if (BaseIsLoaded && RomIsValid)
                    return "0005000262" + Rom.HashCRC16.ToString("X4") + Base.Index.ToString("X2");
                else
                    return "";
            }
        }

        public SNESInjector()
            : base()
        {
            BasePath = Environment.CurrentDirectory + "\\base_snes";
            Base = GetLoadedBase();

            AspectRatioValue = AspectRatio.Default;
            Speed = 60;
            Players = 2;
            SoundVolume = 100;
        }

        public override void SetRom(string romPath)
        {
            RomPath = romPath;
            Rom = new RomSNES(romPath);
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
                InjectMeta(shortName, longName);
            InjectRom();

            if (encrypt)
                NusContent.Encrypt(BasePath, outPath);
            else if (!Useful.DirectoryCopy(BasePath, outPath, true))
                throw new Exception("\"" + BasePath + "\" copy failed.");
        }

        protected override void InjectRom()
        {
            byte speed = (byte)(Speed == 50 ? 50 : 60);
            byte players = (byte)(Players == 4 ? 4 : Players == 3 ? 3 : 2);
            byte romType = 0;
            if (RomIsValid)
            {
                switch ((Rom as RomSNES).Mode)
                {
                    case RomSNES.Subformat.LoROM:
                        romType = 20;
                        break;
                    case RomSNES.Subformat.HiROM:
                        romType = 21;
                        break;
                    default:
                        romType = 0;
                        break;
                }
            }
            short widthTv;
            short widthDrc;
            switch (AspectRatioValue)
            {
                case AspectRatio.H8V7:
                    widthTv = 2240;
                    widthDrc = 996;
                    break;
                case AspectRatio.H16V9:
                    widthTv = 1440;
                    widthDrc = 640;
                    break;
                default:
                    widthTv = 1920;
                    widthDrc = 854;
                    break;
            }

            if (!Directory.Exists(Environment.CurrentDirectory + "\\resources"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\resources");

            DirectoryInfo code = new DirectoryInfo(BasePath + "\\code");
            FileInfo[] rpxFiles = code.GetFiles("*.rpx");
            RPXSNES.Inject(rpxFiles[0].FullName, RomPath, Environment.CurrentDirectory + "\\resources\\snes.rpx", speed, players, SoundVolume, romType, widthTv, widthDrc);

            File.Delete(rpxFiles[0].FullName);
            File.Move(Environment.CurrentDirectory + "\\resources\\snes.rpx", rpxFiles[0].FullName);
        }

        protected override WiiUVC GetLoadedBase()
        {
            return GetBase(BasePath);
        }

        public VCSNES GetBase(string path)
        {
            try
            {
                ValidateBase(path, false);
                RPXSNES vc = ValidateRPX(path);
                return VCSNES.GetVC(vc.CRCsSum);
            }
            catch
            {
                return null;
            }
        }

        private RPXSNES ValidateRPX(string path)
        {
            DirectoryInfo code = new DirectoryInfo(path + "\\code");
            if (!code.Exists)
                throw new Exception("The \"" + path + "\\code\" folder not exist.");

            FileInfo[] rpxFiles = code.GetFiles("*.rpx");
            if (rpxFiles.Length != 1)
                throw new Exception("The \"" + path + "\\code\" folder has more than one RPX file.");

            if (!(rpxFiles[0].Name.StartsWith("WUP-J") && (
                rpxFiles[0].Name.EndsWith("E.rpx") ||
                rpxFiles[0].Name.EndsWith("P.rpx") ||
                rpxFiles[0].Name.EndsWith("D.rpx") ||
                rpxFiles[0].Name.EndsWith("F.rpx") ||
                rpxFiles[0].Name.EndsWith("J.rpx"))))
                throw new Exception("The \"" + rpxFiles[0].Name + "\" RPX file is not an RPX SNES.");

            return new RPXSNES(rpxFiles[0].FullName);
        }

        private void ValidateBase(string path, bool validateRPX)
        {
            string[] folders = {
                path + "\\content\\msg",
                path + "\\content\\snd",
            };

            string[] files = {
                path + "\\code\\app.xml",
                path + "\\code\\cos.xml",
                path + "\\content\\texture2D_206.gsh",
                path + "\\meta\\iconTex.tga",
                path + "\\meta\\bootTvTex.tga",
                path + "\\meta\\bootDrcTex.tga",
                path + "\\meta\\meta.xml"
            };

            ValidateBase(folders, files);
            if (validateRPX)
                ValidateRPX(path);
        }

        public override void ValidateBase(string path)
        {
            ValidateBase(path, true);
        }

        public override void ValidateEncryptedBase(string path)
        {
            string appFileName = GetAppFileName(path);

            if (!(appFileName.StartsWith("WUP-J") && (
                appFileName.EndsWith("E.rpx") ||
                appFileName.EndsWith("P.rpx") ||
                appFileName.EndsWith("D.rpx") ||
                appFileName.EndsWith("F.rpx") ||
                appFileName.EndsWith("J.rpx"))))
                throw new Exception("The \"" + appFileName + "\" RPX file is not an RPX SNES.");
        }
    }
}

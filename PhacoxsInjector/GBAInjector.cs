using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace PhacoxsInjector
{
    public class GBAInjector : WiiUInjector
    {
        public override string TitleId
        {
            get
            {
                if (BaseIsLoaded && RomIsValid)
                    return "000500026A" + Rom.HashCRC16.ToString("X4") + Base.Index.ToString("X2");
                else
                    return "";
            }
        }

        public GBAInjector()
            : base()
        {
            BasePath = Environment.CurrentDirectory + "\\base_gba";
            Base = GetLoadedBase();
        }

        public override void SetRom(string romPath)
        {
            RomPath = romPath;
            Rom = new RomGBA(romPath);
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
            if (!File.Exists(Environment.CurrentDirectory + "\\resources\\gba\\psb.exe"))
                throw new Exception("The \"" + Environment.CurrentDirectory + "\\resources\\gba\\psb.exe\" file not exist.");

            if (!File.Exists(Environment.CurrentDirectory + "\\resources\\gba\\inject.bat"))
            {
                StreamWriter sw = File.CreateText(Environment.CurrentDirectory + "\\resources\\gba\\inject.bat");
                sw.WriteLine("@echo off");
                sw.WriteLine("cd resources\\gba");
                sw.Write("psb.exe %1 %2 %3");
                sw.Close();
            }

            string tmpRomPath = Path.Combine(Environment.CurrentDirectory, "resources", "gba", "tmp.gba");
            File.Copy(RomPath, tmpRomPath, true);

            int paddingSize = 0;
            if (Rom.Size < 1048576)
                paddingSize = 1048576 - Rom.Size;
            else if (Rom.Size < 2097152)
                paddingSize = 2097152 - Rom.Size;
            else if (Rom.Size < 4194304)
                paddingSize = 4194304 - Rom.Size;
            else if (Rom.Size < 8388608)
                paddingSize = 8388608 - Rom.Size;
            else if (Rom.Size < 16777216)
                paddingSize = 16777216 - Rom.Size;
            else if (Rom.Size < 33554432)
                paddingSize = 33554432 - Rom.Size;

            byte[] padding = new byte[paddingSize];
            FileStream fs = File.Open(tmpRomPath, FileMode.Open);
            fs.Position = fs.Length;
            fs.Write(padding, 0, paddingSize);
            fs.Close();

            Process psb = Process.Start(Environment.CurrentDirectory + "\\resources\\gba\\inject.bat",
            "\"" + BasePath + "\\content\\alldata.psb.m\" \"" + tmpRomPath + "\" \"" + Environment.CurrentDirectory + "\\resources\\gba\\tmp.psb.m\"");
            psb.WaitForExit();

            if (psb.ExitCode == 0)
            {
                psb.Dispose();
                File.Delete(tmpRomPath);
                File.Delete(BasePath + "\\content\\alldata.bin");
                File.Delete(BasePath + "\\content\\alldata.psb.m");
                File.Move(Environment.CurrentDirectory + "\\resources\\gba\\tmp.bin", BasePath + "\\content\\alldata.bin");
                File.Move(Environment.CurrentDirectory + "\\resources\\gba\\tmp.psb.m", BasePath + "\\content\\alldata.psb.m");
            }
            else
            {
                psb.Dispose();
                throw new Exception("psb.exe fail.");
            }
        }

        protected override WiiUVC GetLoadedBase()
        {
            return GetBase(BasePath);
        }

        public VCGBA GetBase(string path)
        {
            try
            {
                ValidateBase(path);
                FileStream fs = File.Open(path + "\\code\\m2engage.rpx", FileMode.Open);
                uint hash = Cll.Security.ComputeCRC32(fs);
                fs.Close();
                return VCGBA.GetVC(hash);
            }
            catch
            {
                return null;
            }
        }

        public override void ValidateBase(string path)
        {
            string[] folders = {
            };

            string[] files = {
                path + "\\code\\app.xml",
                path + "\\code\\cos.xml",
                path + "\\code\\m2engage.rpx",
                path + "\\content\\alldata.psb.m",
                path + "\\content\\alldata.bin",
                path + "\\meta\\iconTex.tga",
                path + "\\meta\\bootTvTex.tga",
                path + "\\meta\\bootDrcTex.tga",
                path + "\\meta\\meta.xml"
            };

            ValidateBase(folders, files);
        }

        public override void ValidateEncryptedBase(string path)
        {
            ValidateEncryptedBase(path, "m2engage.rpx");
        }
    }
}

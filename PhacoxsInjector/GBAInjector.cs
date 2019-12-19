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
                    return "000500006A" + Rom.HashCRC16.ToString("X4") + Base.Index.ToString("X2");
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

            Process decrypt = Process.Start(Environment.CurrentDirectory + "\\resources\\gba\\inject.bat",
            "\"" + BasePath + "\\content\\alldata.psb.m\" \"" + RomPath + "\" \"" + Environment.CurrentDirectory + "\\resources\\gba\\tmp.psb.m\"");
            decrypt.WaitForExit();

            if (decrypt.ExitCode == 0)
            {
                decrypt.Dispose();
                File.Delete(BasePath + "\\content\\alldata.bin");
                File.Delete(BasePath + "\\content\\alldata.psb.m");
                File.Move(Environment.CurrentDirectory + "\\resources\\gba\\tmp.bin", BasePath + "\\content\\alldata.bin");
                File.Move(Environment.CurrentDirectory + "\\resources\\gba\\tmp.psb.m", BasePath + "\\content\\alldata.psb.m");
            }
            else
            {
                decrypt.Dispose();
                throw new Exception("psb.exe fail.");
            }
        }

        protected override WiiUVC GetLoadedBase()
        {
            ValidateBase(BasePath);
            
            FileStream fs = File.Open(BasePath + "\\code\\m2engage.rpx", FileMode.Open);
            uint hash = Cll.Security.ComputeCRC32(fs);
            fs.Close();
            return VCGBA.GetVC(hash);
        }

        protected override void ValidateBase(string path)
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

        protected override void ValidateEncryptedBase(string path)
        {
            ValidateEncryptedBase(path, "m2engage.rpx");
        }
    }
}

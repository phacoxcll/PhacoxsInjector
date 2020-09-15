using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace PhacoxsInjector
{
    public class PhacoxsInjectorCMD
    {
        private RomFile.Format Mode;
        private WiiUInjector Injector;

        private BootImage BootTvImg;
        private BootImage BootDrcImg;
        private MenuIconImage MenuIconImg;
        private string TitleScreenPath;

        public PhacoxsInjectorCMD()
        {
        }

        public void Run(string[] args)
        {
            Cll.Log.SaveIn("PhacoxsInjector.log");
            Cll.Log.WriteLine("Phacox's Injector " + WiiUInjector.Release);
            Cll.Log.WriteLine(DateTime.Now.ToString());

            BootTvImg = new BootImage();
            BootDrcImg = new BootImage();
            MenuIconImg = new MenuIconImage();

            /*if (args.Length == 1)
            {
                RomFile.Format format = RomFile.GetFormat(args[0]);
                if (format != RomFile.Format.Indeterminate)
                {
                    Cll.Log.WriteLine("ROM format: " + format.ToString());
                    ChangeCMD(format);
                }
                else
                {
                    DeterminateBase(args[0]);
                }
            }*/

            Console.ReadLine();
        }

        private void DeterminateBase(string path)
        {
            NusContent.Format format = NusContent.GetFormat(path);

            if (format == NusContent.Format.Decrypted)
            {
                NESInjector nesI = new NESInjector();
                SNESInjector snesI = new SNESInjector();
                N64Injector n64I = new N64Injector();
                GBAInjector gbaI = new GBAInjector();
                NDSInjector ndsI = new NDSInjector();

                VCNES vcnes = nesI.GetBase(path);
                if (vcnes != null)
                    Cll.Log.WriteLine("Base format: NES");

                VCSNES vcsnes = snesI.GetBase(path);
                if (vcsnes != null)
                    Cll.Log.WriteLine("Base format: SNES");

                VCN64 vcn64 = n64I.GetBase(path);
                if (vcn64 != null)
                    Cll.Log.WriteLine("Base format: N64");

                VCGBA vcgba = gbaI.GetBase(path);
                if (vcgba != null)
                    Cll.Log.WriteLine("Base format: GBA");

                VCNDS vcnds = ndsI.GetBase(path);
                if (vcnds != null)
                    Cll.Log.WriteLine("Base format: NDS");

                /*ValidateBase(path);

                if (Directory.Exists(BasePath))
                {
                    Directory.Delete(BasePath, true);
                    Base = null;
                }

                if (Useful.DirectoryCopy(path, BasePath, true))
                    Base = GetLoadedBase();
                else
                    throw new Exception("Could not load base \"" + path + "\".");*/
            }
            else if (format == NusContent.Format.Encrypted)
            {
                /*ValidateEncryptedBase(path);

                if (Directory.Exists(BasePath))
                {
                    Directory.Delete(BasePath, true);
                    Base = null;
                }

                Directory.CreateDirectory(BasePath);
                NusContent.Decrypt(path, BasePath);
                Base = GetLoadedBase();*/
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

        private void ChangeCMD(RomFile.Format mode)
        {
            string bootPath;
            string iconPath;
            string titleScreenPath;

            switch (mode)
            {
                case RomFile.Format.Famicom:
                    Injector = new NESInjector();
                    bootPath = "resources\\images\\boot_fc.png";
                    iconPath = "resources\\images\\icon.png";
                    titleScreenPath = "resources\\images\\title_screen_fc.png";
                    Cll.Log.WriteLine("CMD changed to Famicom.");
                    break;
                case RomFile.Format.NES:
                    Injector = new NESInjector();
                    bootPath = "resources\\images\\boot_nes.png";
                    iconPath = "resources\\images\\icon.png";
                    titleScreenPath = "resources\\images\\title_screen_nes.png";
                    Cll.Log.WriteLine("CMD changed to NES.");
                    break;
                case RomFile.Format.SuperFamicom:
                    Injector = new SNESInjector();
                    bootPath = "resources\\images\\boot_sfc.png";
                    iconPath = "resources\\images\\icon.png";
                    titleScreenPath = "resources\\images\\title_screen_sfc.png";
                    Cll.Log.WriteLine("CMD changed to Super Famicom.");
                    break;
                case RomFile.Format.SNES_EUR:
                    Injector = new SNESInjector();
                    bootPath = "resources\\images\\boot_snes_pal.png";
                    iconPath = "resources\\images\\icon.png";
                    titleScreenPath = "resources\\images\\title_screen_sfc.png";
                    Cll.Log.WriteLine("CMD changed to SNES (EUR).");
                    break;
                case RomFile.Format.SNES_USA:
                    Injector = new SNESInjector();
                    bootPath = "resources\\images\\boot_snes.png";
                    iconPath = "resources\\images\\icon.png";
                    titleScreenPath = "resources\\images\\title_screen_snes.png";
                    Cll.Log.WriteLine("CMD changed to SNES (USA).");
                    break;
                case RomFile.Format.N64:
                    Injector = new N64Injector();
                    bootPath = "resources\\images\\boot_n64.png";
                    iconPath = "resources\\images\\icon.png";
                    titleScreenPath = "resources\\images\\title_screen_n64.png";
                    Cll.Log.WriteLine("CMD changed to N64.");
                    break;
                case RomFile.Format.GBA:
                    Injector = new GBAInjector();
                    bootPath = "resources\\images\\boot_gba.png";
                    iconPath = "resources\\images\\icon_gba.png";
                    titleScreenPath = "resources\\images\\title_screen_gba.png";
                    Cll.Log.WriteLine("CMD changed to GBA.");
                    break;
                case RomFile.Format.NDS:
                    Injector = new NDSInjector();
                    bootPath = "resources\\images\\boot_nds.png";
                    iconPath = "resources\\images\\icon.png";
                    titleScreenPath = "resources\\images\\title_screen_nds.png";
                    Cll.Log.WriteLine("CMD changed to NDS.");
                    break;
                default:
                    Injector = null;
                    bootPath = "";
                    iconPath = "";
                    titleScreenPath = "";
                    Cll.Log.WriteLine("CMD changed to default.");
                    break;
            }

            if (Injector != null)
            {
                if (Injector.BaseIsLoaded)
                {
                    Cll.Log.WriteLine("Loaded base: " + Injector.LoadedBase);
                }
                else
                {
                    Cll.Log.WriteLine("There is no loaded base!");
                }
            }
            else
            {
                Cll.Log.WriteLine("It was not possible to create the injector!");
            }

            if (File.Exists(bootPath))
            {
                BootTvImg.Frame = new Bitmap(bootPath);
                BootDrcImg.Frame = new Bitmap(bootPath);
            }
            else
            {
                BootTvImg.Frame = null;
                BootDrcImg.Frame = null;
            }
            if (File.Exists(iconPath))
                MenuIconImg.Frame = new Bitmap(iconPath);
            else
                MenuIconImg.Frame = null;
            if (File.Exists(titleScreenPath))
            {
                BootTvImg.TitleScreen = new Bitmap(titleScreenPath);
                BootDrcImg.TitleScreen = new Bitmap(titleScreenPath);
                MenuIconImg.TitleScreen = new Bitmap(titleScreenPath);
            }
            else
            {
                BootTvImg.TitleScreen = null;
                BootDrcImg.TitleScreen = null;
                MenuIconImg.TitleScreen = null;
            }
        }
    }
}

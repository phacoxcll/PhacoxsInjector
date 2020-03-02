using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PhacoxsInjector
{
    public partial class PhacoxsInjectorGUI : Form
    {
        private RomFile.Format Mode;
        private WiiUInjector Injector;

        private BootImage BootTvImg;
        private BootImage BootDrcImg;
        private MenuIconImage MenuIconImg;
        private string TitleScreenPath;

        #region General

        public PhacoxsInjectorGUI()
        {
            Cll.Log.SaveIn("PhacoxsInjector.log");
            Cll.Log.WriteLine("Phacox's Injector " + WiiUInjector.Release);
            Cll.Log.WriteLine(DateTime.Now.ToString());

            InitializeComponent();
            
            BootTvImg = new BootImage();
            BootDrcImg = new BootImage();
            MenuIconImg = new MenuIconImage();

            LoadSettings();
            this.Text = "Phacox's Injector " + WiiUInjector.Release;
            buttonMain.BackColor = Color.FromArgb(16, 110, 190);
            groupBoxHelp.Text = HelpString.Injecting;
            labelHelpText.Text = HelpString.EnableInjecting;

            comboBoxConsole.SelectedIndex = 0;

            StringBuilder sb = new StringBuilder();
            bool warning = false;
            if (!File.Exists("resources\\pack\\CNUSPACKER.exe"))
            {
                sb.AppendLine(HelpString.CNUSPackerWarning);
                sb.AppendLine("");
                warning = true;
            }
            if (!File.Exists("resources\\unpack\\CDecrypt.exe"))
            {
                sb.AppendLine(HelpString.CDecryptWarning);
                warning = true;
            }
            if (warning)
            {
                Cll.Log.WriteLine(sb.ToString());
                MessageBox.Show(sb.ToString(), HelpString.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (NusContent.CheckCommonKeyFiles())
            {
                Cll.Log.WriteLine("Wii U Common Key files: OK!");
                textBoxCommonKey.BackColor = Color.FromArgb(33, 33, 33);
                textBoxCommonKey.Enabled = false;
                panelValidKey.BackgroundImage = Properties.Resources.checkmark_16;
            }
            else
            {
                Cll.Log.WriteLine("Wii U Common Key files: Not found.");
                textBoxCommonKey.BackColor = Color.FromArgb(51, 51, 51);
                textBoxCommonKey.Enabled = true;
                panelValidKey.BackgroundImage = Properties.Resources.x_mark_16;
            }
        }

        private void PhacoxsInjectorGUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }

        private void LoadSettings()
        {
            switch (Properties.Settings.Default.Language)
            {
                case "en-US":
                    comboBoxLanguage.SelectedIndex = 0;
                    break;
                case "es-MX":
                    comboBoxLanguage.SelectedIndex = 1;
                    break;
                default:
                    switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
                    {
                        case "en":
                            comboBoxLanguage.SelectedIndex = 0;
                            break;
                        case "es":
                            comboBoxLanguage.SelectedIndex = 1;
                            break;
                        default:
                            comboBoxLanguage.SelectedIndex = 0;
                            break;
                    }
                    break;
            }
            checkBoxHelp.Checked = Properties.Settings.Default.Help;
        }

        private void SaveSettings()
        {
            switch (comboBoxLanguage.SelectedIndex)
            {
                case 0:
                    Properties.Settings.Default.Language = "en-US";
                    break;
                case 1:
                    Properties.Settings.Default.Language = "es-MX";
                    break;
                default:
                    Properties.Settings.Default.Language = "en-US";
                    break;
            }
            Properties.Settings.Default.Help = checkBoxHelp.Checked;
            Properties.Settings.Default.Save();
        }

        private void LoadLogFile()
        {
            try
            {
                textBoxLog.Clear();
                StreamReader sr = File.OpenText(Cll.Log.Filename);
                textBoxLog.AppendText(sr.ReadToEnd());
                sr.Close();
            }
            catch
            {
                Cll.Log.WriteLine("Error reading log file.");
            }
        }

        private void ButtonMain_Click(object sender, EventArgs e)
        {
            buttonMain.BackColor = Color.FromArgb(16, 110, 190);
            buttonImages.BackColor = Color.FromArgb(17, 17, 17);
            buttonInjecting.BackColor = Color.FromArgb(17, 17, 17);
            buttonSettings.BackColor = Color.FromArgb(17, 17, 17);
            panelMain.Visible = true;
            panelImages.Visible = false;
            panelInjecting.Visible = false;
            panelSettings.Visible = false;
            Cll.Log.WriteLine("Main button clicked.");
        }

        private void ButtonImages_Click(object sender, EventArgs e)
        {
            UpdateBootName();
            UpdateBootTvPictureBox();
            UpdateBootDrcPictureBox();

            buttonMain.BackColor = Color.FromArgb(17, 17, 17);
            buttonImages.BackColor = Color.FromArgb(16, 110, 190);
            buttonInjecting.BackColor = Color.FromArgb(17, 17, 17);
            buttonSettings.BackColor = Color.FromArgb(17, 17, 17);
            panelMain.Visible = false;
            panelImages.Visible = true;
            panelInjecting.Visible = false;
            panelSettings.Visible = false;
            Cll.Log.WriteLine("Images button clicked.");
        }

        private void ButtonInjecting_Click(object sender, EventArgs e)
        {
            LoadLogFile();

            buttonMain.BackColor = Color.FromArgb(17, 17, 17);
            buttonImages.BackColor = Color.FromArgb(17, 17, 17);
            buttonInjecting.BackColor = Color.FromArgb(16, 110, 190);
            buttonSettings.BackColor = Color.FromArgb(17, 17, 17);
            panelMain.Visible = false;
            panelImages.Visible = false;
            panelInjecting.Visible = true;
            panelSettings.Visible = false;
            Cll.Log.WriteLine("Injecting button clicked.");
        }

        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            buttonMain.BackColor = Color.FromArgb(17, 17, 17);
            buttonImages.BackColor = Color.FromArgb(17, 17, 17);
            buttonInjecting.BackColor = Color.FromArgb(17, 17, 17);
            buttonSettings.BackColor = Color.FromArgb(16, 110, 190);
            panelMain.Visible = false;
            panelImages.Visible = false;
            panelInjecting.Visible = false;
            panelSettings.Visible = true;
            Cll.Log.WriteLine("Settings button clicked.");
        }
        
        private void comboBoxConsole_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxConsole.SelectedIndex)
            {
                case 1:
                    Mode = RomFile.Format.Famicom;
                    Cll.Log.WriteLine("Changed mode to Famicom.");
                    break;
                case 2:
                    Mode = RomFile.Format.NES;
                    Cll.Log.WriteLine("Changed mode to NES.");
                    break;
                case 3:
                    Mode = RomFile.Format.SuperFamicom;
                    Cll.Log.WriteLine("Changed mode to Super Famicom.");
                    break;
                case 4:
                    Mode = RomFile.Format.SNES_EUR;
                    Cll.Log.WriteLine("Changed mode to SNES (EUR).");
                    break;
                case 5:
                    Mode = RomFile.Format.SNES_USA;
                    Cll.Log.WriteLine("Changed mode to SNES (USA).");
                    break;
                case 6:
                    Mode = RomFile.Format.N64;
                    Cll.Log.WriteLine("Changed mode to N64.");
                    break;
                case 7:
                    Mode = RomFile.Format.GBA;
                    Cll.Log.WriteLine("Changed mode to GBA.");
                    break;
                case 8:
                    Mode = RomFile.Format.NDS;
                    Cll.Log.WriteLine("Changed mode to NDS.");
                    break;
                default:
                    Mode = RomFile.Format.Indeterminate;
                    Cll.Log.WriteLine("Changed mode to Indeterminate.");
                    break;
            }
            ChangeGUI(Mode);
        }

        private void ChangeGUI(RomFile.Format mode)
        {
            string bootPath;
            string iconPath;
            string titleScreenPath;

            labelRomInfo.Text = "";
            labelTitleId.Text = "Title ID:";

            labelAspectRatio.Visible = false;
            comboBoxAspectRatioNES.Visible = false;
            comboBoxAspectRatioNES.SelectedIndex = 0;
            comboBoxAspectRatioSNES.Visible = false;
            comboBoxAspectRatioSNES.SelectedIndex = 0;
            labelSpeed.Visible = false;
            comboBoxSpeed.Visible = false;
            comboBoxSpeed.SelectedIndex = 0;
            labelPlayers.Visible = false;
            comboBoxPlayers.Visible = false;
            comboBoxPlayers.SelectedIndex = 0;
            labelSoundVolume.Visible = false;
            numericUpDownSoundVolume.Visible = false;
            numericUpDownSoundVolume.Value = 100;

            checkBoxDarkFilter.Visible = false;
            checkBoxDarkFilter.Checked = true;
            checkBoxWidescreen.Visible = false;
            checkBoxWidescreen.Checked = false;
            labelZoomH.Visible = false;
            numericUpDownZoomH.Visible = false;
            numericUpDownZoomH.Value = 100.0M;
            labelZoomV.Visible = false;
            numericUpDownZoomV.Visible = false;
            numericUpDownZoomV.Value = 100.0M;
            labelTranslationX.Visible = false;
            numericUpDownTranslationX.Visible = false;
            numericUpDownTranslationX.Value = 0.0M;
            labelTranslationY.Visible = false;
            numericUpDownTranslationY.Visible = false;
            numericUpDownTranslationY.Value = 0.0M;
            buttonConfigFile.Visible = false;
            labelConfigFile.Visible = false;
            textBoxConfigFile.Visible = false;
            textBoxConfigFile.Text = "";
            buttonEditConfigFile.Visible = false;

            buttonTitleScreen.Enabled = true;
            checkBoxKeepMenuIcon.Checked = false;
            checkBoxKeepBootTv.Checked = false;
            checkBoxKeepBootDrc.Checked = false;
            checkBoxShowName.Checked = true;
            checkBoxUseNDSIcon.Visible = false;
            checkBoxUseNDSIcon.Checked = false;
            panelNDSIcon.Visible = false;
            buttonNDSIconBGColor.Visible = false;
            labelShowPlayers.Visible = false;
            comboBoxShowPlayers.Visible = false;
            comboBoxShowPlayers.SelectedIndex = 0;
            checkBoxUseNDSIcon.Checked = false;
            if (panelNDSIcon.BackgroundImage != null)
            {
                panelNDSIcon.BackgroundImage.Dispose();
                panelNDSIcon.BackgroundImage = null;
            }
            panelNDSIcon.BackColor = Color.Black;

            switch (mode)
            {
                case RomFile.Format.Famicom:
                    panelConsole.BackgroundImage = Properties.Resources.fc;
                    Injector = new NESInjector();
                    bootPath = "resources\\images\\boot_fc.png";
                    iconPath = "resources\\images\\icon.png";
                    titleScreenPath = "resources\\images\\title_screen_fc.png";
                    labelAspectRatio.Visible = true;
                    comboBoxAspectRatioNES.Visible = true;
                    labelSpeed.Visible = true;
                    comboBoxSpeed.Visible = true;
                    labelPlayers.Visible = true;
                    comboBoxPlayers.Visible = true;
                    labelShowPlayers.Visible = true;
                    comboBoxShowPlayers.Visible = true;
                    Cll.Log.WriteLine("GUI changed to Famicom.");
                    break;
                case RomFile.Format.NES:
                    panelConsole.BackgroundImage = Properties.Resources.nes;
                    Injector = new NESInjector();
                    bootPath = "resources\\images\\boot_nes.png";
                    iconPath = "resources\\images\\icon.png";
                    titleScreenPath = "resources\\images\\title_screen_nes.png";
                    labelAspectRatio.Visible = true;
                    comboBoxAspectRatioNES.Visible = true;
                    labelSpeed.Visible = true;
                    comboBoxSpeed.Visible = true;
                    labelPlayers.Visible = true;
                    comboBoxPlayers.Visible = true;
                    labelShowPlayers.Visible = true;
                    comboBoxShowPlayers.Visible = true;
                    Cll.Log.WriteLine("GUI changed to NES.");
                    break;
                case RomFile.Format.SuperFamicom:
                    panelConsole.BackgroundImage = Properties.Resources.sfc;
                    Injector = new SNESInjector();
                    bootPath = "resources\\images\\boot_sfc.png";
                    iconPath = "resources\\images\\icon.png";
                    titleScreenPath = "resources\\images\\title_screen_sfc.png";
                    labelAspectRatio.Visible = true;
                    comboBoxAspectRatioSNES.Visible = true;
                    labelSpeed.Visible = true;
                    comboBoxSpeed.Visible = true;
                    labelPlayers.Visible = true;
                    comboBoxPlayers.Visible = true;
                    labelShowPlayers.Visible = true;
                    comboBoxShowPlayers.Visible = true;
                    if (Injector.BaseSupportsSoundVolume)
                    {
                        labelSoundVolume.Visible = true;
                        numericUpDownSoundVolume.Visible = true;
                    }
                    Cll.Log.WriteLine("GUI changed to Super Famicom.");
                    break;
                case RomFile.Format.SNES_EUR:
                    panelConsole.BackgroundImage = Properties.Resources.snes_pal;
                    Injector = new SNESInjector();
                    bootPath = "resources\\images\\boot_snes_pal.png";
                    iconPath = "resources\\images\\icon.png";
                    titleScreenPath = "resources\\images\\title_screen_sfc.png";
                    labelAspectRatio.Visible = true;
                    comboBoxAspectRatioSNES.Visible = true;
                    labelSpeed.Visible = true;
                    comboBoxSpeed.Visible = true;
                    labelPlayers.Visible = true;
                    comboBoxPlayers.Visible = true;
                    labelShowPlayers.Visible = true;
                    comboBoxShowPlayers.Visible = true;
                    if (Injector.BaseSupportsSoundVolume)
                    {
                        labelSoundVolume.Visible = true;
                        numericUpDownSoundVolume.Visible = true;
                    }
                    Cll.Log.WriteLine("GUI changed to SNES (EUR).");
                    break;
                case RomFile.Format.SNES_USA:
                    panelConsole.BackgroundImage = Properties.Resources.snes;
                    Injector = new SNESInjector();
                    bootPath = "resources\\images\\boot_snes.png";
                    iconPath = "resources\\images\\icon.png";
                    titleScreenPath = "resources\\images\\title_screen_snes.png";
                    labelAspectRatio.Visible = true;
                    comboBoxAspectRatioSNES.Visible = true;
                    labelSpeed.Visible = true;
                    comboBoxSpeed.Visible = true;
                    labelPlayers.Visible = true;
                    comboBoxPlayers.Visible = true;
                    labelShowPlayers.Visible = true;
                    comboBoxShowPlayers.Visible = true;
                    if (Injector.BaseSupportsSoundVolume)
                    {
                        labelSoundVolume.Visible = true;
                        numericUpDownSoundVolume.Visible = true;
                    }
                    Cll.Log.WriteLine("GUI changed to SNES (USA).");
                    break;
                case RomFile.Format.N64:
                    panelConsole.BackgroundImage = Properties.Resources.n64;
                    Injector = new N64Injector();
                    bootPath = "resources\\images\\boot_n64.png";
                    iconPath = "resources\\images\\icon.png";
                    titleScreenPath = "resources\\images\\title_screen_n64.png";
                    checkBoxDarkFilter.Visible = true;
                    checkBoxWidescreen.Visible = true;
                    labelZoomH.Visible = true;
                    numericUpDownZoomH.Visible = true;
                    labelZoomV.Visible = true;
                    numericUpDownZoomV.Visible = true;
                    labelTranslationX.Visible = true;
                    numericUpDownTranslationX.Visible = true;
                    labelTranslationY.Visible = true;
                    numericUpDownTranslationY.Visible = true;
                    buttonConfigFile.Visible = true;
                    labelConfigFile.Visible = true;
                    textBoxConfigFile.Visible = true;
                    buttonEditConfigFile.Visible = true;
                    labelShowPlayers.Visible = true;
                    comboBoxShowPlayers.Visible = true;
                    Cll.Log.WriteLine("GUI changed to N64.");
                    break;
                case RomFile.Format.GBA:
                    panelConsole.BackgroundImage = Properties.Resources.gba;
                    Injector = new GBAInjector();
                    bootPath = "resources\\images\\boot_gba.png";
                    iconPath = "resources\\images\\icon_gba.png";
                    titleScreenPath = "resources\\images\\title_screen_gba.png";
                    Cll.Log.WriteLine("GUI changed to GBA.");
                    break;
                case RomFile.Format.NDS:
                    panelConsole.BackgroundImage = Properties.Resources.nds;
                    Injector = new NDSInjector();
                    bootPath = "resources\\images\\boot_nds.png";
                    iconPath = "resources\\images\\icon.png";
                    titleScreenPath = "resources\\images\\title_screen_nds.png";
                    checkBoxUseNDSIcon.Visible = true;
                    panelNDSIcon.Visible = true;
                    buttonNDSIconBGColor.Visible = true;
                    Cll.Log.WriteLine("GUI changed to NDS.");
                    break;
                default:
                    panelConsole.BackgroundImage = null;
                    Injector = null;
                    bootPath = "";
                    iconPath = "";
                    titleScreenPath = "";
                    Cll.Log.WriteLine("GUI changed to default.");
                    break;
            }

            this.Text = "Phacox's Injector " + WiiUInjector.Release;
            textBoxRom.Text = "";

            if (Injector != null)
            {
                buttonLoadBase.Enabled = true;
                if (Injector.BaseIsLoaded)
                {
                    panelLoadedBase.BackgroundImage = Properties.Resources.checkmark_16;
                    labelLoadedBase.Text = Injector.LoadedBase;
                    Cll.Log.WriteLine("Loaded base: " + Injector.LoadedBase);
                }
                else
                {
                    panelLoadedBase.BackgroundImage = Properties.Resources.x_mark_16;
                    labelLoadedBase.Text = "\nTitle:";
                }
            }
            else
            {
                buttonLoadBase.Enabled = false;
                panelLoadedBase.BackgroundImage = Properties.Resources.x_mark_16;
                labelLoadedBase.Text = "\nTitle:";
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

            UpdateMenuIconPictureBox();
            UpdateBootTvPictureBox();
            UpdateBootDrcPictureBox();            
        }

        #endregion

        #region Main

        private void buttonRom_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.FileName = "";
            openFileDialog.FilterIndex = 0;
            switch (Mode)
            {
                case RomFile.Format.Famicom:
                case RomFile.Format.NES:
                    openFileDialog.Filter = "NES ROM|*.nes;*.fds|All files|*.*";
                    if (Directory.Exists(Properties.Settings.Default.DirectoryRomNES))
                        openFileDialog.InitialDirectory = Properties.Settings.Default.DirectoryRomNES;
                    break;
                case RomFile.Format.SuperFamicom:
                case RomFile.Format.SNES_EUR:
                case RomFile.Format.SNES_USA:
                    openFileDialog.Filter = "SNES ROM|*.sfc;*.smc|All files|*.*";
                    if (Directory.Exists(Properties.Settings.Default.DirectoryRomSNES))
                        openFileDialog.InitialDirectory = Properties.Settings.Default.DirectoryRomSNES;
                    break;
                case RomFile.Format.N64:
                    openFileDialog.Filter = "N64 ROM|*.z64;*.n64;*.v64|All files|*.*";
                    if (Directory.Exists(Properties.Settings.Default.DirectoryRomN64))
                        openFileDialog.InitialDirectory = Properties.Settings.Default.DirectoryRomN64;
                    break;
                case RomFile.Format.GBA:
                    openFileDialog.Filter = "GBA ROM|*.gba|All files|*.*";
                    if (Directory.Exists(Properties.Settings.Default.DirectoryRomGBA))
                        openFileDialog.InitialDirectory = Properties.Settings.Default.DirectoryRomGBA;
                    break;
                case RomFile.Format.NDS:
                    openFileDialog.Filter = "NDS ROM|*.nds|All files|*.*";
                    if (Directory.Exists(Properties.Settings.Default.DirectoryRomNDS))
                        openFileDialog.InitialDirectory = Properties.Settings.Default.DirectoryRomNDS;
                    break;
                default: openFileDialog.Filter = "Game file|*.nes;*.fds;*.sfc;*.smc;*.z64;*.n64;*.v64;*.gba;*.nds" +
                    "|NES ROM|*.nes|SNES ROM|*.sfc;*.smc|N64 ROM|*.z64;*.n64;*.v64|GBA ROM|*.gba|NDS ROM|*.nds|All files|*.*";
                    if (Directory.Exists(Properties.Settings.Default.DirectoryRom))
                        openFileDialog.InitialDirectory = Properties.Settings.Default.DirectoryRom;
                    break;
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                switch (Mode)
                {
                    case RomFile.Format.Famicom:
                    case RomFile.Format.NES:
                        Properties.Settings.Default.DirectoryRomNES = Path.GetDirectoryName(openFileDialog.FileName);
                        break;
                    case RomFile.Format.SuperFamicom:
                    case RomFile.Format.SNES_EUR:
                    case RomFile.Format.SNES_USA:
                        Properties.Settings.Default.DirectoryRomSNES = Path.GetDirectoryName(openFileDialog.FileName);
                        break;
                    case RomFile.Format.N64:
                        Properties.Settings.Default.DirectoryRomN64 = Path.GetDirectoryName(openFileDialog.FileName);
                        break;
                    case RomFile.Format.GBA:
                        Properties.Settings.Default.DirectoryRomGBA = Path.GetDirectoryName(openFileDialog.FileName);
                        break;
                    case RomFile.Format.NDS:
                        Properties.Settings.Default.DirectoryRomNDS = Path.GetDirectoryName(openFileDialog.FileName);
                        break;
                    default:
                        Properties.Settings.Default.DirectoryRom = Path.GetDirectoryName(openFileDialog.FileName);
                        RomFile.Format format = RomFile.GetFormat(openFileDialog.FileName);
                        Cll.Log.WriteLine("ROM format: " + format.ToString());
                        ChangeGUI(format);
                        break;
                }

                if (Injector != null)
                {
                    Cll.Log.WriteLine("Reading ROM...");
                    Injector.SetRom(openFileDialog.FileName);
                    textBoxRom.Text = Path.GetFileName(openFileDialog.FileName);

                    if (Injector.RomIsValid)
                    {
                        Cll.Log.WriteLine("ROM is valid.");
                        Cll.Log.WriteLine("  Hash: " + Injector.RomHashCRC16.ToString("X4"));
                        if (Injector.Console == RomFile.Format.Famicom ||
                            Injector.Console == RomFile.Format.NES ||
                            Injector.Console == RomFile.Format.SuperFamicom ||
                            Injector.Console == RomFile.Format.SNES_EUR ||
                            Injector.Console == RomFile.Format.SNES_USA)
                        {
                            Cll.Log.WriteLine("  Size: " + Useful.ToFileSize(Injector.RomSize));
                            this.Text = "Phacox's Injector " + WiiUInjector.Release;
                            if (!Injector.BaseSupportsRomSize)
                                labelRomInfo.ForeColor = Color.Red;
                            else
                                labelRomInfo.ForeColor = Color.White;
                            labelRomInfo.Text = "ROM size: " + Useful.ToFileSize(Injector.RomSize);
                        }
                        else
                        {
                            string productCode = Injector.RomProductCode +
                                (Injector.RomVersion != 0 ? " (Rev " + Injector.RomRevision + ")" : "");
                            Cll.Log.WriteLine("  Title: " + Injector.RomTitle);
                            Cll.Log.WriteLine("  Product code: " + productCode);
                            this.Text = "Phacox's Injector " + WiiUInjector.Release + " :: " + Injector.RomTitle;
                            labelRomInfo.ForeColor = Color.White;
                            labelRomInfo.Text = "Product code: " + productCode;
                            if (Injector.Console == RomFile.Format.NDS)
                                panelNDSIcon.BackgroundImage = (Injector as NDSInjector).RomIcon;
                        }
                    }
                    else
                        Cll.Log.WriteLine("ROM is invalid.");

                    if (Injector.BaseIsLoaded && Injector.RomIsValid)
                        labelTitleId.Text = "Title ID: " + Injector.TitleId;
                    else
                        labelTitleId.Text = "Title ID:";

                    if (Injector.BaseIsLoaded && textBoxShortName.Text.Length > 0 &&
                        ((Injector.RomIsValid && Injector.BaseSupportsRomSize) ||
                        !Injector.RomIsValid))
                    {
                        buttonInjectPack.Enabled = true;
                        buttonInjectNotPack.Enabled = true;
                        panelPackingQuestion.Visible = false;
                    }
                    else
                    {
                        buttonInjectPack.Enabled = false;
                        buttonInjectNotPack.Enabled = false;
                        panelPackingQuestion.Visible = true;
                    }
                }
                else
                {
                    labelTitleId.Text = "Title ID:";
                    buttonInjectPack.Enabled = false;
                    buttonInjectNotPack.Enabled = false;
                    panelPackingQuestion.Visible = true;
                }
            }
        }

        private void textBoxShortName_TextChanged(object sender, EventArgs e)
        {
            if (Injector != null &&
                Injector.BaseIsLoaded &&
                textBoxShortName.Text.Length > 0 &&
                (
                 (Injector.RomIsValid && Injector.BaseSupportsRomSize) ||
                 !Injector.RomIsValid
                )
               )
            {
                buttonInjectPack.Enabled = true;
                buttonInjectNotPack.Enabled = true;
                panelPackingQuestion.Visible = false;
            }
            else
            {
                buttonInjectPack.Enabled = false;
                buttonInjectNotPack.Enabled = false;
                panelPackingQuestion.Visible = true;
            }

            //UpdateBootName();
            //UpdateBootTvPictureBox();
            //UpdateBootDrcPictureBox();
        }

        private void checkBoxLongName_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLongName.Checked)
            {
                textBoxLNLine1.BackColor = Color.FromArgb(51, 51, 51);
                textBoxLNLine2.BackColor = Color.FromArgb(51, 51, 51);
                textBoxLNLine1.Enabled = true;
                textBoxLNLine2.Enabled = true;
                BootTvImg.Longname = true;
                BootDrcImg.Longname = true;
            }
            else
            {
                textBoxLNLine1.Text = "";
                textBoxLNLine2.Text = "";
                textBoxLNLine1.BackColor = Color.FromArgb(33, 33, 33);
                textBoxLNLine2.BackColor = Color.FromArgb(33, 33, 33);
                textBoxLNLine1.Enabled = false;
                textBoxLNLine2.Enabled = false;
                BootTvImg.Longname = false;
                BootDrcImg.Longname = false;
            }

            //UpdateBootName();
            //UpdateBootTvPictureBox();
            //UpdateBootDrcPictureBox();
        }

        private void buttonLoadBase_Click(object sender, EventArgs e)
        {
            if (Injector != null)
            {
                if ((AskBase() || Injector.BaseIsLoaded) && Injector.RomIsValid)
                    labelTitleId.Text = "Title ID: " + Injector.TitleId;
                else
                    labelTitleId.Text = "Title ID:";

                if (Injector.BaseIsLoaded && textBoxShortName.Text.Length > 0 &&
                    ((Injector.RomIsValid && Injector.BaseSupportsRomSize) ||
                    !Injector.RomIsValid))
                {
                    buttonInjectPack.Enabled = true;
                    buttonInjectNotPack.Enabled = true;
                    panelPackingQuestion.Visible = false;
                }
                else
                {
                    buttonInjectPack.Enabled = false;
                    buttonInjectNotPack.Enabled = false;
                    panelPackingQuestion.Visible = true;
                }

                if (Injector.RomIsValid && !Injector.BaseSupportsRomSize)
                    labelRomInfo.ForeColor = Color.Red;
                else
                    labelRomInfo.ForeColor = Color.White;
            }
        }

        private bool AskBase()
        {
            RomFile.Format mode = Mode;
            if (mode == RomFile.Format.Indeterminate)
                mode = Injector.Console;

            folderBrowserDialog.SelectedPath = "";
            switch (mode)
            {
                case RomFile.Format.Famicom:
                case RomFile.Format.NES:
                    folderBrowserDialog.Description = HelpString.NESBaseSelect;
                    if (Directory.Exists(Properties.Settings.Default.DirectoryBaseNES))
                        folderBrowserDialog.SelectedPath = Properties.Settings.Default.DirectoryBaseNES;
                    break;
                case RomFile.Format.SuperFamicom:
                case RomFile.Format.SNES_EUR:
                case RomFile.Format.SNES_USA:
                    folderBrowserDialog.Description = HelpString.SNESBaseSelect;
                    if (Directory.Exists(Properties.Settings.Default.DirectoryBaseSNES))
                        folderBrowserDialog.SelectedPath = Properties.Settings.Default.DirectoryBaseSNES;
                    break;
                case RomFile.Format.N64:
                    folderBrowserDialog.Description = HelpString.N64BaseSelect;
                    if (Directory.Exists(Properties.Settings.Default.DirectoryBaseN64))
                        folderBrowserDialog.SelectedPath = Properties.Settings.Default.DirectoryBaseN64;
                    break;
                case RomFile.Format.GBA:
                    folderBrowserDialog.Description = HelpString.GBABaseSelect;
                    if (Directory.Exists(Properties.Settings.Default.DirectoryBaseGBA))
                        folderBrowserDialog.SelectedPath = Properties.Settings.Default.DirectoryBaseGBA;
                    break;
                case RomFile.Format.NDS:
                    folderBrowserDialog.Description = HelpString.NDSBaseSelect;
                    if (Directory.Exists(Properties.Settings.Default.DirectoryBaseNDS))
                        folderBrowserDialog.SelectedPath = Properties.Settings.Default.DirectoryBaseNDS;
                    break;
                default:
                    break;
            }

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                switch (mode)
                {
                    case RomFile.Format.Famicom:
                    case RomFile.Format.NES:
                        Properties.Settings.Default.DirectoryBaseNES = folderBrowserDialog.SelectedPath;
                        Cll.Log.WriteLine("Loading NES base...");
                        break;
                    case RomFile.Format.SuperFamicom:
                    case RomFile.Format.SNES_EUR:
                    case RomFile.Format.SNES_USA:
                        Properties.Settings.Default.DirectoryBaseSNES = folderBrowserDialog.SelectedPath;
                        Cll.Log.WriteLine("Loading SNES base...");
                        break;
                    case RomFile.Format.N64:
                        Properties.Settings.Default.DirectoryBaseN64 = folderBrowserDialog.SelectedPath;
                        Cll.Log.WriteLine("Loading N64 base...");
                        break;
                    case RomFile.Format.GBA:
                        Properties.Settings.Default.DirectoryBaseGBA = folderBrowserDialog.SelectedPath;
                        Cll.Log.WriteLine("Loading GBA base...");
                        break;
                    case RomFile.Format.NDS:
                        Properties.Settings.Default.DirectoryBaseNDS = folderBrowserDialog.SelectedPath;
                        Cll.Log.WriteLine("Loading NDS base...");
                        break;
                    default:
                        break;
                }

                labelLoadedBase.Text = HelpString.Loading;
                panelLoadedBase.BackgroundImage = Properties.Resources.x_mark_16;

                try
                {
                    Injector.LoadBase(folderBrowserDialog.SelectedPath);
                    Cll.Log.WriteLine("The base is valid.");
                    labelLoadedBase.Text = Injector.LoadedBase;
                    panelLoadedBase.BackgroundImage = Properties.Resources.checkmark_16;
                    if (Injector.BaseSupportsSoundVolume)
                    {
                        labelSoundVolume.Visible = true;
                        numericUpDownSoundVolume.Visible = true;
                    }
                    else
                    {
                        labelSoundVolume.Visible = false;
                        numericUpDownSoundVolume.Visible = false;
                    }
                    Cll.Log.WriteLine("Loaded base:" + Injector.LoadedBase);
                    return true;
                }
                catch (Exception e)
                {
                    Cll.Log.WriteLine("The base is invalid.");
                    labelLoadedBase.Text = HelpString.BaseInvalid;
                    panelLoadedBase.BackgroundImage = Properties.Resources.x_mark_16;
                    Cll.Log.WriteLine(e.ToString());
                    Cll.Log.WriteLine("Could not load base.");
                }
            }
            return false;
        }

        private void buttonConfigFile_Click(object sender, EventArgs e)
        {
            if ((Injector != null && Injector.Console == RomFile.Format.N64) || Mode == RomFile.Format.N64)
            {
                openFileDialog.FileName = "";
                openFileDialog.FilterIndex = 0;
                openFileDialog.Filter = "N64 Config file|*.ini;*.txt|INI file|*.ini|TXT file|*.txt|All files|*.*";
                if (Directory.Exists(Properties.Settings.Default.DirectoryN64ConfigFile))
                    openFileDialog.InitialDirectory = Properties.Settings.Default.DirectoryN64ConfigFile;
                else
                    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.DirectoryImages = Path.GetDirectoryName(openFileDialog.FileName);

                    Cll.Log.WriteLine("Reading N64 Config file...");
                    (Injector as N64Injector).SetConfigFile(openFileDialog.FileName);
                    textBoxConfigFile.Text = Path.GetFileName(openFileDialog.FileName);

                    if ((Injector as N64Injector).ConfigFileIsValid)
                    {
                        Cll.Log.WriteLine("N64 Config file is valid.");
                        Cll.Log.WriteLine("  Hash: " + (Injector as N64Injector).ConfigFileHashCRC16.ToString("X4"));
                    }
                    else
                    {
                        Cll.Log.WriteLine(HelpString.N64ConfigFileInvalid);
                        MessageBox.Show(HelpString.N64ConfigFileInvalid,
                               "Phacox's Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (Injector.BaseIsLoaded && Injector.RomIsValid)
                        labelTitleId.Text = "Title ID: " + Injector.TitleId;
                }
            }
        }

        private void buttonEditConfigFile_Click(object sender, EventArgs e)
        {
            try
            {
                StartVCN64ConfigEditor();
            }
            catch
            {
                Cll.Log.WriteLine("Error \"VCN64Config.exe\" program not found.");
                MessageBox.Show("\"VCN64Config.exe\" program not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartVCN64ConfigEditor()
        {
            if ((Injector != null && Injector.Console == RomFile.Format.N64) || Mode == RomFile.Format.N64)
            {
                if (!Directory.Exists("resources\\vcn64configs"))
                    Directory.CreateDirectory("resources\\vcn64configs");
            
                string input = "";
                if ((Injector as N64Injector).ConfigFileIsValid)
                    input = (Injector as N64Injector).ConfigFilePath;

                StringBuilder output = new StringBuilder("resources\\vcn64configs\\");
                if (Injector.RomIsValid)
                {
                    output.Append(Injector.RomProductCodeVersion);
                    output.Append(" (" + Injector.RomTitle + ")");
                }
                else
                    output.Append("TempConfigFile");
                output.Append(".ini");

                StringBuilder desc = new StringBuilder();
                if (textBoxShortName.Text.Length > 0)
                    desc.Append(textBoxShortName.Text);
                if (desc.Length > 0)
                    desc.Append(" ");
                if (Injector.RomIsValid)
                    desc.Append(Injector.RomProductCodeVersion);

                VCN64Config.FormEditor editor = new VCN64Config.FormEditor();
                Cll.Log.WriteLine("Start VCN64Config Editor.");
                if (editor.ShowDialog(input, output.ToString(), desc.ToString()) == DialogResult.OK)
                {
                    Cll.Log.WriteLine("Reading N64 Config file...");
                    (Injector as N64Injector).SetConfigFile(output.ToString());
                    textBoxConfigFile.Text = Path.GetFileName(output.ToString());

                    if ((Injector as N64Injector).ConfigFileIsValid)
                    {
                        Cll.Log.WriteLine("N64 Config file is valid.");
                        Cll.Log.WriteLine("  Hash: " + (Injector as N64Injector).ConfigFileHashCRC16.ToString("X4"));
                    }
                    else
                    {
                        Cll.Log.WriteLine(HelpString.N64ConfigFileInvalid);
                        MessageBox.Show(HelpString.N64ConfigFileInvalid,
                               "Phacox's Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (Injector.BaseIsLoaded && Injector.RomIsValid)
                        labelTitleId.Text = "Title ID: " + Injector.TitleId;
                }
                editor.Dispose();
            }
        }

        #endregion

        #region Images

        private void buttonTitleScreen_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.FilterIndex = 0;
            openFileDialog.Filter = "Image file|*.png;*.jpg;*.bmp";
            if (Directory.Exists(Properties.Settings.Default.DirectoryImages))
                openFileDialog.InitialDirectory = Properties.Settings.Default.DirectoryImages;
            else
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.DirectoryImages = Path.GetDirectoryName(openFileDialog.FileName);

                BootTvImg.TitleScreen = new Bitmap(openFileDialog.FileName);
                BootDrcImg.TitleScreen = new Bitmap(openFileDialog.FileName);
                MenuIconImg.TitleScreen = new Bitmap(openFileDialog.FileName);

                UpdateMenuIconPictureBox();
                UpdateBootTvPictureBox();
                UpdateBootDrcPictureBox();

                Cll.Log.WriteLine("Title screen changed.");
            }
        }

        private void buttonMenuIcon_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.FilterIndex = 0;
            openFileDialog.Filter = "Image file|*.png;*.jpg;*.bmp";
            if (Directory.Exists(Properties.Settings.Default.DirectoryImages))
                openFileDialog.InitialDirectory = Properties.Settings.Default.DirectoryImages;
            else
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.DirectoryImages = Path.GetDirectoryName(openFileDialog.FileName);
                MenuIconImg.Frame = new Bitmap(openFileDialog.FileName);
                UpdateMenuIconPictureBox();
                Cll.Log.WriteLine("Menu icon changed.");
            }
        }

        private void checkBoxKeepMenuIcon_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKeepMenuIcon.Checked)
            {
                buttonMenuIcon.Enabled = false;
                Cll.Log.WriteLine("Keep menu icon: true");
            }
            else
            {
                buttonMenuIcon.Enabled = true;
                Cll.Log.WriteLine("Keep menu icon: false");
            }
            UpdateMenuIconPictureBox();
        }

        private void buttonBootTv_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.FilterIndex = 0;
            openFileDialog.Filter = "Image file|*.png;*.jpg;*.bmp";
            if (Directory.Exists(Properties.Settings.Default.DirectoryImages))
                openFileDialog.InitialDirectory = Properties.Settings.Default.DirectoryImages;
            else
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.DirectoryImages = Path.GetDirectoryName(openFileDialog.FileName);
                BootTvImg.Frame = new Bitmap(openFileDialog.FileName);
                UpdateBootTvPictureBox();
                Cll.Log.WriteLine("TV image changed.");
            }
        }

        private void checkBoxKeepBootTv_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKeepBootTv.Checked)
            {
                buttonBootTv.Enabled = false;
                Cll.Log.WriteLine("Keep TV image: true");
            }
            else
            {
                buttonBootTv.Enabled = true;
                Cll.Log.WriteLine("Keep TV image: false");
            }
            UpdateBootTvPictureBox();
        }

        private void buttonBootDrc_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.FilterIndex = 0;
            openFileDialog.Filter = "Image file|*.png;*.jpg;*.bmp";
            if (Directory.Exists(Properties.Settings.Default.DirectoryImages))
                openFileDialog.InitialDirectory = Properties.Settings.Default.DirectoryImages;
            else
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.DirectoryImages = Path.GetDirectoryName(openFileDialog.FileName);
                BootDrcImg.Frame = new Bitmap(openFileDialog.FileName);
                UpdateBootDrcPictureBox();
                Cll.Log.WriteLine("GamePad image changed.");
            }
        }

        private void checkBoxKeepBootDrc_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKeepBootDrc.Checked)
            {
                buttonBootDrc.Enabled = false;
                Cll.Log.WriteLine("Keep GamePad image: true");
            }
            else
            {
                buttonBootDrc.Enabled = true;
                Cll.Log.WriteLine("Keep GamePad image: false");
            }
            UpdateBootDrcPictureBox();
        }

        private void checkBoxShowName_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBootName();
            UpdateBootTvPictureBox();
            UpdateBootDrcPictureBox();
        }

        private void UpdateBootName()
        {
            if (checkBoxShowName.Checked)
            {
                Cll.Log.WriteLine("Show name enabled.");
                if (checkBoxLongName.Checked)
                {
                    Cll.Log.WriteLine("Use long name: " + textBoxLNLine1.Text + " " + textBoxLNLine2.Text);
                    BootTvImg.NameLine1 = textBoxLNLine1.Text;
                    BootTvImg.NameLine2 = textBoxLNLine2.Text;
                    BootDrcImg.NameLine1 = textBoxLNLine1.Text;
                    BootDrcImg.NameLine2 = textBoxLNLine2.Text;
                }
                else
                {
                    Cll.Log.WriteLine("Use short name: " + textBoxShortName.Text);
                    BootTvImg.NameLine1 = textBoxShortName.Text;
                    BootTvImg.NameLine2 = "";
                    BootDrcImg.NameLine1 = textBoxShortName.Text;
                    BootDrcImg.NameLine2 = "";
                }
            }
            else
            {
                Cll.Log.WriteLine("Show name disabled.");
                BootTvImg.NameLine1 = "";
                BootTvImg.NameLine2 = "";
                BootDrcImg.NameLine1 = "";
                BootDrcImg.NameLine2 = "";
            }
        }

        private void checkBoxReleaseDate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxReleaseDate.Checked)
            {
                BootTvImg.Released = (int)numericUpDownReleaseDate.Value;
                BootDrcImg.Released = (int)numericUpDownReleaseDate.Value;
                Cll.Log.WriteLine("Release date enabled: " + ((int)numericUpDownReleaseDate.Value).ToString());
            }
            else
            {
                BootTvImg.Released = 0;
                BootDrcImg.Released = 0;
                Cll.Log.WriteLine("Release date disabled.");
            }
            UpdateBootTvPictureBox();
            UpdateBootDrcPictureBox();
        }

        private void numericUpDownReleaseDate_MouseUp(object sender, MouseEventArgs e)
        {
            if (checkBoxReleaseDate.Checked)
            {
                BootTvImg.Released = (int)numericUpDownReleaseDate.Value;
                BootDrcImg.Released = (int)numericUpDownReleaseDate.Value;
                Cll.Log.WriteLine("Release date changed: " + ((int)numericUpDownReleaseDate.Value).ToString());
                UpdateBootTvPictureBox();
                UpdateBootDrcPictureBox();
            }
        }

        private void numericUpDownReleaseDate_Validated(object sender, EventArgs e)
        {
            if (checkBoxReleaseDate.Checked)
            {
                BootTvImg.Released = (int)numericUpDownReleaseDate.Value;
                BootDrcImg.Released = (int)numericUpDownReleaseDate.Value;
                Cll.Log.WriteLine("Release date validated: " + ((int)numericUpDownReleaseDate.Value).ToString());
                UpdateBootTvPictureBox();
                UpdateBootDrcPictureBox();
            }
        }

        private void comboBoxPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxShowPlayers.SelectedItem.ToString())
            {
                case "1":
                    BootTvImg.Players = 1;
                    BootDrcImg.Players = 1;
                    Cll.Log.WriteLine("Players changed: 1");
                    break;
                case "2":
                    BootTvImg.Players = 2;
                    BootDrcImg.Players = 2;
                    Cll.Log.WriteLine("Players changed: 2");
                    break;
                case "3":
                    BootTvImg.Players = 3;
                    BootDrcImg.Players = 3;
                    Cll.Log.WriteLine("Players changed: 3");
                    break;
                case "4":
                    BootTvImg.Players = 4;
                    BootDrcImg.Players = 4;
                    Cll.Log.WriteLine("Players changed: 4");
                    break;
                default:
                    BootTvImg.Players = 0;
                    BootDrcImg.Players = 0;
                    Cll.Log.WriteLine("Players changed: None");
                    break;
            }
            UpdateBootTvPictureBox();
            UpdateBootDrcPictureBox();
        }

        private void checkBoxUseNDSIcon_CheckedChanged(object sender, EventArgs e)
        {
            if (Injector != null && Injector.RomIsValid && Injector.Console == RomFile.Format.NDS)
            {
                if (BootTvImg.TitleScreen != null)
                    BootTvImg.TitleScreen.Dispose();
                if (BootDrcImg.TitleScreen != null)
                    BootDrcImg.TitleScreen.Dispose();
                if (MenuIconImg.TitleScreen != null)
                    MenuIconImg.TitleScreen.Dispose();

                if (checkBoxUseNDSIcon.Checked)
                {
                    Cll.Log.WriteLine("Use NDS icon enabled.");
                    buttonTitleScreen.Enabled = false;

                    Bitmap ndsIcon = new Bitmap(panelNDSIcon.Width, panelNDSIcon.Height);
                    panelNDSIcon.DrawToBitmap(ndsIcon, new Rectangle(0, 0, panelNDSIcon.Width, panelNDSIcon.Height));

                    BootTvImg.TitleScreen = new Bitmap(ndsIcon);
                    BootDrcImg.TitleScreen = new Bitmap(ndsIcon);
                    MenuIconImg.TitleScreen = new Bitmap(ndsIcon);

                    ndsIcon.Dispose();
                    
                }
                else
                {
                    Cll.Log.WriteLine("Use NDS icon disabled.");
                    buttonTitleScreen.Enabled = true;

                    if (File.Exists(TitleScreenPath))
                    {
                        BootTvImg.TitleScreen = new Bitmap(TitleScreenPath);
                        BootDrcImg.TitleScreen = new Bitmap(TitleScreenPath);
                        MenuIconImg.TitleScreen = new Bitmap(TitleScreenPath);
                    }
                    else
                    {
                        if (File.Exists("resources\\images\\title_screen_nds.png"))
                        {
                            TitleScreenPath = "resources\\images\\title_screen_nds.png";
                            BootTvImg.TitleScreen = new Bitmap(TitleScreenPath);
                            BootDrcImg.TitleScreen = new Bitmap(TitleScreenPath);
                            MenuIconImg.TitleScreen = new Bitmap(TitleScreenPath);
                        }
                        else
                        {
                            BootTvImg.TitleScreen = null;
                            BootDrcImg.TitleScreen = null;
                            MenuIconImg.TitleScreen = null;
                        }
                    }
                }

                UpdateMenuIconPictureBox();
                UpdateBootTvPictureBox();
                UpdateBootDrcPictureBox();                

                GC.Collect();
            }
        }

        private void buttonNDSIconBGColor_Click(object sender, EventArgs e)
        {
            colorDialog.FullOpen = true;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                panelNDSIcon.BackColor = colorDialog.Color;
                Cll.Log.WriteLine("NDS icon back color: " + colorDialog.Color.ToString());
                if (checkBoxUseNDSIcon.Checked)
                {
                    if (BootTvImg.TitleScreen != null)
                        BootTvImg.TitleScreen.Dispose();
                    if (BootDrcImg.TitleScreen != null)
                        BootDrcImg.TitleScreen.Dispose();
                    if (MenuIconImg.TitleScreen != null)
                        MenuIconImg.TitleScreen.Dispose();

                    Bitmap icon = new Bitmap(panelNDSIcon.Width, panelNDSIcon.Height);
                    panelNDSIcon.DrawToBitmap(icon, new Rectangle(0, 0, panelNDSIcon.Width, panelNDSIcon.Height));

                    BootTvImg.TitleScreen = new Bitmap(icon);
                    BootDrcImg.TitleScreen = new Bitmap(icon);
                    MenuIconImg.TitleScreen = new Bitmap(icon);

                    icon.Dispose();

                    UpdateMenuIconPictureBox();
                    UpdateBootTvPictureBox();
                    UpdateBootDrcPictureBox();
                }
            }
        }

        private void UpdateMenuIconPictureBox()
        {
            if (pictureBoxMenuIcon.Image != null)
                pictureBoxMenuIcon.Image.Dispose();

            if (Injector != null)
            {
                if (checkBoxKeepMenuIcon.Checked)
                    pictureBoxMenuIcon.Image = NusContent.TGAToBitmap(Injector.BasePath + "\\meta\\iconTex.tga");
                else
                    pictureBoxMenuIcon.Image = MenuIconImg.Create(Injector.Console);
            }
            else
                pictureBoxMenuIcon.Image = MenuIconImg.Create(Mode);

            Cll.Log.WriteLine("Menu icon preview updated.");
        }

        private void UpdateBootTvPictureBox()
        {
            if (pictureBoxBootTv.Image != null)
                pictureBoxBootTv.Image.Dispose();

            if (Injector != null)
            {
                if (checkBoxKeepBootTv.Checked)
                    pictureBoxBootTv.Image = NusContent.TGAToBitmap(Injector.BasePath + "\\meta\\bootTvTex.tga");
                else
                    pictureBoxBootTv.Image = BootTvImg.Create(Injector.Console);
            }
            else
                pictureBoxBootTv.Image = BootTvImg.Create(Mode);

            Cll.Log.WriteLine("TV preview updated.");
        }

        private void UpdateBootDrcPictureBox()
        {
            if (pictureBoxBootDrc.Image != null)
                pictureBoxBootDrc.Image.Dispose();

            if (Injector != null)
            {
                if (checkBoxKeepBootDrc.Checked)
                    pictureBoxBootDrc.Image = NusContent.TGAToBitmap(Injector.BasePath + "\\meta\\bootDrcTex.tga");
                else
                    pictureBoxBootDrc.Image = BootDrcImg.Create(Injector.Console);
            }
            else
                pictureBoxBootDrc.Image = BootDrcImg.Create(Mode);

            Cll.Log.WriteLine("GamePad preview updated.");
        }

        #endregion

        #region Injecting

        private void buttonInjectNotPack_Click(object sender, EventArgs e)
        {
            Inject(false);
        }

        private void buttonInjectPack_Click(object sender, EventArgs e)
        {
            Inject(true);
        }

        private void Inject(bool encrypt)
        {
            RomFile.Format mode = Mode;
            if (mode == RomFile.Format.Indeterminate)
                mode = Injector.Console;

            folderBrowserDialog.Description = HelpString.FolderResultSelect;            
            if (Directory.Exists(Properties.Settings.Default.DirectoryResult))
                folderBrowserDialog.SelectedPath = Properties.Settings.Default.DirectoryResult;
            else
                folderBrowserDialog.SelectedPath = "";

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.DirectoryResult = folderBrowserDialog.SelectedPath;

                try
                {
                    Cll.Log.WriteLine("Inject encrypt: " + encrypt.ToString());

                    string longName;
                    if (textBoxLNLine1.Text.Length > 0 && textBoxLNLine2.Text.Length > 0)
                        longName = textBoxLNLine1.Text + "\n" + textBoxLNLine2.Text;
                    else if (textBoxLNLine1.Text.Length > 0 && textBoxLNLine2.Text.Length == 0)
                        longName = textBoxLNLine1.Text;
                    else if (textBoxLNLine1.Text.Length == 0 && textBoxLNLine2.Text.Length > 0)
                        longName = textBoxLNLine2.Text;
                    else
                        longName = textBoxShortName.Text;

                    Cll.Log.WriteLine("Short name: " + textBoxShortName.Text);
                    Cll.Log.WriteLine("Long name: " + longName);

                    if (mode == RomFile.Format.Famicom ||
                        mode == RomFile.Format.NES)
                    {
                        switch (comboBoxAspectRatioNES.SelectedIndex)
                        {
                            default: (Injector as NESInjector).AspectRatioValue = NESInjector.AspectRatio.Default; break;
                            case 1: (Injector as NESInjector).AspectRatioValue = NESInjector.AspectRatio.H8V7; break;
                            case 2: (Injector as NESInjector).AspectRatioValue = NESInjector.AspectRatio.H4V3; break;
                            case 3: (Injector as NESInjector).AspectRatioValue = NESInjector.AspectRatio.H16V9; break;
                        }
                        switch (comboBoxSpeed.SelectedIndex)
                        {
                            default: (Injector as NESInjector).Speed = 60; break;
                            case 1: (Injector as NESInjector).Speed = 50; break;
                        }
                        switch (comboBoxPlayers.SelectedIndex)
                        {
                            default: (Injector as NESInjector).Players = 2; break;
                            case 1: (Injector as NESInjector).Players = 3; break;
                            case 2: (Injector as NESInjector).Players = 4; break;
                        }
                        Cll.Log.WriteLine("NES Aspect Ratio: " + (Injector as NESInjector).AspectRatioValue.ToString());
                        Cll.Log.WriteLine("NES Speed (FPS): " + (Injector as NESInjector).Speed.ToString());
                        Cll.Log.WriteLine("NES Players: " + (Injector as NESInjector).Players.ToString());
                    }
                    else if (mode == RomFile.Format.SuperFamicom ||
                        mode == RomFile.Format.SNES_EUR ||
                        mode == RomFile.Format.SNES_USA)
                    {
                        switch (comboBoxAspectRatioSNES.SelectedIndex)
                        {
                            default: (Injector as SNESInjector).AspectRatioValue = SNESInjector.AspectRatio.Default; break;
                            case 1: (Injector as SNESInjector).AspectRatioValue = SNESInjector.AspectRatio.H8V7; break;
                            case 2: (Injector as SNESInjector).AspectRatioValue = SNESInjector.AspectRatio.H16V9; break;
                        }
                        switch (comboBoxSpeed.SelectedIndex)
                        {
                            default: (Injector as SNESInjector).Speed = 60; break;
                            case 1: (Injector as SNESInjector).Speed = 50; break;
                        }
                        switch (comboBoxPlayers.SelectedIndex)
                        {
                            default: (Injector as SNESInjector).Players = 2; break;
                            case 1: (Injector as SNESInjector).Players = 3; break;
                            case 2: (Injector as SNESInjector).Players = 4; break;
                        }
                        (Injector as SNESInjector).SoundVolume = (byte)(numericUpDownSoundVolume.Value);
                        Cll.Log.WriteLine("SNES Aspect Ratio: " + (Injector as SNESInjector).AspectRatioValue.ToString());
                        Cll.Log.WriteLine("SNES Speed (FPS): " + (Injector as SNESInjector).Speed.ToString());
                        Cll.Log.WriteLine("SNES Players: " + (Injector as SNESInjector).Players.ToString());
                        if (Injector.BaseSupportsSoundVolume)
                            Cll.Log.WriteLine("SNES Sound Volume: " + (Injector as SNESInjector).SoundVolume.ToString());
                    }
                    else if (mode == RomFile.Format.N64)
                    {
                        (Injector as N64Injector).DarkFilter = checkBoxDarkFilter.Checked;
                        (Injector as N64Injector).Widescreen = checkBoxWidescreen.Checked;
                        (Injector as N64Injector).ScaleX = (float)(numericUpDownZoomH.Value / 100.0M);
                        (Injector as N64Injector).ScaleY = (float)(numericUpDownZoomV.Value / 100.0M);
                        (Injector as N64Injector).TranslationX = (float)(numericUpDownTranslationX.Value);
                        (Injector as N64Injector).TranslationY = (float)(numericUpDownTranslationY.Value);
                        Cll.Log.WriteLine("N64 DarkFilter: " + (Injector as N64Injector).DarkFilter.ToString());
                        Cll.Log.WriteLine("N64 Widescreen: " + (Injector as N64Injector).Widescreen.ToString());
                        Cll.Log.WriteLine("N64 Horizontal Zoom: " + ((Injector as N64Injector).ScaleX * 100).ToString());
                        Cll.Log.WriteLine("N64 Vertical Zoom: " + ((Injector as N64Injector).ScaleY * 100).ToString());
                        Cll.Log.WriteLine("N64 Translation X: " + (Injector as N64Injector).TranslationX.ToString());
                        Cll.Log.WriteLine("N64 Translation Y: " + (Injector as N64Injector).TranslationY.ToString());
                    }

                    UpdateBootName();
                    UpdateBootTvPictureBox();
                    UpdateBootDrcPictureBox();

                    Bitmap menuIconImg = null;
                    if (!checkBoxKeepMenuIcon.Checked)
                    {
                        menuIconImg = MenuIconImg.Create(Injector.Console);
                        Cll.Log.WriteLine("Change menu icon.");
                    }
                    else
                        Cll.Log.WriteLine("Keep menu icon.");

                    Bitmap bootTvImg = null;
                    if (!checkBoxKeepBootTv.Checked)
                    {
                        bootTvImg = BootTvImg.Create(Injector.Console);
                        Cll.Log.WriteLine("Change TV image.");
                    }
                    else
                        Cll.Log.WriteLine("Keep TV image.");

                    Bitmap bootDrcImg = null;
                    if (!checkBoxKeepBootDrc.Checked)
                    {
                        bootDrcImg = BootDrcImg.Create(Injector.Console);
                        Cll.Log.WriteLine("Change GamePad image.");
                    }
                    else
                        Cll.Log.WriteLine("Keep GamePad image.");

                    Cll.Log.WriteLine("Starting injection...");
                    LoadLogFile();

                    Injector.Inject(encrypt, folderBrowserDialog.SelectedPath, textBoxShortName.Text, longName, menuIconImg, bootTvImg, bootDrcImg);

                    Cll.Log.WriteLine("Injection success!");
                    LoadLogFile();
                    MessageBox.Show(HelpString.InjectionSuccessfully, "Phacox's Injector", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Cll.Log.WriteLine(ex.ToString());
                    Cll.Log.WriteLine("Injection failed.");
                    LoadLogFile();
                    MessageBox.Show(HelpString.InjectionFailed, "Phacox's Injector", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBoxLog.SelectionLength > 0)
                textBoxLog.Copy();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxLog.Select();
            textBoxLog.SelectAll();
        }

        #endregion

        #region Settings

        private void textBoxCommonKey_TextChanged(object sender, EventArgs e)
        {
            if (NusContent.LoadKey(textBoxCommonKey.Text))
            {
                textBoxCommonKey.Text = "";
                textBoxCommonKey.BackColor = Color.FromArgb(33, 33, 33);
                textBoxCommonKey.Enabled = false;
                panelValidKey.BackgroundImage = Properties.Resources.checkmark_16;
                Cll.Log.WriteLine("Valid common key!");
            }
            else
            {
                textBoxCommonKey.BackColor = Color.FromArgb(51, 51, 51);
                textBoxCommonKey.Enabled = true;
                panelValidKey.BackgroundImage = Properties.Resources.x_mark_16;
                Cll.Log.WriteLine("Invalid common key.");
            }
        }

        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string language;

            switch (comboBoxLanguage.SelectedIndex)
            {
                case 0:
                    language = "en-US";
                    Cll.Log.WriteLine("Language to EN.");
                    break;
                case 1:
                    language = "es-MX";
                    Cll.Log.WriteLine("Language to ES.");
                    break;
                default:
                    language = "en-US";
                    Cll.Log.WriteLine("Language to default (EN).");
                    break;
            }

            ComponentResourceManager resources = new ComponentResourceManager(typeof(PhacoxsInjectorGUI));
            ChangeLanguage(resources, this.Controls, language);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            ChangeGUI(Mode);
        }

        private void ChangeLanguage(ComponentResourceManager resources, Control.ControlCollection ctrls, string language)
        {
            foreach (Control c in ctrls)
            {
                if (c.Name != "labelLoadedBase")
                    resources.ApplyResources(c, c.Name, new CultureInfo(language));
                if (c is Panel)
                    ChangeLanguage(resources, c.Controls, language);
                else if (c is GroupBox)
                    ChangeLanguage(resources, c.Controls, language);
            }
        }

        private void checkBoxHelp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHelp.Checked)
            {
                groupBoxHelp.Visible = true;
                Cll.Log.WriteLine("Help enabled.");
            }
            else
            {
                groupBoxHelp.Visible = false;
                Cll.Log.WriteLine("Help disabled.");
            }
        }

        #endregion

        #region HelpGeneral

        private void comboBoxConsole_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.ModeSelect;
            labelHelpText.Text = HelpString.ModeSelectDescription;
        }

        private void panelConsole_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.VirtualConsoleTarget;

            if (Injector != null)
            {
                RomFile.Format mode = Mode;
                if (mode == RomFile.Format.Indeterminate)
                    mode = Injector.Console;
                switch (mode)
                {
                    case RomFile.Format.Famicom:
                        labelHelpText.Text = "Family Computer";
                        break;
                    case RomFile.Format.NES:
                        labelHelpText.Text = "Nintendo Entertainment System";
                        break;
                    case RomFile.Format.SuperFamicom:
                        labelHelpText.Text = "Super Famicom";
                        break;
                    case RomFile.Format.SNES_EUR:
                        labelHelpText.Text = "Super Nintendo Entertainment System (PAL)";
                        break;
                    case RomFile.Format.SNES_USA:
                        labelHelpText.Text = "Super Nintendo Entertainment System (NTSC)";
                        break;
                    case RomFile.Format.N64:
                        labelHelpText.Text = "Nintendo 64";
                        break;
                    case RomFile.Format.GBA:
                        labelHelpText.Text = "Game Boy Advance";
                        break;
                    case RomFile.Format.NDS:
                        labelHelpText.Text = "Nintendo DS";
                        break;
                    default:
                        labelHelpText.Text = "";
                        break;
                }
            }
            else labelHelpText.Text = "";
        }

        private void comboBoxConsole_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void panelConsole_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        #endregion

        #region HelpMain

        private void labelRom_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.ROM;
            labelHelpText.Text = HelpString.ROMDescription;
        }

        private void textBoxRom_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.ROM;
            labelHelpText.Text = HelpString.ROMDescription;
        }

        private void buttonRom_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.ChooseROM;
            labelHelpText.Text = HelpString.ChooseROMDescription;
        }

        private void labelRomInfo_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.ProductCode;
            labelHelpText.Text = HelpString.ProductCodeDescription;
        }

        private void labelRom_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void textBoxRom_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void buttonRom_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void labelRomInfo_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        //********************************************************************************
        private void labeShortName_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.ShortName;
            labelHelpText.Text = HelpString.ShortNameDescription;
        }

        private void textBoxShortName_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.ShortName;
            labelHelpText.Text = HelpString.ShortNameDescription;
        }

        private void labeShortName_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void textBoxShortName_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        //********************************************************************************
        private void checkBoxLongName_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.LongName;
            labelHelpText.Text = HelpString.LongNameDescription;
        }

        private void textBoxLNLine1_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.LongName;
            labelHelpText.Text = HelpString.LongNameBoxesDescription;
        }

        private void textBoxLNLine2_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.LongName;
            labelHelpText.Text = HelpString.LongNameBoxesDescription;
        }

        private void checkBoxLongName_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void textBoxLNLine1_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void textBoxLNLine2_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        //********************************************************************************
        private void buttonLoadBase_MouseEnter(object sender, EventArgs e)
        {
            if (checkBoxHelp.Checked)
            {
                groupBoxHelp.Text = HelpString.LoadBase;
                labelHelpText.Text = HelpString.LoadBaseDescription;
                if (textBoxCommonKey.Enabled)
                    labelHelpText.Text += "\n" + HelpString.LoadBaseWarningWiiUCK;
            }
        }

        private void labelTitleId_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.TitleID;
            labelHelpText.Text = HelpString.TitleIDDescription;
        }

        private void panelLoadedBase_MouseEnter(object sender, EventArgs e)
        {
            if (Injector != null)
            {
                if (Injector.BaseIsLoaded)
                {
                    groupBoxHelp.Text = HelpString.BaseChecked;
                    labelHelpText.Text = HelpString.BaseCheckedDescription;
                }
                else
                {
                    groupBoxHelp.Text = HelpString.BaseError;
                    switch (Injector.Console)
                    {
                        case RomFile.Format.Famicom:
                        case RomFile.Format.NES:
                            labelHelpText.Text = HelpString.NESBaseErrorDescription;
                            break;
                        case RomFile.Format.SuperFamicom:
                        case RomFile.Format.SNES_EUR:
                        case RomFile.Format.SNES_USA:
                            labelHelpText.Text = HelpString.SNESBaseErrorDescription;
                            break;
                        case RomFile.Format.N64:
                            labelHelpText.Text = HelpString.N64BaseErrorDescription;
                            break;
                        case RomFile.Format.GBA:
                            labelHelpText.Text = HelpString.GBABaseErrorDescription;
                            break;
                        case RomFile.Format.NDS:
                            labelHelpText.Text = HelpString.NDSBaseErrorDescription;
                            break;
                        default:
                            break;
                    }
                }
            }
            else labelHelpText.Text = "";
        }

        private void labelLoadedBase_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.Base;
            labelHelpText.Text = HelpString.BaseDescription;
        }

        private void buttonLoadBase_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void labelTitleId_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void panelLoadedBase_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void labelLoadedBase_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        //********************************************************************************
        private void labelAspectRatio_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.AspectRatio;
            labelHelpText.Text = HelpString.AspectRatioDescription;
        }

        private void comboBoxAspectRatioNES_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.AspectRatio;
            labelHelpText.Text = HelpString.AspectRatioDescription;
        }

        private void comboBoxAspectRatioSNES_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.AspectRatio;
            labelHelpText.Text = HelpString.AspectRatioDescription;
        }

        private void labelSpeed_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.Speed;
            labelHelpText.Text = HelpString.SpeedDescription;
        }

        private void comboBoxSpeed_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.Speed;
            labelHelpText.Text = HelpString.SpeedDescription;
        }

        private void labelPlayers_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.Players;
            labelHelpText.Text = HelpString.PlayersDescription;
        }

        private void comboBoxPlayers_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.Players;
            labelHelpText.Text = HelpString.PlayersDescription;
        }

        private void labelSoundVolume_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.SoundVolume;
            labelHelpText.Text = HelpString.SoundVolumeDescription;
        }

        private void labelAspectRatio_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void comboBoxAspectRatioNES_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void comboBoxAspectRatioSNES_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void labelSpeed_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void comboBoxSpeed_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void labelPlayers_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void comboBoxPlayers_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void labelSoundVolume_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        //********************************************************************************
        private void checkBoxDarkFilter_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.DarkFilter;
            labelHelpText.Text = HelpString.DarkFilterDescription;
        }

        private void checkBoxWidescreen_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.Widescreen;
            labelHelpText.Text = HelpString.WidescreenDescription;
        }

        private void labelZoomH_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.Zoom;
            labelHelpText.Text = HelpString.ZoomDescription;
        }

        private void labelZoomV_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.Zoom;
            labelHelpText.Text = HelpString.ZoomDescription;
        }

        private void labelTranslationX_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.Translation;
            labelHelpText.Text = HelpString.TranslationDescription;
        }

        private void labelTranslationY_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.Translation;
            labelHelpText.Text = HelpString.TranslationDescription;
        }

        private void checkBoxDarkFilter_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void checkBoxWidescreen_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void labelZoomH_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void labelZoomV_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void labelTranslationX_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void labelTranslationY_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        //********************************************************************************
        private void labelConfigFile_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.N64ConfigFile;
            labelHelpText.Text = HelpString.N64ConfigFileDescription;
        }

        private void textBoxConfigFile_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.N64ConfigFile;
            labelHelpText.Text = HelpString.N64ConfigFileDescription;
        }

        private void buttonConfigFile_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.ChooseN64ConfigFile;
            labelHelpText.Text = HelpString.ChooseN64ConfigFileDescription;
        }

        private void buttonEditConfigFile_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.EditN64ConfigFile;
            labelHelpText.Text = HelpString.EditN64ConfigFileDescription;
        }

        private void labelConfigFile_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void textBoxConfigFile_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void buttonConfigFile_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void buttonEditConfigFile_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        #endregion

        #region HelpImages

        private void buttonMenuIcon_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.Icon;
            labelHelpText.Text = HelpString.IconDescription;
        }

        private void buttonBootDrc_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.GamePad;
            labelHelpText.Text = HelpString.GamePadDescription;
        }

        private void buttonBootTv_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.TV;
            labelHelpText.Text = HelpString.TVDescription;
        }

        private void buttonTitleScreen_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.TitleScreen;
            labelHelpText.Text = HelpString.TitleScreenDescription;
        }

        private void buttonMenuIcon_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void buttonBootDrc_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void buttonBootTv_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void buttonTitleScreen_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }
        
        //********************************************************************************
        private void checkBoxKeepMenuIcon_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.KeepMenuIcon;
            labelHelpText.Text = HelpString.KeepMenuIconDescription;
        }

        private void checkBoxKeepBootTv_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.KeepImageTV;
            labelHelpText.Text = HelpString.KeepImageTVDescription;
        }

        private void checkBoxKeepBootDrc_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.KeepImageGamePad;
            labelHelpText.Text = HelpString.KeepImageGamePadDescription;
        }

        private void checkBoxKeepMenuIcon_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void checkBoxKeepBootTv_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void checkBoxKeepBootDrc_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        //********************************************************************************
        private void checkBoxUseNDSIcon_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.UseNDSIcon;
            labelHelpText.Text = HelpString.UseNDSIconDescription;
        }

        private void panelNDSIcon_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.NDSIconPreview;
            labelHelpText.Text = HelpString.NDSIconPreviewDescription;
        }

        private void buttonNDSIconBGColor_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.NDSIconBackground;
            labelHelpText.Text = HelpString.NDSIconBackgroundDescription;
        }

        private void checkBoxUseNDSIcon_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void panelNDSIcon_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void buttonNDSIconBGColor_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        //********************************************************************************
        private void checkBoxShowName_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.ShowName;
            labelHelpText.Text = HelpString.ShowNameDescription;
        }

        private void checkBoxReleaseDate_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.ReleaseDate;
            labelHelpText.Text = HelpString.ReleaseDateDescription;
        }

        private void labelShowPlayers_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.ShowPlayers;
            labelHelpText.Text = HelpString.ShowPlayersDescription;
        }

        private void comboBoxShowPlayers_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.ShowPlayers;
            labelHelpText.Text = HelpString.ShowPlayersDescription;
        }

        private void checkBoxShowName_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void checkBoxReleaseDate_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void labelShowPlayers_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void comboBoxShowPlayers_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        //********************************************************************************
        private void pictureBoxMenuIcon_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.IconPreview;
            labelHelpText.Text = HelpString.IconPreviewDescription;
        }

        private void pictureBoxBootDrc_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.GamePadPreview;
            labelHelpText.Text = HelpString.GamePadPreviewDescription;
        }

        private void pictureBoxBootTv_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.TVPreview;
            labelHelpText.Text = HelpString.TVPreviewDescription;
        }

        private void pictureBoxMenuIcon_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void pictureBoxBootDrc_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void pictureBoxBootTv_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        #endregion

        #region HelpInjecting

        private void buttonInjectPack_MouseEnter(object sender, EventArgs e)
        {
            if (checkBoxHelp.Checked)
            {
                groupBoxHelp.Text = HelpString.Pack;
                labelHelpText.Text = HelpString.PackDescription;
                if (textBoxCommonKey.Enabled)
                    labelHelpText.Text += "\n" + HelpString.InjectionWarningWiiUCK;
            }
        }

        private void buttonInjectNotPack_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.NotPack;
            labelHelpText.Text = HelpString.NotPackDescription;
        }

        private void panelPackingQuestion_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.Injecting;
            labelHelpText.Text = HelpString.EnableInjecting;
        }

        private void textBoxLog_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.Log;
            labelHelpText.Text = HelpString.LogDescription;
        }

        private void buttonInjectPack_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void buttonInjectNotPack_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void panelPackingQuestion_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void textBoxLog_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        #endregion

        #region HelpSettings

        private void labelCommonKey_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.WiiUCommonKey;
            labelHelpText.Text = HelpString.WiiUCommonKeyDescription;
        }

        private void textBoxCommonKey_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.WiiUCommonKey;
            labelHelpText.Text = HelpString.WiiUCommonKeyBoxDescription;
        }

        private void panelValidKey_MouseEnter(object sender, EventArgs e)
        {
            if (textBoxCommonKey.Enabled)
            {
                groupBoxHelp.Text = HelpString.WiiUCommonKeyError;
                labelHelpText.Text = HelpString.WiiUCommonKeyErrorDescription;
            }
            else
            {
                groupBoxHelp.Text = HelpString.WiiUCommonKeyChecked;
                labelHelpText.Text = HelpString.WiiUCommonKeyCheckedDescription;
            }
        }

        private void checkBoxHelp_MouseEnter(object sender, EventArgs e)
        {
            groupBoxHelp.Text = HelpString.HelpBox;
            labelHelpText.Text = HelpString.HelpDescription;
        }

        private void labelCommonKey_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void textBoxCommonKey_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void panelValidKey_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        private void checkBoxHelp_MouseLeave(object sender, EventArgs e)
        {
            groupBoxHelp.Text = "";
            labelHelpText.Text = "";
        }

        #endregion
    }
}

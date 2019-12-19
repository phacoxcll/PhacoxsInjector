using System;

namespace PhacoxsInjector
{
    public class VCNES : WiiUVC
    {
        public enum eType
        {
            A,
            B,
            Unknown
        }

        public readonly int ROMSize;
        public readonly eType Type;
        public readonly bool FDSROM;

        public VCNES(int index, uint hash, DateTime release, string title, eType type, int romSize, bool fdsROM)
            : base(index, hash, release, title)
        {
            ROMSize = romSize;
            Type = type;
            FDSROM = fdsROM;
        }

        public VCNES(uint hash)
            : base(hash)
        {
            ROMSize = -1;
            Type = eType.Unknown;
            FDSROM = false;
        }

        public override string ToString()
        {
            return "Hash: " + Hash.ToString("X8") + ", Release date: " + Release.ToString("yyyy/MM/dd") +
                ", Type: " + Type.ToString() + ", ROM size: " + Useful.ToFileSize(ROMSize) + ", FDS ROM: " + FDSROM.ToString() +
                " \nTitle: " + Title;
        }

        public static VCNES GetVC(uint hash)
        {
            switch (hash)
            {
                case 0xD22E3E4B: return VCNES.Title001;
                case 0xBD66A6E6: return VCNES.Title002;
                case 0xC6E63608: return VCNES.Title003;
                case 0xCEE027FA: return VCNES.Title004;
                case 0xBBDD226F: return VCNES.Title005;
                case 0xC46DD2CC: return VCNES.Title006;
                case 0xC16D27F9: return VCNES.Title007;
                case 0xC1EB92AB: return VCNES.Title008;
                case 0xC8449A0A: return VCNES.Title009;
                case 0xC7A7812F: return VCNES.Title010;
                case 0xC2E852BF: return VCNES.Title011;
                case 0xC86F2CE8: return VCNES.Title012;
                case 0xC7EBD352: return VCNES.Title013;
                case 0xCB3ABEDA: return VCNES.Title014;
                case 0xC00652A1: return VCNES.Title015;
                case 0xC43333DE: return VCNES.Title016;
                case 0xB13D573B: return VCNES.Title017;
                case 0xBDAD0834: return VCNES.Title018;
                case 0xD3B70E55: return VCNES.Title019;
                case 0xC44929DB: return VCNES.Title020;
                case 0xBFC52080: return VCNES.Title021;
                case 0xC6323D20: return VCNES.Title022;
                case 0xC69AE3A1: return VCNES.Title023;
                case 0xD67391AD: return VCNES.Title024;
                case 0xC6923E29: return VCNES.Title025;
                case 0xC8D63CD0: return VCNES.Title026;
                case 0xDBEA5DD8: return VCNES.Title027;
                case 0xC3B03C12: return VCNES.Title028;
                case 0xEB4101B3: return VCNES.Title029;
                case 0xD0EED5F1: return VCNES.Title030;
                case 0xD506D904: return VCNES.Title031;
                case 0xD673EA56: return VCNES.Title032;
                case 0xD6C06ED6: return VCNES.Title033;
                case 0xD8BC52C3: return VCNES.Title034;
                case 0xAD283E75: return VCNES.Title035;
                case 0xCB4A53E6: return VCNES.Title036;
                case 0xC3DBB693: return VCNES.Title037;
                case 0xA99C096F: return VCNES.Title038;
                case 0x9D4241AE: return VCNES.Title039;
                case 0xACABE902: return VCNES.Title040;
                case 0xA8D94E63: return VCNES.Title041;
                case 0xAEA271D0: return VCNES.Title042;
                case 0xA930A560: return VCNES.Title043;
                case 0xB87644F0: return VCNES.Title044;
                case 0xB169C8DC: return VCNES.Title045;
                case 0xA06FA68A: return VCNES.Title046;
                case 0xA2FB5D7A: return VCNES.Title047;
                case 0xA9875FA7: return VCNES.Title048;
                case 0xB50E938E: return VCNES.Title049;
                case 0xA973C2D9: return VCNES.Title050;
                case 0xA63C47DE: return VCNES.Title051;
                case 0xB02C79F1: return VCNES.Title052;
                case 0xD7FE8FF9: return VCNES.Title053;
                case 0xA7D18EE9: return VCNES.Title054;
                case 0xABD26A1A: return VCNES.Title055;
                case 0xAC4C2FD7: return VCNES.Title056;
                case 0x9C672378: return VCNES.Title057;
                case 0xC7D7BAD1: return VCNES.Title058;
                case 0xAD15A874: return VCNES.Title059;
                case 0xB2DE4C65: return VCNES.Title060;
                case 0xA6284BE5: return VCNES.Title061;
                case 0xA205BF06: return VCNES.Title062;
                case 0xB4D32813: return VCNES.Title063;
                case 0xB0889319: return VCNES.Title064;
                case 0xA09F8389: return VCNES.Title065;
                case 0xADA1A2CA: return VCNES.Title066;
                case 0xAD3F8697: return VCNES.Title067;
                case 0xB196D48E: return VCNES.Title068;
                case 0x91B6CDEA: return VCNES.Title069;
                case 0xAB3D7B54: return VCNES.Title070;
                case 0xACA51E7D: return VCNES.Title071;
                case 0xB21DDC69: return VCNES.Title072;
                case 0xA8F1FDA7: return VCNES.Title073;
                case 0xA579D44E: return VCNES.Title074;
                case 0xA55AE283: return VCNES.Title075;
                case 0xAE1F7572: return VCNES.Title076;
                case 0x9B33C298: return VCNES.Title077;
                case 0x97FF4164: return VCNES.Title078;
                case 0xB16A49F5: return VCNES.Title079;
                case 0xA849E75F: return VCNES.Title080;
                case 0xA90F75B3: return VCNES.Title081;
                case 0xA8D4B240: return VCNES.Title082;
                case 0xABC75E17: return VCNES.Title083;
                case 0xC5ABBF89: return VCNES.Title084;
                case 0xAAF1485F: return VCNES.Title085;
                case 0xAADBF5F8: return VCNES.Title086;
                case 0x9DE19F58: return VCNES.Title087;
                case 0x9CE222A4: return VCNES.Title088;
                case 0xB1F617D8: return VCNES.Title089;
                case 0x9C9A28FD: return VCNES.Title090;
                case 0xA4366119: return VCNES.Title091;
                case 0xBE937447: return VCNES.Title092;
                case 0xA81596EE: return VCNES.Title093;
                case 0xB1D6FBEC: return VCNES.Title094;
                case 0x9FB62C7E: return VCNES.Title095;
                case 0xA4A0A5FB: return VCNES.Title096;
                case 0xA233B7D2: return VCNES.Title097;
                case 0x9FF9800F: return VCNES.Title098;
                case 0x97C6FDAA: return VCNES.Title099;
                case 0xA41C82BC: return VCNES.Title100;
                case 0xAD093596: return VCNES.Title101;
                case 0x945EFBF5: return VCNES.Title102;
                case 0x9A0D94B9: return VCNES.Title103;
                case 0x9974F949: return VCNES.Title104;
                case 0xA9F85FF3: return VCNES.Title105;
                case 0xAC7C485F: return VCNES.Title106;
                case 0xB9F03109: return VCNES.Title107;
                case 0xAC9AA0DD: return VCNES.Title108;
                case 0xA1523F6B: return VCNES.Title109;
                case 0x960780B2: return VCNES.Title110;
                case 0x9E14EFE9: return VCNES.Title111;
                case 0x9BEBD515: return VCNES.Title112;
                case 0xA8ABF2CF: return VCNES.Title113;
                case 0xBC300EBA: return VCNES.Title114;
                case 0xA0375E86: return VCNES.Title115;
                case 0xA3473EAB: return VCNES.Title116;
                case 0xA647D809: return VCNES.Title117;
                case 0x9B159C4E: return VCNES.Title118;
                case 0x88EF2E3F: return VCNES.Title119;
                case 0x8C1A183D: return VCNES.Title120;
                case 0xAE998892: return VCNES.Title121;
                case 0x94843589: return VCNES.Title122;
                case 0x850404AA: return VCNES.Title123;
                case 0x9C6CAC2E: return VCNES.Title124;
                case 0xA9274531: return VCNES.Title125;
                case 0x8D9BAF5E: return VCNES.Title126;
                case 0xA3C4068A: return VCNES.Title127;
                case 0xB1D3000C: return VCNES.Title128;
                case 0xA1A526AF: return VCNES.Title129;
                case 0x9374FFF1: return VCNES.Title130;
                case 0x9E7691BC: return VCNES.Title131;
                case 0xABD64C76: return VCNES.Title132;
                case 0x9C0929D9: return VCNES.Title133;
                case 0xD87863A4: return VCNES.Title134;
                case 0xE0ACEA0B: return VCNES.Title135;
                case 0x90365DEC: return VCNES.Title136;
                case 0xBA20089D: return VCNES.Title137;
                case 0x9AC0604F: return VCNES.Title138;
                case 0xA1A0B268: return VCNES.Title139;
                case 0xA5526B7C: return VCNES.Title140;
                case 0xC0ABBB9D: return VCNES.Title141;
                case 0xC6ABAE84: return VCNES.Title142;
                case 0x8DD20506: return VCNES.Title143;
                case 0xAF221323: return VCNES.Title144;
                case 0xBB5F4B15: return VCNES.Title145;
                case 0xC5611AF3: return VCNES.Title146;
                case 0xBFD17451: return VCNES.Title147;
                case 0xB2E8BE31: return VCNES.Title148;
                case 0xB618AE6C: return VCNES.Title149;
                case 0xA720F352: return VCNES.Title150;
                case 0xB8936232: return VCNES.Title151;
                case 0x932F3774: return VCNES.Title152;
                case 0xBC173F3C: return VCNES.Title153;
                case 0xAE16FE0A: return VCNES.Title154;
                case 0xAC335C0E: return VCNES.Title155;
                case 0xBC24C474: return VCNES.Title156;
                case 0xA474ADD5: return VCNES.Title157;
                case 0xC0D1E406: return VCNES.Title158;
                case 0xC72B13A7: return VCNES.Title159;
                case 0xAA4C253D: return VCNES.Title160;
                case 0xCCEDDB59: return VCNES.Title161;
                case 0xB28A6A5C: return VCNES.Title162;
                case 0xBFC6A68B: return VCNES.Title163;
                case 0xA6F10177: return VCNES.Title164;
                case 0xB5F514E7: return VCNES.Title165;
                case 0x984FE47A: return VCNES.Title166;
                case 0xB936BEC1: return VCNES.Title167;
                case 0xB2933F7C: return VCNES.Title168;
                case 0xC3F82A6C: return VCNES.Title169;
                default: return null;
            }
        }

        /// <summary>Balloon Fight (USA/JPN)</summary>
        public static readonly VCNES Title001 = new VCNES(001, 0xD22E3E4B, new DateTime(2013, 01, 23), "Balloon Fight (USA/JPN)", eType.A, 24576, false);
        /// <summary>Punch-Out!! Featuring Mr. Dream (USA/EUR/JPN)</summary>
        public static readonly VCNES Title002 = new VCNES(002, 0xBD66A6E6, new DateTime(2013, 03, 20), "Punch-Out!! Featuring Mr. Dream (USA/EUR/JPN)", eType.A, 262144, false);
        /// <summary>Kirby's Adventure (JPN)</summary>
        public static readonly VCNES Title003 = new VCNES(003, 0xC6E63608, new DateTime(2013, 04, 17), "Kirby's Adventure (JPN)", eType.A, 786432, false);
        /// <summary>Kirby's Adventure (USA/EUR)</summary>
        public static readonly VCNES Title004 = new VCNES(004, 0xCEE027FA, new DateTime(2013, 04, 17), "Kirby's Adventure (USA/EUR)", eType.A, 786432, false);
        /// <summary>Donkey Kong (USA/EUR/JPN)/Mario Bros. (USA/EUR/JPN)/Pac-Man (USA/EUR/JPN)/Ice Climber (USA/EUR/JPN)</summary>
        public static readonly VCNES Title005 = new VCNES(005, 0xBBDD226F, new DateTime(2013, 04, 26), "Donkey Kong (USA/EUR/JPN)/Mario Bros. (USA/EUR/JPN)/Pac-Man (USA/EUR/JPN)/Ice Climber (USA/EUR/JPN)", eType.A, 24576, false);
        /// <summary>Excitebike (USA/EUR)</summary>
        public static readonly VCNES Title006 = new VCNES(006, 0xC46DD2CC, new DateTime(2013, 04, 26), "Excitebike (USA/EUR)", eType.A, 40960, false);
        /// <summary>Donkey Kong Jr. (USA/EUR/JPN)</summary>
        public static readonly VCNES Title007 = new VCNES(007, 0xC16D27F9, new DateTime(2013, 04, 26), "Donkey Kong Jr. (USA/EUR/JPN)", eType.A, 24576, false);
        /// <summary>Super Mario Bros. 2 (USA)/Downtown Nekketsu Kōshinkyoku: Soreyuke Daiundōkai (JPN)</summary>
        public static readonly VCNES Title008 = new VCNES(008, 0xC1EB92AB, new DateTime(2013, 04, 27), "Super Mario Bros. 2 (USA)/Downtown Nekketsu Kōshinkyoku: Soreyuke Daiundōkai (JPN)", eType.A, 262144, false);
        /// <summary>Excitebike (JPN)</summary>
        public static readonly VCNES Title009 = new VCNES(009, 0xC8449A0A, new DateTime(2013, 04, 27), "Excitebike (JPN)", eType.A, 40960, false);
        /// <summary>Super Mario Bros. (USA/EUR/JPN)/Spelunker (USA/EUR/JPN)/Lode Runner (USA/EUR/JPN)/Xevious (USA/EUR/JPN)</summary>
        public static readonly VCNES Title010 = new VCNES(010, 0xC7A7812F, new DateTime(2013, 04, 27), "Super Mario Bros. (USA/EUR/JPN)/Spelunker (USA/EUR/JPN)/Lode Runner (USA/EUR/JPN)/Xevious (USA/EUR/JPN)", eType.A, 40960, false);
        /// <summary>Solomon's Key (USA/EUR/JPN)</summary>
        public static readonly VCNES Title011 = new VCNES(011, 0xC2E852BF, new DateTime(2013, 05, 01), "Solomon's Key (USA/EUR/JPN)", eType.A, 65536, false);
        /// <summary>Mega Man 3 (USA/EUR/JPN)</summary>
        public static readonly VCNES Title012 = new VCNES(012, 0xC86F2CE8, new DateTime(2013, 05, 01), "Mega Man 3 (USA/EUR/JPN)", eType.A, 393216, false);
        /// <summary>Mega Man (USA/EUR/JPN)</summary>
        public static readonly VCNES Title013 = new VCNES(013, 0xC7EBD352, new DateTime(2013, 05, 02), "Mega Man (USA/EUR/JPN)", eType.A, 131072, false);
        /// <summary>Mappy (JPN)</summary>
        public static readonly VCNES Title014 = new VCNES(014, 0xCB3ABEDA, new DateTime(2013, 05, 15), "Mappy (JPN)", eType.A, 24576, false);
        /// <summary>Super Mario Bros. 2 (EUR)/Super Mario Bros. USA (JPN)</summary>
        public static readonly VCNES Title015 = new VCNES(015, 0xC00652A1, new DateTime(2013, 05, 16), "Super Mario Bros. 2 (EUR)/Super Mario Bros. USA (JPN)", eType.A, 262144, false);
        /// <summary>Galaga (JPN)/Ikki (JPN)</summary>
        public static readonly VCNES Title016 = new VCNES(016, 0xC43333DE, new DateTime(2013, 05, 22), "Galaga (JPN)/Ikki (JPN)", eType.A, 24576, false);
        /// <summary>Ghosts 'n Goblins (USA/EUR/JPN)</summary>
        public static readonly VCNES Title017 = new VCNES(017, 0xB13D573B, new DateTime(2013, 05, 30), "Ghosts 'n Goblins (USA/EUR/JPN)", eType.A, 131072, false);
        /// <summary>Mega Man 4 (USA/EUR)</summary>
        public static readonly VCNES Title018 = new VCNES(018, 0xBDAD0834, new DateTime(2013, 06, 11), "Mega Man 4 (USA/EUR)", eType.A, 524288, false);
        /// <summary>Mega Man 2 (USA/EUR/JPN)</summary>
        public static readonly VCNES Title019 = new VCNES(019, 0xD3B70E55, new DateTime(2013, 06, 11), "Mega Man 2 (USA/EUR/JPN)", eType.A, 262144, false);
        /// <summary>Mario and Yoshi (EUR)/Yoshi no Tamago (JPN)</summary>
        public static readonly VCNES Title020 = new VCNES(020, 0xC44929DB, new DateTime(2013, 06, 12), "Mario and Yoshi (EUR)/Yoshi no Tamago (JPN)", eType.A, 163840, false);
        /// <summary>Yoshi (USA)</summary>
        public static readonly VCNES Title021 = new VCNES(021, 0xBFC52080, new DateTime(2013, 06, 12), "Yoshi (USA)", eType.A, 163840, false);
        /// <summary>Mega Man 4 (JPN)</summary>
        public static readonly VCNES Title022 = new VCNES(022, 0xC6323D20, new DateTime(2013, 06, 12), "Mega Man 4 (JPN)", eType.A, 524288, false);
        /// <summary>Wrecking Crew (USA/EUR/JPN)/The Tower of Druaga (JPN)</summary>
        public static readonly VCNES Title023 = new VCNES(023, 0xC69AE3A1, new DateTime(2013, 06, 19), "Wrecking Crew (USA/EUR/JPN)/The Tower of Druaga (JPN)", eType.A, 40960, false);
        /// <summary>TwinBee (JPN)/Ninja JaJaMaru-kun (JPN)</summary>
        public static readonly VCNES Title024 = new VCNES(024, 0xD67391AD, new DateTime(2013, 06, 19), "TwinBee (JPN)/Ninja JaJaMaru-kun (JPN)", eType.A, 49152, false);
        /// <summary>Balloon Fight (EUR)</summary>
        public static readonly VCNES Title025 = new VCNES(025, 0xC6923E29, new DateTime(2013, 06, 27), "Balloon Fight (EUR)", eType.A, 24576, false);
        /// <summary>The Legend of Zelda (USA/EUR)/Metroid (USA/EUR)</summary>
        public static readonly VCNES Title026 = new VCNES(026, 0xC8D63CD0, new DateTime(2013, 07, 11), "The Legend of Zelda (USA/EUR)/Metroid (USA/EUR)", eType.A, 131072, false);
        /// <summary>Kid Icarus (USA/EUR)</summary>
        public static readonly VCNES Title027 = new VCNES(027, 0xDBEA5DD8, new DateTime(2013, 07, 11), "Kid Icarus (USA/EUR)", eType.A, 131072, false);
        /// <summary>Galaga (USA/EUR)</summary>
        public static readonly VCNES Title028 = new VCNES(028, 0xC3B03C12, new DateTime(2013, 08, 08), "Galaga (USA/EUR)", eType.A, 40960, false);
        /// <summary>Super Mario Bros. 2 (JPN)</summary>
        public static readonly VCNES Title029 = new VCNES(029, 0xEB4101B3, new DateTime(2013, 08, 08), "Super Mario Bros. 2 (JPN)", eType.A, 65536, true);
        /// <summary>The Legend of Zelda (JPN)/Kid Icarus (JPN)</summary>
        public static readonly VCNES Title030 = new VCNES(030, 0xD0EED5F1, new DateTime(2013, 08, 14), "The Legend of Zelda (JPN)/Kid Icarus (JPN)", eType.A, 131072, true);
        /// <summary>Metroid (JPN)</summary>
        public static readonly VCNES Title031 = new VCNES(031, 0xD506D904, new DateTime(2013, 08, 14), "Metroid (JPN)", eType.A, 131072, true);
        /// <summary>Zelda II: The Adventure of Link (JPN)</summary>
        public static readonly VCNES Title032 = new VCNES(032, 0xD673EA56, new DateTime(2013, 09, 11), "Zelda II: The Adventure of Link (JPN)", eType.A, 131072, true);
        /// <summary>Zelda II: The Adventure of Link (USA/EUR)</summary>
        public static readonly VCNES Title033 = new VCNES(033, 0xD6C06ED6, new DateTime(2013, 09, 12), "Zelda II: The Adventure of Link (USA/EUR)", eType.A, 262144, false);
        /// <summary>Shin Onigashima (JPN)</summary>
        public static readonly VCNES Title034 = new VCNES(034, 0xD8BC52C3, new DateTime(2013, 09, 18), "Shin Onigashima (JPN)", eType.A, 262144, true);
        /// <summary>Baseball (EUR)/Tennis (EUR)/Golf (EUR)/Urban Champion (EUR)/Clu Clu Land (EUR)/Donkey Kong 3 (USA/EUR)</summary>
        public static readonly VCNES Title035 = new VCNES(035, 0xAD283E75, new DateTime(2013, 09, 26), "Baseball (EUR)/Tennis (EUR)/Golf (EUR)/Urban Champion (EUR)/Clu Clu Land (EUR)/Donkey Kong 3 (USA/EUR)", eType.B, 24576, false);
        /// <summary>Gradius (USA/EUR)</summary>
        public static readonly VCNES Title036 = new VCNES(036, 0xCB4A53E6, new DateTime(2013, 09, 26), "Gradius (USA/EUR)", eType.A, 65536, false);
        /// <summary>Gradius (JPN)</summary>
        public static readonly VCNES Title037 = new VCNES(037, 0xC3DBB693, new DateTime(2013, 10, 02), "Gradius (JPN)", eType.A, 65536, false);
        /// <summary>Baseball (USA)/Tennis (USA)/Golf (USA)/Urban Champion (USA)/Clu Clu Land (USA)/Pinball (USA)</summary>
        public static readonly VCNES Title038 = new VCNES(038, 0xA99C096F, new DateTime(2013, 10, 10), "Baseball (USA)/Tennis (USA)/Golf (USA)/Urban Champion (USA)/Clu Clu Land (USA)/Pinball (USA)", eType.B, 24576, false);
        /// <summary>Urban Champion (JPN)</summary>
        public static readonly VCNES Title039 = new VCNES(039, 0x9D4241AE, new DateTime(2013, 10, 23), "Urban Champion (JPN)", eType.B, 24576, false);
        /// <summary>Baseball (JPN)/Tennis (JPN)/Pinball (JPN)/Donkey Kong 3 (JPN)</summary>
        public static readonly VCNES Title040 = new VCNES(040, 0xACABE902, new DateTime(2013, 10, 23), "Baseball (JPN)/Tennis (JPN)/Pinball (JPN)/Donkey Kong 3 (JPN)", eType.B, 24576, false);
        /// <summary>Pinball (EUR)</summary>
        public static readonly VCNES Title041 = new VCNES(041, 0xA8D94E63, new DateTime(2013, 10, 24), "Pinball (EUR)", eType.B, 24576, false);
        /// <summary>Wario's Woods (USA/EUR)/Final Fantasy 3 (JPN)</summary>
        public static readonly VCNES Title042 = new VCNES(042, 0xAEA271D0, new DateTime(2013, 11, 07), "Wario's Woods (USA/EUR)/Final Fantasy 3 (JPN)", eType.B, 524288, false);
        /// <summary>Final Fantasy (JPN)</summary>
        public static readonly VCNES Title043 = new VCNES(043, 0xA930A560, new DateTime(2013, 11, 13), "Final Fantasy (JPN)", eType.B, 262144, false);
        /// <summary>Golf (JPN)</summary>
        public static readonly VCNES Title044 = new VCNES(044, 0xB87644F0, new DateTime(2013, 11, 13), "Golf (JPN)", eType.B, 24576, false);
        /// <summary>Clu Clu Land (JPN)</summary>
        public static readonly VCNES Title045 = new VCNES(045, 0xB169C8DC, new DateTime(2013, 11, 20), "Clu Clu Land (JPN)", eType.B, 65536, true);
        /// <summary>Castlevania (JPN)</summary>
        public static readonly VCNES Title046 = new VCNES(046, 0xA06FA68A, new DateTime(2013, 12, 04), "Castlevania (JPN)", eType.B, 131072, true);
        /// <summary>Downtown Special: Kunio-kun no Jidaigeki da yo Zen'in Shūgō! (JPN)</summary>
        public static readonly VCNES Title047 = new VCNES(047, 0xA2FB5D7A, new DateTime(2013, 12, 04), "Downtown Special: Kunio-kun no Jidaigeki da yo Zen'in Shūgō! (JPN)", eType.B, 262144, false);
        /// <summary>Super Dodge Ball (USA)/Final Fantasy 2 (JPN)</summary>
        public static readonly VCNES Title048 = new VCNES(048, 0xA9875FA7, new DateTime(2013, 12, 11), "Super Dodge Ball (USA)/Final Fantasy 2 (JPN)", eType.B, 262144, false);
        /// <summary>Ice Hockey (JPN)</summary>
        public static readonly VCNES Title049 = new VCNES(049, 0xB50E938E, new DateTime(2013, 12, 11), "Ice Hockey (JPN)", eType.B, 65536, true);
        /// <summary>Double Dragon (USA/EUR/JPN)</summary>
        public static readonly VCNES Title050 = new VCNES(050, 0xA973C2D9, new DateTime(2013, 12, 12), "Double Dragon (USA/EUR/JPN)", eType.B, 262144, false);
        /// <summary>Super Dodge Ball (JPN)</summary>
        public static readonly VCNES Title051 = new VCNES(051, 0xA63C47DE, new DateTime(2013, 12, 18), "Super Dodge Ball (JPN)", eType.B, 262144, false);
        /// <summary>Castlevania (USA/EUR)</summary>
        public static readonly VCNES Title052 = new VCNES(052, 0xB02C79F1, new DateTime(2013, 12, 19), "Castlevania (USA/EUR)", eType.B, 131072, false);
        /// <summary>Super Mario Bros. 3 (USA/EUR/JPN)</summary>
        public static readonly VCNES Title053 = new VCNES(053, 0xD7FE8FF9, new DateTime(2013, 12, 25), "Super Mario Bros. 3 (USA/EUR/JPN)", eType.A, 393216, false);
        /// <summary>Renegade (JPN)</summary>
        public static readonly VCNES Title054 = new VCNES(054, 0xA7D18EE9, new DateTime(2014, 01, 15), "Renegade (JPN)", eType.B, 131072, false);
        /// <summary>NES Open Tournament Golf (USA/JPN)</summary>
        public static readonly VCNES Title055 = new VCNES(055, 0xABD26A1A, new DateTime(2014, 01, 15), "NES Open Tournament Golf (USA/JPN)", eType.B, 262144, false);
        /// <summary>Castlevania II: Simon's Quest (USA/EUR)</summary>
        public static readonly VCNES Title056 = new VCNES(056, 0xAC4C2FD7, new DateTime(2014, 01, 16), "Castlevania II: Simon's Quest (USA/EUR)", eType.B, 262144, false);
        /// <summary>Mighty Bomb Jack (USA)</summary>
        public static readonly VCNES Title057 = new VCNES(057, 0x9C672378, new DateTime(2014, 01, 23), "Mighty Bomb Jack (USA)", eType.B, 65536, false);
        /// <summary>Super Mario Bros.: The Lost Levels (USA/EUR)</summary>
        public static readonly VCNES Title058 = new VCNES(058, 0xC7D7BAD1, new DateTime(2014, 01, 23), "Super Mario Bros.: The Lost Levels (USA/EUR)", eType.A, 65536, true);
        /// <summary>Wario's Woods (JPN)</summary>
        public static readonly VCNES Title059 = new VCNES(059, 0xAD15A874, new DateTime(2014, 01, 29), "Wario's Woods (JPN)", eType.B, 524288, false);
        /// <summary>Bubble Bobble (JPN)</summary>
        public static readonly VCNES Title060 = new VCNES(060, 0xB2DE4C65, new DateTime(2014, 01, 29), "Bubble Bobble (JPN)", eType.B, 131072, true);
        /// <summary>Mighty Bomb Jack (JPN)</summary>
        public static readonly VCNES Title061 = new VCNES(061, 0xA6284BE5, new DateTime(2014, 02, 05), "Mighty Bomb Jack (JPN)", eType.B, 49152, false);
        /// <summary>Ninja Gaiden (USA/JPN)/Super C (USA/JPN)/Ganbare Goemon! Karakuri Dōchū (JPN)</summary>
        public static readonly VCNES Title062 = new VCNES(062, 0xA205BF06, new DateTime(2014, 02, 06), "Ninja Gaiden (USA/JPN)/Super C (USA/JPN)/Ganbare Goemon! Karakuri Dōchū (JPN)", eType.B, 262144, false);
        /// <summary>NES Open Tournament Golf (EUR)</summary>
        public static readonly VCNES Title063 = new VCNES(063, 0xB4D32813, new DateTime(2014, 02, 06), "NES Open Tournament Golf (EUR)", eType.B, 262144, false);
        /// <summary>Dr. Mario (EUR)</summary>
        public static readonly VCNES Title064 = new VCNES(064, 0xB0889319, new DateTime(2014, 02, 13), "Dr. Mario (EUR)", eType.B, 65536, false);
        /// <summary>Elevator Action (JPN)</summary>
        public static readonly VCNES Title065 = new VCNES(065, 0xA09F8389, new DateTime(2014, 02, 19), "Elevator Action (JPN)", eType.B, 40960, false);
        /// <summary>Ice Hockey (USA/EUR)</summary>
        public static readonly VCNES Title066 = new VCNES(066, 0xADA1A2CA, new DateTime(2014, 02, 20), "Ice Hockey (USA/EUR)", eType.B, 40960, false);
        /// <summary>Dr. Mario (USA/JPN)</summary>
        public static readonly VCNES Title067 = new VCNES(067, 0xAD3F8697, new DateTime(2014, 02, 26), "Dr. Mario (USA/JPN)", eType.B, 65536, false);
        /// <summary>Renegade (USA)</summary>
        public static readonly VCNES Title068 = new VCNES(068, 0xB196D48E, new DateTime(2014, 02, 27), "Renegade (USA)", eType.B, 131072, false);
        /// <summary>Castlevania II: Simon's Quest (JPN)</summary>
        public static readonly VCNES Title069 = new VCNES(069, 0x91B6CDEA, new DateTime(2014, 03, 05), "Castlevania II: Simon's Quest (JPN)", eType.B, 131072, true);
        /// <summary>Renegade (EUR)</summary>
        public static readonly VCNES Title070 = new VCNES(070, 0xAB3D7B54, new DateTime(2014, 03, 06), "Renegade (EUR)", eType.B, 131072, false);
        /// <summary>Volleyball (JPN)</summary>
        public static readonly VCNES Title071 = new VCNES(071, 0xACA51E7D, new DateTime(2014, 03, 12), "Volleyball (JPN)", eType.B, 65536, true);
        /// <summary>Super Dodge Ball (EUR)</summary>
        public static readonly VCNES Title072 = new VCNES(072, 0xB21DDC69, new DateTime(2014, 03, 13), "Super Dodge Ball (EUR)", eType.B, 262144, false);
        /// <summary>Nintendo World Cup (JPN)</summary>
        public static readonly VCNES Title073 = new VCNES(073, 0xA8F1FDA7, new DateTime(2014, 03, 19), "Nintendo World Cup (JPN)", eType.B, 262144, false);
        /// <summary>Volleyball (USA/EUR)</summary>
        public static readonly VCNES Title074 = new VCNES(074, 0xA579D44E, new DateTime(2014, 03, 20), "Volleyball (USA/EUR)", eType.B, 40960, false);
        /// <summary>Mighty Bomb Jack (EUR)</summary>
        public static readonly VCNES Title075 = new VCNES(075, 0xA55AE283, new DateTime(2014, 03, 27), "Mighty Bomb Jack (EUR)", eType.B, 65536, false);
        /// <summary>Ninja Gaiden (EUR)/Super C (EUR)</summary>
        public static readonly VCNES Title076 = new VCNES(076, 0xAE1F7572, new DateTime(2014, 03, 27), "Ninja Gaiden (EUR)/Super C (EUR)", eType.B, 262144, false);
        /// <summary>Hanjuku Hero (JPN)</summary>
        public static readonly VCNES Title077 = new VCNES(077, 0x9B33C298, new DateTime(2014, 04, 09), "Hanjuku Hero (JPN)", eType.B, 131072, false);
        /// <summary>Castlevania III: Dracula's Curse (EUR/JPN)/Fire Emblem: Ankoku Ryū to Hikari no Tsurugi (JPN)</summary>
        public static readonly VCNES Title078 = new VCNES(078, 0x97FF4164, new DateTime(2014, 04, 16), "Castlevania III: Dracula's Curse (EUR/JPN)/Fire Emblem: Ankoku Ryū to Hikari no Tsurugi (JPN)", eType.B, 393216, false);
        /// <summary>Mega Man 5 (USA/EUR/JPN)/Joy Mech Fight (JPN)</summary>
        public static readonly VCNES Title079 = new VCNES(079, 0xB16A49F5, new DateTime(2014, 04, 23), "Mega Man 5 (USA/EUR/JPN)/Joy Mech Fight (JPN)", eType.B, 524288, false);
        /// <summary>Soccer (USA/EUR/JPN)/Mach Rider (USA/EUR/JPN)</summary>
        public static readonly VCNES Title080 = new VCNES(080, 0xA849E75F, new DateTime(2014, 05, 01), "Soccer (USA/EUR/JPN)/Mach Rider (USA/EUR/JPN)", eType.B, 40960, false);
        /// <summary>Wagan Land (JPN)</summary>
        public static readonly VCNES Title081 = new VCNES(081, 0xA90F75B3, new DateTime(2014, 05, 07), "Wagan Land (JPN)", eType.B, 196608, false);
        /// <summary>Mega Man 6 (USA/EUR/JPN)</summary>
        public static readonly VCNES Title082 = new VCNES(082, 0xA8D4B240, new DateTime(2014, 05, 14), "Mega Man 6 (USA/EUR/JPN)", eType.B, 524288, false);
        /// <summary>Princess Tomato in the Salad Kingdom (JPN)/Ike Ike! Nekketsu Hockey-bu: Subete Koronde Dairantou (JPN)</summary>
        public static readonly VCNES Title083 = new VCNES(083, 0xABC75E17, new DateTime(2014, 05, 14), "Princess Tomato in the Salad Kingdom (JPN)/Ike Ike! Nekketsu Hockey-bu: Subete Koronde Dairantou (JPN)", eType.B, 262144, false);
        /// <summary>Adventures of Lolo (USA/EUR/JPN)</summary>
        public static readonly VCNES Title084 = new VCNES(084, 0xC5ABBF89, new DateTime(2014, 05, 15), "Adventures of Lolo (USA/EUR/JPN)", eType.B, 65536, false);
        /// <summary>Gargoyle's Quest II (USA/EUR/JPN)</summary>
        public static readonly VCNES Title085 = new VCNES(085, 0xAAF1485F, new DateTime(2014, 05, 21), "Gargoyle's Quest II (USA/EUR/JPN)", eType.B, 262144, false);
        /// <summary>Famicom Tantei Club: Kieta Kōkeisha (JPN)</summary>
        public static readonly VCNES Title086 = new VCNES(086, 0xAADBF5F8, new DateTime(2014, 05, 28), "Famicom Tantei Club: Kieta Kōkeisha (JPN)", eType.B, 262144, true);
        /// <summary>The Legend of Kage (JPN)</summary>
        public static readonly VCNES Title087 = new VCNES(087, 0x9DE19F58, new DateTime(2014, 06, 04), "The Legend of Kage (JPN)", eType.B, 49152, false);
        /// <summary>Pac-Land (USA/EUR/JPN)</summary>
        public static readonly VCNES Title088 = new VCNES(088, 0x9CE222A4, new DateTime(2014, 06, 10), "Pac-Land (USA/EUR/JPN)", eType.B, 40960, false);
        /// <summary>Antarctic Adventure (JPN)</summary>
        public static readonly VCNES Title089 = new VCNES(089, 0xB1F617D8, new DateTime(2014, 06, 19), "Antarctic Adventure (JPN)", eType.B, 24576, false);
        /// <summary>Castlevania III: Dracula's Curse (USA)</summary>
        public static readonly VCNES Title090 = new VCNES(090, 0x9C9A28FD, new DateTime(2014, 06, 26), "Castlevania III: Dracula's Curse (USA)", eType.B, 393216, false);
        /// <summary>Ufouria: The Saga (USA/JPN)/Double Dragon II: The Revenge (USA/EUR/JPN)/Mendel Palace (JPN)</summary>
        public static readonly VCNES Title091 = new VCNES(091, 0xA4366119, new DateTime(2014, 07, 02), "Ufouria: The Saga (USA/JPN)/Double Dragon II: The Revenge (USA/EUR/JPN)/Mendel Palace (JPN)", eType.B, 262144, false);
        /// <summary>Adventure Island (USA/EUR/JPN)</summary>
        public static readonly VCNES Title092 = new VCNES(092, 0xBE937447, new DateTime(2014, 07, 03), "Adventure Island (USA/EUR/JPN)", eType.A, 65536, false);
        /// <summary>Battle City (JPN)</summary>
        public static readonly VCNES Title093 = new VCNES(093, 0xA81596EE, new DateTime(2014, 07, 09), "Battle City (JPN)", eType.B, 24576, false);
        /// <summary>Bases Loaded (USA)</summary>
        public static readonly VCNES Title094 = new VCNES(094, 0xB1D6FBEC, new DateTime(2014, 07, 10), "Bases Loaded (USA)", eType.B, 327680, false);
        /// <summary>Kung-Fu Heroes (JPN)</summary>
        public static readonly VCNES Title095 = new VCNES(095, 0x9FB62C7E, new DateTime(2014, 07, 23), "Kung-Fu Heroes (JPN)", eType.B, 49152, false);
        /// <summary>The Mysterious Murasame Castle (JPN)</summary>
        public static readonly VCNES Title096 = new VCNES(096, 0xA4A0A5FB, new DateTime(2014, 07, 30), "The Mysterious Murasame Castle (JPN)", eType.B, 131072, true);
        /// <summary>Fire Emblem Gaiden (JPN)</summary>
        public static readonly VCNES Title097 = new VCNES(097, 0xA233B7D2, new DateTime(2014, 08, 20), "Fire Emblem Gaiden (JPN)", eType.B, 393216, false);
        /// <summary>Life Force (USA)/Salamander (JPN)</summary>
        public static readonly VCNES Title098 = new VCNES(098, 0x9FF9800F, new DateTime(2014, 08, 21), "Life Force (USA)/Salamander (JPN)", eType.B, 131072, false);
        /// <summary>Dig Dug (EUR/JPN)/Donkey Kong Jr. Math (USA/EUR/JPN)/Nuts and Milk (JPN)</summary>
        public static readonly VCNES Title099 = new VCNES(099, 0x97C6FDAA, new DateTime(2014, 08, 28), "Dig Dug (EUR/JPN)/Donkey Kong Jr. Math (USA/EUR/JPN)/Nuts and Milk (JPN)", eType.B, 24576, false);
        /// <summary>Flying Dragon: The Secret Scroll (USA/EUR/JPN)</summary>
        public static readonly VCNES Title100 = new VCNES(100, 0xA41C82BC, new DateTime(2014, 09, 10), "Flying Dragon: The Secret Scroll (USA/EUR/JPN)", eType.B, 131072, false);
        /// <summary>Yie Ar Kung-Fu (JPN)</summary>
        public static readonly VCNES Title101 = new VCNES(101, 0xAD093596, new DateTime(2014, 09, 17), "Yie Ar Kung-Fu (JPN)", eType.B, 24576, false);
        /// <summary>Life Force (EUR)</summary>
        public static readonly VCNES Title102 = new VCNES(102, 0x945EFBF5, new DateTime(2014, 09, 18), "Life Force (EUR)", eType.B, 131072, false);
        /// <summary>Tsuppari Ōzumō (JPN)</summary>
        public static readonly VCNES Title103 = new VCNES(103, 0x9A0D94B9, new DateTime(2014, 10, 08), "Tsuppari Ōzumō (JPN)", eType.B, 65536, false);
        /// <summary>Ufouria: The Saga (EUR)</summary>
        public static readonly VCNES Title104 = new VCNES(104, 0x9974F949, new DateTime(2014, 10, 09), "Ufouria: The Saga (EUR)", eType.B, 262144, false);
        /// <summary>Mighty Final Fight (USA/EUR/JPN)/Street Fighter 2010: The Final Fight (USA/EUR/JPN)</summary>
        public static readonly VCNES Title105 = new VCNES(105, 0xA9F85FF3, new DateTime(2014, 10, 15), "Mighty Final Fight (USA/EUR/JPN)/Street Fighter 2010: The Final Fight (USA/EUR/JPN)", eType.B, 262144, false);
        /// <summary>Bases Loaded (JPN)</summary>
        public static readonly VCNES Title106 = new VCNES(106, 0xAC7C485F, new DateTime(2014, 10, 22), "Bases Loaded (JPN)", eType.B, 196608, false);
        /// <summary>Tower of Babel (JPN)</summary>
        public static readonly VCNES Title107 = new VCNES(107, 0xB9F03109, new DateTime(2014, 10, 22), "Tower of Babel (JPN)", eType.B, 65536, false);
        /// <summary>Dig Dug (USA)/Gomoku Narabe Renju (JPN)</summary>
        public static readonly VCNES Title108 = new VCNES(108, 0xAC9AA0DD, new DateTime(2014, 10, 29), "Dig Dug (USA)/Gomoku Narabe Renju (JPN)", eType.B, 24576, false);
        /// <summary>Devil World (EUR/JPN)</summary>
        public static readonly VCNES Title109 = new VCNES(109, 0xA1523F6B, new DateTime(2014, 10, 30), "Devil World (EUR/JPN)", eType.B, 24576, false);
        /// <summary>Crash 'n' the Boys: Street Challenge (USA/EUR/JPN)</summary>
        public static readonly VCNES Title110 = new VCNES(110, 0x960780B2, new DateTime(2014, 11, 12), "Crash 'n' the Boys: Street Challenge (USA/EUR/JPN)", eType.B, 262144, false);
        /// <summary>Famicom Wars (JPN)</summary>
        public static readonly VCNES Title111 = new VCNES(111, 0x9E14EFE9, new DateTime(2014, 12, 03), "Famicom Wars (JPN)", eType.B, 196608, false);
        /// <summary>Shadow of the Ninja (EUR)</summary>
        public static readonly VCNES Title112 = new VCNES(112, 0x9BEBD515, new DateTime(2014, 12, 04), "Shadow of the Ninja (EUR)", eType.B, 262144, false);
        /// <summary>S.C.A.T. (USA/EUR)</summary>
        public static readonly VCNES Title113 = new VCNES(113, 0xA8ABF2CF, new DateTime(2014, 12, 04), "S.C.A.T. (USA/EUR)", eType.B, 262144, false);
        /// <summary>Duck Hunt (USA/EUR/JPN)</summary>
        public static readonly VCNES Title114 = new VCNES(114, 0xBC300EBA, new DateTime(2014, 12, 24), "Duck Hunt (USA/EUR/JPN)", eType.B, 24576, false);
        /// <summary>Dragon Buster (JPN)</summary>
        public static readonly VCNES Title115 = new VCNES(115, 0xA0375E86, new DateTime(2015, 01, 21), "Dragon Buster (JPN)", eType.B, 163840, false);
        /// <summary>Shadow of the Ninja (USA)</summary>
        public static readonly VCNES Title116 = new VCNES(116, 0xA3473EAB, new DateTime(2015, 01, 29), "Shadow of the Ninja (USA)", eType.B, 262144, false);
        /// <summary>Valkyrie no Bōken: Toki no Kagi Densetsu (JPN)</summary>
        public static readonly VCNES Title117 = new VCNES(117, 0xA647D809, new DateTime(2015, 02, 04), "Valkyrie no Bōken: Toki no Kagi Densetsu (JPN)", eType.B, 65536, false);
        /// <summary>Mappy-Land (USA)</summary>
        public static readonly VCNES Title118 = new VCNES(118, 0x9B159C4E, new DateTime(2015, 02, 05), "Mappy-Land (USA)", eType.B, 163840, false);
        /// <summary>Mappy-Land (EUR)</summary>
        public static readonly VCNES Title119 = new VCNES(119, 0x88EF2E3F, new DateTime(2015, 02, 12), "Mappy-Land (EUR)", eType.B, 163840, false);
        /// <summary>Blaster Master (EUR)</summary>
        public static readonly VCNES Title120 = new VCNES(120, 0x8C1A183D, new DateTime(2015, 02, 12), "Blaster Master (EUR)", eType.B, 262144, false);
        /// <summary>Yokai Dochuki (JPN)</summary>
        public static readonly VCNES Title121 = new VCNES(121, 0xAE998892, new DateTime(2015, 02, 25), "Yokai Dochuki (JPN)", eType.B, 262144, false);
        /// <summary>Sky Kid (JPN)</summary>
        public static readonly VCNES Title122 = new VCNES(122, 0x94843589, new DateTime(2015, 03, 04), "Sky Kid (JPN)", eType.B, 65536, false);
        /// <summary>Sky Kid (USA)</summary>
        public static readonly VCNES Title123 = new VCNES(123, 0x850404AA, new DateTime(2015, 03, 05), "Sky Kid (USA)", eType.B, 65536, false);
        /// <summary>Kung-Fu Heroes (USA/EUR)</summary>
        public static readonly VCNES Title124 = new VCNES(124, 0x9C6CAC2E, new DateTime(2015, 03, 05), "Kung-Fu Heroes (USA/EUR)", eType.B, 65536, false);
        /// <summary>Sugoro Quest - The Quest of Dice Heros (JPN)/Getsu Fūma Den (JPN)/Ganbare Goemon 2: Kiteretsu Shōgun Magginesu (JPN)</summary>
        public static readonly VCNES Title125 = new VCNES(125, 0xA9274531, new DateTime(2015, 03, 11), "Sugoro Quest - The Quest of Dice Heros (JPN)/Getsu Fūma Den (JPN)/Ganbare Goemon 2: Kiteretsu Shōgun Magginesu (JPN)", eType.B, 262144, false);
        /// <summary>Konami's Ping Pong (JPN)</summary>
        public static readonly VCNES Title126 = new VCNES(126, 0x8D9BAF5E, new DateTime(2015, 03, 18), "Konami's Ping Pong (JPN)", eType.B, 65536, true);
        /// <summary>Atlantis no Nazo (JPN)</summary>
        public static readonly VCNES Title127 = new VCNES(127, 0xA3C4068A, new DateTime(2015, 04, 22), "Atlantis no Nazo (JPN)", eType.B, 49152, false);
        /// <summary>River City Ransom (USA/EUR)/Little Ninja Brothers (USA/EUR)</summary>
        public static readonly VCNES Title128 = new VCNES(128, 0xB1D3000C, new DateTime(2015, 04, 23), "River City Ransom (USA/EUR)/Little Ninja Brothers (USA/EUR)", eType.B, 262144, false);
        /// <summary>Mappy-Land (JPN)</summary>
        public static readonly VCNES Title129 = new VCNES(129, 0xA1A526AF, new DateTime(2015, 05, 13), "Mappy-Land (JPN)", eType.B, 163840, false);
        /// <summary>Ganbare Goemon Gaiden 2: Tenka no Zaihō (JPN)</summary>
        public static readonly VCNES Title130 = new VCNES(130, 0x9374FFF1, new DateTime(2015, 05, 20), "Ganbare Goemon Gaiden 2: Tenka no Zaihō (JPN)", eType.B, 524288, false);
        /// <summary>Blaster Master (JPN)</summary>
        public static readonly VCNES Title131 = new VCNES(131, 0x9E7691BC, new DateTime(2015, 05, 27), "Blaster Master (JPN)", eType.B, 262144, false);
        /// <summary>Flying Warriors (USA/EUR/JPN)/Little Ninja Brothers (JPN)</summary>
        public static readonly VCNES Title132 = new VCNES(132, 0xABD64C76, new DateTime(2015, 05, 28), "Flying Warriors (USA/EUR/JPN)/Little Ninja Brothers (JPN)", eType.B, 262144, false);
        /// <summary>Pooyan (JPN)</summary>
        public static readonly VCNES Title133 = new VCNES(133, 0x9C0929D9, new DateTime(2015, 06, 10), "Pooyan (JPN)", eType.B, 24576, false);
        /// <summary>EarthBound Beginnings (USA/EUR)</summary>
        public static readonly VCNES Title134 = new VCNES(134, 0xD87863A4, new DateTime(2015, 06, 14), "EarthBound Beginnings (USA/EUR)", eType.A, 524288, false);
        /// <summary>Mother (JPN)</summary>
        public static readonly VCNES Title135 = new VCNES(135, 0xE0ACEA0B, new DateTime(2015, 06, 15), "Mother (JPN)", eType.A, 393216, false);
        /// <summary>Seicross (JPN)/Tōkaidō Gojūsan-tsugi (JPN)</summary>
        public static readonly VCNES Title136 = new VCNES(136, 0x90365DEC, new DateTime(2015, 07, 01), "Seicross (JPN)/Tōkaidō Gojūsan-tsugi (JPN)", eType.B, 49152, false);
        /// <summary>Metal Slader Glory (JPN)</summary>
        public static readonly VCNES Title137 = new VCNES(137, 0xBA20089D, new DateTime(2015, 07, 01), "Metal Slader Glory (JPN)", eType.B, 1048576, false);
        /// <summary>Championship Lode Runner (JPN)</summary>
        public static readonly VCNES Title138 = new VCNES(138, 0x9AC0604F, new DateTime(2015, 07, 08), "Championship Lode Runner (JPN)", eType.B, 24576, false);
        /// <summary>Esper Dream (JPN)/Bio Miracle Bokutte Upa (JPN)</summary>
        public static readonly VCNES Title139 = new VCNES(139, 0xA1A0B268, new DateTime(2015, 07, 15), "Esper Dream (JPN)/Bio Miracle Bokutte Upa (JPN)", eType.B, 131072, true);
        /// <summary>Blaster Master (USA)</summary>
        public static readonly VCNES Title140 = new VCNES(140, 0xA5526B7C, new DateTime(2015, 07, 16), "Blaster Master (USA)", eType.B, 262144, false);
        /// <summary>River City Ransom (JPN)</summary>
        public static readonly VCNES Title141 = new VCNES(141, 0xC0ABBB9D, new DateTime(2015, 08, 05), "River City Ransom (JPN)", eType.B, 262144, false);
        /// <summary>VS. Excitebike (USA/JPN)</summary>
        public static readonly VCNES Title142 = new VCNES(142, 0xC6ABAE84, new DateTime(2015, 08, 31), "VS. Excitebike (USA/JPN)", eType.B, 131072, true);
        /// <summary>Wai Wai World 2: SOS!! Parsley Jō (JPN)</summary>
        public static readonly VCNES Title143 = new VCNES(143, 0x8DD20506, new DateTime(2015, 09, 02), "Wai Wai World 2: SOS!! Parsley Jō (JPN)", eType.B, 393216, false);
        /// <summary>Zoda's Revenge: StarTropics II (USA/EUR)</summary>
        public static readonly VCNES Title144 = new VCNES(144, 0xAF221323, new DateTime(2015, 09, 03), "Zoda's Revenge: StarTropics II (USA/EUR)", eType.B, 524288, false);
        /// <summary>StarTropics (USA/EUR)</summary>
        public static readonly VCNES Title145 = new VCNES(145, 0xBB5F4B15, new DateTime(2015, 09, 03), "StarTropics (USA/EUR)", eType.B, 524288, false);
        /// <summary>Double Dragon III: The Sacred Stones (USA/EUR)/Tecmo Bowl (USA/EUR)</summary>
        public static readonly VCNES Title146 = new VCNES(146, 0xC5611AF3, new DateTime(2015, 09, 10), "Double Dragon III: The Sacred Stones (USA/EUR)/Tecmo Bowl (USA/EUR)", eType.B, 262144, false);
        /// <summary>Exerion (JPN)/Formation Z (JPN)</summary>
        public static readonly VCNES Title147 = new VCNES(147, 0xBFD17451, new DateTime(2015, 09, 16), "Exerion (JPN)/Formation Z (JPN)", eType.B, 24576, false);
        /// <summary>Dig Dug II (USA/EUR)</summary>
        public static readonly VCNES Title148 = new VCNES(148, 0xB2E8BE31, new DateTime(2015, 10, 08), "Dig Dug II (USA/EUR)", eType.B, 40960, false);
        /// <summary>Ganbare Goemon Gaiden: Kieta Ōgon Kiseru (JPN)</summary>
        public static readonly VCNES Title149 = new VCNES(149, 0xB618AE6C, new DateTime(2015, 10, 21), "Ganbare Goemon Gaiden: Kieta Ōgon Kiseru (JPN)", eType.B, 524288, false);
        /// <summary>Wild Gunman (EUR)</summary>
        public static readonly VCNES Title150 = new VCNES(150, 0xA720F352, new DateTime(2015, 10, 22), "Wild Gunman (EUR)", eType.B, 24576, false);
        /// <summary>Hogan's Alley (USA/EUR)</summary>
        public static readonly VCNES Title151 = new VCNES(151, 0xB8936232, new DateTime(2015, 10, 22), "Hogan's Alley (USA/EUR)", eType.B, 24576, false);
        /// <summary>Stinger (USA/JPN)</summary>
        public static readonly VCNES Title152 = new VCNES(152, 0x932F3774, new DateTime(2015, 10, 28), "Stinger (USA/JPN)", eType.B, 131072, false);
        /// <summary>Star Luster (JPN)</summary>
        public static readonly VCNES Title153 = new VCNES(153, 0xBC173F3C, new DateTime(2015, 11, 04), "Star Luster (JPN)", eType.B, 40960, false);
        /// <summary>Wagan Land 2 (JPN)</summary>
        public static readonly VCNES Title154 = new VCNES(154, 0xAE16FE0A, new DateTime(2015, 11, 11), "Wagan Land 2 (JPN)", eType.B, 393216, false);
        /// <summary>Metro-Cross (JPN)</summary>
        public static readonly VCNES Title155 = new VCNES(155, 0xAC335C0E, new DateTime(2015, 11, 25), "Metro-Cross (JPN)", eType.B, 65536, false);
        /// <summary>Field Combat (JPN)</summary>
        public static readonly VCNES Title156 = new VCNES(156, 0xBC24C474, new DateTime(2015, 11, 25), "Field Combat (JPN)", eType.B, 24576, false);
        /// <summary>The Adventures of Bayou Billy (USA/EUR/JPN)</summary>
        public static readonly VCNES Title157 = new VCNES(157, 0xA474ADD5, new DateTime(2015, 11, 26), "The Adventures of Bayou Billy (USA/EUR/JPN)", eType.B, 262144, false);
        /// <summary>Ninja Gaiden II: The Dark Sword of Chaos (USA/EUR)</summary>
        public static readonly VCNES Title158 = new VCNES(158, 0xC0D1E406, new DateTime(2015, 11, 26), "Ninja Gaiden II: The Dark Sword of Chaos (USA/EUR)", eType.B, 262144, false);
        /// <summary>Ninja Gaiden III: The Ancient Ship of Doom (USA/EUR)</summary>
        public static readonly VCNES Title159 = new VCNES(159, 0xC72B13A7, new DateTime(2015, 11, 26), "Ninja Gaiden III: The Ancient Ship of Doom (USA/EUR)", eType.B, 262144, false);
        /// <summary>Wild Gunman (USA)/Hogan's Alley (JPN)</summary>
        public static readonly VCNES Title160 = new VCNES(160, 0xAA4C253D, new DateTime(2016, 01, 07), "Wild Gunman (USA)/Hogan's Alley (JPN)", eType.B, 24576, false);
        /// <summary>City Connection (USA)</summary>
        public static readonly VCNES Title161 = new VCNES(161, 0xCCEDDB59, new DateTime(2016, 03, 17), "City Connection (USA)", eType.B, 65536, false);
        /// <summary>Nintendo Zapper (JPN)</summary>
        public static readonly VCNES Title162 = new VCNES(162, 0xB28A6A5C, new DateTime(2016, 06, 22), "Nintendo Zapper (JPN)", eType.B, 24576, false);
        /// <summary>Front Line (JPN)</summary>
        public static readonly VCNES Title163 = new VCNES(163, 0xBFC6A68B, new DateTime(2016, 06, 29), "Front Line (JPN)", eType.B, 24576, false);
        /// <summary>King's Knight (JPN)</summary>
        public static readonly VCNES Title164 = new VCNES(164, 0xA6F10177, new DateTime(2016, 07, 06), "King's Knight (JPN)", eType.B, 65536, false);
        /// <summary>Baseball Simulator 1000 (USA/JPN)</summary>
        public static readonly VCNES Title165 = new VCNES(165, 0xB5F514E7, new DateTime(2016, 07, 07), "Baseball Simulator 1000 (USA/JPN)", eType.B, 262144, false);
        /// <summary>Trojan (JPN)</summary>
        public static readonly VCNES Title166 = new VCNES(166, 0x984FE47A, new DateTime(2016, 08, 31), "Trojan (JPN)", eType.B, 131072, false);
        /// <summary>Dig Dug II (JPN)</summary>
        public static readonly VCNES Title167 = new VCNES(167, 0xB936BEC1, new DateTime(2016, 09, 07), "Dig Dug II (JPN)", eType.B, 40960, false);
        /// <summary>MagMax (JPN)</summary>
        public static readonly VCNES Title168 = new VCNES(168, 0xB2933F7C, new DateTime(2016, 09, 14), "MagMax (JPN)", eType.B, 40960, false);
        /// <summary>City Connection (JPN)</summary>
        public static readonly VCNES Title169 = new VCNES(169, 0xC3F82A6C, new DateTime(2017, 03, 29), "City Connection (JPN)", eType.B, 49152, false);
    }
}

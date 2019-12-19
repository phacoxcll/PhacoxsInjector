using System;

namespace PhacoxsInjector
{
    public class VCSNES : WiiUVC
    {
        public enum eType
        {
            A1,
            A2,
            B1,
            B2,
            Unknown
        }

        public readonly int ROMSize;
        public readonly eType Type;
        public readonly bool ExtendedFooter;
        public readonly bool PCMData;

        public VCSNES(int index, uint hash, DateTime release, string title, eType type, int romSize, bool extendedFooter, bool pcmData)
            : base(index, hash, release, title)
        {
            ROMSize = romSize;
            Type = type;
            ExtendedFooter = extendedFooter;
            PCMData = pcmData;
        }

        public VCSNES(uint hash)
            : base(hash)
        {
            ROMSize = 0;
            Type = eType.Unknown;
            ExtendedFooter = false;
            PCMData = false;
        }

        public override string ToString()
        {
            return "Hash: " + Hash.ToString("X8") + ", Release date: " + Release.ToString("yyyy/MM/dd") +
                ", Type: " + Type.ToString() + ", ROM size: " + Useful.ToFileSize(ROMSize) + ", Extended footer: " + ExtendedFooter.ToString() + ", PCM data: " + PCMData.ToString() +
                " \nTitle: " + Title;
        }

        public static VCSNES GetVC(uint hash)
        {
            switch (hash)
            {
                case 0xCBE3CB53: return VCSNES.Title001;
                case 0xE1E02D1A: return VCSNES.Title002;
                case 0xBFBC63CC: return VCSNES.Title003;
                case 0xC593E5E6: return VCSNES.Title004;
                case 0xC24A566D: return VCSNES.Title005;
                case 0xCED311D0: return VCSNES.Title006;
                case 0xD6CA025F: return VCSNES.Title007;
                case 0xD76B99EF: return VCSNES.Title008;
                case 0xD4727012: return VCSNES.Title009;
                case 0xC618C24B: return VCSNES.Title010;
                case 0xC029C869: return VCSNES.Title011;
                case 0xCB5F48A8: return VCSNES.Title012;
                case 0xCE6601CD: return VCSNES.Title013;
                case 0xE1709184: return VCSNES.Title014;
                case 0xC4D71343: return VCSNES.Title015;
                case 0xB34E1F4B: return VCSNES.Title016;
                case 0xD8B6064A: return VCSNES.Title017;
                case 0xC68EFFD0: return VCSNES.Title018;
                case 0xC4D5EDD7: return VCSNES.Title019;
                case 0xA89A860E: return VCSNES.Title020;
                case 0xB4C803CD: return VCSNES.Title021;
                case 0xD1B8D5E2: return VCSNES.Title022;
                case 0xB651B88E: return VCSNES.Title023;
                case 0xB7E25A8F: return VCSNES.Title024;
                case 0xB4B9278B: return VCSNES.Title025;
                case 0xC1C2CD2D: return VCSNES.Title026;
                case 0xB3CFA8A8: return VCSNES.Title027;
                case 0xB9EDB65E: return VCSNES.Title028;
                case 0xB44E4393: return VCSNES.Title029;
                case 0xB508E784: return VCSNES.Title030;
                case 0x9F06DD9B: return VCSNES.Title031;
                case 0xA2BD21A7: return VCSNES.Title032;
                case 0xBD6948A9: return VCSNES.Title033;
                case 0xC155E3A0: return VCSNES.Title034;
                case 0xD2E7F1A9: return VCSNES.Title035;
                case 0xAA9E5DB3: return VCSNES.Title036;
                case 0xB92ABD19: return VCSNES.Title037;
                case 0xBBA21042: return VCSNES.Title038;
                case 0xCE7E8306: return VCSNES.Title039;
                case 0xCE1BCAF6: return VCSNES.Title040;
                case 0xD262592C: return VCSNES.Title041;
                case 0xAF1A2EE1: return VCSNES.Title042;
                case 0xA547562D: return VCSNES.Title043;
                case 0xAC5E5227: return VCSNES.Title044;
                case 0x9F523E24: return VCSNES.Title045;
                case 0xCBEC9D21: return VCSNES.Title046;
                case 0xA8DCC5DC: return VCSNES.Title047;
                case 0xA74F8046: return VCSNES.Title048;
                case 0xAFFC0DE9: return VCSNES.Title049;
                case 0xB69FF36F: return VCSNES.Title050;
                case 0xBC9DA60E: return VCSNES.Title051;
                case 0x9AE3B68B: return VCSNES.Title052;
                case 0xD5CA4093: return VCSNES.Title053;
                case 0xBC63A1C8: return VCSNES.Title054;
                case 0xA4457AE7: return VCSNES.Title055;
                case 0xB26E5985: return VCSNES.Title056;
                case 0xB7C2C025: return VCSNES.Title057;
                case 0xA609856E: return VCSNES.Title058;
                case 0xA8843CC3: return VCSNES.Title059;
                case 0xB2195DB2: return VCSNES.Title060;
                case 0xAFA3CD5F: return VCSNES.Title061;
                case 0xDA16F30B: return VCSNES.Title062;
                case 0xABA6C3D8: return VCSNES.Title063;
                case 0xC58A1439: return VCSNES.Title064;
                case 0xCFE2F857: return VCSNES.Title065;
                case 0xA1554AFD: return VCSNES.Title066;
                case 0xC20E69E2: return VCSNES.Title067;
                case 0xA8513D59: return VCSNES.Title068;
                case 0xAB870B4E: return VCSNES.Title069;
                case 0xB9FDAD98: return VCSNES.Title070;
                case 0xABFF0731: return VCSNES.Title071;
                case 0xB2FDD1D3: return VCSNES.Title072;
                case 0xAFD0B303: return VCSNES.Title073;
                case 0xC27A2C40: return VCSNES.Title074;
                case 0xA7EFAB22: return VCSNES.Title075;
                case 0xA78161C9: return VCSNES.Title076;
                case 0xA1B1E82D: return VCSNES.Title077;
                case 0xA7B03D27: return VCSNES.Title078;
                case 0xA5456A61: return VCSNES.Title079;
                case 0xB12CAA2C: return VCSNES.Title080;
                case 0xB354F03B: return VCSNES.Title081;
                case 0xCE3DC892: return VCSNES.Title082;
                case 0xD3E4BF9C: return VCSNES.Title083;
                case 0xDE78A430: return VCSNES.Title084;
                case 0xA18DD837: return VCSNES.Title085;
                case 0xB0CBE2B4: return VCSNES.Title086;
                case 0xAE69F2C8: return VCSNES.Title087;
                case 0x9F9DBB67: return VCSNES.Title088;
                case 0xA40B189E: return VCSNES.Title089;
                case 0xADAC5734: return VCSNES.Title090;
                case 0xC969FC68: return VCSNES.Title091;
                case 0xA27A572C: return VCSNES.Title092;
                case 0xB029222E: return VCSNES.Title093;
                case 0xA0C2ECC7: return VCSNES.Title094;
                case 0xB03D8A2F: return VCSNES.Title095;
                case 0xA9603BAD: return VCSNES.Title096;
                case 0xA7891654: return VCSNES.Title097;
                case 0xAA64B1BE: return VCSNES.Title098;
                case 0xC2ECC888: return VCSNES.Title099;
                case 0xCB84B720: return VCSNES.Title100;
                case 0x9F042607: return VCSNES.Title101;
                case 0xB90ECAF1: return VCSNES.Title102;
                case 0x9E7E3D65: return VCSNES.Title103;
                case 0x9A837593: return VCSNES.Title104;
                case 0xD3B2BDB9: return VCSNES.Title105;
                case 0xA279FE7D: return VCSNES.Title106;
                case 0xA0506A67: return VCSNES.Title107;
                case 0xA54DC0F6: return VCSNES.Title108;
                case 0xACC8B7A7: return VCSNES.Title109;
                case 0xB864C674: return VCSNES.Title110;
                case 0x964937AF: return VCSNES.Title111;
                case 0xA6D6CAE0: return VCSNES.Title112;
                case 0x7D9E5904: return VCSNES.Title113;
                case 0xD6FF8995: return VCSNES.Title114;
                case 0xA56BB098: return VCSNES.Title115;
                case 0xA122EEC0: return VCSNES.Title116;
                case 0x94AB097D: return VCSNES.Title117;
                case 0x9B5FB35D: return VCSNES.Title118;
                case 0xB11A158A: return VCSNES.Title119;
                case 0xACEEFB53: return VCSNES.Title120;
                case 0xA85E5134: return VCSNES.Title121;
                case 0xA4570B77: return VCSNES.Title122;
                case 0x94F0D47E: return VCSNES.Title123;
                case 0xABDE2222: return VCSNES.Title124;
                case 0xA254090C: return VCSNES.Title125;
                case 0xB80A3F81: return VCSNES.Title126;
                case 0x8BDD22FE: return VCSNES.Title127;
                case 0x9D738126: return VCSNES.Title128;
                case 0xA64C2EA7: return VCSNES.Title129;
                case 0xADC96E2B: return VCSNES.Title130;
                case 0x9C0BB56D: return VCSNES.Title131;
                case 0xBF36AC9B: return VCSNES.Title132;
                case 0xB46F5D41: return VCSNES.Title133;
                case 0xA9BC61A7: return VCSNES.Title134;
                case 0xC364C797: return VCSNES.Title135;
                case 0xACB1A9B5: return VCSNES.Title136;
                case 0xBE4D1CB6: return VCSNES.Title137;
                case 0xCF1E7641: return VCSNES.Title138;
                case 0xB0D878C7: return VCSNES.Title139;
                case 0xC3918529: return VCSNES.Title140;
                case 0xB13EE167: return VCSNES.Title141;
                case 0xC909118B: return VCSNES.Title142;
                default: return null;
            }
        }

        /// <summary>Super Mario World (USA/EUR/JPN)</summary>
        public static readonly VCSNES Title001 = new VCSNES(001, 0xCBE3CB53, new DateTime(2013, 04, 26), "Super Mario World (USA/EUR/JPN)", eType.A1, 524288, false, true);
        /// <summary>F-Zero (USA/EUR)</summary>
        public static readonly VCSNES Title002 = new VCSNES(002, 0xE1E02D1A, new DateTime(2013, 04, 26), "F-Zero (USA/EUR)", eType.A1, 524288, false, true);
        /// <summary>Fire Emblem: Seisen no Keifu (JPN)</summary>
        public static readonly VCSNES Title003 = new VCSNES(003, 0xBFBC63CC, new DateTime(2013, 04, 27), "Fire Emblem: Seisen no Keifu (JPN)", eType.A1, 4194304, false, true);
        /// <summary>Fire Emblem: Monshō no Nazo (JPN)</summary>
        public static readonly VCSNES Title004 = new VCSNES(004, 0xC593E5E6, new DateTime(2013, 04, 27), "Fire Emblem: Monshō no Nazo (JPN)", eType.A1, 3145728, false, true);
        /// <summary>Mario's Super Picross (EUR)</summary>
        public static readonly VCSNES Title005 = new VCSNES(005, 0xC24A566D, new DateTime(2013, 04, 27), "Mario's Super Picross (EUR)", eType.A1, 1048576, false, false);
        /// <summary>Mother 2 (JPN)</summary>
        public static readonly VCSNES Title006 = new VCSNES(006, 0xCED311D0, new DateTime(2013, 04, 27), "Mother 2 (JPN)", eType.A1, 3145728, false, true);
        /// <summary>Mario's Super Picross (JPN)</summary>
        public static readonly VCSNES Title007 = new VCSNES(007, 0xD6CA025F, new DateTime(2013, 04, 27), "Mario's Super Picross (JPN)", eType.A1, 1048576, false, true);
        /// <summary>Super Ghouls 'n Ghosts (USA/EUR/JPN)</summary>
        public static readonly VCSNES Title008 = new VCSNES(008, 0xD76B99EF, new DateTime(2013, 04, 27), "Super Ghouls 'n Ghosts (USA/EUR/JPN)", eType.A1, 1048576, false, true);
        /// <summary>F-Zero (JPN)</summary>
        public static readonly VCSNES Title009 = new VCSNES(009, 0xD4727012, new DateTime(2013, 04, 27), "F-Zero (JPN)", eType.A1, 524288, false, true);
        /// <summary>Kirby Super Star (USA/EUR/JPN)</summary>
        public static readonly VCSNES Title010 = new VCSNES(010, 0xC618C24B, new DateTime(2013, 05, 01), "Kirby Super Star (USA/EUR/JPN)", eType.A1, 4194304, false, true);
        /// <summary>Kirby's Dream Course (JPN)</summary>
        public static readonly VCSNES Title011 = new VCSNES(011, 0xC029C869, new DateTime(2013, 05, 08), "Kirby's Dream Course (JPN)", eType.A1, 1310720, false, true);
        /// <summary>Kirby's Dream Land 3 (JPN)</summary>
        public static readonly VCSNES Title012 = new VCSNES(012, 0xCB5F48A8, new DateTime(2013, 05, 08), "Kirby's Dream Land 3 (JPN)", eType.A1, 4194304, false, true);
        /// <summary>Kirby's Star Stacker (JPN)</summary>
        public static readonly VCSNES Title013 = new VCSNES(013, 0xCE6601CD, new DateTime(2013, 05, 08), "Kirby's Star Stacker (JPN)", eType.A1, 2097152, false, true);
        /// <summary>Super Metroid (USA/JPN)</summary>
        public static readonly VCSNES Title014 = new VCSNES(014, 0xE1709184, new DateTime(2013, 05, 15), "Super Metroid (USA/JPN)", eType.A1, 3145728, false, true);
        /// <summary>Super Metroid (EUR)</summary>
        public static readonly VCSNES Title015 = new VCSNES(015, 0xC4D71343, new DateTime(2013, 05, 16), "Super Metroid (EUR)", eType.A1, 3145728, false, true);
        /// <summary>Mega Man X (USA/EUR/JPN)</summary>
        public static readonly VCSNES Title016 = new VCSNES(016, 0xB34E1F4B, new DateTime(2013, 05, 22), "Mega Man X (USA/EUR/JPN)", eType.A2, 1572864, false, true);
        /// <summary>Heracles no Eikō III: Kamigami no Chinmoku (JPN)</summary>
        public static readonly VCSNES Title017 = new VCSNES(017, 0xD8B6064A, new DateTime(2013, 05, 22), "Heracles no Eikō III: Kamigami no Chinmoku (JPN)", eType.A1, 1048576, false, true);
        /// <summary>Kirby's Dream Land 3 (USA/EUR)</summary>
        public static readonly VCSNES Title018 = new VCSNES(018, 0xC68EFFD0, new DateTime(2013, 05, 23), "Kirby's Dream Land 3 (USA/EUR)", eType.A1, 4194304, false, true);
        /// <summary>Kirby's Dream Course (USA/EUR)</summary>
        public static readonly VCSNES Title019 = new VCSNES(019, 0xC4D5EDD7, new DateTime(2013, 05, 23), "Kirby's Dream Course (USA/EUR)", eType.A1, 1048576, false, true);
        /// <summary>Pilotwings (USA/EUR/JPN)</summary>
        public static readonly VCSNES Title020 = new VCSNES(020, 0xA89A860E, new DateTime(2013, 05, 29), "Pilotwings (USA/EUR/JPN)", eType.A2, 524288, false, true);
        /// <summary>Panel de Pon (JPN)</summary>
        public static readonly VCSNES Title021 = new VCSNES(021, 0xB4C803CD, new DateTime(2013, 05, 29), "Panel de Pon (JPN)", eType.A2, 1048576, false, true);
        /// <summary>Super Mario Kart (JPN)</summary>
        public static readonly VCSNES Title022 = new VCSNES(022, 0xD1B8D5E2, new DateTime(2013, 06, 19), "Super Mario Kart (JPN)", eType.A1, 524288, false, true);
        /// <summary>Secret of Mana (JPN)</summary>
        public static readonly VCSNES Title023 = new VCSNES(023, 0xB651B88E, new DateTime(2013, 06, 26), "Secret of Mana (JPN)", eType.A2, 2097152, false, true);
        /// <summary>Final Fantasy VI (JPN)</summary>
        public static readonly VCSNES Title024 = new VCSNES(024, 0xB7E25A8F, new DateTime(2013, 06, 26), "Final Fantasy VI (JPN)", eType.A2, 3145728, false, true);
        /// <summary>Vegas Stakes (USA/EUR)</summary>
        public static readonly VCSNES Title025 = new VCSNES(025, 0xB4B9278B, new DateTime(2013, 06, 27), "Vegas Stakes (USA/EUR)", eType.A2, 1048576, false, true);
        /// <summary>Shin Megami Tensei (JPN)</summary>
        public static readonly VCSNES Title026 = new VCSNES(026, 0xC1C2CD2D, new DateTime(2013, 07, 03), "Shin Megami Tensei (JPN)", eType.A2, 1572864, false, true);
        /// <summary>Fire Emblem: Thracia 776 (JPN)</summary>
        public static readonly VCSNES Title027 = new VCSNES(027, 0xB3CFA8A8, new DateTime(2013, 07, 10), "Fire Emblem: Thracia 776 (JPN)", eType.A2, 4194304, false, true);
        /// <summary>Breath of Fire II (JPN)</summary>
        public static readonly VCSNES Title028 = new VCSNES(028, 0xB9EDB65E, new DateTime(2013, 07, 10), "Breath of Fire II (JPN)", eType.A2, 2621440, false, true);
        /// <summary>EarthBound (USA)</summary>
        public static readonly VCSNES Title029 = new VCSNES(029, 0xB44E4393, new DateTime(2013, 07, 18), "EarthBound (USA)", eType.A2, 3145728, false, true);
        /// <summary>EarthBound (EUR)</summary>
        public static readonly VCSNES Title030 = new VCSNES(030, 0xB508E784, new DateTime(2013, 07, 18), "EarthBound (EUR)", eType.A2, 3145728, false, false);
        /// <summary>Romance of the Three Kingdoms IV: Wall of Fire (JPN)</summary>
        public static readonly VCSNES Title031 = new VCSNES(031, 0x9F06DD9B, new DateTime(2013, 07, 24), "Romance of the Three Kingdoms IV: Wall of Fire (JPN)", eType.A2, 3145728, false, true);
        /// <summary>Famicom Tantei Club Part II: Ushiro ni Tatsu Shōjo (JPN)</summary>
        public static readonly VCSNES Title032 = new VCSNES(032, 0xA2BD21A7, new DateTime(2013, 07, 31), "Famicom Tantei Club Part II: Ushiro ni Tatsu Shōjo (JPN)", eType.A2, 3145728, false, true);
        /// <summary>Harvest Moon (USA)</summary>
        public static readonly VCSNES Title033 = new VCSNES(033, 0xBD6948A9, new DateTime(2013, 08, 01), "Harvest Moon (USA)", eType.A2, 2097152, false, true);
        /// <summary>Harvest Moon (EUR)</summary>
        public static readonly VCSNES Title034 = new VCSNES(034, 0xC155E3A0, new DateTime(2013, 08, 01), "Harvest Moon (EUR)", eType.A2, 2097152, false, true);
        /// <summary>Kamaitachi no Yoru (JPN)</summary>
        public static readonly VCSNES Title035 = new VCSNES(035, 0xD2E7F1A9, new DateTime(2013, 08, 07), "Kamaitachi no Yoru (JPN)", eType.A1, 3145728, false, true);
        /// <summary>Romance of the Three Kingdoms IV: Wall of Fire (USA/EUR)</summary>
        public static readonly VCSNES Title036 = new VCSNES(036, 0xAA9E5DB3, new DateTime(2013, 08, 08), "Romance of the Three Kingdoms IV: Wall of Fire (USA/EUR)", eType.A2, 3145728, false, true);
        /// <summary>Famicom Bunko: Hajimari no Mori (JPN)</summary>
        public static readonly VCSNES Title037 = new VCSNES(037, 0xB92ABD19, new DateTime(2013, 08, 21), "Famicom Bunko: Hajimari no Mori (JPN)", eType.A2, 4194304, false, true);
        /// <summary>Street Fighter II: The World Warrior (USA/EUR)</summary>
        public static readonly VCSNES Title038 = new VCSNES(038, 0xBBA21042, new DateTime(2013, 08, 22), "Street Fighter II: The World Warrior (USA/EUR)", eType.A1, 2097152, false, true);
        /// <summary>Super Street Fighter II: The New Challengers (USA/EUR)</summary>
        public static readonly VCSNES Title039 = new VCSNES(039, 0xCE7E8306, new DateTime(2013, 08, 22), "Super Street Fighter II: The New Challengers (USA/EUR)", eType.A1, 4194304, false, true);
        /// <summary>Street Fighter II' Turbo: Hyper Fighting (USA/EUR)</summary>
        public static readonly VCSNES Title040 = new VCSNES(040, 0xCE1BCAF6, new DateTime(2013, 08, 22), "Street Fighter II' Turbo: Hyper Fighting (USA/EUR)", eType.A1, 2621440, false, true);
        /// <summary>The Legend of the Mystical Ninja (USA/JPN)</summary>
        public static readonly VCSNES Title041 = new VCSNES(041, 0xD262592C, new DateTime(2013, 09, 04), "The Legend of the Mystical Ninja (USA/JPN)", eType.A1, 1048576, false, true);
        /// <summary>Breath of Fire II (USA/EUR)</summary>
        public static readonly VCSNES Title042 = new VCSNES(042, 0xAF1A2EE1, new DateTime(2013, 09, 05), "Breath of Fire II (USA/EUR)", eType.A2, 3145728, false, true);
        /// <summary>Super Castlevania IV (USA/EUR/JPN)</summary>
        public static readonly VCSNES Title043 = new VCSNES(043, 0xA547562D, new DateTime(2013, 09, 11), "Super Castlevania IV (USA/EUR/JPN)", eType.A2, 1048576, false, true);
        /// <summary>Wagan Land (JPN)</summary>
        public static readonly VCSNES Title044 = new VCSNES(044, 0xAC5E5227, new DateTime(2013, 09, 18), "Wagan Land (JPN)", eType.B1, 1048576, false, true);
        /// <summary>Shin Megami Tensei II (JPN)</summary>
        public static readonly VCSNES Title045 = new VCSNES(045, 0x9F523E24, new DateTime(2013, 09, 25), "Shin Megami Tensei II (JPN)", eType.B1, 2097152, false, true);
        /// <summary>Ganbare Goemon 2: Kiteretsu Shōgun Magginesu (JPN)</summary>
        public static readonly VCSNES Title046 = new VCSNES(046, 0xCBEC9D21, new DateTime(2013, 09, 25), "Ganbare Goemon 2: Kiteretsu Shōgun Magginesu (JPN)", eType.A1, 2097152, false, true);
        /// <summary>Super Famicom Wars (JPN)</summary>
        public static readonly VCSNES Title047 = new VCSNES(047, 0xA8DCC5DC, new DateTime(2013, 10, 02), "Super Famicom Wars (JPN)", eType.A2, 2097152, false, true);
        /// <summary>Final Fight 3 (USA/EUR)</summary>
        public static readonly VCSNES Title048 = new VCSNES(048, 0xA74F8046, new DateTime(2013, 10, 03), "Final Fight 3 (USA/EUR)", eType.B1, 3145728, false, true);
        /// <summary>Final Fight (USA/EUR/JPN)</summary>
        public static readonly VCSNES Title049 = new VCSNES(049, 0xAFFC0DE9, new DateTime(2013, 10, 03), "Final Fight (USA/EUR/JPN)", eType.A2, 1048576, false, true);
        /// <summary>Final Fight 2 (USA/EUR)</summary>
        public static readonly VCSNES Title050 = new VCSNES(050, 0xB69FF36F, new DateTime(2013, 10, 03), "Final Fight 2 (USA/EUR)", eType.A2, 1310720, false, true);
        /// <summary>Mega Man X2 (JPN)</summary>
        public static readonly VCSNES Title051 = new VCSNES(051, 0xBC9DA60E, new DateTime(2013, 10, 09), "Mega Man X2 (JPN)", eType.B1, 1572864, false, true);
        /// <summary>Shin Megami Tensei If... (JPN)</summary>
        public static readonly VCSNES Title052 = new VCSNES(052, 0x9AE3B68B, new DateTime(2013, 10, 16), "Shin Megami Tensei If... (JPN)", eType.B1, 2097152, false, true);
        /// <summary>Ganbare Goemon 3: Shishijūrokubē no Karakuri Manji Gatame (JPN)</summary>
        public static readonly VCSNES Title053 = new VCSNES(053, 0xD5CA4093, new DateTime(2013, 10, 16), "Ganbare Goemon 3: Shishijūrokubē no Karakuri Manji Gatame (JPN)", eType.A1, 2097152, false, true);
        /// <summary>Uncharted Waters: New Horizons (JPN)</summary>
        public static readonly VCSNES Title054 = new VCSNES(054, 0xBC63A1C8, new DateTime(2013, 10, 30), "Uncharted Waters: New Horizons (JPN)", eType.B1, 2097152, false, true);
        /// <summary>Clock Tower (JPN)</summary>
        public static readonly VCSNES Title055 = new VCSNES(055, 0xA4457AE7, new DateTime(2013, 11, 06), "Clock Tower (JPN)", eType.B1, 3145728, false, true);
        /// <summary>Mega Man X2 (USA/EUR)</summary>
        public static readonly VCSNES Title056 = new VCSNES(056, 0xB26E5985, new DateTime(2013, 11, 14), "Mega Man X2 (USA/EUR)", eType.B1, 1572864, false, true);
        /// <summary>Uncharted Waters: New Horizons (USA/EUR)</summary>
        public static readonly VCSNES Title057 = new VCSNES(057, 0xB7C2C025, new DateTime(2013, 11, 14), "Uncharted Waters: New Horizons (USA/EUR)", eType.B1, 2097152, false, true);
        /// <summary>Ogre Battle: The March of the Black Queen (JPN)</summary>
        public static readonly VCSNES Title058 = new VCSNES(058, 0xA609856E, new DateTime(2013, 11, 20), "Ogre Battle: The March of the Black Queen (JPN)", eType.B1, 1572864, false, true);
        /// <summary>Brawl Brothers (USA/EUR/JPN)</summary>
        public static readonly VCSNES Title059 = new VCSNES(059, 0xA8843CC3, new DateTime(2013, 11, 21), "Brawl Brothers (USA/EUR/JPN)", eType.B1, 1572864, false, true);
        /// <summary>Contra III: The Alien Wars (JPN)</summary>
        public static readonly VCSNES Title060 = new VCSNES(060, 0xB2195DB2, new DateTime(2013, 11, 27), "Contra III: The Alien Wars (JPN)", eType.A2, 1048576, false, true);
        /// <summary>Contra III: The Alien Wars (USA/EUR)</summary>
        public static readonly VCSNES Title061 = new VCSNES(061, 0xAFA3CD5F, new DateTime(2013, 11, 28), "Contra III: The Alien Wars (USA/EUR)", eType.A2, 1048576, false, true);
        /// <summary>The Legend of Zelda: A Link to the Past (EUR/DEU/FRA)</summary>
        public static readonly VCSNES Title062 = new VCSNES(062, 0xDA16F30B, new DateTime(2013, 12, 12), "The Legend of Zelda: A Link to the Past (EUR/DEU/FRA)", eType.A1, 1048576, false, true);
        /// <summary>Romancing SaGa (JPN)</summary>
        public static readonly VCSNES Title063 = new VCSNES(063, 0xABA6C3D8, new DateTime(2013, 12, 18), "Romancing SaGa (JPN)", eType.B1, 1048576, false, true);
        /// <summary>Super Punch-Out!! (USA)</summary>
        public static readonly VCSNES Title064 = new VCSNES(064, 0xC58A1439, new DateTime(2013, 12, 26), "Super Punch-Out!! (USA)", eType.B1, 2097152, false, true);
        /// <summary>The Legend of the Mystical Ninja (EUR)</summary>
        public static readonly VCSNES Title065 = new VCSNES(065, 0xCFE2F857, new DateTime(2014, 01, 16), "The Legend of the Mystical Ninja (EUR)", eType.A1, 1048576, false, true);
        /// <summary>Romancing SaGa 2 (JPN)</summary>
        public static readonly VCSNES Title066 = new VCSNES(066, 0xA1554AFD, new DateTime(2014, 01, 22), "Romancing SaGa 2 (JPN)", eType.B1, 2097152, false, true);
        /// <summary>The Legend of Zelda: A Link to the Past (USA/JPN)</summary>
        public static readonly VCSNES Title067 = new VCSNES(067, 0xC20E69E2, new DateTime(2014, 01, 30), "The Legend of Zelda: A Link to the Past (USA/JPN)", eType.A1, 1048576, false, true);
        /// <summary>Bahamut Lagoon (JPN)</summary>
        public static readonly VCSNES Title068 = new VCSNES(068, 0xA8513D59, new DateTime(2014, 02, 05), "Bahamut Lagoon (JPN)", eType.B1, 3145728, false, true);
        /// <summary>Marvelous: Mōhitotsu no Takarajima (JPN)</summary>
        public static readonly VCSNES Title069 = new VCSNES(069, 0xAB870B4E, new DateTime(2014, 02, 12), "Marvelous: Mōhitotsu no Takarajima (JPN)", eType.B1, 3145728, false, false);
        /// <summary>Final Fantasy IV (JPN)</summary>
        public static readonly VCSNES Title070 = new VCSNES(070, 0xB9FDAD98, new DateTime(2014, 02, 19), "Final Fantasy IV (JPN)", eType.B1, 1048576, false, true);
        /// <summary>Romancing SaGa 3 (JPN)</summary>
        public static readonly VCSNES Title071 = new VCSNES(071, 0xABFF0731, new DateTime(2014, 02, 26), "Romancing SaGa 3 (JPN)", eType.B1, 4194304, false, true);
        /// <summary>Tactics Ogre: Let Us Cling Together (JPN)</summary>
        public static readonly VCSNES Title072 = new VCSNES(072, 0xB2FDD1D3, new DateTime(2014, 03, 12), "Tactics Ogre: Let Us Cling Together (JPN)", eType.B1, 3145728, false, true);
        /// <summary>Final Fantasy V (JPN)</summary>
        public static readonly VCSNES Title073 = new VCSNES(073, 0xAFD0B303, new DateTime(2014, 03, 26), "Final Fantasy V (JPN)", eType.B1, 2097152, false, true);
        /// <summary>Super Mario Kart (USA/EUR)</summary>
        public static readonly VCSNES Title074 = new VCSNES(074, 0xC27A2C40, new DateTime(2014, 03, 27), "Super Mario Kart (USA/EUR)", eType.A1, 524288, false, true);
        /// <summary>Super Punch-Out!! (JPN)</summary>
        public static readonly VCSNES Title075 = new VCSNES(075, 0xA7EFAB22, new DateTime(2014, 04, 09), "Super Punch-Out!! (JPN)", eType.B1, 2097152, false, true);
        /// <summary>Final Fantasy Mystic Quest (JPN)</summary>
        public static readonly VCSNES Title076 = new VCSNES(076, 0xA78161C9, new DateTime(2014, 04, 16), "Final Fantasy Mystic Quest (JPN)", eType.B1, 524288, false, true);
        /// <summary>Castlevania: Dracula X (USA/EUR/JPN)</summary>
        public static readonly VCSNES Title077 = new VCSNES(077, 0xA1B1E82D, new DateTime(2014, 04, 23), "Castlevania: Dracula X (USA/EUR/JPN)", eType.B1, 2097152, false, false);
        /// <summary>Pop'n TwinBee (EUR/JPN)</summary>
        public static readonly VCSNES Title078 = new VCSNES(078, 0xA7B03D27, new DateTime(2014, 05, 07), "Pop'n TwinBee (EUR/JPN)", eType.B1, 1048576, false, false);
        /// <summary>Pop'n TwinBee: Rainbow Bell Adventures (EUR/JPN)</summary>
        public static readonly VCSNES Title079 = new VCSNES(079, 0xA5456A61, new DateTime(2014, 05, 21), "Pop'n TwinBee: Rainbow Bell Adventures (EUR/JPN)", eType.B1, 1048576, false, false);
        /// <summary>Street Fighter Alpha 2 (USA/EUR)</summary>
        public static readonly VCSNES Title080 = new VCSNES(080, 0xB12CAA2C, new DateTime(2014, 05, 22), "Street Fighter Alpha 2 (USA/EUR)", eType.B1, 4194304, false, true);
        /// <summary>Super Punch-Out!! (EUR)</summary>
        public static readonly VCSNES Title081 = new VCSNES(081, 0xB354F03B, new DateTime(2014, 06, 12), "Super Punch-Out!! (EUR)", eType.B1, 2097152, false, true);
        /// <summary>Super Street Fighter II: The New Challengers (JPN)</summary>
        public static readonly VCSNES Title082 = new VCSNES(082, 0xCE3DC892, new DateTime(2014, 06, 25), "Super Street Fighter II: The New Challengers (JPN)", eType.A1, 4194304, false, true);
        /// <summary>Street Fighter II: The World Warrior (JPN)</summary>
        public static readonly VCSNES Title083 = new VCSNES(083, 0xD3E4BF9C, new DateTime(2014, 06, 25), "Street Fighter II: The World Warrior (JPN)", eType.A1, 2097152, false, true);
        /// <summary>Street Fighter II' Turbo: Hyper Fighting (JPN)</summary>
        public static readonly VCSNES Title084 = new VCSNES(084, 0xDE78A430, new DateTime(2014, 06, 25), "Street Fighter II' Turbo: Hyper Fighting (JPN)", eType.A1, 2621440, false, true);
        /// <summary>Kunio-kun no Dodge Ball da yo: Zenin Shūgo (JPN)</summary>
        public static readonly VCSNES Title085 = new VCSNES(085, 0xA18DD837, new DateTime(2014, 07, 16), "Kunio-kun no Dodge Ball da yo: Zenin Shūgo (JPN)", eType.B1, 1572864, true, true);
        /// <summary>Otogirisō (JPN)</summary>
        public static readonly VCSNES Title086 = new VCSNES(086, 0xB0CBE2B4, new DateTime(2014, 07, 30), "Otogirisō (JPN)", eType.B1, 1048576, false, true);
        /// <summary>Mega Man 7 (JPN)/Mega Man X3 (JPN)</summary>
        public static readonly VCSNES Title087 = new VCSNES(087, 0xAE69F2C8, new DateTime(2014, 08, 06), "Mega Man 7 (JPN)/Mega Man X3 (JPN)", eType.B1, 2097152, true, false);
        /// <summary>Cybernator (USA/EUR)</summary>
        public static readonly VCSNES Title088 = new VCSNES(088, 0x9F9DBB67, new DateTime(2014, 08, 07), "Cybernator (USA/EUR)", eType.B1, 1048576, false, true);
        /// <summary>Street Fighter Zero 2 (JPN)</summary>
        public static readonly VCSNES Title089 = new VCSNES(089, 0xA40B189E, new DateTime(2014, 08, 20), "Street Fighter Zero 2 (JPN)", eType.B1, 4194304, false, false);
        /// <summary>Gakkou de atta Kowai Hanashi (JPN)</summary>
        public static readonly VCSNES Title090 = new VCSNES(090, 0xADAC5734, new DateTime(2014, 08, 27), "Gakkou de atta Kowai Hanashi (JPN)", eType.B1, 3145728, true, true);
        /// <summary>Final Fight 2 (JPN)</summary>
        public static readonly VCSNES Title091 = new VCSNES(091, 0xC969FC68, new DateTime(2014, 08, 27), "Final Fight 2 (JPN)", eType.A2, 1310720, false, true);
        /// <summary>Mega Man 7 (USA/EUR)/Mega Man X3 (USA/EUR)/Demon's Crest (USA/EUR/JPN)/Natsume Championship Wrestling (EUR)</summary>
        public static readonly VCSNES Title092 = new VCSNES(092, 0xA27A572C, new DateTime(2014, 08, 28), "Mega Man 7 (USA/EUR)/Mega Man X3 (USA/EUR)/Demon's Crest (USA/EUR/JPN)/Natsume Championship Wrestling (EUR)", eType.B1, 2097152, true, false);
        /// <summary>Nobunaga's Ambition (USA/EUR)</summary>
        public static readonly VCSNES Title093 = new VCSNES(093, 0xB029222E, new DateTime(2014, 09, 04), "Nobunaga's Ambition (USA/EUR)", eType.B1, 524288, true, true);
        /// <summary>Breath of Fire (USA/EUR/JPN)</summary>
        public static readonly VCSNES Title094 = new VCSNES(094, 0xA0C2ECC7, new DateTime(2014, 09, 10), "Breath of Fire (USA/EUR/JPN)", eType.B1, 1572864, true, false);
        /// <summary>Wild Guns (USA/EUR)</summary>
        public static readonly VCSNES Title095 = new VCSNES(095, 0xB03D8A2F, new DateTime(2014, 09, 18), "Wild Guns (USA/EUR)", eType.B1, 1048576, true, true);
        /// <summary>Heisei Shin Onigashima Part 1 (JPN)</summary>
        public static readonly VCSNES Title096 = new VCSNES(096, 0xA9603BAD, new DateTime(2014, 09, 24), "Heisei Shin Onigashima Part 1 (JPN)", eType.B1, 3145728, true, true);
        /// <summary>Heisei Shin Onigashima Part 2 (JPN)</summary>
        public static readonly VCSNES Title097 = new VCSNES(097, 0xA7891654, new DateTime(2014, 09, 24), "Heisei Shin Onigashima Part 2 (JPN)", eType.B1, 3145728, true, true);
        /// <summary>Super Ninja Boy (JPN)</summary>
        public static readonly VCSNES Title098 = new VCSNES(098, 0xAA64B1BE, new DateTime(2014, 10, 01), "Super Ninja Boy (JPN)", eType.B1, 1048576, true, false);
        /// <summary>Donkey Kong Country (EUR)</summary>
        public static readonly VCSNES Title099 = new VCSNES(099, 0xC2ECC888, new DateTime(2014, 10, 16), "Donkey Kong Country (EUR)", eType.A1, 4194304, false, true);
        /// <summary>Donkey Kong Country 2: Diddy's Kong Quest (USA/EUR/JPN)</summary>
        public static readonly VCSNES Title100 = new VCSNES(100, 0xCB84B720, new DateTime(2014, 10, 23), "Donkey Kong Country 2: Diddy's Kong Quest (USA/EUR/JPN)", eType.A1, 4194304, false, true);
        /// <summary>Final Fight 3 (JPN)</summary>
        public static readonly VCSNES Title101 = new VCSNES(101, 0x9F042607, new DateTime(2014, 10, 29), "Final Fight 3 (JPN)", eType.B1, 3145728, false, true);
        /// <summary>Super Nobunaga's Ambition (JPN)</summary>
        public static readonly VCSNES Title102 = new VCSNES(102, 0xB90ECAF1, new DateTime(2014, 10, 29), "Super Nobunaga's Ambition (JPN)", eType.B1, 524288, true, true);
        /// <summary>Donkey Kong Country 3: Dixie Kong's Double Trouble! (USA/EUR)</summary>
        public static readonly VCSNES Title103 = new VCSNES(103, 0x9E7E3D65, new DateTime(2014, 10, 30), "Donkey Kong Country 3: Dixie Kong's Double Trouble! (USA/EUR)", eType.B1, 4194304, true, true);
        /// <summary>Donkey Kong Country 3: Dixie Kong's Double Trouble! (JPN)</summary>
        public static readonly VCSNES Title104 = new VCSNES(104, 0x9A837593, new DateTime(2014, 11, 26), "Donkey Kong Country 3: Dixie Kong's Double Trouble! (JPN)", eType.B1, 4194304, true, true);
        /// <summary>Donkey Kong Country (JPN)</summary>
        public static readonly VCSNES Title105 = new VCSNES(105, 0xD3B2BDB9, new DateTime(2014, 11, 26), "Donkey Kong Country (JPN)", eType.A1, 4194304, false, true);
        /// <summary>Natsume Championship Wrestling (USA)</summary>
        public static readonly VCSNES Title106 = new VCSNES(106, 0xA279FE7D, new DateTime(2014, 12, 18), "Natsume Championship Wrestling (USA)", eType.B1, 2097152, true, true);
        /// <summary>Cybernator (JPN)</summary>
        public static readonly VCSNES Title107 = new VCSNES(107, 0xA0506A67, new DateTime(2015, 01, 14), "Cybernator (JPN)", eType.B1, 1048576, false, true);
        /// <summary>Axelay (USA/EUR)</summary>
        public static readonly VCSNES Title108 = new VCSNES(108, 0xA54DC0F6, new DateTime(2015, 01, 15), "Axelay (USA/EUR)", eType.B1, 1048576, true, true);
        /// <summary>Last Bible III (JPN)</summary>
        public static readonly VCSNES Title109 = new VCSNES(109, 0xACC8B7A7, new DateTime(2015, 01, 28), "Last Bible III (JPN)", eType.B1, 3145728, true, false);
        /// <summary>Sutte Hakkun (JPN)</summary>
        public static readonly VCSNES Title110 = new VCSNES(110, 0xB864C674, new DateTime(2015, 02, 04), "Sutte Hakkun (JPN)", eType.B1, 3145728, true, true);
        /// <summary>Heracles no Eikō IV: Kamigami kara no Okurimono (JPN)</summary>
        public static readonly VCSNES Title111 = new VCSNES(111, 0x964937AF, new DateTime(2015, 02, 10), "Heracles no Eikō IV: Kamigami kara no Okurimono (JPN)", eType.B1, 2097152, true, true);
        /// <summary>Axelay (JPN)</summary>
        public static readonly VCSNES Title112 = new VCSNES(112, 0xA6D6CAE0, new DateTime(2015, 02, 25), "Axelay (JPN)", eType.B1, 1048576, true, true);
        /// <summary>Pac-Attack (EUR)</summary>
        public static readonly VCSNES Title113 = new VCSNES(113, 0x7D9E5904, new DateTime(2015, 02, 26), "Pac-Attack (EUR)", eType.B1, 524288, true, true);
        /// <summary>Donkey Kong Country (USA)</summary>
        public static readonly VCSNES Title114 = new VCSNES(114, 0xD6FF8995, new DateTime(2015, 02, 26), "Donkey Kong Country (USA)", eType.A1, 4194304, false, true);
        /// <summary>Metal Marines (JPN)</summary>
        public static readonly VCSNES Title115 = new VCSNES(115, 0xA56BB098, new DateTime(2015, 03, 04), "Metal Marines (JPN)", eType.B1, 1572864, true, true);
        /// <summary>Cosmo Gang the Puzzle (JPN)</summary>
        public static readonly VCSNES Title116 = new VCSNES(116, 0xA122EEC0, new DateTime(2015, 04, 28), "Cosmo Gang the Puzzle (JPN)", eType.B1, 524288, true, true);
        /// <summary>Metal Marines (USA)</summary>
        public static readonly VCSNES Title117 = new VCSNES(117, 0x94AB097D, new DateTime(2015, 05, 07), "Metal Marines (USA)", eType.B1, 1572864, true, true);
        /// <summary>Taikō Risshiden (JPN)</summary>
        public static readonly VCSNES Title118 = new VCSNES(118, 0x9B5FB35D, new DateTime(2015, 05, 20), "Taikō Risshiden (JPN)", eType.B1, 1572864, true, true);
        /// <summary>Super E.D.F.: Earth Defense Force (USA)</summary>
        public static readonly VCSNES Title119 = new VCSNES(119, 0xB11A158A, new DateTime(2015, 05, 21), "Super E.D.F.: Earth Defense Force (USA)", eType.B2, 1048576, true, true);
        /// <summary>Rival Turf! (USA)</summary>
        public static readonly VCSNES Title120 = new VCSNES(120, 0xACEEFB53, new DateTime(2015, 05, 28), "Rival Turf! (USA)", eType.B2, 1048576, true, true);
        /// <summary>Pac-Attack (USA)</summary>
        public static readonly VCSNES Title121 = new VCSNES(121, 0xA85E5134, new DateTime(2015, 06, 04), "Pac-Attack (USA)", eType.B1, 524288, true, true);
        /// <summary>Live A Live (JPN)</summary>
        public static readonly VCSNES Title122 = new VCSNES(122, 0xA4570B77, new DateTime(2015, 06, 24), "Live A Live (JPN)", eType.B1, 2097152, true, false);
        /// <summary>Majin Tensei (JPN)</summary>
        public static readonly VCSNES Title123 = new VCSNES(123, 0x94F0D47E, new DateTime(2015, 07, 15), "Majin Tensei (JPN)", eType.B1, 1572864, true, true);
        /// <summary>Genghis Khan II: Clan of the Gray Wolf (JPN)</summary>
        public static readonly VCSNES Title124 = new VCSNES(124, 0xABDE2222, new DateTime(2015, 07, 22), "Genghis Khan II: Clan of the Gray Wolf (JPN)", eType.B1, 1572864, true, true);
        /// <summary>Albert Odyssey (JPN)</summary>
        public static readonly VCSNES Title125 = new VCSNES(125, 0xA254090C, new DateTime(2015, 07, 29), "Albert Odyssey (JPN)", eType.B1, 1048576, true, true);
        /// <summary>Super Mario RPG: Legend of the Seven Stars (USA/EUR/JPN)</summary>
        public static readonly VCSNES Title126 = new VCSNES(126, 0xB80A3F81, new DateTime(2015, 08, 05), "Super Mario RPG: Legend of the Seven Stars (USA/EUR/JPN)", eType.B2, 4194304, true, true);
        /// <summary>Gussun Oyoyo (JPN)</summary>
        public static readonly VCSNES Title127 = new VCSNES(127, 0x8BDD22FE, new DateTime(2015, 08, 19), "Gussun Oyoyo (JPN)", eType.B1, 1572864, true, true);
        /// <summary>Genghis Khan II: Clan of the Gray Wolf (USA)</summary>
        public static readonly VCSNES Title128 = new VCSNES(128, 0x9D738126, new DateTime(2015, 08, 20), "Genghis Khan II: Clan of the Gray Wolf (USA)", eType.B2, 1048576, true, true);
        /// <summary>Super E.D.F.: Earth Defense Force (JPN)</summary>
        public static readonly VCSNES Title129 = new VCSNES(129, 0xA64C2EA7, new DateTime(2015, 08, 26), "Super E.D.F.: Earth Defense Force (JPN)", eType.B2, 1048576, true, true);
        /// <summary>The Ignition Factor (USA)</summary>
        public static readonly VCSNES Title130 = new VCSNES(130, 0xADC96E2B, new DateTime(2015, 09, 24), "The Ignition Factor (USA)", eType.B2, 1048576, true, true);
        /// <summary>Rushing Beat (JPN)</summary>
        public static readonly VCSNES Title131 = new VCSNES(131, 0x9C0BB56D, new DateTime(2015, 09, 30), "Rushing Beat (JPN)", eType.B1, 1048576, true, true);
        /// <summary>Pac-Man 2: The New Adventures (USA/EUR)</summary>
        public static readonly VCSNES Title132 = new VCSNES(132, 0xBF36AC9B, new DateTime(2015, 10, 08), "Pac-Man 2: The New Adventures (USA/EUR)", eType.B2, 1572864, true, false);
        /// <summary>Power Instinct (JPN)</summary>
        public static readonly VCSNES Title133 = new VCSNES(133, 0xB46F5D41, new DateTime(2015, 11, 11), "Power Instinct (JPN)", eType.B2, 3145728, true, true);
        /// <summary>Treasure of the Rudras (JPN)</summary>
        public static readonly VCSNES Title134 = new VCSNES(134, 0xA9BC61A7, new DateTime(2015, 12, 02), "Treasure of the Rudras (JPN)", eType.B2, 4194304, true, true);
        /// <summary>Metal Slader Glory (JPN)</summary>
        public static readonly VCSNES Title135 = new VCSNES(135, 0xC364C797, new DateTime(2015, 12, 09), "Metal Slader Glory (JPN)", eType.B2, 3145728, true, false);
        /// <summary>Majin Tensei II: Spiral Nemesis (JPN)</summary>
        public static readonly VCSNES Title136 = new VCSNES(136, 0xACB1A9B5, new DateTime(2016, 06, 01), "Majin Tensei II: Spiral Nemesis (JPN)", eType.B2, 3145728, true, true);
        /// <summary>Kai: Tsukikomori (JPN)</summary>
        public static readonly VCSNES Title137 = new VCSNES(137, 0xBE4D1CB6, new DateTime(2016, 09, 07), "Kai: Tsukikomori (JPN)", eType.B2, 4194304, true, true);
        /// <summary>Wrecking Crew '98 (JPN)</summary>
        public static readonly VCSNES Title138 = new VCSNES(138, 0xCF1E7641, new DateTime(2016, 09, 28), "Wrecking Crew '98 (JPN)", eType.B2, 2097152, true, false);
        /// <summary>Darius Twin (JPN)</summary>
        public static readonly VCSNES Title139 = new VCSNES(139, 0xB0D878C7, new DateTime(2016, 10, 12), "Darius Twin (JPN)", eType.B2, 1048576, true, true);
        /// <summary>Space Invaders: The Original Game (JPN)</summary>
        public static readonly VCSNES Title140 = new VCSNES(140, 0xC3918529, new DateTime(2016, 10, 12), "Space Invaders: The Original Game (JPN)", eType.B2, 262144, true, true);
        /// <summary>Fire Fighting (JPN)</summary>
        public static readonly VCSNES Title141 = new VCSNES(141, 0xB13EE167, new DateTime(2017, 03, 29), "Fire Fighting (JPN)", eType.B2, 1048576, true, true);
        /// <summary>Idol Janshi Suchie-Pai (JPN)</summary>
        public static readonly VCSNES Title142 = new VCSNES(142, 0xC909118B, new DateTime(2017, 03, 29), "Idol Janshi Suchie-Pai (JPN)", eType.B2, 2097152, true, true);
    }
}

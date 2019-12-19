using System;

namespace PhacoxsInjector
{
    public class VCGBA : WiiUVC
    {
        public VCGBA(int index, uint hash, DateTime release, string title)
            : base(index, hash, release, title)
        {
        }

        public VCGBA(uint hash)
            : base(hash)
        {
        }

        public override string ToString()
        {
            return "Hash: " + Hash.ToString("X8") + ", Release date: " + Release.ToString("yyyy/MM/dd") + " \nTitle: " + Title;
        }

        public static VCGBA GetVC(uint hash)
        {
            switch (hash)
            {
                case 0x01A77FE8: return VCGBA.Title01;
                case 0xEF6743BA: return VCGBA.Title02;
                case 0xCE856191: return VCGBA.Title03;
                case 0x56F11002: return VCGBA.Title04;
                case 0xA8D3C38A: return VCGBA.Title05;
                case 0x0C80579D: return VCGBA.Title06;
                case 0x70915748: return VCGBA.Title07;
                case 0x99A634C4: return VCGBA.Title08;
                case 0x94C2FD87: return VCGBA.Title09;
                case 0x4F414483: return VCGBA.Title10;
                case 0xB0435585: return VCGBA.Title11;
                case 0x4B9986EA: return VCGBA.Title12;
                case 0x5C87BC8E: return VCGBA.Title13;
                case 0xC6BA7741: return VCGBA.Title14;
                case 0x7C72A26F: return VCGBA.Title15;
                case 0x95D7E221: return VCGBA.Title16;
                case 0xABE1BE9F: return VCGBA.Title17;
                case 0x3B9FA9AE: return VCGBA.Title18;
                case 0xC134684C: return VCGBA.Title19;
                case 0x09F06972: return VCGBA.Title20;
                case 0xD230D0FE: return VCGBA.Title21;
                case 0xA5ED5CC6: return VCGBA.Title22;
                case 0x2C830C37: return VCGBA.Title23;
                case 0xFD16CD04: return VCGBA.Title24;
                case 0x3A006DCB: return VCGBA.Title25;
                case 0x05B21B89: return VCGBA.Title26;
                case 0x1CC00415: return VCGBA.Title27;
                case 0x33B294C3: return VCGBA.Title28;
                case 0x44BB0C8F: return VCGBA.Title29;
                case 0x399D2324: return VCGBA.Title30;
                case 0xA4EFA860: return VCGBA.Title31;
                case 0x36A8B996: return VCGBA.Title32;
                case 0xC19E196B: return VCGBA.Title33;
                case 0x3DC186C0: return VCGBA.Title34;
                case 0x3EA763EC: return VCGBA.Title35;
                case 0xEE2CF466: return VCGBA.Title36;
                case 0xC1274853: return VCGBA.Title37;
                case 0x2140CBE7: return VCGBA.Title38;
                case 0xAB9CB481: return VCGBA.Title39;
                case 0xC035F1AA: return VCGBA.Title40;
                case 0xC674863A: return VCGBA.Title41;
                case 0xFEEEF572: return VCGBA.Title42;
                case 0x3BB37AC7: return VCGBA.Title43;
                case 0xE115ED51: return VCGBA.Title44;
                case 0xCCACB2CC: return VCGBA.Title45;
                case 0xEC6772C5: return VCGBA.Title46;
                case 0xA11E0980: return VCGBA.Title47;
                case 0x8F918B8B: return VCGBA.Title48;
                case 0xFE058A7F: return VCGBA.Title49;
                case 0xB486B133: return VCGBA.Title50;
                case 0x1BE5D898: return VCGBA.Title51;
                case 0xCD925B01: return VCGBA.Title52;
                case 0xBE53BB55: return VCGBA.Title53;
                default: return null;
            }
        }

        /// <summary>Mario vs. Donkey Kong (USA/EUR/JPN)/The Legend of Zelda: The Minish Cap (USA/EUR/JPN)/Kirby and the Amazing Mirror (USA/EUR/JPN)/Advance Wars (USA/EUR)/Game Boy Wars Advance 1+2 (JPN)</summary>
        public static readonly VCGBA Title01 = new VCGBA(01, 0x01A77FE8, new DateTime(2014, 4, 3), "Mario vs. Donkey Kong (USA/EUR/JPN)/The Legend of Zelda: The Minish Cap (USA/EUR/JPN)/\nKirby & the Amazing Mirror (USA/EUR/JPN)/Advance Wars (USA/EUR)/Game Boy Wars Advance 1+2 (JPN)");
        /// <summary>WarioWare, Inc.: Mega Microgames! (USA)/Mario and Luigi: Superstar Saga (USA)/F-Zero Maximum Velocity (USA)/Metroid Fusion (USA/EUR/JPN)</summary>
        public static readonly VCGBA Title02 = new VCGBA(02, 0xEF6743BA, new DateTime(2014, 4, 3), "WarioWare, Inc.: Mega Microgames! (USA)/Mario & Luigi: Superstar Saga (USA)/\nF-Zero Maximum Velocity (USA)/Metroid Fusion (USA/EUR/JPN)");
        /// <summary>Golden Sun (USA/EUR/JPN)</summary>
        public static readonly VCGBA Title03 = new VCGBA(03, 0xCE856191, new DateTime(2014, 4, 3), "Golden Sun (USA/EUR/JPN)");
        /// <summary>Super Mario World: Super Mario Advance 2 (USA/EUR/JPN)/Kirby: Nightmare in Dream Land (USA/EUR/JPN)/Mario Golf: Advance Tour (USA/EUR/JPN)</summary>
        public static readonly VCGBA Title04 = new VCGBA(04, 0x56F11002, new DateTime(2014, 4, 3), "Super Mario World: Super Mario Advance 2 (USA/EUR/JPN)/\nKirby: Nightmare in Dream Land (USA/EUR/JPN)/Mario Golf: Advance Tour (USA/EUR/JPN)");
        /// <summary>WarioWare, Inc.: Mega Microgames! (EUR/JPN)/Mario and Luigi: Superstar Saga (EUR/JPN)/F-Zero: Maximum Velocity (EUR/JPN)</summary>
        public static readonly VCGBA Title05 = new VCGBA(05, 0xA8D3C38A, new DateTime(2014, 4, 3), "WarioWare, Inc.: Mega Microgames! (EUR/JPN)/Mario & Luigi: Superstar Saga (EUR/JPN)/\nF-Zero: Maximum Velocity (EUR/JPN)");
        /// <summary>Yoshi's Island: Super Mario Advance 3 (USA)</summary>
        public static readonly VCGBA Title06 = new VCGBA(06, 0x0C80579D, new DateTime(2014, 4, 24), "Yoshi's Island: Super Mario Advance 3 (USA)");
        /// <summary>Yoshi's Island: Super Mario Advance 3 (EUR/JPN)</summary>
        public static readonly VCGBA Title07 = new VCGBA(07, 0x70915748, new DateTime(2014, 4, 24), "Yoshi's Island: Super Mario Advance 3 (EUR/JPN)");
        /// <summary>Wario Land 4 (USA/JPN)/Mario Tennis: Power Tour (USA/JPN)/Klonoa: Empire of Dreams (USA/JPN)/Mr. Driller 2 (USA/JPN)</summary>
        public static readonly VCGBA Title08 = new VCGBA(08, 0x99A634C4, new DateTime(2014, 4, 30), "Wario Land 4 (USA/JPN)/Mario Tennis: Power Tour (USA/JPN)/\nKlonoa: Empire of Dreams (USA/JPN)/Mr. Driller 2 (USA/JPN)");
        /// <summary>Fire Emblem (The Blazing Blade) (USA/EUR/JPN))/Mario Pinball Land (USA/EUR/JPN)/Castlevania: Harmony of Dissonance (USA/EUR/JPN)/Pac-Man Collection (USA/EUR/JPN</summary>
        public static readonly VCGBA Title09 = new VCGBA(09, 0x94C2FD87, new DateTime(2014, 5, 14), "Fire Emblem (The Blazing Blade) (USA/EUR/JPN))/Mario Pinball Land (USA/EUR/JPN)/\nCastlevania: Harmony of Dissonance (USA/EUR/JPN)/Pac-Man Collection (USA/EUR/JPN");
        /// <summary>Wario Land 4 (EUR)/Mario Tennis: Power Tour (EUR)/Klonoa: Empire of Dreams (EUR)/Mr. Driller 2 (EUR)</summary>
        public static readonly VCGBA Title10 = new VCGBA(10, 0x4F414483, new DateTime(2014, 5, 15), "Wario Land 4 (EUR)/Mario Tennis: Power Tour (EUR)/Klonoa: Empire of Dreams (EUR)/\nMr. Driller 2 (EUR)");
        /// <summary>Metroid: Zero Mission (USA/EUR/JPN)/Castlevania: Circle of the Moon (USA/EUR/JPN)</summary>
        public static readonly VCGBA Title11 = new VCGBA(11, 0xB0435585, new DateTime(2014, 6, 19), "Metroid: Zero Mission (USA/EUR/JPN)/Castlevania: Circle of the Moon (USA/EUR/JPN)");
        /// <summary>Mega Man Battle Network (USA/EUR/JPN)/Mega Man Battle Chip Challenge (USA/EUR/JPN)/Golden Sun: The Lost Age (USA/EUR/JPN)/Namco Museum (USA/EUR/JPN)/Kuru Kuru Kururin (USA/EUR/JPN)</summary>
        public static readonly VCGBA Title12 = new VCGBA(12, 0x4B9986EA, new DateTime(2014, 7, 9), "Mega Man Battle Network (USA/EUR/JPN)/Mega Man Battle Chip Challenge (USA/EUR/JPN)/\nGolden Sun: The Lost Age (USA/EUR/JPN)/Namco Museum (USA/EUR/JPN)/Kuru Kuru Kururin (USA/EUR/JPN)");
        /// <summary>Super Mario Advance (USA/EUR/JPN)</summary>
        public static readonly VCGBA Title13 = new VCGBA(13, 0x5C87BC8E, new DateTime(2014, 7, 16), "Super Mario Advance (USA/EUR/JPN)");
        /// <summary>Super Mario Advance 4: Super Mario Bros. 3 (USA/EUR/JPN)/Mega Man Zero (USA/EUR/JPN)/Fire Emblem: The Sacred Stones (USA/EUR/JPN)/DK: King of Swing (USA/EUR/JPN)</summary>
        public static readonly VCGBA Title14 = new VCGBA(14, 0xC6BA7741, new DateTime(2014, 8, 6), "Super Mario Advance 4: Super Mario Bros. 3 (USA/EUR/JPN)/Mega Man Zero (USA/EUR/JPN)/\nFire Emblem: The Sacred Stones (USA/EUR/JPN)/DK: King of Swing (USA/EUR/JPN)");
        /// <summary>Mario Party Advance (JPN)/Castlevania: Aria of Sorrow (USA/EUR/JPN)/F-Zero: GP Legend (USA/EUR/JPN)/Pokémon Pinball: Ruby and Sapphire (USA/EUR/JPN)</summary>
        public static readonly VCGBA Title15 = new VCGBA(15, 0x7C72A26F, new DateTime(2014, 10, 1), "Mario Party Advance (JPN)/Castlevania: Aria of Sorrow (USA/EUR/JPN)/\nF-Zero: GP Legend (USA/EUR/JPN)/Pokémon Pinball: Ruby & Sapphire (USA/EUR/JPN)");
        /// <summary>Mega Man Battle Network 2 (USA/JPN)</summary>
        public static readonly VCGBA Title16 = new VCGBA(16, 0x95D7E221, new DateTime(2014, 11, 12), "Mega Man Battle Network 2 (USA/JPN)");
        /// <summary>Mario Kart: Super Circuit (USA/EUR/JPN)</summary>
        public static readonly VCGBA Title17 = new VCGBA(17, 0xABE1BE9F, new DateTime(2014, 11, 13), "Mario Kart: Super Circuit (USA/EUR/JPN)");
        /// <summary>Mega Man Battle Network 2 (EUR)/Mega Man Battle Network 3 (Any) (USA/EUR/JPN)</summary>
        public static readonly VCGBA Title18 = new VCGBA(18, 0x3B9FA9AE, new DateTime(2014, 12, 17), "Mega Man Battle Network 2 (EUR)/Mega Man Battle Network 3 (Any) (USA/EUR/JPN)");
        /// <summary>Mario Party Advance (USA/EUR)</summary>
        public static readonly VCGBA Title19 = new VCGBA(19, 0xC134684C, new DateTime(2014, 12, 25), "Mario Party Advance (USA/EUR)");
        /// <summary>Napoleon (JPN)</summary>
        public static readonly VCGBA Title20 = new VCGBA(20, 0x09F06972, new DateTime(2015, 1, 7), "Napoleon (JPN)");
        /// <summary>Mega Man Zero 2 (USA/EUR/JPN)/Harvest Moon: Friends of Mineral Town (USA)/Harvest Moon: More Friends of Mineral Town (USA)</summary>
        public static readonly VCGBA Title21 = new VCGBA(21, 0xD230D0FE, new DateTime(2015, 1, 7), "Mega Man Zero 2 (USA/EUR/JPN)/Harvest Moon: Friends of Mineral Town (USA)/\nHarvest Moon: More Friends of Mineral Town (USA)");
        /// <summary>Mega Man and Bass (USA/EUR/JPN)/Super Ghouls 'n Ghosts (USA/EUR/JPN)/Kotoba no Puzzle: Mojipittan (JPN)</summary>
        public static readonly VCGBA Title22 = new VCGBA(22, 0xA5ED5CC6, new DateTime(2015, 1, 14), "Mega Man & Bass (USA/EUR/JPN)/Super Ghouls 'n Ghosts (USA/EUR/JPN)/\nKotoba no Puzzle: Mojipittan (JPN)");
        /// <summary>Mega Man Battle Network 5 (Any) (USA/EUR/JPN)/Family Tennis Advance (JPN)/Advance Wars 2: Black Hole Rising (USA/EUR)/Sonic Advance (JPN)</summary>
        public static readonly VCGBA Title23 = new VCGBA(23, 0x2C830C37, new DateTime(2015, 2, 18), "Mega Man Battle Network 5 (Any) (USA/EUR/JPN)/Family Tennis Advance (JPN)/\nAdvance Wars 2: Black Hole Rising (USA/EUR)/Sonic Advance (JPN)");
        /// <summary>Mega Man Zero 4 (USA/EUR/JPN)/Super Street Fighter II Turbo Revival (USA/EUR/JPN)/Medabots (Metabee, Rokusho) (USA/EUR/JPN)/Phoenix Wright: Ace Attorney (JPN)/Mr. Driller (JPN)</summary>
        public static readonly VCGBA Title24 = new VCGBA(24, 0xFD16CD04, new DateTime(2015, 3, 11), "Mega Man Zero 4 (USA/EUR/JPN)/Super Street Fighter II Turbo Revival (USA/EUR/JPN)/\nMedabots (Metabee, Rokusho) (USA/EUR/JPN)/Phoenix Wright: Ace Attorney (JPN)/Mr. Driller (JPN)");
        /// <summary>Klonoa 2: Dream Champ Tournament (USA/JPN)</summary>
        public static readonly VCGBA Title25 = new VCGBA(25, 0x3A006DCB, new DateTime(2015, 3, 19), "Klonoa 2: Dream Champ Tournament (USA/JPN)");
        /// <summary>Mega Man Battle Network 4 (Any) (USA/EUR/JPN)</summary>
        public static readonly VCGBA Title26 = new VCGBA(26, 0x05B21B89, new DateTime(2015, 3, 25), "Mega Man Battle Network 4 (Any) (USA/EUR/JPN)");
        /// <summary>Mega Man Zero 3 (USA/EUR/JPN)/Final Fight One (USA/EUR/JPN)</summary>
        public static readonly VCGBA Title27 = new VCGBA(27, 0x1CC00415, new DateTime(2015, 3, 26), "Mega Man Zero 3 (USA/EUR/JPN)/Final Fight One (USA/EUR/JPN)");
        /// <summary>Onimusha Tactics (USA/EUR/JPN)/Densetsu no Stafy (JPN)/Tomato Adventure (JPN)</summary>
        public static readonly VCGBA Title28 = new VCGBA(28, 0x33B294C3, new DateTime(2015, 7, 29), "Onimusha Tactics (USA/EUR/JPN)/Densetsu no Stafy (JPN)/Tomato Adventure (JPN)");
        /// <summary>Magical Vacation (JPN)</summary>
        public static readonly VCGBA Title29 = new VCGBA(29, 0x44BB0C8F, new DateTime(2015, 8, 19), "Magical Vacation (JPN)");
        /// <summary>Fire Emblem: The Binding Blade (JPN)</summary>
        public static readonly VCGBA Title30 = new VCGBA(30, 0x399D2324, new DateTime(2015, 9, 2), "Fire Emblem: The Binding Blade (JPN)");
        /// <summary>Medabots AX (Metabee, Rokusho) (USA/EUR/JPN)/Car Battler Joe (USA)/Shining Force: Resurrection of the Dark Dragon (JPN)</summary>
        public static readonly VCGBA Title31 = new VCGBA(31, 0xA4EFA860, new DateTime(2015, 9, 17), "Medabots AX (Metabee, Rokusho) (USA/EUR/JPN)/Car Battler Joe (USA)/\nShining Force: Resurrection of the Dark Dragon (JPN)");
        /// <summary>Pocky and Rocky with Becky (USA/EUR)</summary>
        public static readonly VCGBA Title32 = new VCGBA(32, 0x36A8B996, new DateTime(2015, 10, 8), "Pocky & Rocky with Becky (USA/EUR)");
        /// <summary>Contra Advance: The Alien Wars EX (USA/EUR/JPN)/Konami Krazy Racers (USA/EUR/JPN)/Sonic Advance 2 (JPN)</summary>
        public static readonly VCGBA Title33 = new VCGBA(33, 0xC19E196B, new DateTime(2015, 10, 15), "Contra Advance: The Alien Wars EX (USA/EUR/JPN)/Konami Krazy Racers (USA/EUR/JPN)/\nSonic Advance 2 (JPN)");
        /// <summary>ChuChu Rocket! (JPN)</summary>
        public static readonly VCGBA Title34 = new VCGBA(34, 0x3DC186C0, new DateTime(2015, 10, 21), "ChuChu Rocket! (JPN)");
        /// <summary>Mega Man Battle Network 6 (Any) (USA/EUR/JPN)</summary>
        public static readonly VCGBA Title35 = new VCGBA(35, 0x3EA763EC, new DateTime(2015, 11, 18), "Mega Man Battle Network 6 (Any) (USA/EUR/JPN)");
        /// <summary>Phoenix Wright: Ace Attorney: Justice for All (JPN)</summary>
        public static readonly VCGBA Title36 = new VCGBA(36, 0xEE2CF466, new DateTime(2015, 12, 2), "Phoenix Wright: Ace Attorney: Justice for All (JPN)");
        /// <summary>Polarium Advance (USA/EUR/JPN)/Shining Soul II (JPN)/Kururin Paradise (JPN)</summary>
        public static readonly VCGBA Title37 = new VCGBA(37, 0xC1274853, new DateTime(2015, 12, 9), "Polarium Advance (USA/EUR/JPN)/Shining Soul II (JPN)/Kururin Paradise (JPN)");
        /// <summary>Drill Dozer (USA/JPN)/Game and Watch Gallery 4 (USA/EUR/JPN)/Kawa no Nushi Tsuri 3 and 4 (JPN)</summary>
        public static readonly VCGBA Title38 = new VCGBA(38, 0x2140CBE7, new DateTime(2015, 12, 10), "Drill Dozer (USA/JPN)/Game & Watch Gallery 4 (USA/EUR/JPN)/\nKawa no Nushi Tsuri 3 & 4 (JPN)");
        /// <summary>F-Zero Climax (JPN)</summary>
        public static readonly VCGBA Title39 = new VCGBA(39, 0xAB9CB481, new DateTime(2015, 12, 16), "F-Zero Climax (JPN)");
        /// <summary>Mother 3 (JPN)</summary>
        public static readonly VCGBA Title40 = new VCGBA(40, 0xC035F1AA, new DateTime(2015, 12, 17), "Mother 3 (JPN)");
        /// <summary>Final Fantasy VI Advance (JPN)</summary>
        public static readonly VCGBA Title41 = new VCGBA(41, 0xC674863A, new DateTime(2015, 12, 22), "Final Fantasy VI Advance (JPN)");
        /// <summary>Final Fantasy I and II: Dawn of Souls (JPN)</summary>
        public static readonly VCGBA Title42 = new VCGBA(42, 0xFEEEF572, new DateTime(2016, 1, 6), "Final Fantasy I & II: Dawn of Souls (JPN)");
        /// <summary>Drill Dozer (EUR)/Sennen Kazoku (JPN)</summary>
        public static readonly VCGBA Title43 = new VCGBA(43, 0x3BB37AC7, new DateTime(2016, 1, 7), "Drill Dozer (EUR)/Sennen Kazoku (JPN)");
        /// <summary>Rockman EXE 4.5 Real Operation (JPN)</summary>
        public static readonly VCGBA Title44 = new VCGBA(44, 0xE115ED51, new DateTime(2016, 1, 13), "Rockman EXE 4.5 Real Operation (JPN)");
        /// <summary>Final Fantasy Tactics Advance (USA/EUR/JPN)</summary>
        public static readonly VCGBA Title45 = new VCGBA(45, 0xCCACB2CC, new DateTime(2016, 1, 28), "Final Fantasy Tactics Advance (USA/EUR/JPN)");
        /// <summary>Lufia: The Ruins of Lore (JPN)/Chocobo Land: A Game of Dice (JPN)</summary>
        public static readonly VCGBA Title46 = new VCGBA(46, 0xEC6772C5, new DateTime(2016, 2, 10), "Lufia: The Ruins of Lore (JPN)/Chocobo Land: A Game of Dice (JPN)");
        /// <summary>Pokémon Mystery Dungeon: Red Rescue Team (USA/EUR/JPN)</summary>
        public static readonly VCGBA Title47 = new VCGBA(47, 0xA11E0980, new DateTime(2016, 2, 11), "Pokémon Mystery Dungeon: Red Rescue Team (USA/EUR/JPN)");
        /// <summary>Phoenix Wright: Ace Attorney: Trials and Tribulations (JPN)/Shining Soul (JPN)</summary>
        public static readonly VCGBA Title48 = new VCGBA(48, 0x8F918B8B, new DateTime(2016, 2, 17), "Phoenix Wright: Ace Attorney: Trials and Tribulations (JPN)/Shining Soul (JPN)");
        /// <summary>Densetsu no Stafy 2 (JPN)</summary>
        public static readonly VCGBA Title49 = new VCGBA(49, 0xFE058A7F, new DateTime(2016, 3, 16), "Densetsu no Stafy 2 (JPN)");
        /// <summary>Densetsu no Stafy 3 (JPN)/Medarot Navi (Kabuto, Kuwagata) (JPN)</summary>
        public static readonly VCGBA Title50 = new VCGBA(50, 0xB486B133, new DateTime(2016, 4, 6), "Densetsu no Stafy 3 (JPN)/Medarot Navi (Kabuto, Kuwagata) (JPN)");
        /// <summary>Final Fantasy IV Advance (JPN)/Final Fantasy V Advance (JPN)</summary>
        public static readonly VCGBA Title51 = new VCGBA(51, 0x1BE5D898, new DateTime(2016, 4, 13), "Final Fantasy IV Advance (JPN)/Final Fantasy V Advance (JPN)");
        /// <summary>Sonic Advance 3 (JPN)</summary>
        public static readonly VCGBA Title52 = new VCGBA(52, 0xCD925B01, new DateTime(2016, 5, 25), "Sonic Advance 3 (JPN)");
        /// <summary>Rayman Advance (USA/EUR)/Rayman 3: Hoodlum Havoc (USA/EUR)/Kawa no Nushi Tsuri 5 (JPN)/Granbo (JPN)</summary>
        public static readonly VCGBA Title53 = new VCGBA(53, 0xBE53BB55, new DateTime(2016, 6, 1), "Rayman Advance (USA/EUR)/Rayman 3: Hoodlum Havoc (USA/EUR)/\nKawa no Nushi Tsuri 5 (JPN)/Granbo (JPN)");
    }
}

using System;

namespace PhacoxsInjector
{
    public class VCN64 : WiiUVC
    {
        public readonly int SVN;

        public VCN64(int index, uint hash, DateTime release, string title, int svn)
            : base(index, hash, release, title)
        {
            SVN = svn;
        }

        public VCN64(uint hash)
            : base(hash)
        {
            SVN = 0;
        }

        public override string ToString()
        {
            return "Hash: " + Hash.ToString("X8") + ", SVN: " + SVN.ToString() + " TIME: " + Release.ToString("yyyy/MM/dd hh:mm:ss") + "\r\nTitle: " + Title;
        }

        public static VCN64 GetVC(uint hash)
        {
            switch (hash)
            {
                case 0xFB245F10: return VCN64.Title01;
                case 0x8EF60284: return VCN64.Title02;
                case 0xF042E451: return VCN64.Title03;
                case 0xAE933905: return VCN64.Title04;
                case 0xCEB7A833: return VCN64.Title05;
                case 0x7EB7B97D: return VCN64.Title06;
                case 0x17BCC968: return VCN64.Title07;
                case 0x05F20995: return VCN64.Title08;
                case 0x8D3C196C: return VCN64.Title09;
                case 0x307DCE21: return VCN64.Title10;
                case 0xF41BC127: return VCN64.Title11;
                case 0x36C0456E: return VCN64.Title12;
                case 0x5559F831: return VCN64.Title13;
                case 0xD554D2E4: return VCN64.Title14;
                case 0x04F7D67F: return VCN64.Title15;
                case 0xC376B949: return VCN64.Title16;
                case 0xEE8855FF: return VCN64.Title17;
                case 0x71FC1731: return VCN64.Title18;
                case 0x967E7DF0: return VCN64.Title19;
                case 0xBE3CEC5F: return VCN64.Title20;
                case 0x89F2BC09: return VCN64.Title21;
                case 0xFED1FB48: return VCN64.Title22;
                case 0x724C4F5D: return VCN64.Title23;
                case 0x2AF3C23B: return VCN64.Title24;
                default: return null;
            }
        }

        /// <summary>Donkey Kong 64 (USA/EUR)/Super Mario 64 (USA/EUR/JPN)</summary>
        public static readonly VCN64 Title01 = new VCN64(01, 0xFB245F10, new DateTime(2015, 1, 20, 14, 12, 6), "Super Mario 64 (USA/EUR/JPN)/Donkey Kong 64 (USA/EUR)", 1680);
        /// <summary>Donkey Kong 64 (JPN)</summary>
        public static readonly VCN64 Title02 = new VCN64(02, 0x8EF60284, new DateTime(2015, 1, 27, 16, 8, 0), "Donkey Kong 64 (JPN)", 1690);
        /// <summary>The Legend of Zelda: Ocarina of Time (USA/EUR/JPN)</summary>
        public static readonly VCN64 Title03 = new VCN64(03, 0xF042E451, new DateTime(2015, 1, 30, 10, 49, 22), "The Legend of Zelda: Ocarina of Time (USA/EUR/JPN)", 1696);
        /// <summary>Paper Mario (USA/EUR/JPN)</summary>
        public static readonly VCN64 Title04 = new VCN64(04, 0xAE933905, new DateTime(2015, 3, 5, 15, 6, 57), "Paper Mario (USA/EUR/JPN)", 1743);
        /// <summary>Kirby 64: The Crystal Shards (JPN)</summary>
        public static readonly VCN64 Title05 = new VCN64(05, 0xCEB7A833, new DateTime(2015, 3, 19, 16, 15, 32), "Kirby 64: The Crystal Shards (JPN)", 1778);
        /// <summary>Kirby 64: The Crystal Shards (USA/EUR)</summary>
        public static readonly VCN64 Title06 = new VCN64(06, 0x7EB7B97D, new DateTime(2015, 3, 24, 13, 46, 36), "Kirby 64: The Crystal Shards (USA/EUR)", 1790);
        /// <summary>Mario Tennis (JPN)/1080º Snowboarding (JPN)</summary>
        public static readonly VCN64 Title07 = new VCN64(07, 0x17BCC968, new DateTime(2015, 5, 12, 17, 32, 21), "Mario Tennis (JPN)/1080º Snowboarding (JPN)", 1897);
        /// <summary>Mario Tennis (USA/EUR)/1080º Snowboarding (USA/EUR)</summary>
        public static readonly VCN64 Title08 = new VCN64(08, 0x05F20995, new DateTime(2015, 5, 20, 14, 34, 0), "Mario Tennis (USA/EUR)/1080º Snowboarding (USA/EUR)", 1918);
        /// <summary>Mario Golf (JPN)</summary>
        public static readonly VCN64 Title09 = new VCN64(09, 0x8D3C196C, new DateTime(2015, 6, 9, 11, 0, 28), "Mario Golf (JPN)", 1946);
        /// <summary>Mario Golf (USA/EUR)</summary>
        public static readonly VCN64 Title10 = new VCN64(10, 0x307DCE21, new DateTime(2015, 6, 16, 16, 9, 3), "Mario Golf (USA/EUR)", 1955);
        /// <summary>Star Fox 64 (USA/EUR/JPN)</summary>
        public static readonly VCN64 Title11 = new VCN64(11, 0xF41BC127, new DateTime(2015, 6, 30, 14, 7, 35), "Star Fox 64 (USA/EUR/JPN)", 1970);
        /// <summary>Sin and Punishment (USA/EUR/JPN)</summary>
        public static readonly VCN64 Title12 = new VCN64(12, 0x36C0456E, new DateTime(2015, 7, 16, 9, 20, 39), "Sin and Punishment (USA/EUR/JPN)", 1991);
        /// <summary>Mario Kart 64 (USA/EUR/JPN)</summary>
        public static readonly VCN64 Title13 = new VCN64(13, 0x5559F831, new DateTime(2015, 8, 18, 10, 7, 52), "Mario Kart 64 (USA/EUR/JPN)", 2043);
        /// <summary>Yoshi's Story (USA/EUR/JPN)</summary>
        public static readonly VCN64 Title14 = new VCN64(14, 0xD554D2E4, new DateTime(2015, 9, 15, 16, 19, 11), "Yoshi's Story (USA/EUR/JPN)", 2079);
        /// <summary>Wave Race 64 (JPN)</summary>
        public static readonly VCN64 Title15 = new VCN64(15, 0x04F7D67F, new DateTime(2015, 10, 22, 10, 15, 3), "Wave Race 64 (JPN)", 2109);
        /// <summary>Wave Race 64 (USA/EUR)</summary>
        public static readonly VCN64 Title16 = new VCN64(16, 0xC376B949, new DateTime(2015, 11, 18, 12, 41, 26), "Wave Race 64 (USA/EUR)", 2136);
        /// <summary>The Legend of Zelda: Majora's Mask (JPN)</summary>
        public static readonly VCN64 Title17 = new VCN64(17, 0xEE8855FF, new DateTime(2015, 12, 16, 16, 1, 23), "The Legend of Zelda: Majora's Mask (JPN)", 2170);
        /// <summary>The Legend of Zelda: Majora's Mask (USA/EUR)</summary>
        public static readonly VCN64 Title18 = new VCN64(18, 0x71FC1731, new DateTime(2016, 1, 5, 16, 50, 14), "The Legend of Zelda: Majora's Mask (USA/EUR)", 2190);
        /// <summary>Pokémon Snap (USA/EUR/JPN)</summary>
        public static readonly VCN64 Title19 = new VCN64(19, 0x967E7DF0, new DateTime(2016, 1, 8, 9, 42, 51), "Pokémon Snap (USA/EUR/JPN)", 2195);
        /// <summary>Mario Party 2 (USA/EUR/JPN)</summary>
        public static readonly VCN64 Title20 = new VCN64(20, 0xBE3CEC5F, new DateTime(2016, 2, 2, 10, 56, 10), "Mario Party 2 (USA/EUR/JPN)", 2234);
        /// <summary>Custom Robo V2 (JPN)</summary>
        public static readonly VCN64 Title21 = new VCN64(21, 0x89F2BC09, new DateTime(2016, 2, 26, 9, 3, 55), "Custom Robo V2 (JPN)", 2244);
        /// <summary>Ogre Battle 64: Person of Lordly Caliber (USA/EUR/JPN)</summary>
        public static readonly VCN64 Title22 = new VCN64(22, 0xFED1FB48, new DateTime(2016, 8, 30, 13, 57, 2), "Ogre Battle 64: Person of Lordly Caliber (USA/EUR/JPN)", 2395);
        /// <summary>Excitebike 64 (USA/EUR/JPN)</summary>
        public static readonly VCN64 Title23 = new VCN64(23, 0x724C4F5D, new DateTime(2016, 9, 14, 13, 42, 0), "Excitebike 64 (USA/EUR/JPN)", 2404);
        /// <summary>F-Zero X (USA/EUR/JPN)/Bomberman 64 (USA/EUR/JPN)/Harvest Moon 64 (USA/EUR/JPN)</summary>
        public static readonly VCN64 Title24 = new VCN64(24, 0x2AF3C23B, new DateTime(2016, 11, 18, 11, 44, 39), "F-Zero X (USA/EUR/JPN)/Bomberman 64 (USA/EUR/JPN)/Harvest Moon 64 (USA/EUR/JPN)", 2428);
    }
}

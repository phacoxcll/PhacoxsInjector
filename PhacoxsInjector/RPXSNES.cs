using System;
using System.IO;

namespace PhacoxsInjector
{
    public class RPXSNES : RPX
    {
        public enum VCType
        {
            A1,
            A2,
            B1,
            B2,
            Unknown
        }

        public VCType Type
        { private set; get; }

        public RPXSNES(string filename)
            : base(filename)
        {
            Type = GetVCType();
            if (Type == VCType.Unknown)
                throw new FormatException("It is not an RPXSNES file.");
        }

        public static void Inject(string sourcebase, string sourcerom, string destination,
            short widthTv = 1920, short widthDrc = 854, short heightTv = 1080, short heightDrc = 480)
        {
            RPXSNES rpx = new RPXSNES(sourcebase);
            rpx.Edit(sourcerom, widthTv, widthDrc, heightTv, heightDrc, destination);
        }

        private VCType GetVCType()
        {
            if (SectionName.Length == 30 &&
                SectionName[11] == ".fimport_sysapp" &&
                SectionName[12] == ".fimport_zlib125.rpl" &&
                SectionName[13] == ".fimport_gx2" &&
                SectionName[14] == ".fimport_snd_core.rpl" &&
                SectionName[15] == ".dimport_snd_core.rpl" &&
                SectionName[16] == ".fimport_nn_save" &&
                SectionName[17] == ".fimport_vpad.rpl" &&
                SectionName[18] == ".fimport_proc_ui" &&
                SectionName[19] == ".fimport_padscore" &&
                SectionName[20] == ".fimport_coreinit" &&
                SectionName[21] == ".dimport_coreinit" &&
                SectionName[22] == ".fimport_mic.rpl" && //Not in type A2
                SectionName[23] == ".fimport_snd_user.rpl" &&
                SectionName[24] == ".dimport_snd_user.rpl" &&
                SectionName[25] == ".symtab" &&
                SectionName[26] == ".strtab" &&
                SectionName[27] == ".shstrtab")
                return VCType.A1;
            else if (SectionName.Length == 29 &&
                SectionName[11] == ".fimport_sysapp" &&
                SectionName[12] == ".fimport_zlib125.rpl" &&
                SectionName[13] == ".fimport_gx2" &&
                SectionName[14] == ".fimport_snd_core.rpl" &&
                SectionName[15] == ".dimport_snd_core.rpl" &&
                SectionName[16] == ".fimport_nn_save" &&
                SectionName[17] == ".fimport_vpad.rpl" &&
                SectionName[18] == ".fimport_proc_ui" &&
                SectionName[19] == ".fimport_padscore" &&
                SectionName[20] == ".fimport_coreinit" &&
                SectionName[21] == ".dimport_coreinit" &&
                SectionName[22] == ".fimport_snd_user.rpl" &&
                SectionName[23] == ".dimport_snd_user.rpl" &&
                SectionName[24] == ".symtab" &&
                SectionName[25] == ".strtab" &&
                SectionName[26] == ".shstrtab")
                return VCType.A2;
            else if (SectionName.Length == 28 &&
                SectionName[11] == ".dimport_nn_act" &&
                SectionName[12] == ".fimport_sysapp" &&
                SectionName[13] == ".fimport_zlib125" &&
                SectionName[14] == ".fimport_gx2" &&
                SectionName[15] == ".fimport_snd_core" &&
                SectionName[16] == ".fimport_nn_save" && //Index 17 in type B2
                SectionName[17] == ".fimport_vpad" &&    //Index 18 in type B2
                SectionName[18] == ".fimport_proc_ui" && //Index 19 in type B2
                SectionName[19] == ".fimport_padscore" &&//Index 20 in type B2
                SectionName[20] == ".fimport_coreinit" &&//Index 21 in type B2
                SectionName[21] == ".dimport_coreinit" &&//Index 22 in type B2
                SectionName[22] == ".fimport_snd_user" &&//Index 16 in type B2
                SectionName[23] == ".symtab" &&
                SectionName[24] == ".strtab" &&
                SectionName[25] == ".shstrtab")
                return VCType.B1;
            else if (SectionName.Length == 28 &&
                SectionName[11] == ".dimport_nn_act" &&
                SectionName[12] == ".fimport_sysapp" &&
                SectionName[13] == ".fimport_zlib125" &&
                SectionName[14] == ".fimport_gx2" &&
                SectionName[15] == ".fimport_snd_core" &&
                SectionName[16] == ".fimport_snd_user" &&//Index 22 in type B1
                SectionName[17] == ".fimport_nn_save" && //Index 16 in type B1
                SectionName[18] == ".fimport_vpad" &&    //Index 17 in type B1
                SectionName[19] == ".fimport_proc_ui" && //Index 18 in type B1
                SectionName[20] == ".fimport_padscore" &&//Index 19 in type B1
                SectionName[21] == ".fimport_coreinit" &&//Index 20 in type B1
                SectionName[22] == ".dimport_coreinit" &&//Index 21 in type B1             
                SectionName[23] == ".symtab" &&
                SectionName[24] == ".strtab" &&
                SectionName[25] == ".shstrtab")
                return VCType.B2;
            else
                return VCType.Unknown;
        }

        protected override byte[] GetNewRodata(uint crcsSum, byte[] rodata, string rom)
        {
            //int romOffset = ReadInt32(rodata, 0x28); //ROM offset (Always 0x00000030)
            //int footerOffset = ReadInt32(rodata, 0x34); //Footer offset            
            //int romSize = ReadInt32(rodata, footerOffset + 0x21); //ROM size
            int romSize = VCSNES.GetVC(crcsSum).ROMSize;

            if (romSize == -1)
                throw new FormatException("The source RPXSNES is unknown.");

            FileStream fs = File.Open(rom, FileMode.Open);
            byte[] romBytes = new byte[fs.Length];
            fs.Read(romBytes, 0, romBytes.Length);
            fs.Close();

            int smcHeaderSize;
            if (romBytes.Length % 1024 == 0)
                smcHeaderSize = 0;            
            else if (romBytes.Length % 1024 == 0x200)
                smcHeaderSize = 0x200;
            else
                throw new FormatException("The source ROM has an invalid size.");

            if (romBytes.Length - smcHeaderSize > romSize)
                throw new FormatException("The source ROM is too large for this base.");

            int paddingLength = romSize - (romBytes.Length - smcHeaderSize);
            byte[] padding = new byte[paddingLength];

            MemoryStream ms = new MemoryStream(rodata);
            ms.Position = 0x50;//romOffset + 0x20;
            ms.Write(romBytes, smcHeaderSize, romBytes.Length - smcHeaderSize);
            ms.Write(padding, 0, padding.Length);
            rodata = ms.ToArray();
            ms.Close();

            return rodata;
        }

        protected override int GetAspectRatioOffset(uint crcsSum)
        {
            switch (crcsSum)
            {
                case 0x9F042607:
                case 0x9F9DBB67:
                case 0xA0506A67:
                case 0xA1554AFD:
                case 0xA1B1E82D:
                case 0xA4457AE7:
                case 0xA5456A61:
                case 0xA609856E:
                case 0xA74F8046:
                case 0xA78161C9:
                case 0xA7B03D27:
                case 0xA7EFAB22:
                case 0xA8513D59:
                case 0xA8843CC3:
                case 0xAB870B4E:
                case 0xABA6C3D8:
                case 0xABFF0731:
                case 0xAC5E5227:
                case 0xAFD0B303:
                case 0xB0CBE2B4:
                case 0xB26E5985:
                case 0xB2FDD1D3:
                case 0xB354F03B:
                case 0xB7C2C025:
                case 0xB9FDAD98:
                case 0xBC9DA60E:
                case 0xC58A1439: return 0x0001A996;
                case 0xA8DCC5DC:
                case 0xAA9E5DB3:
                case 0xAF1A2EE1:
                case 0xAFA3CD5F:
                case 0xB2195DB2:
                case 0xB3CFA8A8:
                case 0xB44E4393:
                case 0xB4B9278B:
                case 0xB508E784:
                case 0xB651B88E:
                case 0xB69FF36F:
                case 0xB7E25A8F:
                case 0xB92ABD19:
                case 0xB9EDB65E:
                case 0xBD6948A9:
                case 0xC155E3A0:
                case 0xC1C2CD2D:
                case 0xC969FC68: return 0x0001AB6A;
                case 0xCED311D0: return 0x0001AB7E;
                case 0x9F06DD9B:
                case 0xA2BD21A7:
                case 0xA547562D:
                case 0xA89A860E:
                case 0xAFFC0DE9:
                case 0xB34E1F4B:
                case 0xB4C803CD: return 0x0001ABAE;
                case 0x9AE3B68B:
                case 0x9F523E24:
                case 0xBC63A1C8: return 0x0001AC02;
                case 0x8BDD22FE:
                case 0x964937AF:
                case 0x9A837593:
                case 0x9E7E3D65:
                case 0xA0C2ECC7:
                case 0xA122EEC0:
                case 0xA18DD837:
                case 0xA279FE7D:
                case 0xA27A572C:
                case 0xA40B189E:
                case 0xA4570B77:
                case 0xA56BB098:
                case 0xA6D6CAE0:
                case 0xA7891654:
                case 0xA9603BAD:
                case 0xAA64B1BE:
                case 0xACC8B7A7:
                case 0xADAC5734:
                case 0xAE69F2C8:
                case 0xB029222E:
                case 0xB03D8A2F:
                case 0xB12CAA2C:
                case 0xB864C674:
                case 0xB90ECAF1: return 0x0001B38E;
                case 0xA54DC0F6: return 0x0001B54A;
                case 0xC029C869:
                case 0xC4D5EDD7:
                case 0xD2E7F1A9: return 0x0001B6AA;
                case 0xBBA21042:
                case 0xBFBC63CC:
                case 0xC20E69E2:
                case 0xC24A566D:
                case 0xC27A2C40:
                case 0xC2ECC888:
                case 0xC4D71343:
                case 0xC593E5E6:
                case 0xC618C24B:
                case 0xC68EFFD0:
                case 0xCB5F48A8:
                case 0xCB84B720:
                case 0xCBE3CB53:
                case 0xCBEC9D21:
                case 0xCE1BCAF6:
                case 0xCE3DC892:
                case 0xCE6601CD:
                case 0xCE7E8306:
                case 0xCFE2F857:
                case 0xD1B8D5E2:
                case 0xD262592C:
                case 0xD3B2BDB9:
                case 0xD3E4BF9C:
                case 0xD4727012:
                case 0xD5CA4093:
                case 0xD6CA025F:
                case 0xD6FF8995:
                case 0xD76B99EF:
                case 0xD8B6064A:
                case 0xDA16F30B:
                case 0xDE78A430:
                case 0xE1709184:
                case 0xE1E02D1A: return 0x0001B75A;
                case 0x7D9E5904: return 0x0001B876;
                case 0x94AB097D:
                case 0xA254090C:
                case 0xA85E5134: return 0x0001B902;
                case 0x94F0D47E:
                case 0x9B5FB35D:
                case 0x9C0BB56D:
                case 0xABDE2222: return 0x0001BBF6;
                case 0xA64C2EA7: return 0x0001C6BE;
                case 0xACEEFB53:
                case 0xB11A158A: return 0x0001CB76;
                case 0x9D738126: return 0x0001CC92;
                case 0xACB1A9B5:
                case 0xADC96E2B:
                case 0xB13EE167:
                case 0xB46F5D41:
                case 0xB80A3F81:
                case 0xBE4D1CB6:
                case 0xC909118B: return 0x0001D86A;
                case 0xA9BC61A7:
                case 0xB0D878C7:
                case 0xBF36AC9B:
                case 0xC364C797:
                case 0xC3918529:
                case 0xCF1E7641: return 0x0001DAFA;
                default: return 0;
            }
        }
    }
}

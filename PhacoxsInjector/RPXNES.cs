using System;
using System.IO;

namespace PhacoxsInjector
{
    public class RPXNES : RPX
    {
        public enum VCType
        {
            A,
            B,
            Unknown
        }

        public VCType Type
        { private set; get; }

        public RPXNES(string filename)
            : base(filename)
        {
            Type = GetVCType();
            if (Type == VCType.Unknown)
                throw new FormatException("It is not an RPXNES file.");
        }

        public static void Inject(string sourcebase, string sourcerom, string destination,
            short widthTv = 1920, short widthDrc = 854, short heightTv = 1080, short heightDrc = 480)
        {
            RPXNES rpx = new RPXNES(sourcebase);
            rpx.Edit(sourcerom, widthTv, widthDrc, heightTv,  heightDrc, destination);
        }

        private VCType GetVCType()
        {
            if (SectionName.Length == 30 &&
                SectionName[11] == ".fimport_sysapp" &&
                SectionName[12] == ".fimport_zlib125.rpl" &&
                SectionName[13] == ".fimport_gx2" &&
                SectionName[14] == ".fimport_snd_core.rpl" &&
                SectionName[15] == ".dimport_snd_core.rpl" &&
                SectionName[16] == ".fimport_snd_user.rpl" &&
                SectionName[17] == ".dimport_snd_user.rpl" &&
                SectionName[18] == ".fimport_nn_save" &&
                SectionName[19] == ".fimport_vpad.rpl" &&
                SectionName[20] == ".fimport_proc_ui" &&
                SectionName[21] == ".fimport_padscore" &&
                SectionName[22] == ".fimport_coreinit" &&
                SectionName[23] == ".dimport_coreinit" &&
                SectionName[24] == ".fimport_mic.rpl")
                return VCType.A;
            else if (SectionName.Length == 29 &&
                SectionName[11] == ".dimport_nn_act" &&
                SectionName[12] == ".fimport_sysapp" &&
                SectionName[13] == ".fimport_zlib125" &&
                SectionName[14] == ".fimport_gx2" &&
                SectionName[15] == ".fimport_snd_core" &&
                SectionName[16] == ".fimport_snd_user" &&
                SectionName[17] == ".fimport_nn_save" &&
                SectionName[18] == ".fimport_vpad" &&
                SectionName[19] == ".fimport_proc_ui" &&
                SectionName[20] == ".fimport_padscore" &&
                SectionName[21] == ".fimport_coreinit" &&
                SectionName[22] == ".dimport_coreinit" &&
                SectionName[23] == ".fimport_mic")
                return VCType.B;
            else
                return VCType.Unknown;
        }

        protected override byte[] GetNewRodata(uint crcsSum, byte[] rodata, string rom)
        {
            //int romOffset = ReadInt32(rodata, 0x28); //ROM offset (Always 0x00000030)
            //int footerOffset = ReadInt32(rodata, 0x2C); //Footer offset
            /*int romSize =
                (rodata[0x20 + romOffset] == 'N' &&
                rodata[0x21 + romOffset] == 'E' &&
                rodata[0x22 + romOffset] == 'S') ?
                rodata[0x24 + romOffset] * 16384 + rodata[0x25 + romOffset] * 8192 + 16 :            
                footerOffset - romOffset;*/
            VCNES vcnes = VCNES.GetVC(crcsSum);

            if (vcnes.ROMSize == -1)
                throw new FormatException("The source RPXNES is unknown.");

            int romSize = vcnes.ROMSize + (vcnes.FDSROM ? 0 : 16);

            FileStream fs = File.Open(rom, FileMode.Open);
            byte[] romBytes = new byte[fs.Length];
            fs.Read(romBytes, 0, romBytes.Length);
            fs.Close();

            if (romBytes.Length > romSize)
                throw new FormatException("The source ROM is too large for this base.");

            int paddingLength = romSize - romBytes.Length;
            byte[] padding = new byte[paddingLength];

            MemoryStream ms = new MemoryStream(rodata);
            ms.Position = 0x50;//romOffset + 0x20;
            ms.Write(romBytes, 0, romBytes.Length);
            ms.Write(padding, 0, padding.Length);
            rodata = ms.ToArray();
            ms.Close();

            return rodata;
        }

        protected override int GetAspectRatioOffset(uint crcsSum)
        {
            switch (crcsSum)
            {
                case 0x91B6CDEA:
                case 0x97FF4164:
                case 0x9B33C298:
                case 0x9C672378:
                case 0x9C6CAC2E:
                case 0x9C9A28FD:
                case 0x9D4241AE:
                case 0x9DE19F58:
                case 0x9FB62C7E:
                case 0xA06FA68A:
                case 0xA09F8389:
                case 0xA205BF06:
                case 0xA233B7D2:
                case 0xA2FB5D7A:
                case 0xA41C82BC:
                case 0xA55AE283:
                case 0xA579D44E:
                case 0xA6284BE5:
                case 0xA63C47DE:
                case 0xA7D18EE9:
                case 0xA81596EE:
                case 0xA849E75F:
                case 0xA8D4B240:
                case 0xA8D94E63:
                case 0xA8F1FDA7:
                case 0xA90F75B3:
                case 0xA930A560:
                case 0xA973C2D9:
                case 0xA9875FA7:
                case 0xA99C096F:
                case 0xAADBF5F8:
                case 0xAB3D7B54:
                case 0xABC75E17:
                case 0xABD26A1A:
                case 0xAC4C2FD7:
                case 0xACA51E7D:
                case 0xACABE902:
                case 0xAD15A874:
                case 0xAD283E75:
                case 0xAD3F8697:
                case 0xADA1A2CA:
                case 0xAE1F7572:
                case 0xAEA271D0:
                case 0xB02C79F1:
                case 0xB0889319:
                case 0xB169C8DC:
                case 0xB196D48E:
                case 0xB21DDC69:
                case 0xB2DE4C65:
                case 0xB4D32813:
                case 0xB50E938E:
                case 0xB87644F0: return 0x0001AA22;
                case 0xB13D573B:
                case 0xBDAD0834:
                case 0xBE937447:
                case 0xC3B03C12:
                case 0xC43333DE:
                case 0xC6323D20:
                case 0xC69AE3A1:
                case 0xC7D7BAD1:
                case 0xC8D63CD0:
                case 0xD0EED5F1:
                case 0xD506D904:
                case 0xD673EA56:
                case 0xD6C06ED6:
                case 0xD87863A4:
                case 0xD8BC52C3:
                case 0xDBEA5DD8:
                case 0xE0ACEA0B:
                case 0xEB4101B3: return 0x0001ABF2;
                case 0x94843589:
                case 0x8D9BAF5E:
                case 0x945EFBF5:
                case 0x960780B2:
                case 0x97C6FDAA:
                case 0x984FE47A:
                case 0x9974F949:
                case 0x9A0D94B9:
                case 0x9BEBD515:
                case 0x9C0929D9:
                case 0x9E14EFE9:
                case 0x9FF9800F:
                case 0xA0375E86:
                case 0xA1523F6B:
                case 0xA3473EAB:
                case 0xA4366119:
                case 0xA4A0A5FB:
                case 0xA647D809:
                case 0xA8ABF2CF:
                case 0xA9F85FF3:
                case 0xAC9AA0DD:
                case 0xAE998892:
                case 0xB9F03109: return 0x0001B41A;
                case 0x9CE222A4:
                case 0xAAF1485F:
                case 0xAC7C485F:
                case 0xAD093596:
                case 0xB16A49F5:
                case 0xB1D6FBEC:
                case 0xB1F617D8:
                case 0xC5ABBF89: return 0x0001B436;
                case 0xCB3ABEDA: return 0x0001B57E;
                case 0xD22E3E4B: return 0x0001B592;
                case 0xC3DBB693:
                case 0xC86F2CE8:
                case 0xD67391AD: return 0x0001B5F6;
                case 0xBBDD226F:
                case 0xBD66A6E6:
                case 0xBFC52080:
                case 0xC16D27F9:
                case 0xC1EB92AB:
                case 0xC2E852BF:
                case 0xC7A7812F:
                case 0xC7EBD352:
                case 0xCB4A53E6:
                case 0xCEE027FA:
                case 0xD3B70E55:
                case 0xD7FE8FF9: return 0x0001B6A6;
                case 0xC00652A1:
                case 0xC44929DB:
                case 0xC46DD2CC:
                case 0xC6923E29:
                case 0xC6E63608:
                case 0xC8449A0A: return 0x0001B6E2;
                case 0x9374FFF1: return 0x0001B912;
                case 0x850404AA:
                case 0x9E7691BC:
                case 0xA3C4068A:
                case 0xA9274531: return 0x0001B99E;
                case 0xA1A526AF: return 0x0001BCFA;
                case 0x88EF2E3F:
                case 0x8C1A183D:
                case 0x90365DEC:
                case 0x9AC0604F:
                case 0x9B159C4E:
                case 0xA1A0B268:
                case 0xA5526B7C: return 0x0001BDB2;
                case 0xAC335C0E:
                case 0xAE16FE0A: return 0x0001C91A;
                case 0x8DD20506:
                case 0x932F3774: return 0x0001CFC6;
                case 0xABD64C76:
                case 0xB1D3000C:
                case 0xB2933F7C:
                case 0xB618AE6C:
                case 0xBA20089D:
                case 0xBC173F3C:
                case 0xBC24C474:
                case 0xBC300EBA:
                case 0xBFD17451:
                case 0xC0ABBB9D: return 0x0001DBF6;
                case 0xA474ADD5:
                case 0xA6F10177:
                case 0xA720F352:
                case 0xAA4C253D:
                case 0xAF221323:
                case 0xB28A6A5C:
                case 0xB2E8BE31:
                case 0xB5F514E7:
                case 0xB8936232:
                case 0xB936BEC1:
                case 0xBB5F4B15:
                case 0xBFC6A68B:
                case 0xC0D1E406:
                case 0xC3F82A6C:
                case 0xC5611AF3:
                case 0xC6ABAE84:
                case 0xC72B13A7:
                case 0xCCEDDB59: return 0x0001E192;
                default: return 0;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PhacoxsInjector
{
    public abstract class RPX
    {
        public struct Elf32_Ehdr
        {
            public byte[] e_ident;
            public ushort e_type;
            public ushort e_machine;
            public uint e_version;
            public uint e_entry;
            public uint e_phoff;
            public uint e_shoff;
            public uint e_flags;
            public ushort e_ehsize;
            public ushort e_phentsize;
            public ushort e_phnum;
            public ushort e_shentsize;
            public ushort e_shnum;
            public ushort e_shstrndx;

            public Elf32_Ehdr(byte[] data)
            {
                e_ident = new byte[16];
                for (int i = 0; i < 16; i++)
                    e_ident[i] = data[i];
                e_type = ReadUInt16Reverse(data, 0x10);
                e_machine = ReadUInt16Reverse(data, 0x12);
                e_version = ReadUInt32Reverse(data, 0x14);
                e_entry = ReadUInt32Reverse(data, 0x18);
                e_phoff = ReadUInt32Reverse(data, 0x1C);
                e_shoff = ReadUInt32Reverse(data, 0x20);
                e_flags = ReadUInt32Reverse(data, 0x24);
                e_ehsize = ReadUInt16Reverse(data, 0x28);
                e_phentsize = ReadUInt16Reverse(data, 0x2A);
                e_phnum = ReadUInt16Reverse(data, 0x2C);
                e_shentsize = ReadUInt16Reverse(data, 0x2E);
                e_shnum = ReadUInt16Reverse(data, 0x30);
                e_shstrndx = ReadUInt16Reverse(data, 0x32);
            }
        }

        public struct Elf32_Shdr
        {
            public uint sh_name;
            public uint sh_type;
            public uint sh_flags;
            public uint sh_addr;
            public uint sh_offset;
            public uint sh_size;
            public uint sh_link;
            public uint sh_info;
            public uint sh_addralign;
            public uint sh_entsize;

            public Elf32_Shdr(byte[] data, int startIndex)
            {
                sh_name = ReadUInt32Reverse(data, startIndex + 0x00);
                sh_type = ReadUInt32Reverse(data, startIndex + 0x04);
                sh_flags = ReadUInt32Reverse(data, startIndex + 0x08);
                sh_addr = ReadUInt32Reverse(data, startIndex + 0x0C);
                sh_offset = ReadUInt32Reverse(data, startIndex + 0x10);
                sh_size = ReadUInt32Reverse(data, startIndex + 0x14);
                sh_link = ReadUInt32Reverse(data, startIndex + 0x18);
                sh_info = ReadUInt32Reverse(data, startIndex + 0x1C);
                sh_addralign = ReadUInt32Reverse(data, startIndex + 0x20);
                sh_entsize = ReadUInt32Reverse(data, startIndex + 0x24);
            }

            public Elf32_Shdr(Elf32_Shdr shdr)
            {
                sh_name = shdr.sh_name;
                sh_type = shdr.sh_type;
                sh_flags = shdr.sh_flags;
                sh_addr = shdr.sh_addr;
                sh_offset = shdr.sh_offset;
                sh_size = shdr.sh_size;
                sh_link = shdr.sh_link;
                sh_info = shdr.sh_info;
                sh_addralign = shdr.sh_addralign;
                sh_entsize = shdr.sh_entsize;
            }

            public byte[] ToArray()
            {
                byte[] array = new byte[0x28];
                array[0x00] = (byte)(sh_name >> 24);
                array[0x01] = (byte)((sh_name >> 16) & 0xFF);
                array[0x02] = (byte)((sh_name >> 8) & 0xFF);
                array[0x03] = (byte)(sh_name & 0xFF);
                array[0x04] = (byte)(sh_type >> 24);
                array[0x05] = (byte)((sh_type >> 16) & 0xFF);
                array[0x06] = (byte)((sh_type >> 8) & 0xFF);
                array[0x07] = (byte)(sh_type & 0xFF);
                array[0x08] = (byte)(sh_flags >> 24);
                array[0x09] = (byte)((sh_flags >> 16) & 0xFF);
                array[0x0A] = (byte)((sh_flags >> 8) & 0xFF);
                array[0x0B] = (byte)(sh_flags & 0xFF);
                array[0x0C] = (byte)(sh_addr >> 24);
                array[0x0D] = (byte)((sh_addr >> 16) & 0xFF);
                array[0x0E] = (byte)((sh_addr >> 8) & 0xFF);
                array[0x0F] = (byte)(sh_addr & 0xFF);
                array[0x10] = (byte)(sh_offset >> 24);
                array[0x11] = (byte)((sh_offset >> 16) & 0xFF);
                array[0x12] = (byte)((sh_offset >> 8) & 0xFF);
                array[0x13] = (byte)(sh_offset & 0xFF);
                array[0x14] = (byte)(sh_size >> 24);
                array[0x15] = (byte)((sh_size >> 16) & 0xFF);
                array[0x16] = (byte)((sh_size >> 8) & 0xFF);
                array[0x17] = (byte)(sh_size & 0xFF);
                array[0x18] = (byte)(sh_link >> 24);
                array[0x19] = (byte)((sh_link >> 16) & 0xFF);
                array[0x1A] = (byte)((sh_link >> 8) & 0xFF);
                array[0x1B] = (byte)(sh_link & 0xFF);
                array[0x1C] = (byte)(sh_info >> 24);
                array[0x1D] = (byte)((sh_info >> 16) & 0xFF);
                array[0x1E] = (byte)((sh_info >> 8) & 0xFF);
                array[0x1F] = (byte)(sh_info & 0xFF);
                array[0x20] = (byte)(sh_addralign >> 24);
                array[0x21] = (byte)((sh_addralign >> 16) & 0xFF);
                array[0x22] = (byte)((sh_addralign >> 8) & 0xFF);
                array[0x23] = (byte)(sh_addralign & 0xFF);
                array[0x24] = (byte)(sh_entsize >> 24);
                array[0x25] = (byte)((sh_entsize >> 16) & 0xFF);
                array[0x26] = (byte)((sh_entsize >> 8) & 0xFF);
                array[0x27] = (byte)(sh_entsize & 0xFF);
                return array;
            }

            public static int CompareByOffset(Elf32_Shdr sh1, Elf32_Shdr sh2)
            {
                return sh1.sh_offset.CompareTo(sh2.sh_offset);
            }
        }

        public Elf32_Ehdr Header
        { protected set; get; }
        public Elf32_Shdr[] SectionHeader
        { protected set; get; }
        public string[] SectionName
        { protected set; get; }
        public uint[] CRC
        { protected set; get; }

        public uint CRCsSum
        { protected set; get; }
        public int TextSectionIndex
        { protected set; get; }
        public int RodataSectionIndex
        { protected set; get; }

        private string FileName;

        public RPX(string filename)
        {
            FileStream fs = File.Open(filename, FileMode.Open);
            byte[] ehdrBytes = new byte[52];
            fs.Read(ehdrBytes, 0, ehdrBytes.Length);
            fs.Close();

            if (!(ehdrBytes[0] == 0x7F &&
                ehdrBytes[1] == 'E' &&
                ehdrBytes[2] == 'L' &&
                ehdrBytes[3] == 'F'))
                throw new FormatException("It is not an ELF file.");

            if (ehdrBytes[4] != 1 ||
                ehdrBytes[6] != 1)
                throw new FormatException("It is not an ELF32 file.");

            Header = new Elf32_Ehdr(ehdrBytes);

            if (Header.e_ident[5] != 2 ||
                Header.e_ident[7] != 0xCA ||
                Header.e_ident[8] != 0xFE ||
                Header.e_type != 0xFE01)
                throw new FormatException("It is not an RPL/RPX file.");

            if (Header.e_shnum == 0)
                throw new FormatException("This RPL/RPX file has 0 sections.");

            fs = File.Open(filename, FileMode.Open);
            byte[] shdrBytes = new byte[Header.e_shnum * Header.e_shentsize];
            fs.Position = Header.e_shoff;
            fs.Read(shdrBytes, 0, shdrBytes.Length);
            fs.Close();

            SectionHeader = new Elf32_Shdr[Header.e_shnum];

            int sectionCRCsIndex = 0;
            for (int i = 0; i < Header.e_shnum; i++)
            {
                SectionHeader[i] = new Elf32_Shdr(shdrBytes, Header.e_shentsize * i);
                if ((SectionHeader[i].sh_type & 0x80000003) == 0x80000003)//Section Header Type RPL CRCS
                    sectionCRCsIndex = i;
            }

            if (sectionCRCsIndex == 0)
                throw new FormatException("Does not contain CRCs section.");

            fs = File.Open(filename, FileMode.Open);
            byte[] sectionStrBytes = new byte[SectionHeader[Header.e_shstrndx].sh_size];
            fs.Position = SectionHeader[Header.e_shstrndx].sh_offset;
            fs.Read(sectionStrBytes, 0, sectionStrBytes.Length);
            byte[] sectionCRCsBytes = new byte[SectionHeader[sectionCRCsIndex].sh_size];
            fs.Position = SectionHeader[sectionCRCsIndex].sh_offset;
            fs.Read(sectionCRCsBytes, 0, sectionCRCsBytes.Length);
            fs.Close();

            if ((SectionHeader[Header.e_shstrndx].sh_flags & 0x08000000) == 0x08000000)//Section Header Flag RPL ZLIB
                sectionStrBytes = Decompress(sectionStrBytes);

            foreach (byte b in sectionStrBytes)
            {
                if (b > 127)
                    throw new FormatException("Section Strings are not ASCII.");
            }

            SectionName = new string[Header.e_shnum];
            CRC = new uint[Header.e_shnum];

            long sum = 0;
            for (int i = 0; i < Header.e_shnum; i++)
            {
                uint stringLength = 0;
                while (sectionStrBytes[SectionHeader[i].sh_name + stringLength] != 0) stringLength++;
                SectionName[i] = Encoding.ASCII.GetString(sectionStrBytes, (int)SectionHeader[i].sh_name, (int)stringLength);

                CRC[i] = ReadUInt32Reverse(sectionCRCsBytes, i * 4);
                sum += CRC[i];//Add all the CRCs

                if (SectionHeader[i].sh_offset != 0)
                {
                    if (SectionName[i] == ".text")
                        TextSectionIndex = i;//Always 2
                    else if (SectionName[i] == ".rodata")
                        RodataSectionIndex = i;//Always 3
                }
            }
            sum -= (long)CRC[TextSectionIndex] + CRC[RodataSectionIndex] + CRC[CRC.Length - 1];//Subtract ".text" CRC, ".rodata" CRC and "RPL Info" CRC
            CRCsSum = (uint)(sum >> 4);//Discard the least significant 4 bits

            FileName = filename;
        }

        protected void Edit(string rom, string destination,
            byte speed, byte players, byte soundVolume, byte romType,
            short widthTv, short widthDrc, short heightTv, short heightDrc)
        {
            FileStream fs = File.Open(FileName, FileMode.Open);
            byte[] rodata = new byte[SectionHeader[RodataSectionIndex].sh_size];
            fs.Position = SectionHeader[RodataSectionIndex].sh_offset;
            fs.Read(rodata, 0, rodata.Length);
            byte[] textSection = new byte[SectionHeader[TextSectionIndex].sh_size];
            fs.Position = SectionHeader[TextSectionIndex].sh_offset;
            fs.Read(textSection, 0, textSection.Length);
            fs.Close();

            if (rom != null)
            {
                if ((SectionHeader[RodataSectionIndex].sh_flags & 0x08000000) == 0x08000000)//Section Header Flag RPL ZLIB
                    rodata = Decompress(rodata);
                rodata = GetNewRodata(CRCsSum, rodata, rom, speed, players, soundVolume, romType);
                CRC[RodataSectionIndex] = Cll.Security.ComputeCRC32(rodata, 0, rodata.Length);
                if ((SectionHeader[RodataSectionIndex].sh_flags & 0x08000000) == 0x08000000)//Section Header Flag RPL ZLIB
                    rodata = Compress(rodata);
            }

            if ((SectionHeader[TextSectionIndex].sh_flags & 0x08000000) == 0x08000000)//Section Header Flag RPL ZLIB
                textSection = Decompress(textSection);
            int aspectRatioOffset = GetAspectRatioOffset(CRCsSum);
            if (aspectRatioOffset != 0)
            {
                textSection[aspectRatioOffset] = (byte)(heightTv >> 8);
                textSection[aspectRatioOffset + 0x01] = (byte)(heightTv & 0xFF);
                textSection[aspectRatioOffset + 0x04] = (byte)(widthTv >> 8);
                textSection[aspectRatioOffset + 0x05] = (byte)(widthTv & 0xFF);
                textSection[aspectRatioOffset + 0x30] = (byte)(heightDrc >> 8);
                textSection[aspectRatioOffset + 0x31] = (byte)(heightDrc & 0xFF);
                textSection[aspectRatioOffset + 0x34] = (byte)(widthDrc >> 8);
                textSection[aspectRatioOffset + 0x35] = (byte)(widthDrc & 0xFF);
            }
            CRC[TextSectionIndex] = Cll.Security.ComputeCRC32(textSection, 0, textSection.Length);            
            if ((SectionHeader[TextSectionIndex].sh_flags & 0x08000000) == 0x08000000)//Section Header Flag RPL ZLIB
                textSection = Compress(textSection);

            int shSize = Header.e_shnum * 0x2C; // 0x2C = Header.e_shentsize + 4 bytes of CRC32
            int sectionsOffset = GetPhysicalSectionSize(shSize) + 0x40;

            List<KeyValuePair<int, Elf32_Shdr>> shList = new List<KeyValuePair<int, Elf32_Shdr>>();
            List<KeyValuePair<int, Elf32_Shdr>> shNew = new List<KeyValuePair<int, Elf32_Shdr>>();

            for (int i = 0; i < SectionHeader.Length; i++)
                shList.Add(new KeyValuePair<int, Elf32_Shdr>(i, SectionHeader[i]));
            shList.Sort((pair1, pair2) => Elf32_Shdr.CompareByOffset(pair1.Value, pair2.Value));

            FileStream src = File.Open(FileName, FileMode.Open);
            FileStream dest = File.Open(destination, FileMode.Create);

            byte[] srcBytes = new byte[sectionsOffset];
            src.Read(srcBytes, 0, srcBytes.Length);
            dest.Write(srcBytes, 0, srcBytes.Length);

            for (int i = 0; i < shList.Count; i++)
            {
                int key = shList[i].Key;
                Elf32_Shdr shdr = new Elf32_Shdr(shList[i].Value);
                if (shList[i].Value.sh_offset >= sectionsOffset)
                {
                    int padding = 0;
                    shdr.sh_offset = (uint)dest.Position;
                    if (shList[i].Value.sh_offset == SectionHeader[TextSectionIndex].sh_offset)
                    {
                        shdr.sh_size = (uint)textSection.Length;
                        padding = GetPhysicalSectionSize(textSection.Length) - textSection.Length;
                        dest.Write(textSection, 0, textSection.Length);
                    }
                    else if (shList[i].Value.sh_offset == SectionHeader[RodataSectionIndex].sh_offset)
                    {
                        shdr.sh_size = (uint)rodata.Length;
                        padding = GetPhysicalSectionSize(rodata.Length) - rodata.Length;
                        dest.Write(rodata, 0, rodata.Length);
                    }
                    else
                    {
                        srcBytes = new byte[shList[i].Value.sh_size];
                        src.Position = shList[i].Value.sh_offset;
                        src.Read(srcBytes, 0, srcBytes.Length);
                        padding = GetPhysicalSectionSize(srcBytes.Length) - srcBytes.Length;
                        dest.Write(srcBytes, 0, srcBytes.Length);
                    }
                    byte[] paddingBytes = new byte[padding];
                    dest.Write(paddingBytes, 0, paddingBytes.Length);
                }
                shNew.Add(new KeyValuePair<int, Elf32_Shdr>(key, shdr));
            }

            src.Close();

            dest.Position = 0x40;
            shNew.Sort((pair1, pair2) => pair1.Key.CompareTo(pair2.Key));
            for (int i = 0; i < shNew.Count; i++)
                dest.Write(shNew[i].Value.ToArray(), 0, 0x28);

            for (int i = 0; i < CRC.Length; i++)
            {
                dest.WriteByte((byte)(CRC[i] >> 24));
                dest.WriteByte((byte)((CRC[i] >> 16) & 0xFF));
                dest.WriteByte((byte)((CRC[i] >> 8) & 0xFF));
                dest.WriteByte((byte)(CRC[i] & 0xFF));
            }

            dest.Close();
        }

        protected abstract byte[] GetNewRodata(uint crcsSum, byte[] rodata, string rom,
            byte speed, byte players, byte soundVolume, byte romType);

        protected abstract int GetAspectRatioOffset(uint crcsSum);

        protected static byte[] Decompress(byte[] source)
        {
            uint decompressedSize = ReadUInt32Reverse(source, 0);
            uint adler32 = ReadUInt32Reverse(source, source.Length - 4);
            byte[] decompressedData = Cll.Security.ZlibDecompress(source, 4, source.Length - 4);
            uint adler32Calculated = Cll.Security.Adler32(decompressedData, 0, decompressedData.Length);

            if (decompressedSize != decompressedData.Length)
                throw new FormatException("Decompressed size does not match.");

            if (adler32 != adler32Calculated)
                throw new FormatException("Adler-32 does not match.");

            return decompressedData;
        }

        protected static byte[] Compress(byte[] source)
        {
            byte[] sizeBytes = BitConverter.GetBytes(source.Length);
            byte[] compressedData = Cll.Security.ZlibCompress(source, 0, source.Length);
            byte[] result = new byte[compressedData.Length + 4];
            result[0] = sizeBytes[3];
            result[1] = sizeBytes[2];
            result[2] = sizeBytes[1];
            result[3] = sizeBytes[0];
            Array.Copy(compressedData, 0, result, 4, compressedData.Length);
            return result;
        }

        protected static int GetPhysicalSectionSize(int size)
        {
            return size % 0x40 == 0 ? size : size / 0x40 * 0x40 + 0x40;
        }
        
        protected static ushort ReadUInt16Reverse(byte[] value, int startIndex)
        {
            return (ushort)(
                (value[startIndex] << 8) |
                 value[startIndex + 1]);
        }

        protected static uint ReadUInt32Reverse(byte[] value, int startIndex)
        {
            return
                  ((uint)value[startIndex] << 24) |
                  ((uint)value[startIndex + 1] << 16) |
                  ((uint)value[startIndex + 2] << 8) |
                         value[startIndex + 3];
        }

        protected static int ReadInt32(byte[] value, int startIndex)
        {
            return
                  (value[startIndex + 3] << 24) |
                  (value[startIndex + 2] << 16) |
                  (value[startIndex + 1] << 8) |
                   value[startIndex];
        }
    }
}

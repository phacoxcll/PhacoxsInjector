using System;

namespace PhacoxsInjector
{
    public abstract class WiiUVC
    {
        public readonly int Index;
        public readonly uint Hash;
        public readonly DateTime Release;
        public readonly string Title;

        public WiiUVC(int index, uint hash, DateTime release, string title)
        {
            Index = index;
            Hash = hash;
            Release = release;
            Title = title;
        }

        public WiiUVC(uint hash)
        {
            Index = 0;
            Hash = hash;
            Release = new DateTime();
            Title = "Unknown";
        }
    }
}

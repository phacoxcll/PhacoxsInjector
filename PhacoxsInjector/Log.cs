using System;
using System.Diagnostics;
using System.IO;

namespace Cll
{
    public static class Log
    {
        public enum TabMode
        {
            None,
            OnlyFirst,
            ExceptFirst,
            All
        }

        private static string _filename;
        public static string Filename { get { return _filename != null ? _filename : ""; } }
        public static void SaveIn(String filename, bool clear = true)
        {
            if (filename != null && filename.Length > 0)
            {
                _filename = filename;
                if (clear)
                    File.Delete(filename);
            }
            else
                _filename = null;
        }

        public static void Write(string value)
        {
            Debug.Write(value);
            Console.Write(value);
            if (_filename != null)
                File.AppendAllText(_filename, value);
        }

        public static void WriteLine(string value)
        {
            Debug.WriteLine(value);
            Console.WriteLine(value);
            if (_filename != null)
                File.AppendAllText(_filename, value + "\r\n");
        }

        public static void WriteLine(string value, int width, int tab, TabMode mode)
        {
            int tabLength = GetTabLength(value);
            string t = tab > 0 ? new string(' ', tab) : value.Substring(0, tabLength);
            string s = value.Substring(tabLength, value.Length - tabLength);
            string[] words = s.Split(new char[] { ' ' });

            if (words.Length == 1)
                WriteLine((mode == TabMode.All || mode == TabMode.OnlyFirst ? t : "") + s);

            else if (words.Length > 1)
            {
                int charCount = words[0].Length;
                int start = 0;
                int i = 1;
                for (; i < words.Length; i++)
                {
                    if ((mode == TabMode.All || mode == TabMode.OnlyFirst ? t.Length : 0) +
                        charCount + words[i].Length + 1 < width)
                        charCount += words[i].Length + 1;
                    else
                    {
                        WriteLine((mode == TabMode.All || mode == TabMode.OnlyFirst ? t : "") +
                            s.Substring(start, charCount));
                        start += charCount + 1;
                        charCount = words[i].Length;
                        break;
                    }

                    if (i + 1 == words.Length)
                        WriteLine((mode == TabMode.All || mode == TabMode.OnlyFirst ? t : "") +
                            s.Substring(start, charCount));
                }
                i++;
                for (; i < words.Length; i++)
                {
                    if ((mode == TabMode.All || mode == TabMode.ExceptFirst ? t.Length : 0) +
                        charCount + words[i].Length + 1 < width)
                        charCount += words[i].Length + 1;
                    else
                    {
                        WriteLine((mode == TabMode.All || mode == TabMode.ExceptFirst ? t : "") +
                            s.Substring(start, charCount));
                        start += charCount + 1;
                        charCount = words[i].Length;
                    }

                    if (i + 1 == words.Length)
                        WriteLine((mode == TabMode.All || mode == TabMode.ExceptFirst ? t : "") +
                                s.Substring(start, charCount));
                }
            }
            else
                WriteLine(mode == TabMode.All || mode == TabMode.OnlyFirst ? t : "");
        }

        public static void WriteText(string text, int width, int tab, TabMode mode)
        {
            text = text.Replace("\r", "");
            string[] lines = text.Split(new char[] { '\n' });

            foreach (string line in lines)
                WriteLine(line, width, tab, mode);
        }

        private static int GetTabLength(string line)
        {
            int i;

            for (i = 0; i < line.Length; i++)
            {
                if (!char.IsWhiteSpace(line[i]))
                    break;
            }

            return i;
        }
    }
}

using Microsoft.Xna.Framework;
using MusicBuilder.Items;
using System;
using System.Collections.Generic;

namespace MusicBuilder.Registry
{
    public enum Prog
    {
        None = 0
    }

    public class NoteData
    {
        public readonly Color txt, bgc;
        public readonly string name;
        public readonly int type;

        public NoteData(Color txt, Color bgc, string name, int type)
        {
            this.txt = txt;
            this.bgc = bgc;
            this.name = name;
            this.type = type;
        }
    }
    public class DelayData
    {
        public readonly Color bgc;
        public readonly Color off;
        public readonly Color lit;
        public readonly int type;

        public DelayData(Color bgc, Color off, Color lit, int type)
        {
            this.bgc = bgc;
            this.off = off;
            this.lit = lit;
            this.type = type;
        }
    }
    
    public static class Registries
    {
        public static Dictionary<Prog, NoteData> noteData;
        public static Dictionary<int, DelayData> delayData;
        public static List<int> delayers = new List<int>();
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.ModLoader;
using MusicBuilder.Registry;
using MusicBuilder.Utils;
using MusicBuilder.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MusicBuilder
{
    class ModContainer : Mod
    {
        public static IntPtr midiHandle;
        public static Mod instance;
        public static int[] time = new int[]{1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 32, 64, 128};
        public static string[] Instrument = new string[]{
            "acpiano",   "britepno",  "synpiano",  "honkytonk", "epiano1",   "epiano2",
            "hrpschrd",  "clavinet",  "celeste",   "glocken",   "musicbox",  "vibes",
            "marimba",   "xylophon",  "tubebell",  "santur",    "homeorg",   "percorg",
            "rockorg",   "churchorg", "reedorg",   "accordn",   "harmonica", "concrtna",
            "nyguitar",  "acguitar",  "jazzgtr",   "cleangtr",  "mutegtr",   "odguitar",
            "distgtr",   "gtrharm",   "acbass",    "fngrbass",  "pickbass",  "fretless",
            "slapbas1",  "slapbas2",  "synbass1",  "synbass2",  "violin",    "viola",
            "cello",     "contraba",  "marcato1",   "pizzcato",  "harp",      "timpani",
            "marcato2",  "slowstr",   "synstr1",   "synstr2",   "choir",     "doo",
            "voices",    "orchhit",   "trumpet",   "trombone",  "tuba",      "mutetrum",
            "frenchorn", "hitbrass",  "synbras1",  "synbras2",  "sprnosax",  "altosax",
            "tenorsax",  "barisax",   "oboe",      "englhorn",  "bassoon",   "clarinet",
            "piccolo",   "flute",     "recorder",  "woodflut",  "bottle",    "shakazul",
            "whistle",   "ocarina",   "sqrwave",   "sawwave",   "calliope",  "chiflead",
            "charang",   "voxlead",   "lead5th",   "basslead",  "fantasia",  "warmpad",
            "polysyn",   "ghostie",   "bowglass",  "metalpad",  "halopad",   "sweeper1",
            "aurora",    "soundtrk",  "crystal",   "atmosphr",  "freshair",  "unicorn",
            "sweeper2",   "startrak",  "sitar",     "banjo",     "shamisen",  "koto",
            "kalimba",   "bagpipes",  "fiddle",    "shannai",   "carillon",  "agogo",
            "steeldrum", "woodblock", "taiko",     "toms",      "syntom",    "revcymb",
            "fxfret",    "fxblow",    "seashore",  "jungle",    "telephone", "helicptr",
            "applause",  "ringwhsl",  "drum"
        };


        public static Texture2D TextureBorderNT, TexturePitchNT, TextureOctaveNT, TextureBorderDT, TextureInsideDT;
        public static Texture2D TextureBorderNI, TextureInsideNI, TextureBorderDI, TextureInsideDI;

        public const int tailLength = 60;
        public override void Load()
        {
            Random rand = new Random();

            instance = this;
            
            Registries.noteData = new Dictionary<Prog, NoteData>();
            Registries.delayData = new Dictionary<int, DelayData>();
            Registries.delayers = new List<int>();

            TextureBorderNT = GetTexture("Tiles/NoteblockBorder").Value;
            TexturePitchNT = GetTexture("Tiles/NoteblockPitch").Value;
            TextureOctaveNT = GetTexture("Tiles/NoteblockOctave").Value;
            TextureBorderDT = GetTexture("Tiles/DelayerBorder").Value;
            TextureInsideDT = GetTexture("Tiles/DelayerInside").Value;

            TextureBorderNI = GetTexture("Items/NoteblockBorder").Value;
            TextureInsideNI = GetTexture("Items/NoteblockInside").Value;
            TextureBorderDI = GetTexture("Items/DelayerBorder").Value;
            TextureInsideDI = GetTexture("Items/DelayerInside").Value;

            DLLContainer.Load();

            IntPtr nullptr = new IntPtr(0);
            uint result = DLLContainer.midiOutOpen(out midiHandle, 0u, nullptr, nullptr, 0);
            if (result != 0)
                throw new Exception("Failed to open midi device. code " + result);

        }

        public override void PreSaveAndQuit()
        {
            ModContent.GetInstance<Scheduler>().ClearAll();
            ModContent.GetInstance<SoundManager>().StopAll();
        }

        public override void Unload()
        {
            Registries.noteData = null;
            Registries.delayers = null;
            Registries.delayData = null;

            TextureBorderNT = null;
            TexturePitchNT = null;
            TextureOctaveNT = null;
            TextureBorderDT = null;
            TextureInsideDT = null;
            TextureBorderNI = null;
            TextureInsideNI = null;
            TextureBorderDI = null;
            TextureInsideDI = null;

            DLLContainer.midiOutClose(midiHandle);
            DLLContainer.Unload();

            instance = null;
        }
    }

}
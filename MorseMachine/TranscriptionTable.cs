using System;
using System.Collections.Generic;
using System.Text;

namespace MorseMachine
{
    /// <summary>
    /// Stores tables which have a minimum transcription (i.e. vowels and consonants in English) between the source language and the target language
    /// </summary>
    public static class TranscriptionTable
    {
        public static IReadOnlyDictionary<string, string> LatinToMorse => new Dictionary<string, string>
        {
            ["A"] = ".-",
            ["B"] = "-...",
            ["C"] = "-.-.",
            ["D"] = "-..",
            ["E"] = ".",
            ["F"] = "..-.",
            ["G"] = "--.",
            ["H"] = "....",
            ["I"] = "..",
            ["J"] = ".---",
            ["K"] = "-.-",
            ["L"] = ".-..",
            ["M"] = "--",
            ["N"] = "-.",
            ["O"] = "---",
            ["P"] = "--.",
            ["Q"] = "--.-",
            ["R"] = ".-.",
            ["S"] = "...",
            ["T"] = "-",
            ["U"] = "..-",
            ["V"] = "...-",
            ["W"] = ".--",
            ["X"] = "-..-",
            ["Y"] = "-.--",
            ["Z"] = "--.."
        };
    }
}

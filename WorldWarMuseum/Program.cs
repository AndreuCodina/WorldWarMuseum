using System;
using System.IO;
using System.Linq;
using MorseMachine;
using MorseMachine.MorseTranslators;

namespace WorldWarMuseum
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new OverlappedMorseTranslator($"{System.AppDomain.CurrentDomain.BaseDirectory}{Path.DirectorySeparatorChar}EnglishWords.txt", TranscriptionTable.LatinToMorse)
                .Translate("--.--.---.......-.---.-.-.-..-.....--..-....-.-----..-")
                .ToList()
                .ForEach(Console.WriteLine);

            Console.ReadKey();
        }
    }
}
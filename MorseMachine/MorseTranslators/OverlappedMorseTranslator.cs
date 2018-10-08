using System;
using System.Collections.Generic;
using System.IO;

namespace MorseMachine.MorseTranslators
{
    /// <summary>
    /// Returns all possible translations of an overlapped morse code
    /// </summary>
    public sealed class OverlappedMorseTranslator : IMorseTranslator<IEnumerable<string>>
    {
        /// <summary>
        /// Dictionary with all the words of the target language
        /// </summary>
        private readonly IEnumerable<string> _wordDictionary;

        private readonly IReadOnlyDictionary<string, string> _transcriptionTable;

        private OverlappedMorseTranslator(IReadOnlyDictionary<string, string> transcriptionTable)
        {
            _transcriptionTable = transcriptionTable;
        }

        public OverlappedMorseTranslator(in IEnumerable<string> wordDictionary, IReadOnlyDictionary<string, string> transcriptionTable)
            : this(transcriptionTable)
        {
            _wordDictionary = wordDictionary;
        }

        public OverlappedMorseTranslator(in string wordDictionaryPath, IReadOnlyDictionary<string, string> transcriptionTable)
            : this(transcriptionTable)
        {
            _wordDictionary = File.ReadLines(wordDictionaryPath);
        }
        
        public IEnumerable<string> Translate(string morseCode)
        {
            var wordDictionaryWithTranslations = new Dictionary<string, string>();
            var nodes = new Stack<Node>();

            void TranslateWordDictionary()
            {
                foreach (var word in _wordDictionary)
                {
                    var uppercaseWord = word.ToUpper();
                    var wordInMorse = ConvertToMorse(uppercaseWord);
                    wordDictionaryWithTranslations.Add(uppercaseWord, wordInMorse);
                }
            }

            string ConvertToMorse(in string word)
            {
                var conversion = "";

                for (var i = 0; i < word.Length; i++)
                {
                    conversion += _transcriptionTable[word[i].ToString()];
                }

                return conversion;
            }

            void GenerateChildNodes(in string morseCodeToResolve, in string solution)
            {
                foreach (var translation in wordDictionaryWithTranslations)
                {
                    var solutionWithLastWordSeparated = solution == string.Empty
                        ? solution + translation.Key
                        : solution + " " + translation.Key;

                    var node = new Node(translation.Key, morseCodeToResolve, solutionWithLastWordSeparated);

                    nodes.Push(node);
                }
            }

            TranslateWordDictionary();
            GenerateChildNodes(morseCode, "");

            while (nodes.Count != 0)
            {
                var node = nodes.Pop();
                var word = node.Word;
                var wordInMorse = wordDictionaryWithTranslations[word];
                if (node.RestToProcess.StartsWith(wordInMorse))
                {
                    var restToProcess = node.RestToProcess.Substring(wordInMorse.Length, node.RestToProcess.Length - wordInMorse.Length);

                    if (String.IsNullOrEmpty(restToProcess))
                    {
                        yield return node.Solution;
                    }
                    else
                    {
                        GenerateChildNodes(node.RestToProcess.Substring(wordInMorse.Length, node.RestToProcess.Length - wordInMorse.Length), node.Solution);
                    }
                }
            }
        }

        private sealed class Node
        {
            public string Word { get; }
            public string RestToProcess { get; }
            public string Solution { get; }

            public Node(in string word, in string restToProcess, in string solution)
            {
                Word = word;
                RestToProcess = restToProcess;
                Solution = solution;
            }
        }
    }
}
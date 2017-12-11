using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode {
    public class FourthDay : Solution {
        public FourthDay() {
        }
        public override void Run() {
            base.PrintHeader();
            TextReader tr = new StreamReader(@"FourthInput.txt");

            int validPasswordsRepeated = 0;
            int validPasswordsAnagrams = 0;
            string line = tr.ReadLine();
            while (line != null) {
                HashSet<string> words = new HashSet<string>();
                HashSet<string> anagrams = new HashSet<string>();
                bool repeated = false;
                bool anagram = false;
                foreach (string word in line.Split(null)) {
                    if (words.Contains(word)) {
                        repeated = true;
                    }
                    if (anagrams.Contains(Sort(word))) {
                        anagram = true;
                    }
                    words.Add(word);
                    anagrams.Add(Sort(word));
                }
                validPasswordsAnagrams += !anagram ? 1 : 0;
                validPasswordsRepeated += !repeated ? 1 : 0;
                line = tr.ReadLine();
            }

            Console.Out.WriteLine("{0} valid passphrases without repetitions", validPasswordsRepeated);
            Console.Out.WriteLine("{0} valid passphrases without anagrams", validPasswordsAnagrams);

        }

        String Sort(string word) {
            List<char> list = new List<char>(word.ToCharArray());
            list.Sort();
            string result = "";

            foreach (char c in list) {
                result += c;
            }

            return result;
        }
    }
}

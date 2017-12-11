using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode {
    public class FifthDay : Solution {
        public FifthDay() {
        }

        public override void Run() {
            base.PrintHeader();
            TextReader tr = new StreamReader(@"FifthInput.txt");
            int position = 0;
            List<int> jumps = new List<int>();
            int steps = 0;
            string line = tr.ReadLine();
            while (line != null) {
                jumps.Add(int.Parse(line));
                line = tr.ReadLine();
            }
            while (position >= 0 && position < jumps.Count) {
                position = position + GetAndUpdateJump(position, jumps, false);
                steps++;
            }
            Console.Out.WriteLine("{0} steps required to exit procedure.", steps);

        }

        private int GetAndUpdateJump(int position, List<int> jumps, bool firstPart) {
            if (firstPart) {
                return jumps[position]++;
            } else {
                int jump = jumps[position];
                if (jump >= 3) {
                    jumps[position]--;
                } else {
                    jumps[position]++;
                }
                return jump;
            }
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode {
    public class SecondDay : Solution {
        public SecondDay() {
        }

        public override void Run() {
            base.PrintHeader();
            TextReader tr = new StreamReader(@"SecondInput.txt");

            string line = tr.ReadLine();
            int checksumDifference = 0;
            int checksumDivision = 0;


            while (line != null) {
                string[] values = line.Split(null);
                int min = int.MaxValue;
                int max = int.MinValue;
                List<int> row = new List<int>();

                for (int i = 0; i < values.Length; i++) {
                    int current;
                    bool success = int.TryParse(values[i], out current);
                    if (success) {
                        if (current > max) {
                            max = current;
                        }
                        if (current < min) {
                            min = current;
                        }
                        List<int> divisions = new List<int>();
                        foreach (int value in row) {
                            if (value % current == 0) {
                                divisions.Add(value / current);
                            } else if (current % value == 0) {
                                divisions.Add(current / value);
                            }
                        }
                        if (divisions != null && divisions.Count > 0) {
                            foreach (int div in divisions) {
                                checksumDivision += div;
                            }
                        }
                        row.Add(current);
                    }
                }
                checksumDifference += max - min;
                line = tr.ReadLine();
            }
            Console.Out.WriteLine("Difference checksum = " + checksumDifference);

            Console.Out.WriteLine("Division checksum = " + checksumDivision);
        }
    }
}

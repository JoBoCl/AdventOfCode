using System;
using System.Collections.Generic;

namespace AdventOfCode {
  internal class SeventeenthDay : Solution {
    public SeventeenthDay() {
    }

    private static readonly int INPUT = 349;

    public override void Run() {
      base.PrintHeader();

      List<int> entries = new List<int>();
      entries.Add(0);
      int currentPosition = 0;

      int insertions = 1;
      while (insertions < 2018) {
        currentPosition = 1 + (currentPosition + INPUT) % entries.Count;
        entries.Insert(currentPosition, insertions++);
      }

      Console.Out.WriteLine(entries[entries.IndexOf(2017) + 1]);


      int afterZero = entries[1];
      while (insertions < 50_000_000) {
        currentPosition = 1 + (currentPosition + INPUT) % insertions;
        if (currentPosition == 1) {
          afterZero = insertions;
        }
        insertions++;
      }

      Console.Out.WriteLine(afterZero);
    }
  }
}
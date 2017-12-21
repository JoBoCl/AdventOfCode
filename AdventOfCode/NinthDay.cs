using System;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode {
  public class NinthDay : Solution {
    public override void Run() {
      base.PrintHeader();
      TextReader tr = new StreamReader(@"NinthInput.txt");
      Stack<bool> outerGroupScores = new Stack<bool>();
      int score = 0;
      int rubbishCount = 0;
      int c = tr.Read();
      bool inRubbish = false;
      while (c != -1) {
        switch (Convert.ToChar(c)) {
          case '{':
            if (!inRubbish) {
              outerGroupScores.Push(true);
              score += outerGroupScores.Count;
            } else {
              rubbishCount++;
            }
            break;
          case '}':
            if (!inRubbish) {
              outerGroupScores.Pop();
            } else {
              rubbishCount++;
            }
            break;
          case '!':
            tr.Read();
            break;
          case '<':
            if (inRubbish) {
              rubbishCount++;
            }
            inRubbish = true;
            break;
          case '>':
            inRubbish = false;
            break;
          default:
            if (inRubbish) {
              rubbishCount++;
            }
            break;
        }
        c = tr.Read();
      }

      Console.Out.WriteLine("{0} total stream score", score);
      Console.Out.WriteLine("{0} total rubbish count", rubbishCount);
    }

    public NinthDay() {
    }
  }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode {
  class SixteenthDay : Solution {

    private char[] programs = "abcdefghijklmnop".ToCharArray();

    public override void Run() {
      base.PrintHeader();
      TextReader tr = new StreamReader(@"SixteenthInput.txt");
      string input = tr.ReadLine();

      List<string> states = new List<string>();
      states.Add(PrintPrograms());

      Dance(input);
      Console.Out.WriteLine(PrintPrograms());

      states.Add(PrintPrograms());

      for (int i = 1; i < 1_000_000_000; i++) {
        Dance(input);
        string newState = PrintPrograms();
        if (states.Contains(newState)) {
          int cycleLength = states.Count - states.IndexOf(newState);
          int repetitions = (1_000_000_000 - states.Count) / cycleLength;
          Console.Out.WriteLine(states[1_000_000_000 - (cycleLength * repetitions) - states.Count]);
          return;
        } else {
          states.Add(newState);
        }
      }
    }

    private string PrintPrograms() {
      StringBuilder s = new StringBuilder();
      foreach (char c in programs) {
        s.Append(c);
      }
      return s.ToString();
    }

    private void Dance(string input) {
      foreach (string step in input.Split(",")) {
        char instruction = step.ToCharArray()[0];
        string[] args = step.Substring(1).Split("/");
        switch (instruction) {
          case 's':
            int pos = int.Parse(args[0]);
            Spin(programs, pos);
            break;
          case 'x':
            int a = int.Parse(args[0]);
            int b = int.Parse(args[1]);
            Exchange(programs, a, b);
            break;
          case 'p':
            char charA = args[0].ToCharArray()[0];
            char charB = args[1].ToCharArray()[0];
            Partner(programs, charA, charB);
            break;
          default:
            break;
        }
      }
    }

    private int Index(char[] programs, char c) {
      for (int i = 0; i < programs.Length; i++) {
        if (programs[i] == c) {
          return i;
        }
      }
      return -1;
    }

    private void Partner(char[] programs, char charA, char charB) {
      Exchange(programs, Index(programs, charA), Index(programs, charB));
    }

    private void Exchange(char[] programs, int a, int b) {
      char temp = programs[a];
      programs[a] = programs[b];
      programs[b] = temp;
    }

    private void Spin(char[] programs, int count) {
      char[] temp = new char[programs.Length];
      int i = 0;
      while (i < programs.Length) {
        if (i < count) {
          temp[i] = programs[programs.Length - count + i];
        } else {
          temp[i] = programs[i - count];
        }
        i++;
      }
      this.programs = temp;
    }
  }
}

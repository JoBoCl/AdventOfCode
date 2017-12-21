using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode {
  class EighteenthDay : Solution {

    private Dictionary<string, long> registers = new Dictionary<string, long>();

    public override void Run() {
      base.PrintHeader();
      TextReader tr = new StreamReader(@"EighteenthInput.txt");
      List<string> instructions = new List<string>();
      string line = tr.ReadLine();
      int currentInstruction = 0;
      while (line != null) {
        instructions.Add(line);
        registers.TryAdd(line.Split(null)[1], 0);
        line = tr.ReadLine();
      }

      long lastPlayed = 0;
      while (0 <= currentInstruction && currentInstruction < instructions.Count) {
        line = instructions[currentInstruction];        
        string[] tokens = line.Split(null);
        string register = tokens[1];
        Ops op = (Ops)Enum.Parse(typeof(Ops), tokens[0], true);
        switch (op) {
          case Ops.SET:
            if (int.TryParse(tokens[2], out int value)) {
              if (!registers.ContainsKey(tokens[1])) {
                registers.Add(tokens[1], int.Parse(tokens[2]));
              } else {
                registers[register] = value;
              }
            } else {
              if (!registers.ContainsKey(tokens[1])) {
                registers.Add(tokens[1], registers[tokens[2]]);
              } else {
                registers[register] = registers[tokens[2]];
              }
            }
            break;
          case Ops.ADD:
            if (int.TryParse(tokens[2], out value)) {
              registers[register] += value;
            } else {
              registers[register] += registers[tokens[2]];
            }
            break;
          case Ops.MUL:
            if (int.TryParse(tokens[2], out value)) {
              registers[register] *= value;
            } else {
              registers[register] *= registers[tokens[2]];
            }
            break;
          case Ops.MOD:
            if (int.TryParse(tokens[2], out value)) {
              registers[register] %= value;
            } else {
              registers[register] %= registers[tokens[2]];
            }
            break;
          case Ops.SND:
            if (registers[register] != 0) {
              lastPlayed = registers[register];
            }
            break;
          case Ops.RCV:
            if (register != "0" && lastPlayed != 0) {
              Console.Out.WriteLine("{0} last played note", lastPlayed);
              return;
            }
            break;
          case Ops.JGZ:
            if (registers[register] > 0) {
              currentInstruction += int.Parse(tokens[2]) - 1;
            }
            break;
        }
        currentInstruction++;
      }
    }

    enum Ops {
      SET, ADD, MUL, MOD, SND, RCV, JGZ
    }
  }
}

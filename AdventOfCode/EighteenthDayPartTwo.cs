using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode {
  class EighteenthDayPartTwo : Solution {

    private Queue<long> channelAB = new Queue<long>();
    private Queue<long> channelBA = new Queue<long>();

    class Program {

      private readonly List<string> instructions = new List<string>();
      private Dictionary<string, long> registers = new Dictionary<string, long>();
      private int currentInstruction = 0;
      private readonly Queue<long> send;
      private readonly Queue<long> receive;
      private int messagesSent = 0;

      public Program(Queue<long> send, Queue<long> receive, List<string> instructions, List<string> registers) {
        this.send = send;
        this.receive = receive;
        this.instructions = instructions;
        foreach (var r in registers) {
          this.registers.Add(r, 0);
        }
      }

      public long MessagesSent() {
        return messagesSent;
      }

      public bool CanOperate() {
        if (!(0 <= currentInstruction && currentInstruction < instructions.Count)) {
          return false;
        } else if (instructions[currentInstruction].Split(null)[0] == "rcv") {
          return receive.Count > 0;
        } else {
          return true;
        }
      }

      public void RunOnce() {        
        if (0 <= currentInstruction && currentInstruction < instructions.Count) {
          string line = instructions[currentInstruction];
          string[] tokens = line.Split(null);
          string register = tokens[1];
          Operations op = (Operations)Enum.Parse(typeof(Operations), tokens[0], true);
          switch (op) {
            case Operations.SET:
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
            case Operations.ADD:
              if (int.TryParse(tokens[2], out value)) {
                registers[register] += value;
              } else {
                registers[register] += registers[tokens[2]];
              }
              break;
            case Operations.MUL:
              if (int.TryParse(tokens[2], out value)) {
                registers[register] *= value;
              } else {
                registers[register] *= registers[tokens[2]];
              }
              break;
            case Operations.MOD:
              if (int.TryParse(tokens[2], out value)) {
                registers[register] %= value;
              } else {
                registers[register] %= registers[tokens[2]];
              }
              break;
            case Operations.SND:
              if (registers[register] != 0) {
                messagesSent++;
                send.Enqueue(registers[register]);
              }
              break;
            case Operations.RCV:
              registers[register] = receive.Dequeue();
              break;
            case Operations.JGZ:
              if (registers[register] > 0) {
                currentInstruction += int.Parse(tokens[2]) - 1;
              }
              break;
          }
          currentInstruction++;
        }
      }
    }

    public override void Run() {
      base.PrintHeader();
      TextReader tr = new StreamReader(@"EighteenthInput.txt");
      List<string> instructions = new List<string>();
      List<string> registers = new List<string>();
      string line = tr.ReadLine();

      while (line != null) {
        instructions.Add(line);
        string reg = line.Split(null)[1];
        if (!registers.Contains(reg)) {
          if (!int.TryParse(reg, out int ignored)) {
            registers.Add(reg);
          }
        }
        line = tr.ReadLine();
      }

      Program prgA = new Program(channelAB, channelBA, instructions, registers);
      Program prgB = new Program(channelBA, channelAB, instructions, registers);

      while (prgA.CanOperate() || prgB.CanOperate()) {
        if (prgA.CanOperate()) {
          prgA.RunOnce();
        }
        if (prgB.CanOperate()) {
          prgB.RunOnce();
        }
      }

      Console.Out.WriteLine("{0} messages sent.", prgA.MessagesSent());
    }

    enum Operations {
      SET, ADD, MUL, MOD, SND, RCV, JGZ
    }
  }
}

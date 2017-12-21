using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode {
  class EleventhDay : Solution {

    class Hex {

      private int q;
      private int r;
      private int s;

      public Hex(int q, int r, int s) {
        this.q = q;
        this.r = r;
        this.s = s;
      }

      public Hex() {
        q = 0;
        r = 0;
        s = 0;
      }

      public int CentreDistance() {
        return (Math.Abs(q) + Math.Abs(r) + Math.Abs(s)) / 2;
      }

      public void Move(Direction direction) {
        switch (direction) {
          case Direction.N:
            r++;
            s--;
            break;
          case Direction.NE:
            q++;
            s--;
            break;
          case Direction.SE:
            q++;
            r--;
            break;
          case Direction.S:
            r--;
            s++;
            break;
          case Direction.SW:
            q--;
            s++;
            break;
          case Direction.NW:
            q--;
            r++;
            break;
          case Direction.NONE:
            break;
          default:
            break;
        }
      }
    }

    private enum Direction {
      N, NE, SE, S, SW, NW, NONE
    }



    public override void Run() {
      base.PrintHeader();
      TextReader tr = new StreamReader(@"EleventhInput.txt");
      string input = tr.ReadLine();
      string[] directions = input.Split(",");
      Hex pos = new Hex();
      int maxDist = 0;
      foreach (string dir in directions) {
        pos.Move((Direction)Enum.Parse(typeof(Direction), dir.ToUpper()));
        if (pos.CentreDistance() > maxDist) {
          maxDist = pos.CentreDistance();
        }
      }

      Console.Out.WriteLine("{0} steps from centre.", pos.CentreDistance());
      Console.Out.WriteLine("{0} max steps from centre.", maxDist);
    }
  }
}
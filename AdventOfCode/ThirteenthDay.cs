using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AdventOfCode {
  class ThirteenthDay : Solution {
    public override void Run() {
      base.PrintHeader();
      int[] fireWall = BuildFirewall();
      bool[] backwards = new bool[fireWall.Length];
      int[] scanners = new int[fireWall.Length];

      int position = 0;
      int score = 0;
      while (position < fireWall.Length) {
        if (scanners[position] == 0) {
          score += position * fireWall[position];
        }
        UpdateScanners(fireWall, scanners, backwards);
        position++;
      }
      Console.Out.WriteLine("{0} crossing severity", score);

      for (int i = 0; i < fireWall.Length; i++) {
        backwards[i] = false;
        scanners[i] = 0;
      }

      position = 0;
      int delay = 0;
      bool traversed = false;
      while (!traversed) {
        while (position < fireWall.Length) {
          bool caught = ((delay + position) % ((fireWall[position] * 2) - 2) == 0) && fireWall[position] > 0;
          if (caught) {
            break;
          }
          position++;
        }
        traversed = position == fireWall.Length;

        // Setup for the next try.
        delay++;
        for (int i = 0; i < fireWall.Length; i++) {
          backwards[i] = false;
          scanners[i] = 0;
        }
        position = 0;
      }
      Console.Out.WriteLine("{0} min delay", delay - 1);
    }

    private void UpdateScanners(int[] fireWall, int[] scanners, bool[] backwards) {
      for (int i = 0; i < fireWall.Length; i++) {
        if (fireWall[i] == 0) {
          continue;
        }
        if (scanners[i] == 0) {
          backwards[i] = false;
        }
        if (scanners[i] == fireWall[i] - 1) {
          backwards[i] = true;
        }
        scanners[i] += backwards[i] ? -1 : 1;
      }
    }

    private int[] BuildFirewall() {
      TextReader tr = new StreamReader(@"ThirteenthInput.txt");
      string line = tr.ReadLine();
      List<int> firewall = new List<int>();
      int lastSeen = 0;
      while (line != null) {
        string[] tokens = line.Split(": ");
        int depth = int.Parse(tokens[0]);
        int range = int.Parse(tokens[1]);
        while (lastSeen < depth) {
          firewall.Add(0);
          lastSeen++;
        }
        firewall.Add(range);
        lastSeen++;
        line = tr.ReadLine();
      }
      return firewall.ToArray();
    }

  }
}

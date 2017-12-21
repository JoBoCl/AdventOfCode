using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode {
  class FourteenthDay : Solution {

    private static readonly String INPUT = "hxtvlmkl";


    private Dictionary<int, HashSet<int>> groups = new Dictionary<int, HashSet<int>>();

    private static readonly int FILLED = -1;
    private static readonly int EMPTY = -2;

    private int[,] disk = new int[128, 128];

    public override void Run() {
      base.PrintHeader();
      int filled = 0;
      for (int row = 0; row < 128; row++) {
        string hash = TenthDay.KnotHash(String.Format("{0}-{1}", INPUT, row));
        filled += CountOnes(hash, disk, row);
      }

      Console.Out.WriteLine("{0} bytes filled", filled);
      int nextGroup = 1;
      // Console.Out.WriteLine(PrintDisk(disk, false));
      for (int row = 0; row < 128; row++) {
        for (int col = 0; col < 128; col++) {
          // Ignore all empty elements.
          if (disk[row, col] == FILLED) {
            bool leftCellFilled = col != 0 && disk[row, col - 1] != EMPTY;
            bool aboveCellFilled = row != 0 && disk[row - 1, col] != EMPTY;

            if (leftCellFilled && aboveCellFilled) {
              int leftGroup = disk[row, col - 1];
              int aboveGroup = disk[row - 1, col];

              if (leftGroup == aboveGroup) {
                // Groups already joined, simply add to the group.
                disk[row, col] = leftGroup;
              } else {
                // Groups need to be joined, pick the min, join and add to the min group.
                int parentGroup = Math.Min(leftGroup, aboveGroup);
                JoinGroups(parentGroup, Math.Max(leftGroup, aboveGroup));
                disk[row, col] = parentGroup;
              }
            } else if (leftCellFilled) { // Check if we're immediately to the right of a group that exists.
              disk[row, col] = disk[row, col - 1];
            } else if (aboveCellFilled) {// Check if we're immediately below a group that already exists.
              disk[row, col] = disk[row - 1, col];
            } else {
              disk[row, col] = nextGroup;
              groups.Add(nextGroup, new HashSet<int>());
              nextGroup++;
            }
            groups[disk[row, col]].Add(GetPosition(row, col));
          }
        }
      }
      string htmlTable = PrintDisk(disk, true);
      Console.Out.WriteLine((from g in groups.Keys where groups[g].Count > 0 select g).Count());
    }

    private string PrintDisk(int[,] disk, bool pad) {
      StringBuilder result = new StringBuilder().Append("<table>");
      for (int row = 0; row < 128; row++) {
        result.Append("<tr>");
        for (int col = 0; col < 128; col++) {
          result.Append("<td>");
          if (disk[row, col] == FILLED) {
            result.Append("#".PadLeft(pad ? 4 : 0));
          } else if (disk[row, col] == EMPTY) {
            result.Append(".".PadLeft(pad ? 4 : 0));
          } else {
            result.Append(disk[row, col].ToString().PadLeft(4));
          }
          result.Append(pad ? " " : "");
          result.Append("</td>");
        }
        result.Append("</tr>");
        result.Append(Environment.NewLine);
      }
      result.Append("</table>");
      result.Append(Environment.NewLine);
      return result.ToString();
    }

    private void GetCoordinate(int position, out int row, out int col) {
      col = position % 128;
      row = (position - col) / 128;
    }

    private int GetPosition(int row, int col) {
      return row * 128 + col;
    }

    private void JoinGroups(int parent, int child) {
      foreach (int position in groups[child]) {
        GetCoordinate(position, out int row, out int col);
        disk[row, col] = parent;
        groups[parent].Add(position);
      }
      groups[child].Clear();
    }

    // Counts number of 1 in the binary representation of a hex-string.
    private int CountOnes(string input, int[,] disk, int row) {
      int countOnes = 0;
      int col = 4;
      foreach (char c in input) {
        int hex = Convert.ToInt32(c.ToString(), 16);
        for (int i = 0; i < 4; i++) {
          col--;
          int lsb = hex & 1;
          if (lsb == 1) {
            disk[row, col] = FILLED;
          } else {
            disk[row, col] = EMPTY;
          }
          countOnes += lsb;
          hex >>= 1;
        }
        col += 8;
      }
      return countOnes;
    }
  }
}

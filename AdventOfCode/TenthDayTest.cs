using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode {
  internal class TenthDayTest : Solution {
    public TenthDayTest() {
    }

    private static readonly int[] INPUT = { 3, 4, 1, 5 };

    public override void Run() {
      base.PrintHeader();
      int skipSize = 0;
      int currentPosition = 0;
      int[] range = Enumerable.Range(0, 5).ToArray();

      foreach (int length in INPUT) {
        Console.WriteLine(string.Join(',', range));
        Reverse(range, currentPosition, length);
        currentPosition += (length + skipSize) % range.Length;
        skipSize++;
      }
      Console.WriteLine(string.Join(',', range));

      Console.WriteLine("{0} = {1} * {2}", range[0] * range[1], range[0], range[1]);
    }

    // Reverses the section of arr for arr[start, start+length), circling round as needed.
    private void Reverse(int[] arr, int start, int length) {
      if (length == 5) {
        Console.WriteLine("Last iteration");
      }
      if (length <= 1) {
        return;
      }
      Swap(arr, start % arr.Length, (start + length - 1) % arr.Length);
      Reverse(arr, (start + 1) % arr.Length, length - 2);
    }

    private void Swap(int[] arr, int a, int b) {
      if (a == b) {
        return;
      }
      int temp = arr[a];
      arr[a] = arr[b];
      arr[b] = temp;
    }
  }
}
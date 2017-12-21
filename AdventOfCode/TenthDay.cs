using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode {
  internal class TenthDay : Solution {
    public TenthDay() {
    }

    private static readonly int[] INPUT = { 102, 255, 99, 252, 200, 24, 219, 57, 103, 2, 226, 254, 1, 0, 69, 216 };

    private static readonly string TO_HASH = "102,255,99,252,200,24,219,57,103,2,226,254,1,0,69,216";

    private static readonly int[] SUFFIX = { 17, 31, 73, 47, 23 };

    public override void Run() {
      base.PrintHeader();
      int skipSize = 0;
      int currentPosition = 0;
      int[] range = Enumerable.Range(0, 256).ToArray();

      KnotPass(ref skipSize, ref currentPosition, range, INPUT);

      Console.WriteLine("After the first knot pass: {0} = {1} * {2}", range[0] * range[1], range[0], range[1]);

      string hash = KnotHash(TO_HASH);

      Console.WriteLine(hash);
    }

    public static string KnotHash(string input) {
      IEnumerable<int> arr = from c in input select Convert.ToInt32(c);
      List<int> hashBuilder = new List<int>();
      hashBuilder.AddRange(arr);
      hashBuilder.AddRange(SUFFIX);

      int skipSize = 0;
      int currentPosition = 0;
      int[] range = Enumerable.Range(0, 256).ToArray();

      int[] toHash = hashBuilder.ToArray();

      for (int i = 0; i < 64; i++) {
        KnotPass(ref skipSize, ref currentPosition, range, toHash);
      }

      int[] dense = DenseHash(range);

      return PrintArr(dense);
    }

    private static string PrintArr(int[] arr) {
      StringBuilder builder = new StringBuilder();
      foreach (int i in arr) {
        builder.Append(i.ToString("X").ToLower().PadLeft(2, '0'));
      }
      return builder.ToString();
    }

    private static void KnotPass(ref int skipSize, ref int currentPosition, int[] range, int[] input) {
      foreach (int length in input) {
        Reverse(range, currentPosition, length);
        currentPosition += (length + skipSize) % range.Length;
        skipSize++;
      }
    }

    // XORs in batches of 16.
    private static int[] DenseHash(int[] arr) {
      int[] res = new int[arr.Length / 16];
      for (int i = 0; i < res.Length; i++) {
        int hash = 0;
        for (int j = 0; j < 16; j++) {
          hash ^= arr[16 * i + j];
        }
        res[i] = hash;
      }
      return res;
    }

    // Reverses the section of arr for arr[start, start+length), circling round as needed.
    private static void Reverse(int[] arr, int start, int length) {
      if (length <= 1) {
        return;
      }
      Swap(arr, start % arr.Length, (start + length - 1) % arr.Length);
      Reverse(arr, (start + 1) % arr.Length, length - 2);
    }

    private static void Swap(int[] arr, int a, int b) {
      if (a == b) {
        return;
      }
      int temp = arr[a];
      arr[a] = arr[b];
      arr[b] = temp;
    }
  }
}
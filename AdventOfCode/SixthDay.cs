using System;
using System.Collections.Generic;

namespace AdventOfCode {
  public class SixthDay : Solution {

    private static int[] INITIAL_CONFIGURATION = new int[] {
      5 ,1, 10  ,0 ,1 ,7, 13 , 14,  3, 12 , 8, 10  ,7, 12  ,0 ,6
    };

    List<long> history = new List<long>();

    public override void Run() {
      base.PrintHeader();
      int arrayLength = INITIAL_CONFIGURATION.Length;
      int[] currentConfiguration = INITIAL_CONFIGURATION;
      int iterations = 0;
      while (!history.Contains(HashConfig(currentConfiguration))) {
        history.Add(HashConfig(currentConfiguration));
        int maxIndex = MaxAt(currentConfiguration);
        int oldValue = currentConfiguration[maxIndex];
        for (int i = 1; i <= oldValue; i++) {
          currentConfiguration[(maxIndex + i) % arrayLength]++;
        }
        currentConfiguration[maxIndex] -= oldValue;
        iterations++;
      }

      Console.Out.WriteLine("{0} iterations required.", iterations);
      Console.Out.WriteLine(
          "{0} loop length.",
          iterations - history.IndexOf(HashConfig(currentConfiguration)));
    }

    public SixthDay() {
    }

    private long HashConfig(int[] arr) {
      long result = 0;
      foreach (int val in arr) {
        result = (arr.Length * result) + val;
      }
      return result;
    }

    private int MaxAt(int[] arr) {
      int index = 0;
      int maxValue = int.MinValue;
      for (int i = 0; i < arr.Length; i++) {
        if (arr[i] > maxValue) {
          index = i;
          maxValue = arr[i];
        }
      }
      return index;
    }
  }
}

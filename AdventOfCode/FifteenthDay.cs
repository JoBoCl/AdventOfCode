using System;

namespace AdventOfCode {
  class FifteenthDay : Solution {

    private static readonly int INPUT_A = 289;
    private static readonly int INPUT_B = 629;

    private static readonly int MUL_A = 16807;
    private static readonly int MUL_B = 48271;

    class Judge {
      private readonly int mask = (1 << 16) - 1;

      int count = 0;

      public void Match(long a, long b) {
        if ((a & mask) == (b & mask)) {
          count++;
        }
      }

      public int Count() {
        return count;
      }
    }

    class Generator {
      private readonly long factor;
      private long previous;
      private readonly long divisor = 2147483647;
      private readonly int multiples;

      public Generator(int factor, int seed, int multiples) {
        this.factor = factor;
        this.previous = seed;
        this.multiples = multiples;
      }

      public long Next() {
        long next;
        do {
          next = (previous * factor) % divisor;
          previous = next;
        } while (previous % multiples != 0);
        return next;
      }
    }

    public override void Run() {
      base.PrintHeader();

      Generator genA = new Generator(MUL_A, INPUT_A, 1);
      Generator genB = new Generator(MUL_B, INPUT_B, 1);
      Judge judge = new Judge();

      for (int i = 0; i < 5; i++) {
        var a = genA.Next();
        var b = genB.Next();
        judge.Match(a, b);
      }

      for (int i = 0; i < 40_000_000 - 5; i++) {
        judge.Match(genA.Next(), genB.Next());
      }

      Console.Out.WriteLine("{0} matches", judge.Count());

      genA = new Generator(MUL_A, INPUT_A, 4);
      genB = new Generator(MUL_B, INPUT_B, 8);
      judge = new Judge();

      for (int i = 0; i < 5; i++) {
        var a = genA.Next();
        var b = genB.Next();
        judge.Match(a, b);
      }

      for (int i = 0; i < 5_000_000 - 5; i++) {
        judge.Match(genA.Next(), genB.Next());
      }
      Console.Out.WriteLine("{0} matches", judge.Count());
    }
  }
}

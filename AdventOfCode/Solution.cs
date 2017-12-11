using System;
namespace AdventOfCode {
    public abstract class Solution {
    abstract public void Run();
    protected void PrintHeader() {
      Console.Out.WriteLine("====" + this.GetType().ToString() + "====");
      Console.Out.WriteLine();
    }
    }
}

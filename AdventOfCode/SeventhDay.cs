using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode {
  public class SeventhDay : Solution {
    public SeventhDay() {
    }

    Dictionary<string, Tree> forest = new Dictionary<string, Tree>();

    public override void Run() {
      base.PrintHeader();
      TextReader tr = new StreamReader(@"SeventhInput.txt");
      Tree lastTree = null;
      string line = tr.ReadLine();
      while (line != null) {
        if (line.Contains(" -> ")) {
          string[] subTrees = line.Split(null);
          string name = subTrees[0];
          string weightString = subTrees[1].Substring(1, subTrees[1].Length - 2);
          int weight = int.Parse(weightString);
          Tree parent = null;
          if (forest.ContainsKey(name)) {
            parent = forest[name];
            parent.Weight = weight;
          } else {
            parent = new Tree(name, weight);
            forest.Add(name, parent);
          }
          for (int i = 3; i < subTrees.Length; i++) {
            // Remove trailing comma.
            string subTreeName = subTrees[i].Substring(0, subTrees[i].Length - (i != subTrees.Length - 1 ? 1 : 0)).Trim();
            Tree subTree = new Tree(subTreeName);
            if (forest.ContainsKey(subTreeName)) {
              subTree = forest[subTreeName];
            } else {
              forest.Add(subTreeName, subTree);
            }
            parent.Add(subTree);
          }
          lastTree = parent;
        } else {
          string[] nameWeight = line.Split(null);
          string name = nameWeight[0].Trim();
          string weightString = nameWeight[1].Substring(1, nameWeight[1].Length - 2);
          int weight = int.Parse(weightString);
          if (forest.ContainsKey(name)) {
            Tree tree = forest[name];
            tree.Weight = weight;
          } else {
            forest.Add(name, new Tree(name, weight));
          }
        }
        line = tr.ReadLine();
      }

      while (lastTree != null && lastTree.Parent != null) {
        lastTree = lastTree.Parent;
      }
      if (lastTree != null) {
        Console.Out.WriteLine(lastTree.Name);
      }

      Tree rootTree = lastTree;

      Stack<Tree> treeStack = new Stack<Tree>();
      AssignWeights(rootTree);
      Console.Out.WriteLine("{0} total weight", rootTree.TotalWeight);
      // By inspection, the odd weight is in tulwp, which is 8 heavier and should weight 256.
    }

    private void AssignWeights(Tree tree) {
      foreach (Tree child in tree.Children) {
        AssignWeights(child);
      }
      if (tree.Children.Count == 0) {
        tree.TotalWeight = tree.Weight;
      } else {
        tree.TotalWeight = (from child in tree.Children select child.TotalWeight).Sum() + tree.Weight;
      }
    }

    private class Tree {
      List<Tree> subnodes = new List<SeventhDay.Tree>();

      public string Name { get; }
      public int Weight { get; set; }

      public Tree Parent { get; set; }
      private int totalWeight;
      public int TotalWeight {
        get {
          return this.totalWeight;
        }
        set {
          int oldWeight = this.totalWeight;
          totalWeight = value;
          if (this.Parent != null) {
            this.Parent.TotalWeight += value - oldWeight;
          }
        }
      }
      public List<Tree> Children {
        get {
          return subnodes;
        }
      }

      public Tree(string name) {
        this.Name = name;
      }

      public Tree(string name, int weight) {
        this.Name = name;
        this.Weight = weight;
      }


      public void Add(Tree tree) {
        tree.Parent = this;
        this.TotalWeight += tree.TotalWeight;
        subnodes.Add(tree);
      }
    }
  }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode {
  class TwelthDay : Solution {

    private Dictionary<int, Node> nodes = new Dictionary<int, Node>();

    class Node {
      List<Node> neighbours = new List<Node>();
      bool visited = false;
      public int Value { get; private set; }
      public bool Visited { get { return visited; } }

      public Node(int node) {
        this.Value = node;
      }

      public List<Node> Visit() {
        visited = true;
        return neighbours;
      }

      public void AddNeighbour(Node node) {
        neighbours.Add(node);
      }

      public void Unvisit() {
        this.visited = false;
      }
    }


    public override void Run() {
      base.PrintHeader();

      TextReader tr = new StreamReader(@"TwelthInput.txt");
      string line = tr.ReadLine();
      while (line != null) {
        string[] elems = line.Split(null);
        int srcIdx = int.Parse(elems[0]);
        Node source = nodes.GetValueOrDefault(srcIdx, new Node(srcIdx));
        if (!nodes.ContainsKey(srcIdx)) {
          nodes.Add(srcIdx, source);
        }
        for (int i = 2; i < elems.Length; i++) {
          int v = int.Parse(elems[i].Replace(",", ""));
          Node recip = nodes.GetValueOrDefault(v, new Node(v));
          if (!nodes.ContainsKey(v)) {
            nodes.Add(v, recip);
          }
          source.AddNeighbour(recip);
        }
        line = tr.ReadLine();
      }

      Node zero = nodes[0];
      Queue<Node> queue = new Queue<Node>();
      int count = 0;
      queue.Enqueue(zero);

      while (queue.Count > 0) {
        Node current = queue.Dequeue();
        if (!current.Visited) {
          foreach (Node node in current.Visit()) {
            queue.Enqueue(node);
          }
          count++;
        }
      }

      Console.Out.WriteLine("{0} nodes connected to 0", count);

      foreach (Node node in nodes.Values) {
        node.Unvisit();
      }

      int groups = 0;

      while (nodes.Count > 0) {
        int min = (from key in nodes.Keys select key).Min();
        queue.Enqueue(nodes[min]);
        while (queue.Count > 0) {
          Node current = queue.Dequeue();
          nodes.Remove(current.Value);
          if (!current.Visited) {
            foreach (Node node in current.Visit()) {
              queue.Enqueue(node);
            }
          }
        }
        groups++;
      }
      Console.Out.WriteLine("{0} groups of nodes", groups);
    }
  }
}
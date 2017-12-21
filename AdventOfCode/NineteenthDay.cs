using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode {
  class NineteenthDay : Solution {
    public override void Run() {
      base.PrintHeader();
      TextReader tr = new StreamReader(@"NineteenthInput.txt");
      List<char[]> lines = new List<char[]>();
      string line;
      do {
        line = tr.ReadLine();
        lines.Add(line.ToCharArray());
      } while (line != null);

      char[][] maze = lines.ToArray();
      List<char> visited = new List<char>();

      int y = 0;
      int x = FindFirstPipe(maze);
      char currentChar = '|';
      Direction dir = Direction.DOWN;
      Position current = new Position(x, y);

      while (currentChar != 'Q') {
        NextPosition(dir, current, out Position next);
        char nextChar = maze[next.X][next.Y];
        switch (nextChar) {
          case '+':
            switch (dir) {
              case Direction.UP:
              case Direction.DOWN:
                NextPosition(Direction.LEFT, current, out Position left);
                NextPosition(Direction.RIGHT, current, out Position right);
                if (maze[left.X][left.Y] == ' ') {
                  current = right;
                } else {
                  current = left;
                }
                break;
              case Direction.LEFT:
              case Direction.RIGHT:
                NextPosition(Direction.UP, current, out Position up);
                NextPosition(Direction.DOWN, current, out Position down);
                if (maze[up.X][up.Y] == ' ') {
                  next = down;
                } else {
                  next = up;
                }
                break;
            }
            break;
          case '|':
            break;
          case '-':
            break;

        }
      }
    }

    private void NextPosition(Direction dir, Position current, out Position next) {
      int x, y;
      switch (dir) {
        case Direction.DOWN:
          x = current.X;
          y = current.Y + 1;
          break;
        case Direction.UP:
          x = current.X;
          y = current.Y - 1;
          break;
        case Direction.LEFT:
          x = current.X + 1;
          y = current.Y;
          break;
        case Direction.RIGHT:
          x = current.X - 1;
          y = current.Y;
          break;
      }
      next = new Position(x, y);
    }

    class Position {
      public int X { get; }
      public int Y { get; }

      public Position(int x, int y) {
        this.X = x;
        this.Y = y;
      }
    }

    enum Direction { UP, DOWN, LEFT, RIGHT }

    int FindFirstPipe(char[][] maze) {
      for (int i = 0; i < maze[0].Length; i++) {
        char c = maze[0][i];
        if (c == '|') {
          return i;
        }
      }
      return -1;
    }
  }
}

using System;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode {
    public class EighthDay : Solution {
        public EighthDay() {
        }

        static Dictionary<string, long> registers = new Dictionary<string, long>();
        static long largestValue = 0;

        public override void Run() {
            base.PrintHeader();
            TextReader tr = new StreamReader(@"EighthInput.txt");
            string line = tr.ReadLine();
            List<Instruction> instructions = new List<Instruction>();
            while (line != null) {
                string[] elems = line.Split(null);
                instructions.Add(Instruction.CreateBuilder().SetTarget(elems[0])
                           .SetOp(elems[1] == "inc" ? Operation.INC : Operation.DEC)
                           .SetValue(int.Parse(elems[2]))
                           .SetTestTarget(elems[4])
                           .SetCond(CreateCondition(elems[5]))
                           .SetTestValue(int.Parse(elems[6]))
                                 .Build());

                if (!registers.ContainsKey(elems[0])) {
                    registers.Add(elems[0], 0);
                }
                line = tr.ReadLine();
            }

            foreach (Instruction instr in instructions) {
                instr.Eval();
            }

            long max = long.MinValue;

            foreach (long val in registers.Values) {
                if (val != 0 && val > max) {
                    max = val;
                }
            }
            Console.Out.WriteLine("{0} = largest value", max);
            Console.Out.WriteLine("{0} = maximum alloc", largestValue);
        }

        Condition CreateCondition(string str) {
            switch (str) {
                case "==": return Condition.EQ;
                case "!=": return Condition.NEQ;
                case "<": return Condition.LT;
                case "<=": return Condition.LET;
                case ">": return Condition.GT;
                case ">=": return Condition.GET;
            }
            return Condition.EQ;
        }

        private enum Operation {
            INC,
            DEC
        }

        private enum Condition {
            EQ, NEQ, LT, LET, GT, GET
        }


        class Instruction {
            public string Target { get; }
            public Operation Op { get; }
            public int Value { get; }
            public Condition Test { get; }
            public string TestTarget { get; }
            public int TestValue { get; }

            private Instruction(string target, Operation op, int val, Condition test, string testTarget, int testValue) {
                this.Target = target;
                this.Op = op;
                this.Value = val;
                this.Test = test;
                this.TestTarget = testTarget;
                this.TestValue = testValue;
            }

            public static Builder CreateBuilder() => new Builder();

            public class Builder {
                string target;
                Operation op;
                int val;
                Condition test;
                string testTarget;
                int testValue;

                public Builder() { }

                public Builder SetTarget(string target) {
                    this.target = target;
                    return this;
                }

                public Builder SetTestTarget(string target) {
                    this.testTarget = target;
                    return this;
                }

                public Builder SetValue(int val) {
                    this.val = val;
                    return this;
                }

                public Builder SetTestValue(int val) {
                    this.testValue = val;
                    return this;
                }


                public Builder SetOp(Operation op) {
                    this.op = op;
                    return this;
                }

                public Builder SetCond(Condition test) {
                    this.test = test;
                    return this;
                }

                public Instruction Build() {
                    return new Instruction(target, op, val, test, testTarget, testValue);
                }
            }

            public void Eval() {
                bool change = false;
                switch (this.Test) {
                    case Condition.EQ:
                        change = registers[TestTarget] == TestValue;
                        break;
                    case Condition.NEQ:
                        change = registers[TestTarget] != TestValue;
                        break;
                    case Condition.LT:
                        change = registers[TestTarget] < TestValue;
                        break;
                    case Condition.LET:
                        change = registers[TestTarget] <= TestValue;
                        break;
                    case Condition.GT:
                        change = registers[TestTarget] > TestValue;
                        break;
                    case Condition.GET:
                        change = registers[TestTarget] >= TestValue;
                        break;
                }
                if (change) {
                    switch (this.Op) {
                        case Operation.INC:
                            registers[Target] += Value;

                            break;
                        case Operation.DEC:
                            registers[Target] -= Value;
                            break;
                    }
                }
                if (largestValue < registers[Target]) {
                    largestValue = registers[Target];
                }
            }
        }
    }
}

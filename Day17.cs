using System.Numerics;

namespace AdventOfCode2024;

internal class Day17
{
    int RegA, RegB, RegC;
    byte[] program;

    struct Machine(byte[] program)
    {
        public long A;
        public long B;
        public long C;
        public int InstrPtr = 0;
        public byte[] Program = program;

        public readonly string Output => string.Join(",", outputs);
        public List<byte> outputs = [];

        public void Exec()
        {
            while (InstrPtr < Program.Length)
            {
                switch (Program[InstrPtr])
                {
                    case 0:
                        A >>= (int)GetOperand(Program[InstrPtr + 1]);
                        break;
                    case 1:
                        B ^= Program[InstrPtr + 1];
                        break;
                    case 2:
                        B = GetOperand(Program[InstrPtr + 1]) % 8;
                        break;
                    case 3:
                        if (A != 0)
                            InstrPtr = Program[InstrPtr + 1] - 2;
                        break;
                    case 4:
                        B ^= C;
                        break;
                    case 5:
                        outputs.Add((byte)(GetOperand(Program[InstrPtr + 1]) % 8));
                        break;
                    case 6:
                        B = A >> (int)GetOperand(Program[InstrPtr + 1]);
                        break;
                    case 7:
                        C = A >> (int)GetOperand(Program[InstrPtr + 1]);
                        break;
                    default:
                        throw new NotImplementedException();
                }
                InstrPtr += 2;
            }
        }

        long GetOperand(byte op)
        {
            if (op <= 3) return op;
            else if (op == 4) return A;
            else if (op == 5) return B;
            else if (op == 6) return C;
            return 0; // invalid
        }
    }

    public Day17()
    {
        string[] lines = new StreamReader("inputs/day17.txt").ReadToEnd().Split('\n');
        RegA = int.Parse(lines[0]["Register A: ".Length..]);
        RegB = int.Parse(lines[1]["Register B: ".Length..]);
        RegC = int.Parse(lines[2]["Register C: ".Length..]);

        program = lines[4]["Program: ".Length..].Split(',').Select(c => (byte)(c[0] - '0')).ToArray();
    }

    public string Part1()
    {
        var m = new Machine(program)
        {
            A = RegA,
            B = RegB,
            C = RegC
        };
        m.Exec();
        return m.Output;
    }

    IEnumerable<long> GenA(byte[] prog)
    {
        if (prog.Length == 0)
        {
            yield return 0;
            yield break;
        }

        foreach (var high in GenA(prog[1..]))
        {
            for (var low = 0; low < 8; low++)
            {
                var a = high * 8 + low;
                var m = new Machine(program) { A = a, B = RegB, C = RegC };
                m.Exec();
                if (m.outputs.SequenceEqual(prog)) yield return a;
            }
        }
    }

    public long Part2()
    {
        return GenA(program).Min();
    }
}

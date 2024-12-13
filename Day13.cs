namespace AdventOfCode2024;

internal class Day13 {
    internal struct Machine
    {
        public (int dx, int dy) buttonA;
        public (int dx, int dy) buttonB;
        public (long x, long y) prize;
    }
    Machine[] machines;

    public Day13()
    {
        string[] strMachines = new StreamReader("inputs/day13.txt").ReadToEnd()
            .Replace("\r", "")
            .Split("\n\n");
        machines = strMachines.Select(s => {
            string[] l = s.Split('\n');
            string[] p = l[2][("Prize: ".Length)..].Split(", ");
            return new Machine() {
                buttonA = (
                    int.Parse(l[0][("Button A: X+".Length)..("Button A: X+00".Length)]),
                    int.Parse(l[0][("Button A: X+00, Y+".Length)..("Button A: X+00, Y+00".Length)])
                ),
                buttonB = (
                    int.Parse(l[1][("Button B: X+".Length)..("Button B: X+00".Length)]),
                    int.Parse(l[1][("Button B: X+00, Y+".Length)..("Button B: X+00, Y+00".Length)])
                ),
                prize = (
                    int.Parse(p[0][("X=".Length)..]),
                    int.Parse(p[1][("Y=".Length)..])
                )
            };
        }).ToArray();
    }

    long Cheapest(Machine m)
    {
        int det = m.buttonA.dx * m.buttonB.dy - m.buttonB.dx * m.buttonA.dy;
        if (det == 0) return 0;
        long pa = m.prize.x * m.buttonB.dy - m.prize.y * m.buttonB.dx;
        long pb = m.prize.y * m.buttonA.dx - m.prize.x * m.buttonA.dy;
        if (pa % det == 0 && pb % det == 0) return 3 * pa / det + pb / det;
        else return 0;
    }


    public long Part1() => machines.Select(Cheapest).Sum();

    public long Part2() => machines
        .Select(m => new Machine() {
            buttonA = m.buttonA,
            buttonB = m.buttonB,
            prize = (10000000000000 + m.prize.x, 10000000000000 + m.prize.y)
        }).Select(Cheapest).Sum();
}
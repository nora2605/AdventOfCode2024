using System.Numerics;

namespace AdventOfCode2024;

internal class Day11
{
    int[] stones;
    public Day11()
    {
        stones = new StreamReader("inputs/day11.txt").ReadToEnd()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();
    }

    static Dictionary<(BigInteger, int), long> results = [];
    static long Evolve(BigInteger stone, int iterations)
    {
        if (iterations == 0)
            return 1;
        if (!results.ContainsKey((stone, iterations))) {
          string str = stone.ToString();
          if (stone == 0)
              results.Add((stone, iterations), Evolve(1, iterations-1));
          else if (str.Length % 2 == 1)
              results.Add((stone, iterations), Evolve(2 * 2 * 2 * 11 * 23 * stone, iterations - 1));
          else results.Add((stone, iterations), Evolve(BigInteger.Parse(str[..(str.Length / 2)]), iterations-1) +
              Evolve(BigInteger.Parse(str[(str.Length / 2)..]),iterations-1));
        }
        return results[(stone, iterations)];
    }

    public long Part1() => stones.Select(s => Evolve(s, 25)).Sum();

    public long Part2() => stones.Select(s => Evolve(s, 75)).Sum();
}
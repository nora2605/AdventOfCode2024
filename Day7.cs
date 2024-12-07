using System.Numerics;

namespace AdventOfCode2024;

internal class Day7
{
    (long result, int[] terms)[] data;
    IEnumerable<IGrouping<bool, (long result, int[] terms)>>? partitioned;
    long firstSum = 0;

    public Day7()
    {
        data = new StreamReader("inputs/day7.txt").ReadToEnd().Split('\n').Select(x =>
        {
            var p = x.Split(": ");
            return (long.Parse(p[0]), p[1].Split(' ').Select(int.Parse).ToArray());
        }).ToArray();
    }

    static bool TermPossibleRec(long result, int[] terms, long aggregate, int pos)
    {
        if (aggregate > result) return false;
        if (pos == terms.Length) return aggregate == result;
        return TermPossibleRec(result, terms, aggregate + terms[pos], pos + 1) ||
            TermPossibleRec(result, terms, aggregate * terms[pos], pos + 1);
    }
    static bool TermPossibleRec2(long result, int[] terms, long aggregate, int pos)
    {
        if (aggregate > result) return false;
        if (pos == terms.Length) return aggregate == result;
        return TermPossibleRec2(result, terms, aggregate + terms[pos], pos + 1) ||
            TermPossibleRec2(result, terms, aggregate * terms[pos], pos + 1) ||
            TermPossibleRec2(result, terms, long.Parse($"{aggregate}{terms[pos]}"), pos + 1);
    }

    public long Part1()
    {
        return firstSum = (partitioned = data
            .GroupBy(x => TermPossibleRec(x.result, x.terms, x.terms[0], 1))
        ).First(x => x.Key).Sum(x => x.result);
    }

    public long Part2()
    {
        return firstSum + partitioned!
            .First(x => !x.Key)
            .Where(x => TermPossibleRec2(x.result, x.terms, x.terms[0], 1))
            .Sum(x => x.result);
    }
}

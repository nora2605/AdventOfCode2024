namespace AdventOfCode2024;
internal class Day2
{
    int[][] reports;
    public Day2()
    {
        reports = new StreamReader("inputs/day2.txt").ReadToEnd()
            .Split('\n')
            .Select(l => l
                .Split(' ')
                .Select(int.Parse)
                .ToArray()
            ).ToArray();
    }

    public int Part1() => reports.Where(Safe(0)).Count();

    public int Part2() => reports.Where(Safe(1)).Count();

    static Func<int[], bool> Safe(int thresh)
    {
        return (int[] seq) =>
        {
            int c = 0;
            bool inc = seq[0] < seq[1];
            for (int i = 1; i < seq.Length; i++)
            {
                int diff = seq[i] - seq[i - 1];
                if (
                    Math.Abs(diff) > 3 ||
                    diff == 0 ||
                    (diff < 0 && inc) ||
                    (diff > 0 && !inc)
                ) if (++c > thresh) return false;
            }
            return true;
        };
    }
}

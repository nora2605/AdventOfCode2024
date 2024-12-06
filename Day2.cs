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

    public int Part1()
    {
        return reports.Where(Safe).Count();
    }
    static bool Safe(int[] seq)
    {
        bool inc = seq[0] < seq[1];
        for (int i = 1; i < seq.Length; i++)
        {
            int diff = seq[i] - seq[i - 1];
            if (
                Math.Abs(diff) > 3 ||
                diff == 0 ||
                (diff < 0 && inc) ||
                (diff > 0 && !inc)
            ) return false;
        }
        return true;
    }

    static T[][] VariantsRemove1<T>(T[] arr)
    {
        T[][] set = new T[arr.Length + 1][];
        for (int i = 0; i < arr.Length; i++)
        {
            T[] els = new T[arr.Length - 1];
            int offset = 0;
            for (int j = 0; j < arr.Length; j++)
            {
                if (j != i)
                    els[j - offset] = arr[j];
                else offset++;
            }
            set[i] = els;
        }
        set[arr.Length] = arr;
        return set;
    }

    public int Part2()
    {
        return reports.Select(VariantsRemove1).Where(r => r.Any(Safe)).Count();
    }
}

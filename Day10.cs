namespace AdventOfCode2024;

internal class Day10
{
    int[][] heights;
    int[] zeroes;
    int w, h;

    public Day10()
    {
        string input = new StreamReader("inputs/day10.txt").ReadToEnd();
        zeroes = input.Replace("\r\n", "").Index().Where(x => x.Item == '0').Select(x => x.Index).ToArray();
        heights = input
            .Split("\r\n")
            .Select(x => x.ToCharArray().Select(c => c - '0').ToArray())
            .ToArray();
        w = heights[0].Length;
        h = heights.Length;
    }
    void FindPeaks(bool[,] peaks, (int y, int x) pos, int prevNumber)
    {
        int num = heights[pos.y][pos.x];
        if (num - prevNumber != 1) return;

        if (num == 9)
            peaks[pos.x, pos.y] = true;
        else
        {
            if (pos.x + 1 < w)
                FindPeaks(peaks, (pos.y, pos.x + 1), num);
            if (pos.x - 1 >= 0)
                FindPeaks(peaks, (pos.y, pos.x - 1), num);
            if (pos.y + 1 < h)
                FindPeaks(peaks, (pos.y + 1, pos.x), num);
            if (pos.y - 1 >= 0)
                FindPeaks(peaks, (pos.y - 1, pos.x), num);
        }
    }

    int FindRating((int y, int x) pos, int prevNumber)
    {
        int num = heights[pos.y][pos.x];
        if (num - prevNumber != 1) return 0;

        if (num == 9)
            return 1;
        else
        {
            int sum = 0;
            if (pos.x + 1 < w)
                sum += FindRating((pos.y, pos.x + 1), num);
            if (pos.x - 1 >= 0)
                sum += FindRating((pos.y, pos.x - 1), num);
            if (pos.y + 1 < h)
                sum += FindRating((pos.y + 1, pos.x), num);
            if (pos.y - 1 >= 0)
                sum += FindRating((pos.y - 1, pos.x), num);
            return sum;
        }
    }

    public int Part1()
    {
        return zeroes.Select(zero =>
        {
            (int y, int x) pos = Math.DivRem(zero, w);
            bool[,] peaks = new bool[w, h];
            FindPeaks(peaks, pos, -1);
            return peaks.Cast<bool>().Where(x => x).Count();
        }).Sum();
    }

    public int Part2()
    {
        return zeroes.Select(zero => FindRating(Math.DivRem(zero, w), -1)).Sum();
    }
}

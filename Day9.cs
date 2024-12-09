namespace AdventOfCode2024;

internal class Day9
{
    List<(int? index, int length)> files;
    public Day9()
    {
        int[] blockSizes = new StreamReader("inputs/day9.txt").ReadToEnd().ToCharArray().Select(x => x - '0').ToArray();
        files = [];
        bool free = false;
        int index = 0;
        for (int i = 0; i < blockSizes.Length; i++)
        {
            files.Add((free ? null : index++, blockSizes[i]));
            free = !free;
        }
    }

    public long Part1()
    {
        var alloc = files.Aggregate(Enumerable.Empty<int?>(), (a, b) => a.Concat(Enumerable.Repeat(b.index, b.length))).ToArray();
        var firstFree = 0;
        for (int i = alloc.Length - 1; i > 0; i--)
        {
            if (alloc[i] == null) continue;
            firstFree = Array.FindIndex(alloc, firstFree, x => x == null);
            if (firstFree < i)
            {
                alloc[firstFree] = alloc[i];
                alloc[i] = null;
            }
            else break;
        }
        return alloc.TakeWhile(x => x != null).Index().Select(i => (long)i.Item! * i.Index).Sum();
    }

    public long Part2()
    {
        for (int i = files.Count - 1; i > 0; i--)
        {
            var f = files[i];
            if (f.index == null) continue;
            var b = files.FindIndex(x => x.index == null && x.length >= f.length);
            if (b == -1)
                continue;
            if (b < i)
            {
                if (files[b].length == f.length)
                {
                    files[i] = files[b];
                    files[b] = f;
                }
                else
                {
                    files[i++] = (null, f.length);
                    files.Insert(b, f);
                    files[b + 1] = (null, files[b + 1].length - f.length);
                }
            }
        }
        List<int?> alloc = [];
        foreach (var file in files)
            alloc.AddRange(Enumerable.Repeat(file.index, file.length));
        return alloc.Index().Select(x => x.Item == null ? 0 : (long)x.Item! * x.Index).Sum();
    }
}

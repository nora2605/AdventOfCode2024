namespace AdventOfCode2024;

internal class Day6
{
    string[] lines;
    int w, h;
    int pos;
    bool[,] visited;

    public Day6()
    {
        string input = new StreamReader("inputs/day6.txt").ReadToEnd();
        lines = input.Replace("\r", "").Split('\n');
        w = lines[0].Length;
        h = lines.Length;
        pos = input.Replace("\r\n", "").IndexOf('^');
        visited = new bool[w, h];
    }

    static (int x, int y) Step((int x, int y) pos, int dir)
    {
        if (dir == 0)
            return (pos.x, pos.y - 1);
        if (dir == 1)
            return (pos.x + 1, pos.y);
        if (dir == 2)
            return (pos.x, pos.y + 1);
        else
            return (pos.x - 1, pos.y);
    }

    public int Part1()
    {
        int direction = 0;
        (int x, int y) guardPos = (pos % w, pos / w);

        while (true)
        {
            visited[guardPos.x, guardPos.y] = true;
            (int x, int y) newPos = Step(guardPos, direction);
            if (newPos.x >= w || newPos.x < 0 || newPos.y >= h || newPos.y < 0)
                break;
            else if (lines[newPos.y][newPos.x] == '#')
                direction = (direction + 1) % 4;
            else
                guardPos = newPos;
        }
        return visited.Cast<bool>().Where(x => x).Count();
    }

    public int Part2()
    {
        bool Looped((int x, int y) obstacle)
        {
            byte[,] visited = new byte[w, h];
            int direction = 0;
            (int x, int y) guardPos = (pos % w, pos / w);

            while (true)
            {
                if ((visited[guardPos.x, guardPos.y] & (byte)(1 << direction)) > 0)
                    return true;
                visited[guardPos.x, guardPos.y] |= (byte)(1 << direction);
                (int x, int y) newPos = Step(guardPos, direction);
                if (newPos.x >= w || newPos.x < 0 || newPos.y >= h || newPos.y < 0)
                    return false;
                else if (lines[newPos.y][newPos.x] == '#' || obstacle == newPos)
                    direction = (direction + 1) % 4;
                else
                    guardPos = newPos;
            }
        }
        visited[pos % w, pos / w] = false; // avoid initial position
        int c = 0;
        Parallel.For(0, w, (i) =>
        {
            Parallel.For(0, h, (j) =>
            {
                if (visited[i, j])
                    if (Looped((i, j))) Interlocked.Increment(ref c);
            });
        });
        return c;
    }
}

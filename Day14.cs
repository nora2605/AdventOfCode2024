namespace AdventOfCode2024;

internal class Day14
{
    (int x, int y, int vx, int vy)[] robots;
    int w = 101, h = 103;

    public Day14()
    {
        robots = new StreamReader("inputs/day14.txt").ReadToEnd().Split('\n')
            .Select(l => {
                var z = l.Split(' ');
                var p = z[0].Split(',');
                var v = z[1].Split(',');
                return (
                    int.Parse(p[0][2..]),
                    int.Parse(p[1]),
                    int.Parse(v[0][2..]),
                    int.Parse(v[1])
                );
            })
            .ToArray();
        
    }

    (int x, int y) Future((int x, int y, int vx, int vy) robot, int time)
    {
        return (
            PosMod(robot.x + time * robot.vx, w),
            PosMod(robot.y + time * robot.vy, h)
        );
    }

    int PosMod(int l, int r)
    {
        var m = l % r;
        return m < 0 ? m + r : m;
    }

    public int Part1()
    {
        return robots.Select(r => Future(r, 100)).GroupBy(pos => {
            bool lh = pos.x < (w - 1) / 2;
            bool uh = pos.y < (h - 1) / 2;
            bool rh = pos.x > (w - 1) / 2;
            bool dh = pos.y > (h - 1) / 2;
            return lh ? (uh ? 0 : dh ? 3 : 4) : (rh ? (uh ? 1 : dh ? 2 : 4) : 4);
        })
        .Where(g => g.Key != 4)
        .Select(g => g.Count())
        .Aggregate((x, y) => x * y);
    }

    public int Part2()
    {
        // comment this if you want to search manually
        // you can find the picture fast by going next until you see a suspicious horizontal cluster,
        // then press 'h' to increase by the height (and j if you miss it)
        return 0;
        int i = 0;
        while (true) {
            bool[,] grid = new bool[w,h];
            var f = robots.Select(r => Future(r, i));
            foreach (var (x,y) in f)
                grid[x, y] = true;
            string s = "";
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                    s += grid[x, y] ? "MM" : "  ";
                s += "\n";
            }
            Console.WriteLine(s);
            Console.WriteLine(i);
            var k = Console.ReadKey();
            if (k.KeyChar == 'y') return i;
            else if (k.KeyChar == 'b') i--;
            else if (k.KeyChar == 'w') i += w;
            else if (k.KeyChar == 's') i -= w;
            else if (k.KeyChar == 'h') i += h;
            else if (k.KeyChar == 'j') i -= h;
            else i++;
        }
    }
}
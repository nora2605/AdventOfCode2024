using AdventOfCode2024;

LabelledTimer t = new();

Console.WriteLine("Solutions:");

// Day 1

t.Start();
Day1 d1 = new();
t.Round("Day 1 - Init");

Console.WriteLine(t.RoundWith("Day 1 - Star 1", d1.Part1));
Console.WriteLine(t.RoundWith("Day 1 - Star 2", d1.Part2));

// Day 2

Day2 d2 = new();
t.Round("Day 2 - Init");

Console.WriteLine(t.RoundWith("Day 2 - Star 1", d2.Part1));
Console.WriteLine(t.RoundWith("Day 2 - Star 2", d2.Part2));

// Timing

Console.WriteLine("\nTimes:");
Console.WriteLine(t);
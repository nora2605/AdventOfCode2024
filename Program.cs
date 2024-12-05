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

// Day 3

Day3 d3 = new();
t.Round("Day 3 - Init");

Console.WriteLine(t.RoundWith("Day 3 - Star 1", d3.Part1));
Console.WriteLine(t.RoundWith("Day 3 - Star 2", d3.Part2));

// Day 4
Day4 d4 = new();
t.Round("Day 4 - Init");

Console.WriteLine(t.RoundWith("Day 4 - Star 1", d4.Part1));
Console.WriteLine(t.RoundWith("Day 4 - Star 2", d4.Part2));

// Day 5
Day5 d5 = new();
t.Round("Day 5 - Init");

Console.WriteLine(t.RoundWith("Day 5 - Star 1", d5.Part1));
Console.WriteLine(t.RoundWith("Day 5 - Star 2", d5.Part2));

// Timing

Console.WriteLine("\nTimes:");
Console.WriteLine(t);
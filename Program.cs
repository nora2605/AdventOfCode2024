﻿using AdventOfCode2024;

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

// Day 6

Day6 d6 = new();
t.Round("Day 6 - Init");

Console.WriteLine(t.RoundWith("Day 6 - Star 1", d6.Part1));
Console.WriteLine(t.RoundWith("Day 6 - Star 2", d6.Part2));

// Day 7

Day7 d7 = new();
t.Round("Day 7 - Init");

Console.WriteLine(t.RoundWith("Day 7 - Star 1", d7.Part1));
Console.WriteLine(t.RoundWith("Day 7 - Star 2", d7.Part2));

// Day 8

Day8 d8 = new();
t.Round("Day 8 - Init");

Console.WriteLine(t.RoundWith("Day 8 - Star 1", d8.Part1));
Console.WriteLine(t.RoundWith("Day 8 - Star 2", d8.Part2));

// Day 9

Day9 d9 = new();
t.Round("Day 9 - Init");

Console.WriteLine(t.RoundWith("Day 9 - Star 1", d9.Part1));
Console.WriteLine(t.RoundWith("Day 9 - Star 2", d9.Part2));

// Day 10

Day10 d10 = new();
t.Round("Day 10 - Init");

Console.WriteLine(t.RoundWith("Day 10 - Star 1", d10.Part1));
Console.WriteLine(t.RoundWith("Day 10 - Star 2", d10.Part2));

// Day 11

Day11 d11 = new();
t.Round("Day 11 - Init");

Console.WriteLine(t.RoundWith("Day 11 - Star 1", d11.Part1));
Console.WriteLine(t.RoundWith("Day 11 - Star 2", d11.Part2));

// Timing

Console.WriteLine("\nTimes:");
Console.WriteLine(t);
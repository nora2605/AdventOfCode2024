using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    internal class LabelledTimer
    {
        private readonly Stopwatch init;
        private readonly Stopwatch sw;
        private readonly List<(string Label, TimeSpan Time)> entries;

        public LabelledTimer()
        {
            entries = [];
            init = new();
            sw = new();
        }

        /// <summary>
        /// Start the Round and Global Timer
        /// </summary>
        public void Start()
        {
            init.Start();
            sw.Start();
        }

        /// <summary>
        /// Add current elapsed time to entries and reset round time
        /// </summary>
        /// <param name="label">Time Table Label</param>
        public void Round(string label)
        {
            Snap(label);
            sw.Restart();
        }

        /// <summary>
        /// Add current elapsed time to entries without resetting time
        /// </summary>
        /// <param name="label">Time Table Label</param>
        public void Snap(string label)
        {
            entries.Add((Label: label, Time: sw.Elapsed));
        }

        /// <summary>
        /// Start round timer, execute provided action and add elapsed time to round table
        /// </summary>
        /// <typeparam name="T">Result Type of the provided function</typeparam>
        /// <param name="label">Time Table Label</param>
        /// <param name="action">Function to Time</param>
        public T RoundWith<T>(string label, Func<T> action)
        {
            sw.Start();
            T res = action();
            Snap(label);
            return res;
        }

        /// <summary>
        /// Makes a TimeSpan readable in common SI units from nanoseconds to seconds; defaults to standard display if the TimeSpan is over 10 minutes
        /// </summary>
        /// <param name="ts">the TimeSpan to prettify</param>
        /// <returns></returns>
        private static string Pretty(TimeSpan ts)
        {
            if ((int)ts.TotalMicroseconds == 0)
                return $"{ts.TotalNanoseconds:0.000}ns";
            else if ((int)ts.TotalMilliseconds == 0)
                return $"{ts.TotalMicroseconds:0.000}µs";
            else if ((int)ts.TotalSeconds == 0)
                return $"{ts.TotalMilliseconds:0.000}ms";
            else if (ts.TotalMinutes < 10.0)
                return $"{ts.TotalSeconds:0.000}s";
            else return ts.ToString();
        }

        /// <summary>
        /// Snapshot current global time, collects and prints time entries
        /// </summary>
        /// <param name="t">instance of timer</param>
        public static implicit operator string(LabelledTimer t)
        {
            var now = t.init.Elapsed;
            int maxWidth = t.entries.Max(x => x.Label.Length);
            return t.entries
                .Select(e => $"{e.Label}".PadRight(maxWidth+2) + Pretty(e.Time))
                .Aggregate((a, b) => $"{a}\n{b}") + 
                "\n" + "Total".PadRight(maxWidth+2) + Pretty(now);
        }
    }
}

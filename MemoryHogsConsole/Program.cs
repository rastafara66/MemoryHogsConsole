using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Collections;

namespace MemoryHogs
{
    class Program
    {
        /// <summary>
        ///     Memory Hogs
        ///     lists all processes that are using more memory than a specified threshold.
        ///     The memory usage is displayed in megabytes.
        ///     The terminal is not closed after the program finishes running.
        ///     The columns have headers "id", "process", and "memory" and are formatted by columns.
        /// </summary>
        static void Main(string[] args)
        {
            int TotalMemory = 0;
            var memoryThreshold = 0;
            Console.WriteLine("Memory threshold, Mb:");
            memoryThreshold = int.Parse(Console.ReadLine());
            //if (memoryThreshold == null) {
            //    memoryThreshold = 0;
            //    }
            List<Process> processes = Process.GetProcesses().ToList();
            processes.Sort((a, b) => a.WorkingSet64.CompareTo(b.WorkingSet64));
            Console.WriteLine("| id      | memory   | process              ");
            foreach (Process process in processes)
                {
                if (process.WorkingSet64 / 1048576 > memoryThreshold)
                {
                    string[] row = new string[3];
                    row[0] = process.Id.ToString();
                    if (process.ProcessName.Length > 35)
                    {
                        string processNameShortened = process.ProcessName.Substring(0, 35);
                        row[2] = processNameShortened;
                    }
                    else
                    {
                        row[2] = process.ProcessName;
                    }
                    int memo = (int)(process.WorkingSet64 / 1048576);
                    TotalMemory = TotalMemory + memo;                  
                    row[1] = (process.WorkingSet64 / 1048576).ToString();
                    Console.WriteLine("|{0,8} | {1,8} | {2,35} ", row[0], row[1], row[2]);
                }
            }
            Console.WriteLine("Total memory of processes:");
                //" that are using more memory than a specified threshold:");
            Console.WriteLine(TotalMemory + "Mb");
            Console.WriteLine();
            Console.WriteLine("To be continued... Keep trying...");
            Console.WriteLine();
            Console.WriteLine("Press 'Enter' to exit");
            Console.ReadLine();
        }
    }
}

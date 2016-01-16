using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetCPUUsageOfProcess
{
    class Program
    {
        //Usage:
        // .\GetCPUUsageOfProcess <program name> <full path of text file to write to>
        static void Main(string[] args)
        {
            start:
            try
            {
                PerformanceCounter process = null;
                if (args.Length == 0)
                {
                    process = new PerformanceCounter("Process", "% Processor Time", "VHMultiWriterExt2");
                }
                else
                {
                    process = new PerformanceCounter("Process", "% Processor Time", args[0]);
                }
                while (true)
                {
                    var processCpuUsage = (process.NextValue())/Environment.ProcessorCount;
                    var processCpuUsageString = String.Format("{0:##.##}", processCpuUsage);
                    System.Console.WriteLine(processCpuUsage);

                    using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter((args.Length>1) ? @"C:\Users\Gunny\Desktop\cpuusage.txt":args[1]))
                    {
                        file.WriteLine(processCpuUsageString);
                    }
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch(System.InvalidOperationException e)
            {
                System.Console.WriteLine(e);
                System.Threading.Thread.Sleep(1000);
                goto start;
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e);
                System.Console.ReadLine();
            }
        }
    }
}

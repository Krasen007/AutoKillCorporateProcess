using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        // Enter the name of the processes running on the local computer.
        Start:
        Console.WriteLine("Enter the name of the process: ");
        string? annoyingProcess = Console.ReadLine();
        if ((annoyingProcess == "") || (annoyingProcess == null))
        {
            Console.Clear();
            goto Start;
        }
        bool noProc = false;
        GetProcess:
        Process[] runningProcesses = Process.GetProcessesByName(annoyingProcess);

        if (runningProcesses.Length == 0)
        {
            if (noProc)
            {
                // Check every 10 minutes if the process is running.
                Thread.Sleep(600000);
                goto GetProcess;
            }
            else
            {
                Console.WriteLine("No such process is running, waiting...");
                noProc = true;
                Thread.Sleep(5000);
                goto GetProcess;
            }
        }
        else
        {
            foreach (Process process in runningProcesses)
            {
                process.Kill();
                Console.WriteLine("Success!");
                noProc = false;

                using StreamWriter logFile = File.AppendText("log.txt");
                logFile.WriteLine(process.ProcessName + " " + process.Id + " killed at " + DateTime.UtcNow);
            }
            goto GetProcess;
        }
    }
}
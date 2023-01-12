using System.Diagnostics;
using System.Runtime.InteropServices;

internal class Program
{
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int SW_HIDE = 0;
    private const int SW_SHOW = 5;

    private static void Main(string[] args)
    {

        var handle = GetConsoleWindow();
        ShowWindow(handle, SW_SHOW);

        // Enter the name of the processes running on the local computer.
        string? annoyingProcess;
        if (args.Length > 0)
        {
            annoyingProcess = args[0];
        }
        else
        {
            Start:
            Console.Write("Enter the name of the process: ");
            annoyingProcess = Console.ReadLine();
            if ((annoyingProcess == "") || (annoyingProcess == null))
            {
                Console.Clear();
                goto Start;
            }
        }
        int userWaitTime = 0;
        if (args.Length > 1)
        {
            if (int.TryParse(args[1], out int time))
            {
                userWaitTime = time;
            }
            else
            {
                Console.WriteLine("Invalid time argument provided. Using default.");
                userWaitTime = 600000;
            }
        } else
        {
            userWaitTime = 600000;
        }

        // Hides the console window, because we don't need it anymore.
        ShowWindow(handle, SW_HIDE);

        bool noProc = false;

        GetProcess:
        Process[] runningProcesses = Process.GetProcessesByName(annoyingProcess);

        if (runningProcesses.Length == 0)
        {
            if (noProc)
            {
                // Check every 10 minutes if the process is running.
                Thread.Sleep(userWaitTime);
                goto GetProcess;
            }
            else
            {
                Console.WriteLine("No such process is running, waiting...");
                noProc = true;               
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
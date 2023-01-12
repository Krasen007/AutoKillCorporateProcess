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
        Start:
        Console.Write("Enter the name of the process: ");
        string? annoyingProcess = Console.ReadLine();
        if ((annoyingProcess == "") || (annoyingProcess == null))
        {
            Console.Clear();
            goto Start;
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
                Thread.Sleep(6000); //600000
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
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        // Get the processes running on the local computer.
        Process[] runningProcesses = Process.GetProcessesByName("notepad");

        if (runningProcesses.Length == 0)
        {
            Console.WriteLine("No such process");
        }
        else
        {
            //DateTime.UtcNow
            foreach (Process process in runningProcesses)
            {
                process.Kill();

                Console.WriteLine(process.ProcessName + " " + process.Id + " killed at " + DateTime.UtcNow);
                using (StreamWriter w = File.AppendText("log.txt"))
                {                    
                    w.WriteLine(process.ProcessName + " " + process.Id + " killed at " + DateTime.UtcNow);
                }
            }
        }
    }
}
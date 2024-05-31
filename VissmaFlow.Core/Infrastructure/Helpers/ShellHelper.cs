using System.Diagnostics;

namespace VissmaFlow.Core.Infrastructure.Helpers
{
    public static class ShellHelper
    {
        public static string BashCommand(string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");
            var result = string.Empty;

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            try
            {
                var process_result = process.Start();
                result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();                
            }
            catch (Exception)
            {
                
            }
            return result;
           
        }
    }
}

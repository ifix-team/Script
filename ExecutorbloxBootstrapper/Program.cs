// Roblox Executor Bootstrapper
// Repo: https://github.com/ifix-team/RScript
// SEO keywords (for repository files/comments only): roblox executor, roblox executors, roblox script executor, executor roblox, roblox executor pc, best roblox executor, free roblox executor
//
// NOTE: This bootstrapper downloads a packaged executor build and extracts it.
//       Make sure release assets exist at the releases URLs below before using.
//
// Usage: build and run. Choose "1" for Monaco (standard) or "2" for Lightweight (low-end).
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;

namespace ExecutorBootstrapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LogoConsole();

            Console.WriteLine("Roblox Executor - Bootstrapper");
            Thread.Sleep(900);

            // Local helper methods
            void ErrorDump(Exception err)
            {
                try
                {
                    const string errFile = "err.txt";
                    if (!File.Exists(errFile)) File.Create(errFile).Close();
                    File.WriteAllText(errFile, err.ToString());

                    Console.WriteLine($"Wrote error to {errFile}! Please open an issue at:");
                    Console.WriteLine("https://github.com/roblox-scriptsx/Roblox-Executor/issues");
                    Console.WriteLine("Press Enter to open the issues page...");
                    Console.ReadLine();
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "https://github.com/roblox-scriptsx/Roblox-Executor/issues",
                        UseShellExecute = true
                    });
                }
                catch
                {
                    // fallback: just print to console
                    Console.WriteLine("Unable to open the issues page. Please visit the repo manually.");
                }
            }

            void Green() => Console.ForegroundColor = ConsoleColor.Green;
            void Red() => Console.ForegroundColor = ConsoleColor.Red;
            void LogoConsole() => Console.ForegroundColor = ConsoleColor.DarkYellow;
            void Info() => Console.ForegroundColor = ConsoleColor.Blue;
            void Normal() => Console.ForegroundColor = ConsoleColor.White;
            void Space() => Console.WriteLine();

            Space();
            Info();
            Console.WriteLine("Would you like to download the Monaco (standard) or Lightweight (low-end) Roblox Executor?");
            Space();
            Normal();
            Console.WriteLine("Enter \"1\" for Monaco Executor. Enter \"2\" for Lightweight Executor");
            Space();

            string decision = Console.ReadLine()?.Trim() ?? "";

            if (decision == "1")
            {
                DownloadAndExtract(
                    "Roblox-Executor.zip",
                    "https://github.com/roblox-scriptsx/Roblox-Executor/releases/download/latest/Roblox-Executor.zip",
                    "roblox-executor",
                    "Downloading Monaco (standard) Roblox Executor...",
                    ErrorDump);
            }
            else if (decision == "2")
            {
                DownloadAndExtract(
                    "Roblox-Executor-Lightweight.zip",
                    "https://github.com/roblox-scriptsx/Roblox-Executor/releases/download/latest/Roblox-Executor-lightweight.zip",
                    "roblox-executor-lightweight",
                    "Downloading Lightweight Roblox Executor (for lower-end machines)...",
                    ErrorDump);
            }
            else
            {
                Red();
                Console.WriteLine("Invalid choice. Exiting.");
                Normal();
                Thread.Sleep(1000);
            }

            // Local function that performs download + extract
            void DownloadAndExtract(string localZipName, string remoteUrl, string extractFolder, string startMessage, Action<Exception> onError)
            {
                using (var client = new WebClient())
                {
                    try
                    {
                        Space();
                        Green();
                        Console.WriteLine(startMessage);
                        Space();
                        Normal();

                        if (File.Exists(localZipName))
                        {
                            Red();
                            Console.WriteLine($"{localZipName} already exists in this directory!");
                            Normal();
                            Space();
                        }
                        else
                        {
                            Green();
                            client.DownloadFile(remoteUrl, localZipName);
                            Thread.Sleep(1200);
                            Console.WriteLine($"Successfully downloaded {localZipName}!");
                            Normal();
                            Space();
                        }
                    }
                    catch (Exception ex)
                    {
                        Space();
                        Red();
                        Console.WriteLine("Error during download phase.");
                        onError(ex);
                        Environment.Exit(1);
                    }

                    try
                    {
                        Green();
                        Console.WriteLine($"Extracting {localZipName}...");
                        Thread.Sleep(700);

                        string archivePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, localZipName);

                        ZipFile.ExtractToDirectory(archivePath, AppDomain.CurrentDomain.BaseDirectory);
                        File.Delete(archivePath);

                        Space();
                        Console.WriteLine("Successfully extracted archive!");
                        Thread.Sleep(500);

                        // Open the extracted folder (platform default)
                        string startPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, extractFolder);
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = startPath,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        Space();
                        Red();
                        Console.WriteLine("Error during extraction phase.");
                        onError(ex);
                        Environment.Exit(1);
                    }
                }
            }
        }
    }
}

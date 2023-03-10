using System.Runtime.InteropServices;
using System.Text;

namespace adblock
{

    static class DnsCache
    {
        [DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCache")]
        private static extern uint DnsFlushResolverCache();
        public static uint Flush()
        {
            return DnsFlushResolverCache();
        }
    }


    internal class Program
    {
        const double version = 1.0;
        private const double ByteSizeInKiloBytes = 0.008;

        static void Main()
        {
            try
            {
                Console.Title = $"adblock, version {version:0.0} - https://github.com/ciao1092/adblock";
                string hostsFileName;

                if (!OperatingSystem.IsWindows() || Environment.OSVersion.Version.Major > 10)
                {
                    throw new PlatformNotSupportedException("Detected platform (" + Environment.OSVersion.VersionString + ") is not supported. " + Environment.NewLine +
                        "This program requires Windows 95, 98, ME, or Windows NT 3, 4, 5, 6 and 10");
                }

                string osEnv = string.Empty + Environment.GetEnvironmentVariable("OS");

                if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                {
                    // Operating system is Windows 9x/ME
                    hostsFileName = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + @"\hosts";
                }
                else
                {
                    // Operating system is Windows 3/4/5/6/10
                    hostsFileName = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + @"\System32\drivers\etc\hosts";
                }

                Console.WriteLine("Using file {0}", hostsFileName);

                string programLocation = string.Empty + Path.GetDirectoryName(AppContext.BaseDirectory);
                string backupLocation = programLocation + @"\Backups";
                if (!Directory.Exists(backupLocation))
                {
                    Directory.CreateDirectory(backupLocation);
                }

                Console.WriteLine("Making backups in {0}", backupLocation);

                string backupFileName = string.Empty;

                int oldWindowWidth = Console.WindowWidth;

                for (int i = 1; i <= 9999; i++)
                {
                    backupFileName = "hosts" + i.ToString("0000") + ".backup.txt";
                    if (File.Exists(Path.Combine(new string[] { backupLocation, backupFileName })))
                    {
#if DEBUG
                        Console.WriteLine("File {0} already exists. ", backupFileName);
#endif
                        continue;
                    }
                    else
                    {
#if DEBUG
                        Console.WriteLine("File {0} does not exist. ", backupFileName);
#endif
                        break;
                    }
                }

                if (string.IsNullOrWhiteSpace(backupFileName))
                {
                    Console.WriteLine("The maximum number of backup files has been created. Please delete old backup files to continue.");
                    Environment.Exit(1);
                }

                backupFileName = Path.Combine(new string[] { backupLocation, backupFileName });
                Console.WriteLine("Using backup file {0}", backupFileName);
                Console.Write("Backing up . . . ");
                File.Copy(hostsFileName, backupFileName);
                Console.WriteLine("Done. ");
                Console.WriteLine();

                Console.WriteLine("Starting to Install server list. ");
                const string serverListUrl = "https://raw.githubusercontent.com/anudeepND/blacklist/master/adservers.txt";
                Console.WriteLine("Fetching adserver list . . . ({0}) ", serverListUrl);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.ResetColor();
                string serverListWebResult = new HttpClient().GetStringAsync(serverListUrl).Result;
                string serverList = "# generated by adblock by ciao1092, version " + version.ToString("0.0") + Environment.NewLine + "# Ad domain list: " + serverListUrl;

                string processingDataMessage = $"Processing {Encoding.Default.GetBytes(serverListWebResult).Length * ByteSizeInKiloBytes:0.00} KiloBytes of data (this could take more than 10 minutes, depending on Internet speed, processor capabilities and length of server list file located at \"{serverListUrl}\") . . . ";
                Console.WriteLine(processingDataMessage);
                Console.CursorVisible = false;
                string[] serverListWebResultArray = serverListWebResult.ReplaceLineEndings(Environment.NewLine).Split(Environment.NewLine);
                for (int i = 0; i < serverListWebResultArray.Length; i++)
                {
                    string line = serverListWebResultArray[i];
                    if (!line.StartsWith("#"))
                    {
                        serverList += Environment.NewLine + line;
                    }

                    int progressBarWidth = Console.WindowWidth - 10;
                    if (Console.WindowWidth != oldWindowWidth)
                    {
                        oldWindowWidth = Console.WindowWidth;
                        Console.CursorLeft = 0;
                        for (int x = 0; x < Console.WindowHeight; x++)
                        {
                            for (int y = 0; y < Console.WindowWidth; y++)
                            {
                                Console.Write(' ');
                            }
                            Console.CursorLeft = 0;
                            Console.CursorTop++;
                        }

                        Console.WriteLine(processingDataMessage);
                    }

                    int progress = (i + 1) * progressBarWidth / serverListWebResultArray.Length;
                    int oldCursorLeft = Console.CursorLeft;
                    int oldCursorTop = Console.CursorTop;
                    //string progressString = progress.ToString("000") + '%';
                    string percentageString = " " + ((i + 1) * 100 / serverListWebResultArray.Length).ToString("000") + "% ";
                    string progressString = new('#', progress);
                    string toGoString = new('-', progressBarWidth - progress);
                    string progressOutput = $"[{progressString}{percentageString}{toGoString}]";
                    Console.Write(progressOutput);
                    try
                    {
                        Console.CursorLeft = oldCursorLeft;
                        Console.CursorTop = oldCursorTop;
                    }
                    catch { }
                }

                Console.WriteLine();
                Console.CursorVisible = true;

                Console.WriteLine("Making modifications . . . ");
                File.WriteAllText(hostsFileName, serverList);

                Console.WriteLine("Flushing DNS cache . . . ");
                DnsCache.Flush();

                Console.WriteLine("You may need to reboot your PC for the changes to take effect. ");

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("All done!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                Environment.Exit(1);
                return;
            }
        }
    }
}

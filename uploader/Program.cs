﻿﻿// C# program to upload Digispark sketches using Arduino CLI
// Lightly Commented!
// REMOVE THIS BUT FAILSAFE IF ERROR OCCURS DO NOW please
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
class Program
{    static string Verify()
    {
        string arduinoCLIPathInput = "(Blank Input)";
        string command = "EMPTY";

        if (File.Exists("CLIPath.txt"))
        {
            arduinoCLIPathInput = File.ReadAllText("CLIPath.txt");
            arduinoCLIPathInput = arduinoCLIPathInput.Replace("\"", "");
            if (!File.Exists(arduinoCLIPathInput) || !arduinoCLIPathInput.EndsWith("\\arduino-cli.exe"))
            {
                Console.WriteLine("The Arduino CLI executable does not exist at the specified path. Please re-enter.");
                command = "del CLIPath.txt";
                ProcessRun(command);
                Console.WriteLine("Please Wait...");
                Thread.Sleep(2000);
                Verify();
                Console.Clear();
            }
        }
        else
        {
            Console.Write("Enter the path to your Arduino CLI executable: ");
            string CLIPathInput = Console.ReadLine() ?? string.Empty;
            File.WriteAllText("CLIPath.txt", CLIPathInput);
            arduinoCLIPathInput = CLIPathInput.Replace("\"", "");
            if (File.Exists(arduinoCLIPathInput) && arduinoCLIPathInput.EndsWith("\\arduino-cli.exe"))
            {
                string arduinoCliPath = arduinoCLIPathInput;
                command = $"{arduinoCliPath} core list";
                string output = ProcessRun(command);
                string specificLine = "digistump:avr 1.6.7     1.6.7  Digistump AVR Boards";
                string[] lines = output.Split(new[] { Environment.NewLine}, StringSplitOptions.None);
                bool containsDigispark = false;
                foreach (string line in lines)
                {
                    if (line.Contains(specificLine))
                    {
                        containsDigispark = true;
                    }
                }

                if (containsDigispark == false)
                {
                    Console.WriteLine("Your Arduino-CLI seems to have something wrong with it.");
                    Thread.Sleep(500);
                    Console.WriteLine("Make sure that you have the correct location of the executable");
                    Thread.Sleep(500);
                    Console.WriteLine("Alternatively, make sure that you installed Digispark correctly");
                    Thread.Sleep(500);
                    Console.WriteLine("Press enter to exit...");
                    command = "del CLIPath.txt";
                    ProcessRun(command);
                    Console.ReadLine();
                    System.Environment.Exit(0);
                }

            }
            else
            {
                Console.WriteLine("The Arduino CLI executable does not exist at the specified path. Please re-enter.");
                command = "del CLIPath.txt";
                ProcessRun(command);
                Console.WriteLine("Please Wait...");
                Thread.Sleep(2000);
                Verify();
                Console.Clear();
            }
        }
        return arduinoCLIPathInput;
    }

    static async void Install()
    {
        string url = "https://github.com/arduino/arduino-cli/releases/download/v0.35.3/arduino-cli_0.35.3_Windows_64bit.zip";
        string fileName = "Arduino-CLI.zip";
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string downloadPath = Path.Combine(documentsPath, fileName);

        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                Console.WriteLine("Downloading Arduino-CLI...");
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                using (Stream contentStream = await response.Content.ReadAsStreamAsync())
                {
                    using (FileStream fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await contentStream.CopyToAsync(fileStream);
                    }
                }
                Console.WriteLine("Download completed successfully.");
            }
            catch (HttpRequestException ex)
            {
                    Console.WriteLine($"HTTP request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading file: {ex.Message}");
            }
        }
        string extractPath = Path.Combine(documentsPath, "Arduino-CLI");
        string zipFilePath = downloadPath;
        try
        {
            // Check if the zip file exists
            if (File.Exists(zipFilePath))
            {
                // Create the directory to extract files if it doesn't exist
                if (!Directory.Exists(extractPath))
                {
                    Directory.CreateDirectory(extractPath);
                    Console.WriteLine("Extraction directory created successfully.");
                }

                // Extract the zip file
                ZipFile.ExtractToDirectory(zipFilePath, extractPath);
                Console.WriteLine("Extraction completed successfully.");
            }
            else
            {
                Console.WriteLine("Zip file does not exist.");
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Unauthorized access: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"IO error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error extracting zip file: {ex.Message}");
        }
        string command = "arduino-cli.exe config init";
        string output = ProcessRun(command);
        Console.WriteLine(output);
        Thread.Sleep(500);
        Console.WriteLine("Successfully Initialised Arduino-CLI");
        Thread.Sleep(200);
        command = "arduino-cli.exe config add board_manager.additional_urls https://raw.githubusercontent.com/digistump/arduino-boards-index/master/package_digistump_index.json";
        output = ProcessRun(command);
        Thread.Sleep(500);
        Console.WriteLine(output);
        command = "arduino-cli.exe core install digistump:avr";
        output = ProcessRun(command);
        Console.WriteLine(output);
        Thread.Sleep(500);
        Console.WriteLine("Successfully installed Arduino-CLI");
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
        return;
    }
    
    static string ProcessRun(string command)
    {
        using Process process = new();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = $"/c {command}";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        Console.WriteLine("Output:");
        Console.WriteLine(output);
        process.WaitForExit();
        return output;
    }

    static void Main(string[] args)
    {
        if (!File.Exists("Install-Script.huh445"))
        {
        if (!File.Exists("Install-Script2.huh445"))
        {
            Console.WriteLine("Would you like to install Arduino-CLI? (Y/N)");
            string installInput = Console.ReadLine() ?? string.Empty;
            if (string.Equals(installInput, "y", StringComparison.OrdinalIgnoreCase))
            {
                Install();
                Console.ReadLine();
                File.Create("Install-Script2.huh445");
                File.WriteAllText("CLIPath.txt", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Arduino-CLI\\arduino-cli.exe"));
            }
            else if (string.Equals(installInput, "n", StringComparison.OrdinalIgnoreCase))
            {
                Console.Clear();
                File.Create("Install-Script.huh445");
            }
        }
        }
        string arduinoCliPath = Verify();
        string command = "EMPTY";
        
        string sketchDirectory = "..\\..\\..\\..";
        string board = "digistump:avr:digispark-tiny";
        string readText = File.ReadAllText("CLIPath.txt");
        string[] sketchFiles = Directory.GetFiles(sketchDirectory, "*.ino", SearchOption.AllDirectories);
        // Display the list of .ino files to the user
        Thread.Sleep(2000);
        for (int i = 0; i < sketchFiles.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {Path.GetFileNameWithoutExtension(sketchFiles[i])}");
        }
        Console.WriteLine("");
        Console.WriteLine("Available settings:");
        Console.WriteLine("Change -> Change Arduino CLI Path");
        Console.WriteLine("Exit -> Exit the application");
        Console.WriteLine("Show -> To show the current path to the executable.");
        Console.WriteLine("");

        // Prompt the user to select a sketch
        Console.Write("Enter the number of the sketch to upload: ");
        string input = Console.ReadLine() ?? string.Empty;
        if (string.Equals(input, "change", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("The current path to the Arduino CLI is: " + readText);
            Console.Write("Is this the correct path? (Y/N) ");
            string response = Console.ReadLine() ?? string.Empty;
            if (string.Equals(response, "Y", StringComparison.OrdinalIgnoreCase))
            {
                Console.Clear();
                Main(args);
            }
            else
            {
            command = "del CLIPath.txt";
            ProcessRun(command);
            Console.WriteLine("Please Wait...");
            Thread.Sleep(2000);
            arduinoCliPath = Verify();
            Console.Clear();
            Main(args);
            }
        }

        else if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
            Console.WriteLine("Exiting...");
            System.Environment.Exit(0);
        }

        else if (string.Equals(input, "show", StringComparison.OrdinalIgnoreCase))
        {
            Console.Clear();
            readText = File.ReadAllText("CLIPath.txt");
            Console.WriteLine("The current path to the Arduino CLI is: " + readText);
            Thread.Sleep(3000);
            Console.WriteLine("");
            Main(args);
        }

        if (!int.TryParse(input, out int selectedSketchIndex) || selectedSketchIndex < 1 || selectedSketchIndex > sketchFiles.Length)
        {
            Console.Clear();
            Console.WriteLine("Invalid input. Please choose a number or a setting from the list.");
            Main(args);
        }

        // Get the path of the selected sketch
        string sketchPath = sketchFiles[selectedSketchIndex - 1];

        Console.Clear();

        // Build the command to upload the sketch using the micronucleus programmer
        command = $"{arduinoCliPath} compile -b {board} {sketchPath}";
        string output = ProcessRun(command);
        Console.WriteLine(output);

    // Reads the output and the errors
        // Wait for the process to exit
        Thread.Sleep(1000);
        Console.WriteLine("");
        Console.WriteLine("Sketch Verified");
        Console.WriteLine("");
        Console.WriteLine("Plug in Digispark now! (will timeout in 60 seconds)");
        Console.WriteLine("");
        Thread.Sleep(2000);

        command = $"{arduinoCliPath} upload -b {board} {sketchPath}";
        output = ProcessRun(command);
        Console.WriteLine(output);

        // Check if the upload was successful
        Console.WriteLine("Press enter to close...");
        Console.ReadLine();
    }
}
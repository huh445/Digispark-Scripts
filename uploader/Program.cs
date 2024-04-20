﻿﻿// C# program to upload Digispark sketches using Arduino CLI
// Lightly Commented!
// REMOVE THIS BUT FAILSAFE IF ERROR OCCURS DO NOW please
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml;
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
                processRun(command);
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
                string output = processRun(command);
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
                    processRun(command);
                    Console.ReadLine();
                    System.Environment.Exit(0);
                }

            }
            else
            {
                Console.WriteLine("The Arduino CLI executable does not exist at the specified path. Please re-enter.");
                command = "del CLIPath.txt";
                processRun(command);
                Console.WriteLine("Please Wait...");
                Thread.Sleep(2000);
                Verify();
                Console.Clear();
            }
        }
        return arduinoCLIPathInput;
    }
    
    static string processRun(string command)
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
        string command = "EMPTY";
        string arduinoCliPath = Verify();
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
            processRun(command);
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
            Console.WriteLine("Invalid input. Please choose a number or a setting from the list.");
            Main(args);
        }

        // Get the path of the selected sketch
        string sketchPath = sketchFiles[selectedSketchIndex - 1];

        Console.Clear();

        // Build the command to upload the sketch using the micronucleus programmer
        command = $"{arduinoCliPath} compile -b {board} {sketchPath}";
        processRun(command);

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
        processRun(command);

        // Check if the upload was successful
        Console.WriteLine("Press enter to close...");
        Console.ReadLine();
    }
}
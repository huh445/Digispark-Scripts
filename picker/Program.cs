﻿// C# program to upload Digispark sketches using Arduino CLI
// Lightly Commented!
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Threading;

class Program
{
    static string Verify()
    {
        string arduinoCLIPathInput = "(Blank Input)";
        if (File.Exists("CLIPath.txt"))
        {
            arduinoCLIPathInput = File.ReadAllText("CLIPath.txt");
            arduinoCLIPathInput = arduinoCLIPathInput.Replace("\"", "");
            if (!File.Exists(arduinoCLIPathInput) || !arduinoCLIPathInput.EndsWith("\\arduino-cli.exe"))
            {
                Console.WriteLine("The Arduino CLI executable does not exist at the specified path. Please re-enter.");
                arduinoCLIPathInput = Console.ReadLine() ?? string.Empty;
                File.WriteAllText("CLIPath.txt", arduinoCLIPathInput);
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
                Console.WriteLine("Arduino CLI path accepted.");
            }
            else
            {
                Console.WriteLine("The Arduino CLI executable does not exist at the specified path. Please re-enter.");
                arduinoCLIPathInput = Console.ReadLine() ?? string.Empty;
                File.WriteAllText("CLIPath.txt", arduinoCLIPathInput);
            }
        }
        return arduinoCLIPathInput;
    }
    
    static void Main(string[] args)
    {
        string arduinoCliPath = Verify();
        string sketchDirectory = "..\\..\\..\\..";
        string board = "digistump:avr:digispark-tiny";
        string[] sketchFiles = Directory.GetFiles(sketchDirectory, "*.ino", SearchOption.AllDirectories);
        Console.WriteLine(arduinoCliPath);
        // Display the list of .ino files to the user
        Console.WriteLine("Available sketches:");
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
            string command3 = "del CLIPath.txt";
            using Process process3 = new();
            process3.StartInfo.FileName = "cmd.exe";
            process3.StartInfo.Arguments = $"/c {command3}";
            process3.StartInfo.UseShellExecute = false;
            process3.StartInfo.CreateNoWindow = true;
            process3.Start();
            Console.WriteLine("Waiting...");
            Thread.Sleep(2000);
            arduinoCliPath = Verify();
            Main(args);
        }

        else if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Exiting...");
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
            System.Environment.Exit(0);
        }

        else if (string.Equals(input, "show", StringComparison.OrdinalIgnoreCase))
        {
            string readText = File.ReadAllText("CLIPath.txt");
            Console.WriteLine("The current path to the Arduino CLI is: " + readText);
            Thread.Sleep(3000);
        }

        if (!int.TryParse(input, out int selectedSketchIndex) || selectedSketchIndex < 1 || selectedSketchIndex > sketchFiles.Length)
        {
            Main(args);
        }

        // Get the path of the selected sketch
        string sketchPath = sketchFiles[selectedSketchIndex - 1];

        // Build the command to upload the sketch using the micronucleus programmer
        string command = $"{arduinoCliPath} compile -b {board} {sketchPath}";

        // Execute the command
        using Process process = new();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = $"/c {command}";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.Start();

        // Reads the output and the errors
        string output = process.StandardOutput.ReadToEnd();
        Console.WriteLine("Output:");
        Console.WriteLine(output);
        // Wait for the process to exit
        process.WaitForExit();
        Console.WriteLine("");
        Console.WriteLine("Sketch Verified");
        Console.WriteLine("");
        Console.WriteLine("Plug in Digispark now! (will timeout in 60 seconds)");
        Thread.Sleep(2000);


        string command2 = $"{arduinoCliPath} upload -b {board} {sketchPath}";
        // Execute the command
        using Process process2 = new();
        process2.StartInfo.FileName = "cmd.exe";
        process2.StartInfo.Arguments = $"/c {command2}";
        process2.StartInfo.RedirectStandardOutput = true;
        process2.StartInfo.UseShellExecute = false;
        process2.StartInfo.CreateNoWindow = true;
        process2.Start();
        string output2 = process2.StandardOutput.ReadToEnd();
        Console.WriteLine("Output 2:");
        Console.WriteLine(output2);
        process2.WaitForExit();

        // Check if the upload was successful
        Console.WriteLine("Press enter to close...");
        Console.ReadLine();
    }
}
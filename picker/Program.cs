﻿using System;
using System.Diagnostics;
using System.IO;

class Program
{
    
    static void Main(string[] args)
    {
        Console.Write("Enter the path to your Arduino CLI executable: ");
        string arduinoCliPath = Console.ReadLine();
        string sketchDirectory = "..\\..\\..\\.."; // Replace with the path to the directory containing your Arduino sketches
        string board = "digistump:avr:digispark-tiny"; // Replace with the appropriate board identifier for Digispark ATTINY85
        string programmer = "micronucleus"; // Specify the programmer as "micronucleus"
        string currentDirectory = Directory.GetCurrentDirectory(); // Get the current directory
        Console.WriteLine(Path.GetRelativePath(currentDirectory, "C:\\Users\\charc\\OneDrive\\Documents\\GitHub\\Digispark-Scripts"));

        // Now you can use parentDirectory.FullName to get the full path of the parent directory
        // Get the list of .ino files in the sketch directory
        string[] sketchFiles = Directory.GetFiles(sketchDirectory, "*.ino", SearchOption.AllDirectories);

        // Display the list of .ino files to the user
        Console.WriteLine("Available sketches:");
        for (int i = 0; i < sketchFiles.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {Path.GetFileNameWithoutExtension(sketchFiles[i])}");
        }

        // Prompt the user to select a sketch
        Console.Write("Enter the number of the sketch to upload: ");
        string input = Console.ReadLine();
        if (!int.TryParse(input, out int selectedSketchIndex) || selectedSketchIndex < 1 || selectedSketchIndex > sketchFiles.Length)
        {
            Console.WriteLine("Invalid input. Exiting...");
            return;
        }

        // Get the path of the selected sketch
        string sketchPath = sketchFiles[selectedSketchIndex - 1];

        // Build the command to upload the sketch using the micronucleus programmer
        string command = $"{arduinoCliPath} compile --fqbn {board} --programmer {programmer} {sketchPath}";

        // Execute the command
        Process process = new Process();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = $"/c {command}";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.Start();

        // Read the output
        string output = process.StandardOutput.ReadToEnd();

        // Wait for the process to exit
        process.WaitForExit();

        // Check if the upload was successful
        if (output.Contains("Done uploading"))
        {
            Console.WriteLine("Upload successful!");
        }
        else
        {
            Console.WriteLine("Upload failed!");
        }
    }
}
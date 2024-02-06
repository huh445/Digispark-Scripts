// C# program to upload Digispark sketches using Arduino CLI
// Lightly Commented!
using System;
using System.Diagnostics;
using System.IO;

class Program
{
    
    static void Main(string[] args)
    {
        string arduinoCLIPathInput = "(Empty File)";
        if (File.Exists("CLIPath.txt"))
        {
            arduinoCLIPathInput = File.ReadAllText("CLIPath.txt");
            arduinoCLIPathInput = arduinoCLIPathInput.Replace("\"", "");
            if (!File.Exists(arduinoCLIPathInput))
                {
                    Console.WriteLine("The Arduino CLI executable does not exist at the specified path. Please re-enter.");
                    arduinoCLIPathInput = @Console.ReadLine() ?? string.Empty;
                    File.WriteAllText("CLIPath.txt", arduinoCLIPathInput);
                }
        }
        else
        {
            Console.Write("Enter the path to your Arduino CLI executable: ");
            string CLIPathInput = Console.ReadLine() ?? string.Empty;
            File.WriteAllText("CLIPath.txt", CLIPathInput);
        }
        string arduinoCliPath = File.ReadAllText("CLIPath.txt");
        string sketchDirectory = "..\\..\\..\\..";
        string board = "digistump:avr:digispark-tiny";
        string currentDirectory = Directory.GetCurrentDirectory();

        string[] sketchFiles = Directory.GetFiles(sketchDirectory, "*.ino", SearchOption.AllDirectories);

        // Display the list of .ino files to the user
        Console.WriteLine("Available sketches:");
        for (int i = 0; i < sketchFiles.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {Path.GetFileNameWithoutExtension(sketchFiles[i])}");
        }
        Console.WriteLine("Available settings");
        Console.WriteLine("Change -> Change Arduino CLI Path");

        // Prompt the user to select a sketch
        Console.Write("Enter the number of the sketch to upload: ");
        string input = Console.ReadLine() ?? string.Empty;
        if (input.ToLower() == "change")
        {
            Console.Write("Enter the path to your Arduino CLI executable: ");
            string CLIPathInput = Console.ReadLine() ?? string.Empty;
            File.WriteAllText("CLIPath.txt", CLIPathInput);
            Console.WriteLine("Path Changed");
            return;
        }
        if (!int.TryParse(input, out int selectedSketchIndex) || selectedSketchIndex < 1 || selectedSketchIndex > sketchFiles.Length)
        {
            Console.WriteLine("Invalid input. Exiting...");
            return;
        }

        // Get the path of the selected sketch
        string sketchPath = sketchFiles[selectedSketchIndex - 1];

        // Build the command to upload the sketch using the micronucleus programmer
        string command = $"{arduinoCliPath} compile -b {board} {sketchPath}";

        // Execute the command
        Process process = new Process();
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
        Thread.Sleep(2000);

        string command2 = $"{arduinoCliPath} upload -b {board} {sketchPath}";
        // Execute the command
        Process process2 = new Process();
        process2.StartInfo.FileName = "cmd.exe";
        process2.StartInfo.Arguments = $"/c {command2}";
        process2.StartInfo.RedirectStandardOutput = true;
        process2.StartInfo.UseShellExecute = false;
        process2.StartInfo.CreateNoWindow = true;
        process2.Start();
        string output2 = process.StandardOutput.ReadToEnd();
        Console.WriteLine("Output 2:");
        Console.WriteLine(output);
        process2.WaitForExit();

        

        // Check if the upload was successful
        Console.WriteLine("Press enter to close...");
        Console.ReadLine();
    }
}

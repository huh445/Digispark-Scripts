# Digispark Scripts
Just a few scripts that I have made to troll my friends along the way!
- Tested and working Windows 11
- Made 2024
## What scripts are there currently?
>Force a Blue Screen of Death (BSOD)

>Change a background to any image

## What is the Picker?
>I need to make a better name for it!

The Picker is a program built in C# that automatically uploads the sketches in this repository.
It is useful when you don't have much time as the Arduino IDE takes so long to launch.

It is of course a lot quicker if you have the Arduino Extension set up in VSCode!
I find it very funny that the majority of this repository is now C#.

>This is still a WIP.

>It still needs to save from the last instance where your Arduino CLI is located.

## How does the background changer work?
1. Go to ChangeBackground.ps1.
2. Open it in a text editor (any works).
3. Edit the link to any image link address.
4. Go to a text hosting site such as https://pastebin.com/.
5. Put the text in.
6. Copy the link to the raw text.
7. Paste the link in the ino file.
8. Run!

# How do you set up the Digispark ATTINY85 for use with this program?
1. Download the Arduino CLI (https://github.com/arduino/arduino-cli).
2. Open a CMD window.
3. CD to the folder where your Arduino CLI is located.
4. Run the command "arduino-cli.exe config init".
5. Run the command "arduino-cli core update-index --additional-urls https://raw.githubusercontent.com/digistump/arduino-boards-index/master/package_digistump_index.json".
6. Run the command "arduino-cli.exe core install digistump:avr".
7. Right click on arduino-cli and Copy As Path.
8. Paste that in when the "picker" program asks for the path.

## Is a full release planned?
A release is now planned as the picker is now actually fully built and able to be used.
This is good news as it will help to show people who are not as experienced in technology how to use a Digispark.
The release will most likely be at the end of Febuary 2024
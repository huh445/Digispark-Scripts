// Changes wallpaper to whatever you want.
// Tested working Windows 11. 
// Change the link to the image you want to use in jpg form.
#include "DigiKeyboard.h"
#define link "https://i.ibb.co/jg1v95Y/unnamed.png"
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

  DigiKeyboard.println("$client = new-object System.Net.WebClient");
  DigiKeyboard.delay(1000);
  DigiKeyboard.println("Remove-Item $env:USERPROFILE\\devon.jpg");
  DigiKeyboard.delay(500);
  DigiKeyboard.println("$client.DownloadFile(\""link"\" , \"devon.jpg\")");
  DigiKeyboard.delay(10000);

  DigiKeyboard.print("set-itemproperty -path \"HKCU:\\Control Panel\\Desktop\\\" -name Wallpaper -value $env:USERPROFILE\\devon.jpg"); // Set the wallpaper
  DigiKeyboard.delay(500);
  DigiKeyboard.sendKeyStroke(KEY_ENTER);
  DigiKeyboard.delay(1000);

  DigiKeyboard.println("rundll32.exe user32.dll, UpdatePerUserSystemParameters");
  DigiKeyboard.delay(500);
  
  DigiKeyboard.println("exit");

  digitalWrite(1, LOW);
  for(;;){}
}

// Changes wallpaper to whatever you want.
// Tested working Windows 11. 
// Change the link to the image you want to use in jpg form.
#include "DigiKeyboard.h"
#define link "https://img.freepik.com/free-photo/painting-mountain-lake-with-mountain-background_188544-9126.jpg"
void setup() {
  //empty
}
void loop() {
  DigiKeyboard.sendKeyStroke(0);
  DigiKeyboard.sendKeyStroke(KEY_D, MOD_GUI_LEFT);
  DigiKeyboard.delay(500);
  DigiKeyboard.sendKeyStroke(KEY_R, MOD_GUI_LEFT);
  DigiKeyboard.delay(500);
  DigiKeyboard.println("powershell");
  DigiKeyboard.delay(2000);
  DigiKeyboard.println("$client = new-object System.Net.WebClient");
  DigiKeyboard.delay(1000);
  DigiKeyboard.println("Remove-Item $env:USERPROFILE\\devon.jpg");
  DigiKeyboard.delay(500);
  DigiKeyboard.println("$client.DownloadFile(\""link"\" , \"devon.jpg\")");
  DigiKeyboard.delay(5000);
  DigiKeyboard.print("set-itemproperty -path ""HKCU:\\Control Panel\\Desktop\\"" -name Wallpaper -value $env:USERPROFILE\\devon.jpg");
  DigiKeyboard.delay(500);
  DigiKeyboard.sendKeyStroke(KEY_ENTER);
  DigiKeyboard.delay(1000);
  DigiKeyboard.println("rundll32.exe user32.dll, UpdatePerUserSystemParameters");
  DigiKeyboard.delay(500);
  DigiKeyboard.sendKeyStroke(KEY_D, MOD_GUI_LEFT);
  DigiKeyboard.delay(500);
  for(;;){ /*empty*/ }
}
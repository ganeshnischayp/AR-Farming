#include <SPI.h>
#include <MFRC522.h>
#include <ESP8266WiFi.h>
#include <FirebaseArduino.h>
//
#define FIREBASE_HOST "dashkart-b8214.firebaseio.com"
#define FIREBASE_AUTH "Brb6pjzyhOrKj10IPDeHhYU3GbHeCPNQtub95Grq"
#define WIFI_SSID "gpn"
#define WIFI_PASSWORD "happygpn"




void setup() { 
   Serial.begin(115200);
   /////////////////////////////////
  WiFi.begin(WIFI_SSID, WIFI_PASSWORD);
Serial.print("connecting");
while (WiFi.status() != WL_CONNECTED) {
Serial.print(".");
delay(500);
}
Serial.println();
Serial.print("connected: ");
Serial.println(WiFi.localIP());
Firebase.begin(FIREBASE_HOST, FIREBASE_AUTH);
}
 
void loop() {
  
  String name = Firebase.pushString("string","gpn");
  
//handle error
if (Firebase.failed()) {
Serial.print("pushing /logs failed:");
Serial.println(Firebase.error()); 
return;
}

Serial.print("pushed: /logs/");
//Serial.println(name);
delay(1000);

}

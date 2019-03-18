#include <ESP8266WiFi.h>
#include <SPI.h>

#include <FirebaseArduino.h>

const int ldr = A0;
const int ir = D1;
#ifdef ESP32
#pragma message(THIS EXAMPLE IS FOR ESP8266 ONLY!)
#error Select ESP8266 board.
#endif

#define FIREBASE_HOST "arfarming-b253f.firebaseio.com"
#define FIREBASE_AUTH "Fyu7Hp9DJ54grr52y2eUDuxUpNNt8R7AKmHjIA59"
#define WIFI_SSID "avengers"
#define WIFI_PASSWORD "bharath1"


void setup()
{
  Serial.begin(115200);
  String thisBoard= ARDUINO_BOARD;
  Serial.println(thisBoard);


    
    WiFi.begin(WIFI_SSID, WIFI_PASSWORD);
    Serial.print("connecting");
    while (WiFi.status() != WL_CONNECTED) 
    {
    Serial.print(".");
    delay(500);
    }
    Serial.println();
    Serial.print("connected: ");
    Serial.println(WiFi.localIP());
    Firebase.begin(FIREBASE_HOST, FIREBASE_AUTH);

      
  pinMode(ldr,INPUT);
    pinMode(ir,INPUT);



  
}

void loop()
{
  

  Serial.print(analogRead(ldr));

    String name = Firebase.pushFloat("ldr",analogRead(ldr));
      ////handle error
    if (Firebase.failed()) {
    Serial.print("pushing ldr/logs failed:");
    Serial.println(Firebase.error()); 
    return;
    }

       int x = digitalRead(ir);
       Serial.print("IR ");
  Serial.println(x);

    String name1 = Firebase.pushFloat("IR ",x);
      ////handle error
    if (Firebase.failed()) {
    Serial.print("pushing IR/logs failed:");
    Serial.println(Firebase.error()); 
    return;
    }

    
      

  
  
    


  delay(2000);






  

}

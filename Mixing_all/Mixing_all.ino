#include "DHTesp.h"
#include <ESP8266WiFi.h>
#include <SPI.h>

#include <FirebaseArduino.h>

const int gassensor = A0;

#ifdef ESP32
#pragma message(THIS EXAMPLE IS FOR ESP8266 ONLY!)
#error Select ESP8266 board.
#endif

#define FIREBASE_HOST "arfarming-b253f.firebaseio.com"
#define FIREBASE_AUTH "Fyu7Hp9DJ54grr52y2eUDuxUpNNt8R7AKmHjIA59"
#define WIFI_SSID "avengers"
#define WIFI_PASSWORD "bharath1"

DHTesp dht;

void setup()
{
  Serial.begin(115200);
  Serial.println();
  Serial.println("Status\tHumidity (%)\tTemperature (C)\t(F)\tHeatIndex (C)\t(F)");
  String thisBoard= ARDUINO_BOARD;
  Serial.println(thisBoard);

  // Autodetect is not working reliable, don't use the following line
  // dht.setup(17);
  // use this instead: 
  dht.setup(5, DHTesp::DHT11); // Connect DHT sensor to GPIO 17 D1
    
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

      
  pinMode(gassensor,INPUT);


  
}

void loop()
{
  delay(dht.getMinimumSamplingPeriod());

  float humidity = dht.getHumidity();
  float temperature = dht.getTemperature();

  Serial.print(dht.getStatusString());
  Serial.print("humidity\t");
  Serial.print(humidity, 1);
    String name = Firebase.pushFloat("humidity",humidity);
      ////handle error
    if (Firebase.failed()) {
    Serial.print("pushing humidity/logs failed:");
    Serial.println(Firebase.error()); 
    return;
    }
      

  
  Serial.print("\ttemperature\t");
  Serial.print(temperature, 1);
   String name3 = Firebase.pushFloat("temperature",temperature);
      ////handle error
    if (Firebase.failed()) {
    Serial.print("pushing temperature/logs failed:");
    Serial.println(Firebase.error()); 
    return;
    }

  
  Serial.print("\t\tFahrenheit\t");
  Serial.print(dht.toFahrenheit(temperature), 1);

  
  Serial.print("\t\tHeatIndex\t");
  Serial.print(dht.computeHeatIndex(temperature, humidity, false), 1);
   String name2 = Firebase.pushFloat("Heat index ",dht.computeHeatIndex(temperature, humidity, false));
      ////handle error
    if (Firebase.failed()) {
    Serial.print("pushing heat index/logs failed:");
    Serial.println(Firebase.error()); 
    return;
    }


  
//  Serial.print("\t\t");
//  Serial.println(dht.computeHeatIndex(dht.toFahrenheit(temperature), humidity, true), 1);
  
  Serial.print("Gas sensor\t\t");
  Serial.println(analogRead(gassensor));

     String name1 = Firebase.pushFloat("gasSensor",analogRead(gassensor));
      ////handle error
    if (Firebase.failed()) {
    Serial.print("pushing gas sensor/logs failed:");
    Serial.println(Firebase.error()); 
    return;
    }


  delay(2000);






  

}

int ldr = D0;
void setup() {
  // put your setup code here, to run once:

pinMode(ldr,INPUT);
Serial.begin(115200);

}

void loop() {
  // put your main code here, to run repeatedly
 
  int x = digitalRead(ldr);
  Serial.println(x);
  delay(1000);

}

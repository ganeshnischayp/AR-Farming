int input = D6;
const int gassensor = A0;
void setup() {
  // put your setup code here, to run once:
//  pinMode(D6,INPUT);
pinMode(A0,INPUT);
}

void loop() {
  // put your main code here, to run repeatedly:
  int val;
  Serial.begin(115200);
//  val = digitalRead(input);
//  Serial.print(val);   
  Serial.println(analogRead(gassensor));
  delay(100);
}

#define cdsPin A7

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  int cds = analogRead(cdsPin);
  int bright = map(cds, 1023, 0, 0, 100);
  Serial.println(bright);
  delay(1000);
}

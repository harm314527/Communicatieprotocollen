#include <MQ135.h>



int sensorValue;
void setup() {
  Serial.begin(9600);
}
void loop() {
  MQ135 Sensor = MQ135(A0);
  float ResistanceZero = Sensor.getRZero();
  float ppm = Sensor.getPPM();
  Serial.print("AirQua=");
  Serial.print(ppm);
  Serial.println(" PPM");
  delay(1000);
}

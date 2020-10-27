#include <MQ135.h>
#include <WiFiNINA.h>

char ssid[] = "H369AF6D8C6";   //  your network SSID (name)
char pass[] = "23ECAC495E6C";   // your network password

byte ip[] = {192, 168, 2, 9}; // the server ip
int port = 5000;

WiFiClient client;
int status = WL_IDLE_STATUS;
int sensorValue;
bool ClientConnected = false;

void ConnectToWIFI()
{
  while (status != WL_CONNECTED) {
    Serial.print("Attempting to connect to WPA SSID: ");
    Serial.println(ssid);
    // Connect to WPA/WPA2 network:
    status = WiFi.begin(ssid, pass);

    delay(2000);
  }
}

void ConnectToServer()
{
  if (client.connect(ip, port))
  {
    ClientConnected = true;
    Serial.println("Connected");

  }
  else
  {
    Serial.println("not connected");
  }
}

void sendtoServer(String Sensordata)
{
  String message = "Sensordata:" + Sensordata;
  int send = client.print(message);
  if (send > 0)
  {
    Serial.println(send + "Bytes send");
  }
}

float GetSensorData()
{
  MQ135 Sensor = MQ135(A0);
  float ResistanceZero = Sensor.getRZero();
  float ppm = Sensor.getPPM();
  Serial.print("AirQua=");
  Serial.print(ppm);
  Serial.println(" PPM");
  return ppm;
}

void setup() {
  Serial.begin(9600);
  ConnectToWIFI();

}

void loop() {

  float ppm = GetSensorData();
  if (ClientConnected == false)
  {
    ConnectToServer();
  }
  String Sensordata = "";
  Sensordata += ppm;
  Sensordata += "PPM"; 
  sendtoServer(Sensordata);
  delay(1000);
}

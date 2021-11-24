#include <ESP8266WiFi.h>
#include <FastLED.h>
#include <WiFiClient.h>
#include <PubSubClient.h>
#include <ArduinoJson.h>

const char* ssid = "17066 2.4G";     // replace with your wifi ssid and wpa2 key
const char* pass = "1234567890";
const char* broker = "broker.hivemq.com";
const char* inTopic = "7C123F6551FB49E0A2DF99402906DFF81EF2632D12424DA88229EA6D431C8CFC"; //API Key
const char* conTopic = "7C123F6551FB49E0A2DF99402906DFF81EF2632D12424DA88229EA6D431C8CFC/server";
#define DeviceSize 4
#define L1 D1
#define L2 D2
#define L3 D3
#define L4 D4
#define Red D5
#define Blue D6
#define Green D7
#define Light1 "C1D99C500A9749B0B7D15E85E26FDFBB"  //Device Id/Key
#define Light2 "F68F2DD9E90E47F0A9723B3693D43DD3"  //Device Id/Key
#define Light3 "2F4D114961414FBE90FB8F5FB7DE0007"  //Device Id/Key
#define Light4 "5EB020C874F04CF3B2EB9E3E4D863310" //

char message[3600];
#define DeviceSize 4
String deivcesId[DeviceSize] = { Light1, Light2, Light3, Light4}; //
WiFiClient espClient;
PubSubClient client(espClient);
StaticJsonDocument<400> mqttData;

void turnOff(String deviceId) {
  if (deviceId == Light1)
  {
    Serial.println("Turn on Device ID: OfficeLight1: ");
    digitalWrite(L1, HIGH);  // turn on relay with voltage HIGH
    connectionStatusOnServer(deviceId, "OFF");
  }
  if (deviceId == Light2)
  {
    Serial.println("Turn on Device ID: OfficeLight2: ");
    digitalWrite(L2, HIGH);  // turn on relay with voltage HIGH
    connectionStatusOnServer(deviceId, "OFF");
  }
  if (deviceId == Light3)
  {
    Serial.println("Turn on Device ID: TvLight: ");
    digitalWrite(L3, HIGH);  // turn on relay with voltage HIGH
    connectionStatusOnServer(deviceId, "OFF");
  }
  if (deviceId == Light4)
  {
    Serial.println("Turn on Device ID: TvUnitLight: ");
    digitalWrite(L4, HIGH);  // turn on relay with voltage HIGH
    connectionStatusOnServer(deviceId, "OFF");
  }
}

void turnOn(String deviceId)
{
  if (deviceId == Light1)
  {
    Serial.println("Turn on Device ID: Light1: ");
    digitalWrite(L1, LOW);  // turn on relay with voltage HIGH
    connectionStatusOnServer(deviceId, "ON");
  }
  if (deviceId == Light2)
  {
    Serial.println("Turn on Device ID: OfficeLight2: ");
    digitalWrite(L2, LOW);  // turn on relay with voltage HIGH
    connectionStatusOnServer(deviceId, "ON");
  }
  if (deviceId == Light3)
  {
    Serial.println("Turn on Device ID: Light3: ");
    digitalWrite(L3, LOW);  // turn on relay with voltage HIGH
    connectionStatusOnServer(deviceId, "ON");
  }
  if (deviceId == Light4)
  {
    Serial.println("Turn on Device ID: Light4: ");
    digitalWrite(L4, LOW);  // turn on relay with voltage HIGH
    connectionStatusOnServer(deviceId, "ON");
  }
}

void setup() {
  pinMode(L1, OUTPUT);
  pinMode(L2, OUTPUT);
  pinMode(L3, OUTPUT);
  pinMode(L4, OUTPUT);
  pinMode(Red, OUTPUT);
  pinMode(Blue, OUTPUT);
  pinMode(Green, OUTPUT);
  digitalWrite(L1, HIGH);
  digitalWrite(L2, HIGH);
  digitalWrite(L3, HIGH);
  digitalWrite(L4, HIGH);
  Serial.begin(9600);
  delay(10);
  Serial.println("Connecting to ");
  Serial.println(ssid);

  WiFi.begin(ssid, pass);
  while (WiFi.status() != WL_CONNECTED)
  {
    wifiStatus("Blue",true);
    delay(400);
    wifiStatus("Blue",false);
    delay(100);
    Serial.print(".");
  }
  Serial.println("");
  Serial.println("WiFi connected");
  wifiStatus("Red",true);
  client.setServer(broker, 1883);
  client.setCallback(callback);
}

void loop() {
  if (!client.connected()) {
    reconnect();
  }
  client.loop();
}
void connectionStatusOnServer(String deviceId, String Status)
{

  if (deviceId != "")
  {
    String devices = "[";
    devices += "'" + deviceId + "'";
    devices += "]";

    snprintf(message, 3600, "{'wifi':'%s','devices':%s \n,'status':'%s','ip':'%s'}", ssid, devices.c_str(), Status, WiFi.localIP().toString().c_str());
    client.publish(conTopic, message);
  }
}
void serverPing()
{

  for (int i = 0; i < DeviceSize; i++)
  {
    String devices = "['";
    devices += deivcesId[i];
    devices += "']";

    snprintf(message, 3600, "{'wifi':'%s','devices':%s \n,'status':'','ip':'%s'}", ssid, devices.c_str(), WiFi.gatewayIP().toString().c_str());
    client.publish(conTopic, message);
  }
  for(int i=0;i<5;i++)
  {
    wifiStatus("Green",true);
    delay(100);
    wifiStatus("false",true);
  }
  wifiStatus("Red",true);
}
void reconnect() {
  while (!client.connected())
  {
    Serial.print("\n Connecting To ");
    Serial.println(broker);
    if (client.connect("Web_Iot_"+millis()))
    {
      Serial.print("\n Connected To ");
      Serial.println(broker);
      client.subscribe(inTopic);
      Serial.println("subscribe to " + String(inTopic));
    }
    else
    {
      Serial.print("\n Trying to Connect from");
      Serial.println(broker);
      delay(5000);
    }
  }
}
void callback(char* topic, byte* payload, unsigned int length)
{
  Serial.print("Msg Received");
  char data[200];
  for (int i = 0; i < length; i++)
  {
    data[i] = (char)payload[i];
  }

  DeserializationError error = deserializeJson(mqttData, data);
  if (error) {
    Serial.print(F("deserializeJson() failed: "));
    Serial.println(error.f_str()); //unable to convert mqtt data/incoming device data from server
    return;
  }
  Serial.println("Msg Converted");
  String deviceId = mqttData["deviceId"];
  String action = mqttData["action"];
  String value = mqttData["value"]["on"];
  String brightness = mqttData["value"]["brightness"];
  String timerVal = mqttData["value"]["timer"];
  String colorVal = mqttData["value"]["color"]["spectrumRGB"];
  Serial.println(action);
  Serial.println("brightness");
  Serial.println(brightness);
  Serial.println("timer");
  Serial.println(timerVal);
  Serial.println("color");
  Serial.println(colorVal);
  if (action == "ping")
  {
    Serial.println("ping command received");
    serverPing();
    Serial.println("ping command Replied");
    return;
  }
  if (action == "action.devices.commands.OnOff")
  {
    wifiStatus("Green",true);
   
    if (value == "true") {
      turnOn(deviceId);
    }
    else {
      turnOff(deviceId);
    }
     delay(100);
    wifiStatus("Red",true);
  }
}
void wifiStatus(String ledColor,bool isOn)
{
  digitalWrite(Red,HIGH);
   digitalWrite(Blue,HIGH);
    digitalWrite(Green,HIGH);
  if(ledColor=="Red")
  {
    if(isOn)
    digitalWrite(Red,LOW);
    else
    digitalWrite(Red,HIGH);
  }
  if(ledColor=="Blue")
  {
    if(isOn)
    digitalWrite(Blue,LOW);
    else
    digitalWrite(Blue,HIGH);
  }
  if(ledColor=="Green")
  {
    if(isOn)
    digitalWrite(Green,LOW);
    else
    digitalWrite(Green,HIGH);
  }
  else
  digitalWrite(Red,LOW);
}

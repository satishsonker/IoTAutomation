#include <ESP8266WiFi.h>

#include <FastLED.h>
#include <WiFiClient.h>
#include <PubSubClient.h>
#include <ArduinoJson.h>

const char *ssid =  "17066 2.4G";     // replace with your wifi ssid and wpa2 key
const char *pass =  "1234567890";
const char *broker = "broker.hivemq.com";
const char* inTopic = "7C123F6551FB49E0A2DF99402906DFF81EF2632D12424DA88229EA6D431C8CFC"; //API Key
const char* conTopic = "7C123F6551FB49E0A2DF99402906DFF81EF2632D12424DA88229EA6D431C8CFC/server";
#define L1 D1
#define L2 D2
#define L3 D3
#define L4 D4
#define L5 D5
#define BalconlyLight "6448A3BC8D174E709AEFFE67C4D91FD9"  //Device Id/Key
#define Fan "B53C9EEED04E4B01BDF2AF323BCF087C"  //Device Id/Key
#define Light1 "4B29A32E2F034B29AB8F7707E0CAB2A1"  //Device Id/Key
#define Light2 "2571BD601DF74C5E81EB8969139FFC39"  //Device Id/Key
#define MoonLight "EBC05DFFF34E4417860A64C346BAA912"  //Device Id/Key
// For led chips like WS2812, which have a data line, ground, and power, you just
// need to define DATA_PIN.  For led chipsets that are SPI based (four wires - data, clock,
// ground, and power), like the LPD8806 define both DATA_PIN and CLOCK_PIN
// Clock pin only needed for SPI based chipsets when not using hardware SPI
// Define the array of leds
char message[3600];
#define DeviceSize 5
String deivcesId[DeviceSize] = {BalconlyLight, Fan, Light1, Light2, MoonLight}; //
WiFiClient espClient;
PubSubClient client(espClient);
StaticJsonDocument<400> mqttData;

void turnOff(String deviceId) {
  if (deviceId == BalconlyLight)
  {
    Serial.println("Turn Off Device ID: BalconlyLight: ");
    digitalWrite(L1, HIGH);  // turn Off relay with voltage HIGH
    connectionStatusOnServer(deviceId, "OFF");
  }
  if (deviceId == Fan)
  {
    Serial.println("Turn Off Device ID: Fan: ");
    digitalWrite(L2, HIGH);  // turn Off relay with voltage HIGH
    connectionStatusOnServer(deviceId, "OFF");
  }
  if (deviceId == Light1)
  {
    Serial.println("Turn OFF Device ID: Light1: ");
    digitalWrite(L3, HIGH);  // turn on relay with voltage HIGH
    connectionStatusOnServer(deviceId, "OFF");
  }
  if (deviceId == Light2)
  {
    Serial.println("Turn OFF Device ID: Light2: ");
    digitalWrite(L4, HIGH);  // turn OFF relay with voltage HIGH
    connectionStatusOnServer(deviceId, "OFF");
  }
  if (deviceId == MoonLight)
  {
    Serial.println("Turn OFF Device ID: MoonLight: ");
    digitalWrite(L5, HIGH);  // turn OFF relay with voltage HIGH
    connectionStatusOnServer(deviceId, "OFF");
  }
}

void turnOn(String deviceId)
{
  if (deviceId == BalconlyLight)
  {
    Serial.println("Turn ON Device ID: BalconlyLight: ");
    digitalWrite(L1, LOW);  // turn on relay with voltage HIGH
    connectionStatusOnServer(deviceId, "ON");
  }
  if (deviceId == Fan)
  {
    Serial.println("Turn ON Device ID: Fan: ");
    digitalWrite(L2, LOW);  // turn on relay with voltage HIGH
    connectionStatusOnServer(deviceId, "ON");
  }
  if (deviceId == Light1)
  {
    Serial.println("Turn ON Device ID: Light1: ");
    digitalWrite(L3, LOW);  // turn on relay with voltage HIGH
    connectionStatusOnServer(deviceId, "ON");
  }
  if (deviceId == Light2)
  {
    Serial.println("Turn ON Device ID: Light2: ");
    digitalWrite(L4, LOW);  // turn on relay with voltage HIGH
    connectionStatusOnServer(deviceId, "ON");
  }
  if (deviceId == MoonLight)
  {
    Serial.println("Turn ON Device ID: MoonLight: ");
    digitalWrite(L5, LOW);  // turn on relay with voltage HIGH
    connectionStatusOnServer(deviceId, "ON");
  }
}

void setup() {
  pinMode(L1, OUTPUT);
  digitalWrite(L1, HIGH);
  pinMode(L2, OUTPUT);
  digitalWrite(L2, HIGH);
  pinMode(L3, OUTPUT);
  digitalWrite(L3, HIGH);
  pinMode(L4, OUTPUT);
  digitalWrite(L4, HIGH);
  pinMode(L5, OUTPUT);
  digitalWrite(L5, HIGH);
  Serial.begin(9600);
  delay(10);
  Serial.println("Connecting to ");
  Serial.println(ssid);

  WiFi.begin(ssid, pass);
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.println("WiFi connected");
  client.setServer(broker, 1883);
  client.setCallback(callback);
}

void loop() {
  if (!client.connected()) {
    reconnect();
  }
  connectionStatusOnServer("", "");
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
}
void reconnect() {
  while (!client.connected())
  {
    Serial.print("\n Connecting To ");
    Serial.println(broker);
    if (client.connect("Web_Iot_" + millis(), "", ""))
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
    Serial.println(action);
    if (action == "ping")
  {
    Serial.println("ping command received");
    serverPing();
    Serial.println("ping command Replied");
    return;
  } 
    if (action == "action.devices.commands.OnOff")
    {
      if (value == "true" || value == "ON" || value == "on" || value == "On") {
        turnOn(deviceId);
      }
      else {
        turnOff(deviceId);
      }
    }
}

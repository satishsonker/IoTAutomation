#include<ESP8266WiFi.h>
#include <WiFiClient.h>
#include <PubSubClient.h>
#include <ArduinoJson.h>

const char* ssid = "17066 2.4G";     // replace with your wifi ssid and wpa2 key
const char* pass = "1234567890";
const char* broker = "broker.hivemq.com";
const char* inTopic = "7C123F6551FB49E0A2DF99402906DFF81EF2632D12424DA88229EA6D431C8CFC"; //API Key
const char* conTopic = "7C123F6551FB49E0A2DF99402906DFF81EF2632D12424DA88229EA6D431C8CFC/server";
#define SmokeSensorPin A0 //Analog PIN
#define SmokeSensor "74517D4C682E49ECB9437A633F7717CB" //DeviceID

int sensorThres = 400;
int analogSensor = 0;
char message[3600];
#define DeviceSize 1
String deivcesId[DeviceSize] = { SmokeSensor}; //
WiFiClient espClient;
PubSubClient client(espClient);
StaticJsonDocument<400> mqttData;

void setup() {  
  pinMode(SmokeSensorPin, INPUT);
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
    client.loop();
  analogSensor = analogRead(SmokeSensorPin);
  Serial.println("Gas Level:"+analogSensor);
  if(analogSensor>sensorThres)
  {
    GasReport(SmokeSensor,analogSensor);
  }
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
        if (client.connect("Web_Iot_"+millis(), "", ""))
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
    String action = mqttData["action"];
    if (action == "ping")
    {
        Serial.println("ping command received");
        serverPing();
        Serial.println("ping command Replied");
        return;
    }
}
void GasReport(String deviceId,int val)
{
  snprintf(message, 3600, "{'wifi':'%s','devices':['%s'] ,'status':'ON','ip':'%s','gasMax':1024,'gasCurrent':%d,'gasThreshold':%d}",ssid, deviceId.c_str(), WiFi.localIP().toString().c_str(),val,sensorThres);
  client.publish(conTopic, message);
  snprintf(message, 3600, "{'deviceId':'%s','action':'doorbell'}", deviceId.c_str());
  client.publish(inTopic, message);
  Serial.println("Doorbell Pressed");
}

#include<ESP8266WiFi.h>
#include <WiFiClient.h>
#include <PubSubClient.h>
#include <ArduinoJson.h>

const char* ssid = "17066 2.4G";     // replace with your wifi ssid and wpa2 key
const char* pass = "1234567890";
const char* broker = "broker.hivemq.com";
const char* inTopic = "7C123F6551FB49E0A2DF99402906DFF81EF2632D12424DA88229EA6D431C8CFC"; //API Key
const char* conTopic = "7C123F6551FB49E0A2DF99402906DFF81EF2632D12424DA88229EA6D431C8CFC/server";
#define L1 D8
#define L2 D1
#define L3 D2
#define L4 D3
#define L5 D4
#define L6 D5
#define L7 D6
#define L8 D0
#define HallLight1 "4BBE91702F5F4BDB9C3DE49EDF13BEDC"  //Device Id/Key
#define HallLight2 "A33B479D92F0486D8943BE9A8F87B02D"  //Device Id/Key
#define HallLight3 "383E08641AB4400993B56DBD3A57F0AD"  //Device Id/Key
#define HallFan1 "8C2C5997585B4DEAB6781FE0DEEA3601"
#define HallFan2 "4E3E98D44E274557BD05DF16D6B5A600" 
#define HallMoonLight "8D714DFA298A43F6B7435E8388D3F239" 
#define HallBalconyLight "DBD3ADFBF30543C2B5C951AC5EE148C6" 
#define Jhumar "6F82691AEFA848458DD0D66D919F50A7" 
char message[3600];
#define DeviceSize 8
String deivcesId[DeviceSize] = { HallLight1,HallLight2,HallLight3,HallFan1,HallFan2,HallMoonLight,HallBalconyLight,Jhumar }; //
WiFiClient espClient;
PubSubClient client(espClient);
StaticJsonDocument<400> mqttData;

void turnOn(String deviceId) {
  if (deviceId == HallMoonLight)
    {   digitalWrite(L6, LOW);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "ON");
    }
    if (deviceId == HallBalconyLight)
    {   digitalWrite(L7, LOW);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "ON");
    }
    if (deviceId == Jhumar)
    {   digitalWrite(L8, LOW);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "ON");
    }
    if (deviceId == HallLight1)
    {   digitalWrite(L1, LOW);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "ON");
    }
    if (deviceId == HallLight2)
    {   digitalWrite(L2, LOW);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "ON");
    }
    if (deviceId == HallLight3)
    {   digitalWrite(L3, LOW);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "ON");
    }
    if (deviceId == HallFan1)
    {   digitalWrite(L4, LOW);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "ON");
    }
    if (deviceId == HallFan2)
    {   digitalWrite(L5, LOW);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "ON");
    }
}

void turnOff(String deviceId)
{
     if (deviceId == HallMoonLight)
    {   digitalWrite(L6, HIGH);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "OFF");
    }
    if (deviceId == HallBalconyLight)
    {   digitalWrite(L7, HIGH);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "OFF");
    }
    if (deviceId == Jhumar)
    {   digitalWrite(L8, HIGH);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "OFF");
    }
    if (deviceId == HallLight1)
    {   digitalWrite(L1, HIGH);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "OFF");
    }
    if (deviceId == HallLight2)
    {   digitalWrite(L2, HIGH);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "OFF");
    }
    if (deviceId == HallLight3)
    {   digitalWrite(L3, HIGH);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "OFF");
    }
    if (deviceId == HallFan1)
    {   digitalWrite(L4, HIGH);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "OFF");
    }
    if (deviceId == HallFan2)
    {   digitalWrite(L5, HIGH);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "OFF");
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
    pinMode(L6, OUTPUT);
    digitalWrite(L6, HIGH);
    pinMode(L7, OUTPUT);
    digitalWrite(L7, HIGH);
    pinMode(L8, OUTPUT);
    digitalWrite(L8, HIGH);
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
    String deviceId = mqttData["deviceId"];
    String action = mqttData["action"];
    String value = mqttData["value"]["on"];
    if (action == "ping")
    {
        Serial.println("ping command received");
        serverPing();
        Serial.println("ping command Replied");
        return;
    }
    if (action == "action.devices.commands.OnOff")
    {
        if (value == "true") {
            turnOn(deviceId);
        }
        else {
            turnOff(deviceId);
        }
    }
}

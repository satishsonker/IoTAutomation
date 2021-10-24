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
#define L1 D1
#define L2 D2
#define L3 D3
#define L4 D4
#define TempleLight "1704DC32EED44696B17F6C488571676A"  //Device Id/Key
#define TvColorLight "DE7C70044FA7414F85E383CAF4946835"  //Device Id/Key
#define TvLight "32BAB164570A480DA569EB4A27BE0C2E"  //Device Id/Key
#define TvUnitLight "1ECA217174274A88B5D4AB424D2DB81A" 
char message[3600];
#define DeviceSize 4
String deivcesId[DeviceSize] = { TempleLight,TvColorLight,TvLight,TvUnitLight }; //
WiFiClient espClient;
PubSubClient client(espClient);
StaticJsonDocument<400> mqttData;

void turnOn(String deviceId) {
    if (deviceId == TempleLight)
    {
        Serial.println("Turn on Device ID: OfficeLight1: ");
        digitalWrite(L1, HIGH);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "ON");
    }
    if (deviceId == TvColorLight)
    {
        Serial.println("Turn on Device ID: OfficeLight2: ");
        digitalWrite(L2, HIGH);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "ON");
    }
    if (deviceId == TvLight)
    {
        Serial.println("Turn on Device ID: TvLight: ");
        digitalWrite(L3, HIGH);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "ON");
    }
    if (deviceId == TvUnitLight)
    {
        Serial.println("Turn on Device ID: TvUnitLight: ");
        digitalWrite(L5, HIGH);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "ON");
    }
}

void turnOff(String deviceId)
{
    if (deviceId == TempleLight)
    {
        Serial.println("Turn on Device ID: OfficeLight1: ");
        digitalWrite(L1, LOW);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "OFF");
    }
    if (deviceId == TvColorLight)
    {
        Serial.println("Turn on Device ID: OfficeLight2: ");
        digitalWrite(L2, LOW);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "OFF");
    }
    if (deviceId == TvLight)
    {
        Serial.println("Turn on Device ID: TvLight: ");
        digitalWrite(L3, LOW);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "OFF");
    }
    if (deviceId == TvUnitLight)
    {
        Serial.println("Turn on Device ID: TvUnitLight: ");
        digitalWrite(L5, LOW);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "OFF");
    }
}

void setup() {
    pinMode(L1, OUTPUT);
    digitalWrite(L1, LOW);
    pinMode(L2, OUTPUT);
    digitalWrite(L2, LOW);
    pinMode(L3, OUTPUT);
    digitalWrite(L3, LOW);
    pinMode(L5, OUTPUT);
    digitalWrite(L5, LOW);
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
        if (client.connect("Web_Iot_1234", "", ""))
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
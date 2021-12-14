#include<ESP8266WiFi.h>
#include <WiFiClient.h>
#include <PubSubClient.h>
#include <ArduinoJson.h>

const char* ssid = "17066 2.4G";     // replace with your wifi ssid and wpa2 key
const char* pass = "1234567890";
const char* broker = "broker.hivemq.com";
const char* inTopic = "7C123F6551FB49E0A2DF99402906DFF81EF2632D12424DA88229EA6D431C8CFC"; //API Key
const char* conTopic = "7C123F6551FB49E0A2DF99402906DFF81EF2632D12424DA88229EA6D431C8CFC/server";
const int trigPin = D6;
const int echoPin = D5;

//define sound velocity in cm/uS
#define SOUND_VELOCITY 0.034
#define CM_TO_INCH 0.393701
long duration;
float distanceCm;
float distanceInch;
#define UltrasonicSensor "18B791EC54FC4C8AB8D8CEB9767CE840"
char message[3600];
#define DeviceSize 1
String deivcesId[DeviceSize] = { UltrasonicSensor}; //
WiFiClient espClient;
PubSubClient client(espClient);
StaticJsonDocument<400> mqttData;

void setup() {  
   pinMode(trigPin, OUTPUT); // Sets the trigPin as an Output
  pinMode(echoPin, INPUT); 
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
 long duration, inches, cm;
   digitalWrite(trigPin, LOW);
  delayMicroseconds(2);
  // Sets the trigPin on HIGH state for 10 micro seconds
  digitalWrite(trigPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(trigPin, LOW);
  
  // Reads the echoPin, returns the sound wave travel time in microseconds
  duration = pulseIn(echoPin, HIGH);
  
  // Calculate the distance
  distanceCm = duration * SOUND_VELOCITY/2;
  
  // Convert to inches
  distanceInch = distanceCm * CM_TO_INCH;
  
  // Prints the distance on the Serial Monitor
  Serial.print("Distance (cm): ");
  Serial.println(distanceCm);
  delay(4000);
   Distance(UltrasonicSensor,distanceCm);
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
        }
        else {
        }
    }
}
void Distance(String deviceId,long cm)
{
  snprintf(message, 3600, "{'wifi':'%s','devices':['%s'] ,'status':'ON','ip':'%s','ultraDistanceCm':%d}",ssid, deviceId.c_str(), WiFi.localIP().toString().c_str(),cm);
  client.publish(conTopic, message);
}

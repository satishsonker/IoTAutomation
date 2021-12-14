#include<ESP8266WiFi.h>
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
#define MainDoorSensor "74517D4C682E49ECB9437A633F7717CB"
#define EntranceLight "D9076D6F311D4454A3398581E503E5AB"  //Device Id/Key

int ledState = LOW; /* Variable to hold the current state of LED Pin. Initialized to LOW */
int buttonState = LOW; /* Variable to hold current state of Button Pin. Initialized to LOW */
int buttonValue; /* Variable to store state of the Button */
int lastButtonState = LOW; /* Variable to hold the previous state of the Button. Initialized to LOW */

long lastDebounceTime = 0; /* Variable to hold the last time the LED Pin was toggled */
long debounceDelay = 50; /* Debounce Time */
char message[3600];
#define DeviceSize 2
String deivcesId[DeviceSize] = { EntranceLight,MainDoorSensor}; //
WiFiClient espClient;
PubSubClient client(espClient);
StaticJsonDocument<400> mqttData;

void turnOn(String deviceId) {
  if (deviceId == EntranceLight)
    {   digitalWrite(L1, LOW);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "ON");
    }
}

void turnOff(String deviceId)
{
     if (deviceId == EntranceLight)
    {   digitalWrite(L1, HIGH);  // turn on relay with voltage HIGH
        connectionStatusOnServer(deviceId, "OFF");
    }
}

void setup() {
    pinMode(L1, OUTPUT);
    pinMode(L2, INPUT);
    digitalWrite(L1, HIGH);
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
    
  buttonValue = digitalRead(L2); /* Read the state of the Button into the variable: buttonValue */
  /* Reset the debounce timer after button press */
  if(buttonValue != lastButtonState)
  {
    lastDebounceTime = millis();
  }

  /* Use the button state after waiting for debouncing */ 
  if((millis() - lastDebounceTime) > debounceDelay)
  {
    if(buttonValue != buttonState) /* Check if the button state has changed */
    {
      buttonState = buttonValue;
      if(buttonState == LOW) /* If the button state is HIGH, toggle the LED state */
      {
        ledState = !ledState;
       pressDoorBell(MainDoorSensor);
      }
    }
  } /* Set the new state of the LED */
  lastButtonState = buttonValue; /* Store the present button state for next loop */
    
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
void pressDoorBell(String deviceId)
{
  snprintf(message, 3600, "{'deviceId':'%s','action':'doorbell'}", deviceId.c_str());
  client.publish(inTopic, message);
  Serial.println("Doorbell Pressed");
}

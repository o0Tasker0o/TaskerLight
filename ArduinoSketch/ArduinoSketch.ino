#include <SPI.h>

#include "Adafruit_WS2801.h"

#define DATA_PIN 2    //Yellow cable
#define CLOCK_PIN 3   //Green cable
#define NUM_OF_PIXELS 25

Adafruit_WS2801 m_strip = Adafruit_WS2801(NUM_OF_PIXELS, DATA_PIN, CLOCK_PIN);

int mReadCount = 0;
int mReadBuffer[NUM_OF_PIXELS * 3];

void setup() 
{
  Serial.begin(115200); 
   
  m_strip.begin();
   
  WriteBufferToLeds();
    
  m_strip.show();
}

void loop() 
{    
  if(Serial.available() > 0)
  {
    mReadBuffer[mReadCount++] = Serial.read();
  }
  
  if(mReadCount >= m_strip.numPixels() * 3)
  {
    WriteBufferToLeds();
    
    m_strip.show();
    
    mReadCount = 0;
    Serial.write(mReadBuffer[1]);
  }
}

void WriteBufferToLeds()
{
  for(int pixelIndex = 0; pixelIndex < m_strip.numPixels(); ++pixelIndex)
  {                                   
    m_strip.setPixelColor(pixelIndex, 
                          mReadBuffer[(pixelIndex * 3)],
                          mReadBuffer[(pixelIndex * 3) + 1],
                          mReadBuffer[(pixelIndex * 3) + 2]); 
  } 
}

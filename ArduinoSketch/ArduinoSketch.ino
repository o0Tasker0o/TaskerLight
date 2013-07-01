#include <SPI.h>

#include "Adafruit_WS2801.h"

#define DATA_PIN 2    //Yellow cable
#define CLOCK_PIN 3   //Green cable
#define NUM_OF_PIXELS 25

Adafruit_WS2801 m_strip = Adafruit_WS2801(NUM_OF_PIXELS, DATA_PIN, CLOCK_PIN);

byte mFadeRate = 40;
unsigned long mLastUpdate;

uint32_t mDstBuffer[NUM_OF_PIXELS];
uint32_t mCurrentBuffer[NUM_OF_PIXELS];

void setup() 
{
  Serial.begin(115200); 
          
  m_strip.begin();  
  
  for(int bufferIndex = 0; bufferIndex < m_strip.numPixels(); ++bufferIndex)
  {
    mCurrentBuffer[bufferIndex] = 0;
  }
    
  WriteBufferToLeds();
  
  mLastUpdate = millis();
}

void loop() 
{     
  if(Serial.available() > 0)
  {  
    char serialBuffer[76];
    Serial.readBytes(serialBuffer, 76);
      
    int pixelsRead = 0;
      
    for(int bufferIndex = 0; bufferIndex < 75; )
    {
      mDstBuffer[pixelsRead] = BytesToColour(serialBuffer[bufferIndex++], 
                                             serialBuffer[bufferIndex++], 
                                             serialBuffer[bufferIndex++]);
                                            
      pixelsRead++;
    }  
    
    mFadeRate = serialBuffer[75];
    
    Serial.write(0);
  }
  
  if(millis() - mLastUpdate > 25)
  {
    for(int pixelIndex = 0; pixelIndex < m_strip.numPixels(); ++pixelIndex)
    {      
      byte currentR, currentG, currentB;
      byte destR, destG, destB;
            
      ColourToBytes(mCurrentBuffer[pixelIndex], &currentR, &currentG, &currentB);
      ColourToBytes(mDstBuffer[pixelIndex], &destR, &destG, &destB);
      
      int differenceR = destR - currentR;
      int differenceG = destG - currentG;
      int differenceB = destB - currentB;
      
      normaliseToOne(&differenceR, &differenceG, &differenceB);
      differenceR *= mFadeRate;
      differenceG *= mFadeRate;
      differenceB *= mFadeRate;

      if(currentR + differenceR >= 0 &&
         currentR + differenceR <= 255 &&
         currentG + differenceG >= 0 &&
         currentG + differenceG <= 255 &&
         currentB + differenceB >= 0 &&
         currentB + differenceB <= 255)
      {
        currentR += differenceR;
        currentG += differenceG;
        currentB += differenceB;
        
        mCurrentBuffer[pixelIndex] = BytesToColour(currentR, currentG, currentB);
      }
      else
      {
        mCurrentBuffer[pixelIndex] = mDstBuffer[pixelIndex];
      }
    }
    
    WriteBufferToLeds();
    mLastUpdate = millis();
  }
}

void WriteBufferToLeds()
{
  for(int pixelIndex = 0; pixelIndex < m_strip.numPixels(); ++pixelIndex)
  {                           
    m_strip.setPixelColor(pixelIndex, 
                          mCurrentBuffer[pixelIndex]); 
  } 
  
  m_strip.show();
}

void normaliseToOne(int *r, int *g, int *b)
{
  int minimum = 256;
  
  if(*r > 0 && *r < minimum)
  {
    minimum = *r;
  }
  
  if(*g > 0 && *g < minimum)
  {
    minimum = *g;
  }
  
  if(*b > 0 && *b < minimum)
  {
    minimum = *b;
  }
    
  *r = *r / minimum;
  *g = *g / minimum;
  *b = *b / minimum;
}

void ColourToBytes(uint32_t colour, byte *r, byte *g, byte *b)
{
  *b = colour & 255;
  colour >>= 8;
  *g = colour & 255;
  colour >>= 8;
  *r = colour & 255;
}

uint32_t BytesToColour(byte r, byte g, byte b)
{
  uint32_t returnColour;
  
  returnColour = r;
  returnColour <<= 8;
  
  returnColour |= g;
  returnColour <<= 8;
  
  returnColour |= b;
  return returnColour;
}

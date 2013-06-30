#include <SPI.h>

#include "Adafruit_WS2801.h"

#define DATA_PIN 2    //Yellow cable
#define CLOCK_PIN 3   //Green cable
#define NUM_OF_PIXELS 25

Adafruit_WS2801 m_strip = Adafruit_WS2801(NUM_OF_PIXELS, DATA_PIN, CLOCK_PIN);

int mFadeRate = 40;
unsigned long mLastUpdate;

uint32_t mSrcBuffer[NUM_OF_PIXELS];
uint32_t mDstBuffer[NUM_OF_PIXELS];
uint32_t mCurrentBuffer[NUM_OF_PIXELS];

void setup() 
{
  Serial.begin(115200); 
        
  randomSeed(1);
  
  m_strip.begin();  
  
  for(int bufferIndex = 0; bufferIndex < m_strip.numPixels(); ++bufferIndex)
  {
    mCurrentBuffer[bufferIndex] = 16711680;
  }
    
  WriteBufferToLeds();
  
  mLastUpdate = millis();
}

void loop() 
{     
  if(Serial.available() > 0)
  {  
    char colourBuffer[75];
    Serial.readBytes(colourBuffer, 75);
      
    int pixelsRead = 0;
      
    for(int bufferIndex = 0; bufferIndex < 75; )
    {
      mDstBuffer[pixelsRead] = Color(colourBuffer[bufferIndex++], 
                                      colourBuffer[bufferIndex++], 
                                      colourBuffer[bufferIndex++]);
                                      
      mSrcBuffer[pixelsRead] = mCurrentBuffer[pixelsRead];
      
      pixelsRead++;
    }  
    
    Serial.write(0);
  }
  
  if(millis() - mLastUpdate > 25)
  {
    for(int pixelIndex = 0; pixelIndex < m_strip.numPixels(); ++pixelIndex)
    {      
      uint32_t destinationColour = mDstBuffer[pixelIndex];
      byte destB = destinationColour & 255;
      destinationColour >>= 8;
      byte destG = destinationColour & 255;
      destinationColour >>= 8;
      byte destR = destinationColour & 255;
      
      uint32_t sourceColour = mSrcBuffer[pixelIndex];
      byte srcB = sourceColour & 255;
      sourceColour >>= 8;
      byte srcG = sourceColour & 255;
      sourceColour >>= 8;
      byte srcR = sourceColour & 255;
      
      int differenceR = destR - srcR;
      int differenceG = destG - srcG;
      int differenceB = destB - srcB;
      
      normaliseToOne(&differenceR, &differenceG, &differenceB);
      differenceR *= mFadeRate;
      differenceG *= mFadeRate;
      differenceB *= mFadeRate;
      
      uint32_t currentColour = mCurrentBuffer[pixelIndex];
      byte currentB = currentColour & 255;
      currentColour >>= 8;
      byte currentG = currentColour & 255;
      currentColour >>= 8;
      byte currentR = currentColour & 255;
      
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
        
        mCurrentBuffer[pixelIndex] = Color(currentR, currentG, currentB);
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

uint32_t Color(byte r, byte g, byte b)
{
  uint32_t c;
  c = r;
  c <<= 8;
  c |= g;
  c <<= 8;
  c |= b;
  return c;
}

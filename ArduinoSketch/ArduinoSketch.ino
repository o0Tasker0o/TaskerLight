#include <SPI.h>

#include "Adafruit_WS2801.h"

#define DATA_PIN 2    //Yellow cable
#define CLOCK_PIN 3   //Green cable
#define NUM_OF_PIXELS 25

Adafruit_WS2801 m_strip = Adafruit_WS2801(NUM_OF_PIXELS, DATA_PIN, CLOCK_PIN);

long mFadeTime = 500;
long mLastUpdate;

long mSrcBuffer[NUM_OF_PIXELS];
long mDstBuffer[NUM_OF_PIXELS];

void setup() 
{
  Serial.begin(115200); 
          
  m_strip.begin();  
  
  for(int bufferIndex = 0; bufferIndex < m_strip.numPixels(); ++bufferIndex)
  {
    mSrcBuffer[bufferIndex] = 0;
    mDstBuffer[bufferIndex] = 0;
  }
    
  m_strip.show();
  
  mLastUpdate = millis();
}

void loop() 
{     
  for(;;)
  {
    if(Serial.available() > 0)
    {  
      char serialBuffer[77];
      Serial.readBytes(serialBuffer, 77);
        
      int pixelsRead = 0;
        
      for(int bufferIndex = 0; bufferIndex < 75; )
      {
        mSrcBuffer[pixelsRead] = mDstBuffer[pixelsRead];
        mDstBuffer[pixelsRead] = BytesToColour(serialBuffer[bufferIndex++], 
                                               serialBuffer[bufferIndex++], 
                                               serialBuffer[bufferIndex++]);
                                             
        pixelsRead++;
      }  
      
      mFadeTime = word(serialBuffer[76], serialBuffer[75]);
      
      Serial.write(0);
      mLastUpdate = millis();
    }
    
    long timeSinceLastUpdate = millis() - mLastUpdate;
    
    if(timeSinceLastUpdate > 25)
    {
      long srcColour, destColour, finalColour;
      long srcR, srcG, srcB;
      long destR, destG, destB;
      long finalR, finalG, finalB;
            
      for(int pixelIndex = 0; pixelIndex < m_strip.numPixels(); ++pixelIndex)
      {   
        if(timeSinceLastUpdate >= mFadeTime)
        {
          m_strip.setPixelColor(pixelIndex,             
                                mDstBuffer[pixelIndex]);
        }
        else
        {
          srcColour = mSrcBuffer[pixelIndex];     
          srcB = srcColour & 255;
          srcColour >>= 8;
          srcG = srcColour & 255;
          srcColour >>= 8;
          srcR = srcColour & 255;
          
          destColour = mDstBuffer[pixelIndex]; 
          destB = destColour & 255;
          destColour >>= 8;
          destG = destColour & 255;
          destColour >>= 8;
          destR = destColour & 255;
          
          finalR = srcR + (((destR - srcR) * timeSinceLastUpdate) / mFadeTime);
          finalG = srcG + (((destG - srcG) * timeSinceLastUpdate) / mFadeTime);
          finalB = srcB + (((destB - srcB) * timeSinceLastUpdate) / mFadeTime);
                    
          finalColour = BytesToColour(finalR & 255, finalG & 255, finalB & 255);
          
          m_strip.setPixelColor(pixelIndex,             
                                finalColour); 
        }
      }
      
      m_strip.show();
    }
  }
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

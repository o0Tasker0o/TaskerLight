#include "WS2801.h"

int dataPin = 2;       
int clockPin = 3;      

uint32_t pixels[25];
byte colours[3];

// Set the first variable to the NUMBER of pixels. 25 = 25 pixels in a row
WS2801 strip = WS2801(25, dataPin, clockPin);

void setup() 
{
  Serial.begin(9600);  
  
  strip.begin();

  // Update the strip, to start they are all 'off'
  strip.show();
}

void loop() 
{
    int pixelsRead = 0;
    int coloursRead = 0;
    
    Serial.flush();
    
    while(pixelsRead < 25)
    {
        if(Serial.available() > 0)
        {
            colours[coloursRead] = Serial.read();
            ++coloursRead;
        }
        
        if(coloursRead == 3)
        {
            pixels[pixelsRead] = Colour(colours[0], colours[1], colours[2]);
            ++pixelsRead;
            coloursRead = 0;
        }
    }
    
    for(int i = 0; i < 25; ++i)
    {
        strip.setPixelColor(i, pixels[i]);
    }
    
    strip.show();
    
    Serial.write(128);
}

/* Helper functions */

// Create a 24 bit color value from R,G,B
uint32_t Colour(byte r, byte g, byte b)
{
    uint32_t c;
    c = r;
    c <<= 8;
    c |= g;
    c <<= 8;
    c |= b;
    return c;
}

#include "WS2801.h"

#define FADE_TIME_MS 200
#define DATA_PIN 2
#define CLOCK_PIN 3     
#define NUM_OF_PIXELS 25

//Buffer to contain pixels streamed via the USB
uint32_t m_bufferPixels[NUM_OF_PIXELS];

//What colours the USB last set
uint32_t m_sourcePixels[NUM_OF_PIXELS];

//What the pixels are currently showing
uint32_t m_currentPixels[NUM_OF_PIXELS];

//What colours the pixels should be
uint32_t m_destinationPixels[NUM_OF_PIXELS];

byte m_colours[3];
int m_pixelsRead;
int m_coloursRead;

WS2801 m_strip = WS2801(NUM_OF_PIXELS, DATA_PIN, CLOCK_PIN);

unsigned long m_lastUpdate;

void setup() 
{
    Serial.begin(115200);  
  
    m_strip.begin();

    int startupDelay = 15;

    //Sweep red across the pixels
    for(int i = 0; i < NUM_OF_PIXELS; ++i)
    {
        m_strip.setPixelColor(i, Colour(255, 0, 0));
        m_strip.show();
        delay(startupDelay);
    }
    
    //Sweep green across the pixels
    for(int i = 0; i < NUM_OF_PIXELS; ++i)
    {
        m_strip.setPixelColor(i, Colour(0, 255, 0));
        m_strip.show();
        delay(startupDelay);
    }
    
    //Sweep blue across the pixels
    for(int i = 0; i < NUM_OF_PIXELS; ++i)
    {
        m_strip.setPixelColor(i, Colour(0, 0, 255));
        m_strip.show();
        delay(startupDelay);
    }
        
    m_lastUpdate = millis();
    
    m_pixelsRead = 0;
    m_coloursRead = 0;
}

void loop() 
{    
    //If something has been written to the serial port
    if(Serial.available() > 0)
    {
        //Store the serial data as an r, g or b byte
        m_colours[m_coloursRead] = Serial.read();
        
        ++m_coloursRead;
        
        //If you've read a full RGB value
        if(m_coloursRead == 3)
        {
            //Store the RGB value as a pixel colour
            m_bufferPixels[m_pixelsRead] = Colour(m_colours[0], m_colours[1], m_colours[2]);
            ++m_pixelsRead;
            
            //Prepare to read the next pixel
            m_coloursRead = 0;
        }
        
        //If you've read colours for all of the pixels
        if(m_pixelsRead == NUM_OF_PIXELS)
        {
            for(int i = 0; i < NUM_OF_PIXELS; ++i)
            {
                m_sourcePixels[i] = m_currentPixels[i];
                m_destinationPixels[i] = m_bufferPixels[i];
            }
            
            Serial.write((uint8_t) 0);
            
            m_pixelsRead = 0;
            m_coloursRead = 0;
            
            m_lastUpdate = millis();
        }
    }
    else
    {    
        //How many milliseconds has it been since the colours were updated
        unsigned long timeSinceLastUpdate = millis() - m_lastUpdate;
        
        for(int i = 0; i < NUM_OF_PIXELS; ++i)
        {
           if(timeSinceLastUpdate >= FADE_TIME_MS)
           {
               m_currentPixels[i] = m_destinationPixels[i];
               m_sourcePixels[i] = m_destinationPixels[i];
               m_strip.setPixelColor(i, m_sourcePixels[i]);
           }
           else
           {
               uint32_t destinationColour = m_destinationPixels[i];
               byte destB = destinationColour & 255;
               destinationColour >>= 8;
               byte destG = destinationColour & 255;
               destinationColour >>= 8;
               byte destR = destinationColour & 255;
                
               uint32_t sourceColour = m_sourcePixels[i];
               byte srcB = sourceColour & 255;
               sourceColour >>= 8;
               byte srcG = sourceColour & 255;
               sourceColour >>= 8;
               byte srcR = sourceColour & 255;
               
               int differenceR = destR - srcR;
               int differenceG = destG - srcG;
               int differenceB = destB - srcB;
                              
               differenceR *= timeSinceLastUpdate;  
               differenceR /= FADE_TIME_MS;               
               differenceG *= timeSinceLastUpdate;  
               differenceG /= FADE_TIME_MS;              
               differenceB *= timeSinceLastUpdate;
               differenceB /= FADE_TIME_MS; 
               
               m_currentPixels[i] = Colour(srcR + differenceR, srcG + differenceG, srcB + differenceB);
               
               m_strip.setPixelColor(i, m_currentPixels[i]);
           }
        }
            
        m_strip.show();
    }
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

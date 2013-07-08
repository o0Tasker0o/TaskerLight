using System;
using System.Drawing;

class TaskerLightScript
{
    //An array of colour values. One for each LED in the strip
    private static Color[] mLEDColors = new Color[25];

    /**
    * @brief Calculates the colour of the LEDs based on the current time
    * @param elapsedMS Passed in by the ControlPanel application.
    *        Represents the number of milliseconds elapsed since the
    *        application was started.
    * @return An array of colours representing an RGB value for each LED
    */
    public static Color[] TickLighting(long elapsedMS)
    {
        Random rand = new Random((int)elapsedMS);

        //For each LED in the strip
        for (int ledIndex = 0; ledIndex < 25; ++ledIndex)
        {
            mLEDColors[ledIndex] = HSVToRGB((float)rand.NextDouble(), 1.0f, 1.0f);
        }

        return mLEDColors;
    }

    private static Color HSVToRGB(float hue,
                                  float saturation,
                                  float value)
    {
        if (hue > 1.0f)
        {
            hue = 1.0f;
        }
        else if (hue < 0.0f)
        {
            hue = 0.0f;
        }

        if (saturation > 1.0f)
        {
            saturation = 1.0f;
        }
        else if (saturation < 0.0f)
        {
            saturation = 0.0f;
        }

        if (value > 1.0f)
        {
            value = 1.0f;
        }
        else if (value < 0.0f)
        {
            value = 0.0f;
        }

        float m, n, f;
        float r = 0;
        float g = 0;
        float b = 0;

        hue *= 6.0f;

        int i;

        if (-1 == hue)
        {
            r = value;
            g = value;
            b = value;
        }

        i = (int)hue;

        f = hue - i;

        if (i % 2 == 0) f = 1 - f; // if i is even
        m = value * (1 - saturation);
        n = value * (1 - saturation * f);
        switch (i)
        {
            case 6:
            case 0:
                r = value;
                g = n;
                b = m;
                break;
            case 1:
                r = n;
                g = value;
                b = m;
                break;
            case 2:
                r = m;
                g = value;
                b = n;
                break;
            case 3:
                r = m;
                g = n;
                b = value;
                break;
            case 4:
                r = n;
                g = m;
                b = value;
                break;
            case 5:
                r = value;
                g = m;
                b = n;
                break;
        }

        return Color.FromArgb((int) (r * 255),
                              (int) (g * 255),
                              (int) (b * 255));
    }
};
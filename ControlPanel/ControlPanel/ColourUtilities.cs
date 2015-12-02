using System.Drawing;

namespace ControlPanel
{
    public class ColourUtilities
    {
        public static void ColorToHSV(Color rgbColor, 
                                      ref float hue,
                                      ref float saturation,
                                      ref float value)
        {
            float min = 9999.0f;
            float max = 0.0f;
            float r = rgbColor.R / 255.0f;
            float g = rgbColor.G / 255.0f;
            float b = rgbColor.B / 255.0f;

            if (r < min)
            {
                min = r;
            }

            if (g < min)
            {
                min = g;
            }

            if (b < min)
            {
                min = b;
            }

            if (r > max)
            {
                max = r;
            }

            if (g > max)
            {
                max = g;
            }

            if (b > max)
            {
                max = b;
            }

            if (max == min)
            {
                hue = 0;
            }
            else if (r == max)
            {
                hue = (60 * ((g - b) / (max - min)) + 360) % 360;
            }
            else if (g == max)
            {
                hue = 60 * ((b - r) / (max - min)) + 120;
            }
            else if (b == max)
            {
                hue = 60 * ((r - g) / (max - min)) + 240;
            }

            if (0.0f == max)
            {
                saturation = 0.0f;
            }
            else
            {
                saturation = 1 - (min / max);
            }

            value = max;

            hue /= 360.0f;
        }

        public static Color HSVToColor(float hue,
                                         float saturation,
                                         float value)
        {
            if(hue > 1.0f)
            {
                hue = 1.0f;
            }
            else if(hue < 0.0f)
            {
                hue = 0.0f;
            }

            if(saturation > 1.0f)
            {
                saturation = 1.0f;
            }
            else if(saturation < 0.0f)
            {
                saturation = 0.0f;
            }

            if(value > 1.0f)
            {
                value = 1.0f;
            }
            else if(value < 0.0f)
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
    }
}
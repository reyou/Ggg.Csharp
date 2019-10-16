using System;
using System.Collections.Generic;

namespace intro1
{
    class CustomComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            Console.WriteLine($"Equals(): {x} vs {y}");
            int xPos = 0;
            int yPos = 0;
            while (true)
            {
                // ... Fail if past end.
                if (xPos >= x.Length)
                {
                    return false;
                }
                if (yPos >= y.Length)
                {
                    return false;
                }
                // ... Skip past hyphens.
                if (x[xPos] == '-')
                {
                    xPos++;
                    continue;
                }
                if (y[yPos] == '-')
                {
                    yPos++;
                    continue;
                }
                // ... Fail if different.
                if (x[xPos] != y[yPos])
                {
                    return false;
                }
                // ... If we have traversed both strings with no error, we match.
                if (xPos == x.Length - 1 &&
                    yPos == y.Length - 1)
                {
                    return true;
                }
                // ... Increment both places.
                xPos++;
                yPos++;
            }
        }

        public int GetHashCode(string obj)
        {
            int code = 0;
            // ... Add together all chars.
            for (int i = 0; i < obj.Length; i++)
            {
                if (obj[i] != '-')
                {
                    code += obj[i];
                }
            }
            Console.WriteLine($"GetHashCode(): {obj}:{code}");
            return code;
        }
    }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MyUtil
{
    private static Random random = new Random();

    public static int GetRandom(int bound)
    {
        return random.Next(bound);
    }
}
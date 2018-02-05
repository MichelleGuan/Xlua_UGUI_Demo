using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MyTimeUtil
{
    public static long GetCurrTimeMM()
    {
        return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
    }
}
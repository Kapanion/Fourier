using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Helpers
{
    public static float RoundN(this float x, int n)
    {
        return (float)Math.Round(x, n);
    }

    public static float Round1(this float x)
    {
        return x.RoundN(1);
    }

    public static float Round2(this float x)
    {
        return x.RoundN(2);
    }
}

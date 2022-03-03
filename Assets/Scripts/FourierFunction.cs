using System.Collections;
using System.Collections.Generic;
using System.Numerics;
public class FourierFunction
{
    private Complex[] coefficients;
    public FourierFunction(Complex[] coefficients)
    {
        this.coefficients = coefficients;
    }
    public Complex ValueAt(float t)
    {
        Complex res = Complex.Zero;
        for(int i = 0; i < coefficients.Length; ++i)
        {
            int freq = (i + 1) / 2;
            if (i % 2 == 0) freq = -freq;
            res += coefficients[i] * Complex.Exp(freq * 2f * System.Math.PI * Complex.ImaginaryOne * t);
        }
        return res;
    }

    public static FourierFunction Of(Complex[] vals, int numberOfVectors)
    {
        Complex[] coeffs = new Complex[numberOfVectors * 2 + 1];
        for (int i = -numberOfVectors; i <= numberOfVectors; ++i)
        {
            int ind = UnityEngine.Mathf.Abs(i) * 2;
            if (i > 0) ind--;
            coeffs[ind] = CalculateCoefficient(vals, i);
            UnityEngine.Debug.Log(i + ": " + coeffs[ind]);
        }
        return new FourierFunction(coeffs);
    }
    public static FourierFunction Of(UnityEngine.Vector2[] coords, int numberOfVectors)
    {
        Complex[] vals = new Complex[coords.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            vals[i] = new Complex(coords[i].x, coords[i].y);
        }
        return Of(vals, numberOfVectors);
    }

    static Complex CalculateCoefficient(Complex[] vals, int freq, int precision = 10000)
    {
        Complex sum = Complex.Zero;
        for (int i = 0; i < vals.Length; ++i)
        {
            for (int j = 0; j < precision; j++)
            {
                Complex val = (vals[i] * (precision - j) + vals[(i + 1) % vals.Length] * j) / precision;
                double t = (double)(i * precision + j ) / (vals.Length * precision);
                sum += val * Complex.Exp(-freq * 2 * System.Math.PI * Complex.ImaginaryOne * t);
            }            
        }
        sum /= (vals.Length * precision);
        return sum;
    }
}

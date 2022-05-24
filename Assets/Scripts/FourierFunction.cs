using System.Collections;
using System.Collections.Generic;
using Complex = System.Numerics.Complex;
using UnityEngine;

public class FourierFunction
{
    private Complex[] coefficients;
    public FourierFunction(Complex[] coefficients)
    {
        this.coefficients = coefficients;
    }

    public int Size => coefficients.Length;

    public Complex ValueAt(float t)
    {
        Complex res = Complex.Zero;
        for (int i = 0; i < coefficients.Length; ++i)
        {
            int freq = IndexToFrequency(i);
            res += coefficients[i] * Complex.Exp(freq * 2f * System.Math.PI * Complex.ImaginaryOne * t);
        }
        return res;
    }

    public Complex[] ValuesAt(float t)
    {
        Complex[] res = new Complex[coefficients.Length];
        Complex last = Complex.Zero;
        for (int i = 0; i < coefficients.Length; ++i)
        {
            int freq = IndexToFrequency(i);
            last += coefficients[i] * Complex.Exp(freq * 2f * System.Math.PI * Complex.ImaginaryOne * t);
            res[i] = last;
        }
        return res;
    }

    public float GetStartRotation(int freq)
    {
        int ind = FrequencyToIndex(freq);
        return Mathf.Atan2((float)coefficients[ind].Imaginary, (float)coefficients[ind].Real);
    }

    public float GetMagnitude(int freq)
    {
        int ind = FrequencyToIndex(freq);
        return (float)coefficients[ind].Magnitude;
    }

    public static int IndexToFrequency(int ind)
    {
        return ind % 2 == 0 ? -ind/2 : (ind+1)/2;
    }

    public static int FrequencyToIndex(int freq)
    {
        return freq > 0 ? freq * 2 - 1 : -freq * 2;
    }

    public static FourierFunction Of(Complex[] vals, int numberOfVectors)
    {
        Complex[] coeffs = new Complex[numberOfVectors];
        for (int i = 0; i < numberOfVectors; ++i)
        {
            int freq = IndexToFrequency(i);
            coeffs[i] = CalculateCoefficient(vals, freq);
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
    static Complex CalculateCoefficient(Complex[] vals, int freq, double maxDist = 0.005)
    {
        Complex sum = Complex.Zero;
        int numberOfPoints = 0;
        int currentPoint = 0;
        for (int i = 0; i < vals.Length; ++i)
        {
            int extraPoints = (int)((vals[i] - vals[(i + 1) % vals.Length]).Magnitude / maxDist) + 1;
            numberOfPoints += extraPoints;
        }
        for (int i = 0; i < vals.Length; ++i)
        {
            int extraPoints = (int)((vals[i] - vals[(i + 1) % vals.Length]).Magnitude / maxDist) + 1;
            for (int j = 0; j < extraPoints; j++)
            {
                Complex val = (vals[i] * (extraPoints - j) + vals[(i + 1) % vals.Length] * j) / extraPoints;
                double t = (double)currentPoint / numberOfPoints;
                currentPoint++;
                sum += val * Complex.Exp(-freq * 2 * System.Math.PI * Complex.ImaginaryOne * t);
            }
        }
        sum /= numberOfPoints;
        return sum;
    }
}

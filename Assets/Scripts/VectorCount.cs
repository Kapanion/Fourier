using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VectorCount : SingletonBase<VectorCount>
{
    public int minAllowedAmount = 2;
    public int maxAllowedAmount = 800;

    public Text vectorAmountText;
    public AnimationCurve curve;

    public void VectorCountChanged(float value)
    {
        int amount = Mathf.Clamp((int)(value * curve.Evaluate(value / maxAllowedAmount)), minAllowedAmount, maxAllowedAmount);

        Brush.Instance.UpdateVectorAmm(amount);
        FourierDrawer.Instance.UpdateVectorAmount(amount);

        vectorAmountText.text = $"VECTOR AMOUNT: {amount}";
    }
}

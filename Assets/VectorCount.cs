using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorCount : SingletonBase<VectorCount>
{
    public int minAllowedAmount = 2;
    public int maxAllowedAmount = 800;

    public void VectorCountChanged(float value)
    {
        int amount = Mathf.Clamp((int)value, minAllowedAmount, maxAllowedAmount);

        Brush.Instance.UpdateVectorAmm(amount);
        FourierDrawer.Instance.UpdateVectorAmount(amount);
    }
}

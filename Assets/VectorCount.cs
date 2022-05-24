using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorCount : SingletonBase<VectorCount>
{
    public int minAllowedAmount = 1;
    public int maxAllowedAmount = 800;

    public void VectorCountChanged(float value)
    {
        Brush.Instance.UpdateVectorAmm(value);
        FourierDrawer.Instance.UpdateVectorAmount((int)value);
    }
}

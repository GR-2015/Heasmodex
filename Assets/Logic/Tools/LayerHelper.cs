using UnityEngine;
using System.Collections;

public static class LayerHelper
{
    public static bool IsLayerMaskLayer(int layer, LayerMask layerMask)
    {
        int value = (1 << layer);
        return (value & layerMask.value) > 0;
    }
}

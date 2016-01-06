using UnityEngine;
using System.Collections;

public static class LayerHelper
{
    public static bool IsLayerMaskLayer(int layer, LayerMask layerMask)
    {
        return ((layer << layer) & layerMask.value) == layerMask.value;
    }
}

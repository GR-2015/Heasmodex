using UnityEngine;
using System.Collections;

public class BaseView : MonoBehaviour
{
    protected virtual void Start() {}
    public virtual void LoadContent(MonoBehaviour Object) { }
}

using UnityEngine;
using System.Collections;

public class GridStreamer : MonoBehaviour
{
    private const string GizmoName = "Hdd_Icon.png";
    public Vector3 endlLine = Vector3.zero;
    public Vector3 direction = Vector3.zero;
    public int maxDistance = 100;
    private void Update()
    {
        if (Physics.Raycast(this.transform.position, direction, maxDistance))
            Debug.DrawLine(this.transform.position, endlLine, Color.red);
        else
            Debug.DrawLine(this.transform.position, endlLine, Color.green);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(this.transform.position, GizmoName, true);
    }
}

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Streamer : MonoBehaviour
{
    [SerializeField]
    private string mapSegmentName;

    [SerializeField]
    private GameObject mapSegmentObject = null;
    
    [SerializeField]
    private float distanceToPlayer;
    ResourceRequest resourceRequest;

    private void Awake()
    {
        this.name = mapSegmentName;
    }

    private void Start()
    {
        StreamingManager.Instance.RegisterStreamer(this);
    }

    private void Update()
    {
        foreach (var player in CharacterManager.Instance.Players)
        {
            distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        }

        if (distanceToPlayer <= StreamingManager.Instance.StreamingDistance & resourceRequest == null)
        {
            resourceRequest = Resources.LoadAsync(mapSegmentName, typeof(GameObject));
        }
        if (resourceRequest != null)
        {
            if (resourceRequest.isDone && mapSegmentObject == null)
            {
                mapSegmentObject = Instantiate(resourceRequest.asset) as GameObject;
                mapSegmentObject.transform.SetParent(this.transform);
            }
        }

        if (distanceToPlayer > StreamingManager.Instance.StreamingDistance & mapSegmentObject != null)
        {
            GameObject.Destroy(mapSegmentObject);
            resourceRequest = null; 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(this.transform.position, "Hdd_Icon.png", true);
    }
}

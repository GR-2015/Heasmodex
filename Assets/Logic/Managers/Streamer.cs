using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[ExecuteInEditMode]
class Streamer : MonoBehaviour
{
    #region GUI, Gizmos, Log

    private const string GizmoName = "Hdd_Icon.png";

    private const string startLoading = "Streaming started.";
    private const string loadingDone = "Streaming is done.";
    private const string unloading = "Unstreanibg object.";

    #endregion

    [SerializeField]
    private bool streamInEditor = true;

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
        if (Application.isPlaying == false & streamInEditor == true)
        {
            Stream();
        }

        if (Application.isPlaying == true)
        {
            CalculateDistanceToPlayer();
            StreamOnPlay();
        }
    }

    private void CalculateDistanceToPlayer()
    {
        foreach (var player in CharacterManager.Instance.Players)
        {
            distanceToPlayer =  Mathf.Round(Vector3.Distance(this.transform.position, player.transform.position));
        }
    }

    private void StreamOnPlay()
    {
        if (distanceToPlayer <= StreamingManager.Instance.StreamingDistance & resourceRequest == null)
        {
            if (StreamingManager.Instance.StreamingLoging == true)
            {
                Debug.Log(startLoading + ": " + this.mapSegmentName);
            }

            resourceRequest = Resources.LoadAsync(mapSegmentName, typeof(GameObject));
        }

        if (resourceRequest != null)
        {
            if (resourceRequest.isDone && mapSegmentObject == null)
            {
                if (StreamingManager.Instance.StreamingLoging == true)
                {
                    Debug.Log(loadingDone + ": " + this.mapSegmentName);
                }

                mapSegmentObject = Instantiate(resourceRequest.asset) as GameObject;
                mapSegmentObject.transform.SetParent(this.transform);
            }
        }

        if (distanceToPlayer > StreamingManager.Instance.StreamingDistance & mapSegmentObject != null)
        {
            if (StreamingManager.Instance.StreamingLoging == true)
            {
                Debug.Log(unloading + ": " + this.mapSegmentName);
            }

            GameObject.Destroy(mapSegmentObject);
            resourceRequest = null;
        }
    }

    private void Stream()
    {
        resourceRequest = Resources.LoadAsync(mapSegmentName, typeof(GameObject));

        if (resourceRequest != null)
        {
            if (resourceRequest.isDone && mapSegmentObject == null)
            {
                mapSegmentObject = Instantiate(resourceRequest.asset) as GameObject;
                mapSegmentObject.transform.SetParent(this.transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(this.transform.position, GizmoName, true);
    }
}

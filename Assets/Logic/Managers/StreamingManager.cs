using UnityEngine;
using System.Collections.Generic;

internal class StreamingManager: MonoBehaviour
{
    [SerializeField] private float streamingDistance = 15;
    [SerializeField]
    private readonly List<string> streamersNames = new List<string>();

    [SerializeField]
    private bool streamingLoging = false;

    public float StreamingDistance
    {
        get { return streamingDistance; }
    }

    public bool StreamingLoging
    {
        get { return streamingLoging; }
    }

    private readonly List<Streamer> streamersList = new List<Streamer>(); 

    public static StreamingManager Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterStreamer(Streamer newStreamer)
    {
        streamersList.Add(newStreamer);
        streamersNames.Add(newStreamer.name);
    }

    public void UnregisterStreamer(Streamer streamer)
    {
        streamersList.Remove(streamer);
        streamersNames.Remove(streamer.name);
    }
}
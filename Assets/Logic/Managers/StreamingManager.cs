using System.Collections.Generic;
using UnityEngine;

internal class StreamingManager : MonoBehaviour
{
    [SerializeField] private float streamingDistance = 15;
    [SerializeField] private readonly List<string> _streamersNames = new List<string>();
    [SerializeField] private bool streamingLoging = false;

    public float StreamingDistance { get { return streamingDistance; } }
    public bool StreamingLoging { get { return streamingLoging; } }

    private readonly List<Streamer> _streamersList = new List<Streamer>();

    public static StreamingManager Instance { get; private set;  }

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterStreamer(Streamer newStreamer)
    {
        _streamersList.Add(newStreamer);
        _streamersNames.Add(newStreamer.name);
    }

    public void UnregisterStreamer(Streamer streamer)
    {
        _streamersList.Remove(streamer);
        _streamersNames.Remove(streamer.name);
    }
}
using UnityEngine;
using System.Collections;

public class GridStreamingManager : MonoBehaviour
{
    public GridStreamingManager Instance { get; private set; }

    [SerializeField] private int height = 100;
    [SerializeField] private int widtch = 100;

    private void Awake()
    {
        Instance = this;
        GridInitialization();
    }

    private void GridInitialization()
    {
        GameObject newGridStreamer;
        Vector3 newPosition;
        GridStreamer newGridStreamerComponent;

        for (int i = 0; i < height; i++)
        {
            newGridStreamer = new GameObject();
            newGridStreamerComponent = newGridStreamer.AddComponent<GridStreamer>();

            newPosition = Vector3.zero;
            newPosition.y = i;

            newGridStreamerComponent.endlLine = newPosition;
            newGridStreamerComponent.endlLine.x = widtch;
            newGridStreamerComponent.maxDistance = widtch;
            newGridStreamerComponent.direction = Vector3.right;

            newGridStreamer.transform.position = newPosition;
        }

        for (int i = 0; i < widtch; i++)
        {
            newGridStreamer = new GameObject();
            newGridStreamerComponent = newGridStreamer.AddComponent<GridStreamer>();

            newPosition = Vector3.zero;
            newPosition.x = i;
            newPosition.y = height;

            newGridStreamerComponent.endlLine = newPosition;
            newGridStreamerComponent.endlLine.y = 0f;
            newGridStreamerComponent.maxDistance = height;
            newGridStreamerComponent.direction = Vector3.down;

            newGridStreamer.transform.position = newPosition;
        }
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}

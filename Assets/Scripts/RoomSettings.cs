using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RoomSettings : MonoBehaviour
{
    // Members
    [SerializeField]
    private LineRenderer _outlineRender;

    // Room Settings
    [SerializeField]
    [Range(1.0f, 10.0f)]
    private float _roomLength;
    [SerializeField]
    [Range(1.0f, 10.0f)]
    private float _roomHeight;
    
    // Variables
    private Vector3[] _outlineVertices;


    // Start is called before the first frame update
    void Start()
    {
        // Set Room Defaults;
        _roomHeight = 1;
        _roomLength = 1;

        // Set up outline renderer
        _outlineRender.positionCount = 4;
        _outlineRender.loop = true;
        _outlineRender.startColor = new Color(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        // Update Room Size
        transform.localScale = new Vector3(_roomLength, _roomHeight, 0);

        // Draw outline
        _outlineVertices = getOutlinePoints();
        _outlineRender.SetPositions(_outlineVertices);
    }

    private Vector3[] getOutlinePoints() {
        Vector3[] vertices = new Vector3[] {
            new Vector3(0.5f, 0.5f, -1.0f),
            new Vector3(0.5f, -0.5f, -1.0f),
            new Vector3(-0.5f, -0.5f, -1.0f),
            new Vector3(-0.5f, 0.5f, -1.0f)
        };

        for (int i = 0; i < vertices.Length; i++) {
            vertices[i].x *= _roomLength;
            vertices[i].y *= _roomHeight;
        }

        return vertices;
    }
}

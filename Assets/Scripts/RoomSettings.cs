using System;
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
    [SerializeField]
    private MeshFilter _meshFilter;
    [SerializeField]
    private EdgeCollider2D _collider;
    [SerializeField]
    private Camera _camera;
    
    

    // Room Settings
    [SerializeField]
    [Range(1.0f, 10.0f)]
    private float _roomLength;
    [SerializeField]
    [Range(1.0f, 10.0f)]
    private float _roomHeight;
    
    // Variables
    private Vector2[] _colliderVertices;
    private Vector3[] _outlineVertices;
    private int[] _roomTriangles;
    private Mesh _mesh;


    // Start is called before the first frame update
    void Start()
    {
        // Set Room Defaults;
        _roomHeight = 3;
        _roomLength = 5;

        // Set up room Renderer
        _mesh = new Mesh();
        _meshFilter.mesh = _mesh;

        // Set up outline renderer
        _outlineRender.positionCount = 4;
        _outlineRender.loop = true;
        _outlineRender.startColor = new Color(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        // Update room graphics
        UpdateCamera();
        _outlineVertices = GetOutlinePoints();
        _roomTriangles = GetRoomTriangles();
        _mesh.Clear();
        _mesh.vertices = _outlineVertices;
        _mesh.triangles = _roomTriangles;

        // Draw outline
        _outlineRender.SetPositions(_outlineVertices);
        _colliderVertices = GetColliderVertices(_outlineVertices);
        _collider.points = _colliderVertices;
    }

    private void UpdateCamera() {
        _camera.transform.position = new Vector3(_roomLength / 2, _roomHeight / 2, -10);
    }

    private Vector3[] GetOutlinePoints() {
        Vector3[] vertices = new Vector3[] {
            new Vector3(0, 0, -1.0f),
            new Vector3(0, 1, -1.0f),
            new Vector3(1, 1, -1.0f),
            new Vector3(1, 0, -1.0f)
        };

        for (int i = 0; i < vertices.Length; i++) {
            vertices[i].x *= _roomLength;
            vertices[i].y *= _roomHeight;
        }

        return vertices;
    }

    private Vector2[] GetColliderVertices(Vector3[] outlineVertices) {
        Vector2[] vertices = new Vector2[4];
        for (int i = 0; i < outlineVertices.Length; i++) {
            vertices[i] = outlineVertices[i];
        }
        return vertices;
    }

    private int[] GetRoomTriangles()
    {
        return new int[] {
            0, 1, 3, 
            2, 1, 3
        };
    }

    public float[] GetDimensions() {
        return new float[] {_roomLength, _roomHeight};
    }

    public float GetColliderZ() {
        return 0;
    }
}

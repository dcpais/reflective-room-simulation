using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserCast : MonoBehaviour
{
    // Members
    [SerializeField]
    private LineRenderer _laserRenderer;
    [SerializeField]
    private MeshFilter _meshFilter;
    [SerializeField]
    private RoomSettings _room;


    // Laser Settings
    [SerializeField]
    private float _x;
    [SerializeField]
    private float _y;
    [SerializeField]
    private Vector3 _direction;
    [SerializeField]
    [Range(0f, 200f)]
    private float _distance;

    // other vars
    private Vector3[] _laserPoints;
    private float _roomLength;
    private float _roomHeight;


    // Start is called before the first frame update
    void Start()
    {
        _x = 1f;
        _y = 1f;
        transform.position = new Vector3(_x, _y, 0);
        
        _direction = new Vector3(1, 0, 0);
        _distance = 2;
        _laserRenderer.loop = false;

        float[] dims = _room.GetDimensions();
        _roomLength = dims[0];
        _roomHeight = dims[1];

        // Initialize the circle render stuff
        InitCircle();
    }

    // Update is called once per frame
    void Update()
    {
        // Redraw laser and circle
        UpdateTransform();
        DrawLaser();
        DrawCircle();
    }

    private void UpdateTransform() {
        transform.position = new Vector3(_x, _y, 0);
    }

    private void DrawLaser() {
        _laserPoints = Raycast();
        _laserRenderer.positionCount = _laserPoints.Count();
        _laserRenderer.SetPositions(_laserPoints);
    }

    private Vector3[] Raycast() {
        // Intersections to return to the renderer
        List<Vector3> intersections = new List<Vector3> {
            new Vector3(_x, _y, _room.GetColliderZ())
        };

        // Wall positions and lines
        float miles = _distance;
        Vector3 dir = _direction;
        while (miles > 0) {
            // Using unity ray intersection functions:
            Vector3 origin = intersections[intersections.Count() - 1];
            Ray ray = new Ray(dir, origin);
            
            RaycastHit2D res = Physics2D.GetRayIntersection(ray, miles);
            print(res.distance);

            break;
            // miles -= Vector3.Distance(previous, next);
            // intersections.Add(next);
        }
        intersections.Add(intersections[intersections.Count() - 1] + (Vector3) dir * miles);
        return intersections.ToArray();
    }



    // CIRCLE RENDER CODE
    [Serializable]
    public class CircleSettings 
    {
        public Mesh _mesh;
        [Range(0.125f, 1f)]
        public float _radius;
        public int _splits;
        public Vector3[] _vertices;
        public int[] _triangles;
    } public CircleSettings circle;


    private void InitCircle() {
        // Setup mesh for render
        circle._mesh = new Mesh();
        _meshFilter.mesh = circle._mesh;
        circle._radius = 0.125f;
        circle._splits = 20;
        DrawCircle();
    }

    private void DrawCircle() {
        circle._vertices = GenerateCirclePoints(circle._radius, circle._splits);
        circle._triangles = GenerateCircleTriangles(circle._vertices);
        circle._mesh.Clear();
        circle._mesh.vertices = circle._vertices;
        circle._mesh.triangles = circle._triangles;
    }

    private Vector3[] GenerateCirclePoints(float radius, int splits) {

        double stepsize = 2 * Math.PI / splits;
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i <= splits; i++) {
            points.Add(
                new Vector3(
                    radius * (float) Math.Cos(i * stepsize), 
                    radius * (float) Math.Sin(i * stepsize), 
                    0
                )
            );
        }
        return points.ToArray();
    }

    private int[] GenerateCircleTriangles(Vector3[] vertices) {

        List<int> triangles = new List<int>();
        for (int i = 0; i < vertices.Length - 2; i++) {
            triangles.Add(0);
            triangles.Add(i + 2);
            triangles.Add(i + 1);
        }
        return triangles.ToArray();
    }
    
}

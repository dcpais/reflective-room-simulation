using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCast : MonoBehaviour
{
    // Members
    [SerializeField]
    private LineRenderer _laserRenderer;
    [SerializeField]
    private MeshFilter _meshFilter;
    [SerializeField]
    private GameObject _room;
    [SerializeField]





    // Start is called before the first frame update
    void Start()
    {


        // Draw the circle
        InitCircle();
    }

    // Update is called once per frame
    void Update()
    {
        

        // Redraw origin of laser circle
        DrawCircle();
    }


    // CIRCLE RENDER CODE
    [System.Serializable]
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
        Array.ForEach(circle._vertices, x => print(x.ToString()));
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

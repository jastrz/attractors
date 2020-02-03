using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Plotter
{
    private Mesh mesh;
    private int[] indices;
    private Vector2[] uv;
    public bool IsShowingAnimation { get; private set; } = false;

    [SerializeField] private int maxLength = 1000000;

    public void Initialize(Mesh _mesh)
    {
        mesh = _mesh;
    }

    public void Show(Vector3[] points)
    {
        Plot(ref points);
    }

    private void Plot(ref Vector3[] points)
    {
        if(points.Length > maxLength)
        {
            Debug.LogError("Too many points sent to plotter.");
            return;
        }

        UpdateMesh(ref points);
    }
    
    private void UpdateMesh(ref Vector3[] points)
    {
        SetMeshData(points.Length, out indices, out uv);
        mesh.Clear();
        mesh.vertices = points;
        mesh.uv = this.uv;
        mesh.SetIndices(indices, MeshTopology.LineStrip, 0);
    }

    private void SetMeshData(int numPoints, out int[] indices, out Vector2[] uv)
    {
        indices = new int[numPoints];
        uv = new Vector2[numPoints];
        for (int i = 0; i < numPoints; i++)
        {
            indices[i] = i;
            uv[i] = new Vector2((float)i / (float)numPoints, (float)i / (float)numPoints);
        }
    }
}

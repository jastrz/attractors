using System.Linq;
using UnityEngine;

[System.Serializable]
public class Plotter : MonoBehaviour
{
    [SerializeField] private int maxLength = 1000000;
    [SerializeField] private float maxDistance = 100000;
    [SerializeField] private GameObject plot;
    private Mesh mesh;
    private int[] indices;
    private Vector2[] uv;

    public void Start()
    {
        mesh = plot.GetComponent<MeshFilter>().mesh;
    }

    public void Show(Vector3[] points)
    {
        Plot(ref points);
    }

    private void Plot(ref Vector3[] points)
    {
        if(points.Length > maxLength)
        {
            Debug.LogWarning("Too many points sent to plotter.");
            return;
        }

        if(points.Any(point => point.magnitude > maxDistance))
        {
            Debug.LogWarning("Plot's too big or couldn't find solution for current parameters.");
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

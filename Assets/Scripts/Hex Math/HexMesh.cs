using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
	private Mesh mesh;
	private List<Vector3> vertices;
	private List<int> triangles;
	[HideInInspector] public MeshCollider mc;
	public List<Color> colors;

	void Awake()
	{
		mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;
		mesh.name = "Hex Mesh";
		vertices = new List<Vector3>();
		colors = new List<Color>();
		triangles = new List<int>();
		mc = gameObject.AddComponent<MeshCollider>();
	}

	public void Triangulate(Tile[] tiles)
	{
		mesh.Clear();
		vertices.Clear();
		colors.Clear();
		triangles.Clear();
		for (int i = 0; i < tiles.Length; i++)
		{
			Triangulate(tiles[i]);
		}
		mesh.vertices = vertices.ToArray();
		mesh.colors = colors.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.RecalculateNormals();
		mc.sharedMesh = mesh;
	}

	private void Triangulate(Tile tile)
	{
		Vector3 center = tile.transform.localPosition;
        for (int i = 0; i < 6; i++)
		{  
			AddTriangle(center, center + MathH.corners[i], center + MathH.corners[i + 1]);
			ColorTriangle(tile.color);
        }
	}

	private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
	{
		int vertexIndex = vertices.Count;
		vertices.Add(v1);
		vertices.Add(v2);
		vertices.Add(v3);
		triangles.Add(vertexIndex);
		triangles.Add(vertexIndex + 1);
		triangles.Add(vertexIndex + 2);
	}
	private void ColorTriangle(Color color)
	{
		colors.Add(color);
		colors.Add(color);
		colors.Add(color);
	}

}

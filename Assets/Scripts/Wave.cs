using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Wave : MonoBehaviour {

    public WaveHeight WaveHeight;
    [Range(0, 30)]
    public float Length = 10;

    public const int NUM_POINTS = 1000;

    Mesh mesh;
    readonly float[] heights = new float[NUM_POINTS];
    readonly Vector3[] vertices = new Vector3[NUM_POINTS * 2];
    readonly int[] triangles = new int[(NUM_POINTS - 1) * 2 * 3];

    void Start() {
        // setup mesh
        {
            mesh = new Mesh {
                vertices = vertices,
                triangles = triangles,
            };
            GetComponent<MeshFilter>().mesh = mesh;
        }
    }

    void Update() {
        var step = Length / heights.Length;
        // update heights
        {
            for (int i = 0; i < heights.Length; i++) {
                heights[i] = WaveHeight.GetHeight(i * step);
            }
        }
        // heights to verts
        {
            // even verts are at the top
            for (int i = 0; i < vertices.Length; i += 2) {
                vertices[i].x = (i / 2) * step;
                vertices[i].y = heights[i / 2];
            }
            // odd verts at the bottom
            for (int i = 1; i < vertices.Length; i += 2) {
                vertices[i].x = (i / 2) * step;
                vertices[i].y = 0;
            }
            mesh.vertices = vertices;
        }
        // triangles
        {
            for (int i = 0, offset = 0; i < triangles.Length; i += 6, offset += 2) {
                triangles[i + 0] = 0 + offset;
                triangles[i + 1] = 3 + offset;
                triangles[i + 2] = 1 + offset;
                triangles[i + 3] = 0 + offset;
                triangles[i + 4] = 2 + offset;
                triangles[i + 5] = 3 + offset;
            }
            mesh.triangles = triangles;
        }
    }
}
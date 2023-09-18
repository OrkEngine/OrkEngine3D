using System.Collections.Generic;
using OrkEngine3D.Mathematics;

namespace OrkEngine3D.Graphics.MeshData;

/// <summary>
/// VoxelData is a class that contains all the data nesseccary to generate a voxel/cube mesh
/// </summary>
public static class VoxelData
{

    public static readonly Vector3[] voxelVerts = new Vector3[8] {

        new Vector3(0.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 1.0f, 0.0f),
        new Vector3(0.0f, 1.0f, 0.0f),
        new Vector3(0.0f, 0.0f, 1.0f),
        new Vector3(1.0f, 0.0f, 1.0f),
        new Vector3(1.0f, 1.0f, 1.0f),
        new Vector3(0.0f, 1.0f, 1.0f),

    };

    public static readonly int[,] voxelTris = new int[6, 6] {

        {0, 3, 1, 1, 3, 2}, // Back Face
        {5, 6, 4, 4, 6, 7}, // Front Face
        {3, 7, 2, 2, 7, 6}, // Top Face
        {1, 5, 0, 0, 5, 4}, // Bottom Face
        {4, 7, 0, 0, 7, 3}, // Left Face
        {1, 2, 5, 5, 2, 6} // Right Face

    };

    public static readonly Vector2[] voxelUvs = new Vector2[6] {

        new Vector2 (0.0f, 0.0f),
        new Vector2 (0.0f, 1.0f),
        new Vector2 (1.0f, 0.0f),
        new Vector2 (1.0f, 0.0f),
        new Vector2 (0.0f, 1.0f),
        new Vector2 (1.0f, 1.0f)

    };

    public static readonly Vector3[] faceChecks = new Vector3[6] {

        new Vector3(0.0f, 0.0f, -1.0f),
        new Vector3(0.0f, 0.0f, 1.0f),
        new Vector3(0.0f, 1.0f, 0.0f),
        new Vector3(0.0f, -1.0f, 0.0f),
        new Vector3(-1.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 0.0f, 0.0f)

	    };

    public static MeshInformation GenerateVoxelInformation(){
        uint vertexIndex = 0;
        List<Vector3> vertices = new List<Vector3> ();
        List<uint> triangles = new List<uint> ();
        List<Vector2> uvs = new List<Vector2> ();
        List<Vector3> normals = new List<Vector3>();

        for (int p = 0; p < 6; p++) { 
            for (int i = 0; i < 6; i++) {

                int triangleIndex = VoxelData.voxelTris [p, i];
                vertices.Add (VoxelData.voxelVerts [triangleIndex] - (Vector3.One * 0.5f));
                triangles.Add (vertexIndex);
                normals.Add(faceChecks[p]);

                uvs.Add (VoxelData.voxelUvs [i]);

                vertexIndex++;

            }
        }

        return new MeshInformation(vertices.ToArray(), new Color4[0], uvs.ToArray(), normals.ToArray(), triangles.ToArray(), new int[0]);
    }


}
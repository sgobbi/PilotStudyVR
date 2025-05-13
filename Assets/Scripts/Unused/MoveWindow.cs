using UnityEngine;
using UnityEngine.ProBuilder;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class MoveWindow : MonoBehaviour
{
    public ProBuilderMesh proBuilderMesh; // Reference to the ProBuilder mesh
    public int faceIndex = 0; // Index of the face to move
    public Vector3 moveDirection = new Vector3(0, 0, 1); // Direction to move the face
    public KeyCode moveKey = KeyCode.A; // Key to press for moving the face
    public float moveSpeed = 5f; // Speed at which the face moves

    void Update()
    {
        if (Input.GetKey(moveKey))
        {
            MoveFaceVertices();
        }
    }

    void MoveFaceVertices()
    {
        Face face = proBuilderMesh.faces[faceIndex];
        ReadOnlyCollection<int> indices = face.distinctIndexes;
        IList<Vector3> vertices = proBuilderMesh.positions;  

        for (int i = 0; i < indices.Count; i++)
        {
            vertices[indices[i]] += moveDirection * moveSpeed * Time.deltaTime;
        }

        proBuilderMesh.positions = new List<Vector3>(vertices);
        proBuilderMesh.ToMesh();
        proBuilderMesh.Refresh();
    }
}

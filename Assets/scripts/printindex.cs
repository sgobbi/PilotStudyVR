using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

public class printindex : MonoBehaviour
{
    public ProBuilderMesh proBuilderMesh;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Press 'P' to print the face index
        {
            PrintSelectedFaceIndex();
        }
    }

    void PrintSelectedFaceIndex()
    {
        Face selectedFace = proBuilderMesh.faces[0]; // Assuming the first face is selected for simplicity
        for (int i = 0; i < proBuilderMesh.faces.Count; i++)
        {
            if (proBuilderMesh.faces[i] == selectedFace)
            {
                Debug.Log("Selected Face Index: " + i);
                break;
            }
        }
    }
}
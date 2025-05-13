using UnityEngine;

public class moveWall : MonoBehaviour
{
   public float moveSpeed = 5f; // Speed at which the object moves
   public KeyCode moveKeyForward = KeyCode.Q;
   public KeyCode moveKeyBackward = KeyCode.W;
   public Vector3 moveDirection = new Vector3(0, 0, 1);

    void Update()
    {
        if (Input.GetKey(moveKeyForward))
        {
            MoveForward();
        }

        if (Input.GetKey(moveKeyBackward))
        {
            MoveBackward();
        }
    }

    void MoveForward()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    void MoveBackward()
    {
        transform.position -= moveDirection * moveSpeed * Time.deltaTime;
    }
}

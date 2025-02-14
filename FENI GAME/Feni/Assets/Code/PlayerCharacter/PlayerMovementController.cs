using UnityEngine;


public class PlayerMovementController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    void start()
    {

        // Set the player's position to the starting position
        //transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        // Player movement mit Tastatur
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        

        Vector2 direction = new Vector2(xInput, yInput).normalized;
        rb.linearVelocity = direction * speed;
    }


}

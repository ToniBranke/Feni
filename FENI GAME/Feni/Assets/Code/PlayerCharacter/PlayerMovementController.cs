using UnityEngine;


public class PlayerMovementController : MonoBehaviour
{
    public float accelleration; 
    public Rigidbody2D rb;
    public BoxCollider2D CheckGroundCollider;
    public LayerMask groundMask;

    [Range (0f, 1f)]
    public float groundDecay;
    public float groundSpeed;
    public float jumpSpeed;
    public bool grounded; 

    float xInput;
    float yInput;
    

    void start()
    {

        // Set the player's position to the starting position
        //transform.position = new Vector3(0, 0, 0);
    }

    //Update wird mit jedem Frame gepulled deshalb soll man hier eher sachen rein packen die nicht zu krank viel Leistung ziehen also es muss nicht 60x pro sekunde überprüft werden, ob man zum Beispiel Friction hat
    void Update()
    {
        // Player movement mit Tastatur
        getInput();
        HandleJump();

        //Bessere Steuerung überprüft wenn eine Bewegung auf der jeweiligen Achse durchgeführt wird und 
        // dann macht packt der den Multiplikator von groundSpeed drauf sodass der Charakter sich tatsächlich bewegt
       
        //Normale Steuerung (falls alle stränge reißen kann man die benutzen)
        //Vector2 direction = new Vector2(xInput, yInput).normalized;
        //rb.linearVelocity = direction * groundSpeed;
    }
    void getInput()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    //Tickrate mit der von Online Game zu vergleichen
    void FixedUpdate()
    {
        CheckGround();
        ApplyFriction();
        // legt fest wie viel der Charakter abgebremst wird wenn er auf dem Boden ist (kann bei Eis Plattformen oder so nützlich sein)
    }

    void moveWithInput()
    {
       
        // Bessere Steuerung überprüft wenn eine Bewegung auf der jeweiligen Achse durchgeführt wird und 
        // dann macht packt der den Multiplikator von groundSpeed drauf sodass der Charakter sich tatsächlich bewegt
    if (Mathf.Abs(xInput)> 0 ) 
        {
            float increment = xInput * accelleration;
            float newSpeed = Mathf.Clamp(rb.linearVelocity.x + increment, -groundSpeed, groundSpeed);
            rb.linearVelocity = new Vector2(newSpeed, rb.linearVelocity.y);
            
          //  float direction = Mathf.Sign(xInput);
          //  transform.localscale = new Vector3(direction, 1, 1);
        }

       /* if(Mathf.Abs(yInput) > 0 && grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, yInput * groundSpeed);
        }
        */

        
    }

    void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        }
    }

    void ApplyFriction()
    {
         // legt fest wie viel der Charakter abgebremst wird wenn er auf dem Boden ist (kann bei Eis Plattformen oder so nützlich sein)
        if(grounded && xInput == 0 && rb.linearVelocity.y <= 0)
        {
            rb.linearVelocity *= groundDecay;
        }
    }

// überprüft ob der Spieler auf dem Boden ist
    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(CheckGroundCollider.bounds.min, CheckGroundCollider.bounds.max, groundMask).Length > 0;
    }
}

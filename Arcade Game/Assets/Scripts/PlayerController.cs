using UnityEngine;

public class PlayerController :MonoBehaviour
{
    public float verticalInput;
    public float horizontalInput;
    public float speed;
    public Rigidbody2D playerRb;
    private float rotation;
    private float input;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if(verticalInput != 0)
        {
            direction = Vector2.up;
            input = verticalInput;

            if(verticalInput < 0)
            {
                rotation = 180;
            }

            else if(verticalInput > 0)
            {
                rotation = 0;
            }
        }    

        else if(horizontalInput != 0)
        {
            direction = Vector2.right;
            input = horizontalInput;

            if(horizontalInput < 0)
            {
                rotation = 90;
            }

            else if(horizontalInput > 0)
            {
                rotation = 270;
            }
        }

        playerRb.MoveRotation(rotation);
        playerRb.MovePosition(playerRb.position + (input * direction * Time.deltaTime) * speed);

        input = 0;
        horizontalInput = 0;
        verticalInput = 0;
    }
}

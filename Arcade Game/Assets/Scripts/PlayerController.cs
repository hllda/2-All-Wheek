using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController:MonoBehaviour
{
    public Rigidbody2D playerRb;

    public float verticalInput;
    public float horizontalInput;
    public float speed;
    private float rotation;
    private float input;
    private Vector2 direction;
	private GameObject player;

	public GameManager gameManager;
    // Bounds of the screen
    readonly private float xBound = 8.6f;
    readonly private float yBound = 4.7f;
	public int lives;
	public GameObject spawn;

    // Start is called before the first frame update
    void Start()
    {		
		spawn = GameObject.Find("Spawn");
		playerRb = GetComponent<Rigidbody2D>();
		player = GameObject.Find("Player");
		player.transform.position = new Vector2(spawn.transform.position.x, spawn.transform.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
	
		if(gameManager.gameIsActive == true)
		{
			lives = gameManager.lives;
			MovePlayer();
			ConstrainPosition();
		}
    }

	// Moves the player based on input
	void MovePlayer()
	{
		// Get input horizontally and vertically
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        // Check if player is moving vertically
        if(verticalInput != 0)
        {
            direction = Vector2.up;
            input = verticalInput;

            if(verticalInput < 0)
            {
                // Rotate to south
                rotation = 180;
            }

            else if(verticalInput > 0)
            {
                // Rotate to north
                rotation = 0;
            }
        }

        // Check if player is moving horizontally
        else if(horizontalInput != 0)
        {
            direction = Vector2.right;
            input = horizontalInput;


            if(horizontalInput < 0)
            {
                // Rotate to west
                rotation = 90;
            }

            else if(horizontalInput > 0)
            {
                // Rotate to east
                rotation = 270;
            }
        }

        // Rotate Rigidbody in direction of movement
        playerRb.MoveRotation(rotation);

        // Move Rigidbody
        playerRb.MovePosition(playerRb.position + (input * direction * Time.deltaTime) * speed);

        // Stop movement
        input = 0;
	}
	
	// Limits where the player can go
	void ConstrainPosition()
	{
		// Check x upper bound
        if(transform.position.x > xBound)
        {
            transform.position = new Vector2(xBound, transform.position.y);
        }

        // Check x lower bound
        else if(transform.position.x < -xBound)
        {
            transform.position = new Vector2(-xBound, transform.position.y);
        }

        // Check y upper bound
        if(transform.position.y > yBound)
        {
            transform.position = new Vector2(transform.position.x, yBound);
        }

        // Check y lower bound
        else if(transform.position.y < -yBound)
        {
            transform.position = new Vector2(transform.position.x, -yBound);
        }
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag("Enemy"))
		{
			player.SetActive(false);
			gameManager.lives--;
		}

		if(collision.gameObject.CompareTag("Prints"))
		{
			collision.gameObject.SetActive(false);
			gameManager.score += 10;
		}

		if(collision.gameObject.CompareTag("Finish"))
		{
			gameManager.gameIsActive = false;
		}
	}
}
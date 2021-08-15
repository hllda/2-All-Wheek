using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController:MonoBehaviour
{
	// Reference to other scripts
	public GameManager gameManager;

	// Player
	public GameObject player;
	public Rigidbody2D playerRb;
	private float speed = 4;
	public int lives = -1;
	public float energy = 0;
	public Slider energyMeter;

	// Spawn
	public GameObject spawn;
	public Vector2 spawnPosition;

	// Inputs
	private float input;
	public float verticalInput;
	public float horizontalInput;
	public float rotation;
	public float previousRotation;
	private Vector2 direction;
	public Vector2 playerRoundedPosition;
	public bool movingVertically;
	public bool movingHorizontally;

	// Finish
	readonly private float xFinish = 16f;
	readonly private float yFinish = 10f;

	// Start is called before the first frame update
	void Start()
	{
		//player = GameObject.Find("Player");
		player = GameObject.FindGameObjectWithTag("Player");
		playerRb = GetComponent<Rigidbody2D>();
		spawnPosition = spawn.transform.position;
		player.transform.position = spawnPosition;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if(gameManager.gameIsActive == true)
		{
			FinishPosition();
			Energy();
			PlayerMovement();
		}
	}

	// Moves the player based on input
	void PlayerMovement()
	{
		// Get input horizontally and vertically
		verticalInput = Input.GetAxis("Vertical");
		horizontalInput = Input.GetAxis("Horizontal");


		// Check if player is moving vertically
		if(verticalInput != 0 && horizontalInput == 0)
		{
			// Sets the movement to vertical
			direction = Vector2.up;

			// Saves previous rotation
			previousRotation = rotation;

			// Sets direction to South & that Y input is negative
			if(verticalInput < 0)
			{
				input = -1;
				// Rotate to south
				rotation = 180;
			}

			// Sets direction to North & that Y input is positive
			else if(verticalInput > 0)
			{
				input = 1;
				// Rotate to north
				rotation = 0;
			}

			horizontalInput = 0;

			// Drains energy
			if(energy > 0)
			{
				energy -= 0.5f * Time.deltaTime;
			}

			// Making sure the player stays on the grid
			if(previousRotation == 90)
			{
				playerRoundedPosition = new Vector2(Convert.ToSingle(Math.Ceiling(playerRb.transform.position.x)), playerRb.position.y);
			}

			if(previousRotation == 270)
			{
				playerRoundedPosition = new Vector2(Convert.ToSingle(Math.Floor(playerRb.transform.position.x)), playerRb.position.y);
			}

			playerRoundedPosition = new Vector2(Convert.ToSingle(Math.Round(playerRb.transform.position.x)), playerRb.position.y);
		}

		// Check if player is moving horizontally
		else if(horizontalInput != 0 && verticalInput == 0)
		{
			// Sets the movement to horizontal
			direction = Vector2.right;
			
			// Saves previous rotation
			previousRotation = rotation;

			// Sets direction to West & that X input is negative
			if(horizontalInput < 0)
			{
				input = -1;
				// Rotate to west
				rotation = 90;
			}

			// Sets direction to East & that X input is positive
			else if(horizontalInput > 0)
			{
				input = 1;
				// Rotate to east
				rotation = 270;
			}
			verticalInput = 0;

			// Drains energy
			if(energy > 0)
			{
				energy -= 0.5f * Time.deltaTime;
			}

			// Making sure the player stays on the grid
			if(previousRotation == 0)
			{
				playerRoundedPosition = new Vector2(playerRb.position.x, Convert.ToSingle(Math.Ceiling(playerRb.transform.position.y)));;
			}

			else if(previousRotation == 180)
			{
				playerRoundedPosition = new Vector2(playerRb.position.x, Convert.ToSingle(Math.Floor(playerRb.transform.position.y)));
			}

			else
			{
				playerRoundedPosition = new Vector2(playerRb.position.x, Convert.ToSingle(Math.Round(playerRb.transform.position.y)));
			}
		}

		// No input is given
		else
		{
			input = 0;
		}

		if(previousRotation != rotation)
		{

		}

		// Rotates Rigidbody
		playerRb.MoveRotation(rotation);

		// Moves Rigidbody
		playerRb.MovePosition(playerRoundedPosition + (input * direction * speed * Time.deltaTime));
	}

	void FinishPosition()
	{
		// Check x upper bound
		if(transform.position.x > xFinish)
		{
			gameManager.LevelChanger();
		}

		// Check x lower bound
		else if(transform.position.x < -xFinish)
		{
			gameManager.LevelChanger();
		}

		// Check y upper bound
		if(transform.position.y > yFinish)
		{
			gameManager.LevelChanger();
		}

		// Check y lower bound
		else if(transform.position.y < -yFinish)
		{
			gameManager.LevelChanger();
		}
	}

	private void Energy()
	{
		if(energy <= 0)
		{
			speed = 1;
		}

		else if(energy > 0)
		{
			speed = 4;
		}

		if(energy > 0)
		{
			energy -= 1f * Time.deltaTime;
		}

		if(energy > 100)
		{
			energy = 100;
		}

		energyMeter.value = energy;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag("Enemy") && gameManager.coolDown == 0)
		{
			player.SetActive(false);
			lives--;
			gameManager.coolDown = 2;
			if(lives > 0 && gameManager.luna == true && gameManager.kanel == false)
			{
				energy = 100;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Prints"))
		{
			Debug.Log("Walked on tile.");
			SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();

			if(spriteRenderer.enabled == false)
			{
				gameManager.printsCounter++;

				// Add points
				gameManager.score += 1 * lives;
				Debug.Log("Point earned.");
			}
			spriteRenderer.enabled = true;
			Transform spriteTransform = collision.GetComponent<Transform>();
			spriteTransform.rotation = playerRb.transform.rotation;
		}

		if(collision.gameObject.CompareTag("Energy"))
		{
			Debug.Log("Energy refilled.");
			energy += 25;
			Destroy(collision.gameObject);
		}
	}
}
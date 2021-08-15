using System;
using UnityEngine;

public class Enemy:MonoBehaviour
{
	// Reference to other scripts
	public GameManager gameManager;

	// Enemies
	public bool walker;
	public bool runner;
	public bool stalker;
	public bool swooper;

	// Bounds of the screen
	readonly private float xBound = 14f;
	readonly private float yBound = 8f;

	//Enemy
	private Rigidbody2D enemyRb;
	private Vector2 enemyRoundedPosition;
	public Vector2 direction;
	public float speed;

	// Random
	public int randomSeed;
	private System.Random random = new System.Random();

	public int directionRandom;
	public int directionWall;

	public float countdownRandom;
	public float countdownWall;

	// Stalking
	private GameObject player;
	private float enemyX;
	private float enemyY;
	private float playerX;
	private float playerY;
	public float focusCooldown;
	public int focus;
	public bool focusX;
	public bool focusY;

	// Swooping
	public float swoopCooldown;
	public float swoopX;
	public float swoopY;
	public int directionSwoop;

	// Start is called before the first frame update
	void Start()
	{
		random = new System.Random(randomSeed);

		enemyRb = GetComponent<Rigidbody2D>();
		player = GameObject.Find("Player");

		swoopCooldown = random.Next(6, 16);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if(gameManager.gameIsActive == true && gameManager.isGameLost == false && gameManager.isGameWon == false)
		{
			// Which enemy behavior the enemy has
			if(walker == true)
			{
				Walking();
			}

			else if(runner == true)
			{
				Running();
			}

			else if(stalker == true)
			{
				Stalking();
			}

			else if(swooper == true)
			{
				Swooping();
			}

			// Counting down to the next random change of direction
			if(countdownRandom != 0)
			{
				if(countdownRandom < 0)
				{
					countdownRandom = 0;
				}

				else if(countdownRandom > 0)
				{
					countdownRandom -= 1 * Time.deltaTime;
				}
			}

			// Counting down so the next change of direction due to a wall only happens once
			if(countdownWall != 0)
			{
				if(countdownWall < 0)
				{
					countdownWall = 0;
				}

				else if(countdownWall > 0)
				{
					countdownWall -= 1 * Time.deltaTime;
				}
			}

			// Countdown for the stalking to not change direction too fast
			if(focusCooldown != 0)
			{
				if(focusCooldown < 0)
				{
					focusCooldown = 0;
				}

				else if(focusCooldown > 0)
				{
					focusCooldown -= 1 * Time.deltaTime;
				}
			}

			// Counting down to the next attack by the swooper
			if(swoopCooldown != 0)
			{
				if(swoopCooldown < 0)
				{
					swoopCooldown = 0;
				}

				else if(swoopCooldown > 0)
				{
					swoopCooldown -= 1 * Time.deltaTime;
				}
			}
		}
	}

	// Keeps the enemy inside of the map
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

	// Walker behavior
	void Walking()
	{
		// Makes the enemy randomly change direction
		RandomlyChangeDirection();

		if(direction == Vector2.up || direction == Vector2.down)
		{
			enemyRoundedPosition = new Vector2(Convert.ToSingle(Math.Round(enemyRb.position.x)), enemyRb.position.y);
		}

		else if(direction == Vector2.left || direction == Vector2.right)
		{
			enemyRoundedPosition = new Vector2(enemyRb.position.x, Convert.ToSingle(Math.Round(enemyRb.position.y)));
		}

		enemyRb.MovePosition(enemyRoundedPosition + (direction * speed * Time.deltaTime));

		ConstrainPosition();
	}

	// Runner behavior
	void Running()
	{
		// Makes the enemy randomly change direction	
		RandomlyChangeDirection();

		if(direction == Vector2.up || direction == Vector2.down)
		{
			enemyRoundedPosition = new Vector2(Convert.ToSingle(Math.Round(enemyRb.position.x)), enemyRb.position.y);
		}

		else if(direction == Vector2.left || direction == Vector2.right)
		{
			enemyRoundedPosition = new Vector2(enemyRb.position.x, Convert.ToSingle(Math.Round(enemyRb.position.y)));
		}

		enemyRb.MovePosition(enemyRoundedPosition + (direction * speed * Time.deltaTime));

		ConstrainPosition();
	}

	// Stalker behavior
	void Stalking()
	{
		FocusChangeDirection();

		if(direction == Vector2.up || direction == Vector2.down)
		{
			enemyRoundedPosition = new Vector2(Convert.ToSingle(Math.Round(enemyRb.position.x)), enemyRb.position.y);
		}

		else if(direction == Vector2.left || direction == Vector2.right)
		{
			enemyRoundedPosition = new Vector2(enemyRb.position.x, Convert.ToSingle(Math.Round(enemyRb.position.y)));
		}

		enemyRb.MovePosition(enemyRoundedPosition + (direction * speed * Time.deltaTime));

		ConstrainPosition();
	}

	// Swooper behavior
	void Swooping()
	{
		directionSwoop = random.Next(0, 4);

		if(swoopCooldown == 0)
		{
			playerX = player.transform.position.x;
			playerY = player.transform.position.y;

			// Fly in from bottom
			if(directionSwoop == 0)
			{
				direction = Vector2.up;
				swoopX = playerX;
				swoopY = -10;
				enemyRb.rotation = 0;
			}

			// Fly in from top
			else if(directionSwoop == 1)
			{
				direction = Vector2.down;
				swoopX = playerX;
				swoopY = 10;
				enemyRb.rotation = 180;
			}

			// Fly in from right
			else if(directionSwoop == 2)
			{
				direction = Vector2.left;
				swoopX = 18;
				swoopY = playerY;
				enemyRb.rotation = 90;
			}

			// Fly in from left
			else if(directionSwoop == 3)
			{
				direction = Vector2.right;
				swoopX = -18;
				swoopY = playerY;
				enemyRb.rotation = 270;
			}

			enemyRb.position = new Vector2(swoopX, swoopY);
			swoopCooldown = random.Next(10, 20);
		}

		if(enemyRb.position.x < 20 && enemyRb.position.x > -20 && enemyRb.position.y < 12 && enemyRb.position.y > -12)
		enemyRb.MovePosition(enemyRb.position + (direction * speed * Time.deltaTime));
	}

	// Changes the direction on collision with walls
	void WallChangeDirection()
	{
		if(countdownWall == 0)
		{
			directionWall = random.Next(0, 3);

			// If the enemy is moving along the y-axis
			if(direction == Vector2.up || direction == Vector2.down)
			{
				if(direction == Vector2.up && directionWall == 0)
					direction = Vector2.down;

				else if(direction == Vector2.down && directionWall == 0)
					direction = Vector2.up;

				else if(directionWall == 1)
					direction = Vector2.left;

				else if(directionWall == 2)
					direction = Vector2.right;
			}

			// If the enemy is moving along the x-axis
			else if(direction == Vector2.left || direction == Vector2.right)
			{
				if(directionWall == 0)
					direction = Vector2.up;

				else if(directionWall == 1)
					direction = Vector2.down;

				else if(direction == Vector2.left && directionWall == 2)
					direction = Vector2.left;

				else if(direction == Vector2.right && directionWall == 2)
					direction = Vector2.right;
			}

			countdownWall = 0.07f;
			countdownRandom = random.Next(1, 5);
		}
	}

	// Changes the direction randomly
	void RandomlyChangeDirection()
	{
		if(countdownRandom == 0 && countdownWall == 0)
		{
			Debug.Log("Random change of direction");
			directionRandom = random.Next(0, 4);

			if(directionRandom == 0)
				direction = Vector2.up;

			else if(directionRandom == 1)
				direction = Vector2.down;

			else if(directionRandom == 2)
				direction = Vector2.left;

			else if(directionRandom == 3)
				direction = Vector2.right;

			countdownRandom = random.Next(1, 5);
		}
	}

	void FocusChangeDirection()
	{
		if(focusCooldown == 0)
		{
			focusCooldown = 1;
			countdownRandom = random.Next(1, 5);

			enemyX = enemyRb.transform.position.x;
			enemyY = enemyRb.transform.position.y;

			playerX = player.transform.position.x;
			playerY = player.transform.position.y;

			if(Math.Round(playerX) - Math.Round(enemyX) < Math.Round(playerY) - Math.Round(enemyY))
			{
				focus = 0;
			}

			else if(Math.Round(playerX) - Math.Round(enemyX) > Math.Round(playerY) - Math.Round(enemyY))
			{
				focus = 1;
			}

			if(focus == 0)
			{
				focusX = true;
				focusY = false;
			}

			else if(focus == 1)
			{
				focusY = true;
				focusX = false;
			}

			if(focusX == true)
			{
				if(Math.Round(enemyX) > Math.Round(playerX))
				{
					direction = Vector2.left;
				}

				else if(Math.Round(enemyX) == Math.Round(playerX))
				{
					focusY = true;
					focusX = false;

					if(Math.Round(enemyY) > Math.Round(playerY))
					{
						direction = Vector2.down;
					}

					else if(Math.Round(enemyY) < Math.Round(playerY))
					{
						direction = Vector2.up;
					}

					focusCooldown = 0;
				}

				else if(Math.Round(enemyX) < Math.Round(playerX))
				{
					direction = Vector2.right;
				}
			}

			else if(focusY == true)
			{
				if(Math.Round(enemyY) > Math.Round(playerY))
				{
					direction = Vector2.down;
				}

				else if(Math.Round(enemyY) == Math.Round(playerY))
				{
					focusX = true;
					focusY = false;

					if(Math.Round(enemyY) > Math.Round(playerY))
					{
						direction = Vector2.down;
					}

					else if(Math.Round(enemyX) < Math.Round(playerX))
					{
						direction = Vector2.right;
					}

					focusCooldown = 0;
				}

				else if(Math.Round(enemyY) < Math.Round(playerY))
				{
					direction = Vector2.up;
				}
			}
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		// If the enemy hits a wall it will randomly change to another direction
		if(collision.collider.CompareTag("Wall") && stalker == false && swooper == false)
		{
			Debug.Log("Touched Wall");
			WallChangeDirection();
		}

		if(collision.collider.CompareTag("Player"))
		{
			FocusChangeDirection();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.CompareTag("Wall") && stalker == true)
		{
			speed = 0.25f;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if(collision.collider.CompareTag("Wall") && stalker == true)
		{
			speed = 1f;
		}
	}
}
using System;
using UnityEngine;

public class Enemy:MonoBehaviour
{
	// Reference to other scripts
	public GameManager gameManager;

	// Target
	private GameObject player;

	// Bounds of the screen
	readonly private float xBound = 15f;
	readonly private float yBound = 8f;

	//Enemy
	private Rigidbody2D enemyRb;
	public Vector2 enemyRoundedPosition;
	public Vector2 direction;
	public float speed;

	// Walker - Slower enemy & Faster enemy
	public bool enemyWalkerFast;
	public bool enemyWalkerSlow;

	public int randomSeed;
	readonly System.Random random = new System.Random();

	public int randomDirectionChange;
	public int randomDirection;
	public int randomDirectionWall;
	public float directionChangerCountdown;

	// Chaser - Very slow enemy that stalks the player and can go through walls, with a delay
	public bool enemyChaser;

	// Swooper - Super fast sweeps across screen in a line towards the player
	public bool enemySwooper;
	private float swooperCountdown;

	// Start is called before the first frame update
	void Start()
	{
		enemyRb = GetComponent<Rigidbody2D>();
		player = GameObject.Find("Player");

		randomDirection = random.Next(0, 4);

		if(randomDirection == 0)
			direction = Vector2.up;

		else if(randomDirection == 1)
			direction = Vector2.down;

		else if(randomDirection == 2)
			direction = Vector2.left;

		else if(randomDirection == 3)
			direction = Vector2.right;

		directionChangerCountdown = 3;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if(gameManager.gameIsActive == true)
		{
			if(enemyWalkerFast == true)
			{
				randomDirection = random.Next(0, 4);
				randomDirectionWall = random.Next(0, 3);

				if(gameManager.gameIsActive == true)
				{
					if(directionChangerCountdown > 0)
					{
						directionChangerCountdown -= 1 * Time.deltaTime;

						if(directionChangerCountdown < 0)
						{
							directionChangerCountdown = 0;
						}
					}

					if(directionChangerCountdown > 0)
					{
						directionChangerCountdown -= 1 * Time.deltaTime;

						if(directionChangerCountdown < 0)
						{
							directionChangerCountdown = 0;
						}
					}

					Running();
				}
				ConstrainPosition();
			}

			else if(enemyWalkerSlow == true)
			{
				randomDirection = random.Next(0, 4);
				randomDirectionWall = random.Next(0, 3);

				if(gameManager.gameIsActive == true)
				{
					if(directionChangerCountdown > 0)
					{
						directionChangerCountdown -= 1 * Time.deltaTime;

						if(directionChangerCountdown < 0)
						{
							directionChangerCountdown = 0;
						}
					}

					if(directionChangerCountdown > 0)
					{
						directionChangerCountdown -= 1 * Time.deltaTime;

						if(directionChangerCountdown < 0)
						{
							directionChangerCountdown = 0;
						}
					}

					Walking();
				}
				ConstrainPosition();
			}

			else if(enemyChaser == true)
			{
				if(gameManager.gameIsActive == true)
				{
					Stalking();
				}
				ConstrainPosition();
			}

			else if(enemySwooper == true)
			{
				if(gameManager.gameIsActive == true)
				{
					Swooping();
				}
			}
		}
	}

	// Movement
	void Walking()
	{
		// Makes the enemy randomly change direction
		if(enemyRb.position.x % 1 == 0 || enemyRb.position.y % 1 == 0)
		{
			if(directionChangerCountdown == 0)
			{
				directionChangerCountdown = 1;
				Debug.Log("Random change of direction");

				randomDirection = random.Next(0, 2);

				if(direction == Vector2.up || direction == Vector2.down)
				{
					if(direction == Vector2.up)
					{
						if(randomDirection == 0)
							direction = Vector2.down;

						else if(randomDirection == 1)
							direction = Vector2.left;

						else if(randomDirection == 2)
							direction = Vector2.right;
					}

					else if(direction == Vector2.down)
					{
						if(randomDirection == 0)
							direction = Vector2.up;

						else if(randomDirection == 1)
							direction = Vector2.left;

						else if(randomDirection == 2)
							direction = Vector2.right;
					}
					enemyRoundedPosition = new Vector2(Convert.ToSingle(Math.Round(enemyRb.position.x)), enemyRb.position.y);
				}

				// If the is moving along the x-axis
				else if(direction == Vector2.left || direction == Vector2.right)
				{
					if(direction == Vector2.left)
					{
						if(randomDirection == 0)
							direction = Vector2.up;

						else if(randomDirection == 1)
							direction = Vector2.down;

						else if(randomDirection == 2)
							direction = Vector2.right;
					}

					else if(direction == Vector2.right)
					{
						if(randomDirection == 0)
							direction = Vector2.up;

						else if(randomDirection == 1)
							direction = Vector2.down;

						else if(randomDirection == 2)
							direction = Vector2.left;
					}
					enemyRoundedPosition = new Vector2(enemyRb.position.x, Convert.ToSingle(Math.Round(enemyRb.position.y)));
				}

			}
		}

		if(direction == Vector2.up || direction == Vector2.down)
		{
			enemyRoundedPosition = new Vector2(Convert.ToSingle(Math.Round(enemyRb.position.x)), enemyRb.position.y);
		}

		else if(direction == Vector2.left || direction == Vector2.right)
		{
			enemyRoundedPosition = new Vector2(enemyRb.position.x, Convert.ToSingle(Math.Round(enemyRb.position.y)));
		}

		enemyRb.MovePosition(enemyRoundedPosition + (direction * speed * Time.deltaTime));
	}

	void Running()
	{
		// Makes the enemy randomly change direction
		if(enemyRb.position.x % 1 == 0 || enemyRb.position.y % 1 == 0)
		{
			if(directionChangerCountdown == 0)
			{
				directionChangerCountdown = 1;
				Debug.Log("Random change of direction");

				randomDirection = random.Next(0, 2);

				if(direction == Vector2.up || direction == Vector2.down)
				{
					if(direction == Vector2.up)
					{
						if(randomDirection == 0)
							direction = Vector2.down;

						else if(randomDirection == 1)
							direction = Vector2.left;

						else if(randomDirection == 2)
							direction = Vector2.right;
					}

					else if(direction == Vector2.down)
					{
						if(randomDirection == 0)
							direction = Vector2.up;

						else if(randomDirection == 1)
							direction = Vector2.left;

						else if(randomDirection == 2)
							direction = Vector2.right;
					}
					enemyRoundedPosition = new Vector2(Convert.ToSingle(Math.Round(enemyRb.position.x)), enemyRb.position.y);
				}

				// If the is moving along the x-axis
				else if(direction == Vector2.left || direction == Vector2.right)
				{
					if(direction == Vector2.left)
					{
						if(randomDirection == 0)
							direction = Vector2.up;

						else if(randomDirection == 1)
							direction = Vector2.down;

						else if(randomDirection == 2)
							direction = Vector2.right;
					}

					else if(direction == Vector2.right)
					{
						if(randomDirection == 0)
							direction = Vector2.up;

						else if(randomDirection == 1)
							direction = Vector2.down;

						else if(randomDirection == 2)
							direction = Vector2.left;
					}
					enemyRoundedPosition = new Vector2(enemyRb.position.x, Convert.ToSingle(Math.Round(enemyRb.position.y)));
				}

			}
		}

		if(direction == Vector2.up || direction == Vector2.down)
		{
			enemyRoundedPosition = new Vector2(Convert.ToSingle(Math.Round(enemyRb.position.x)), enemyRb.position.y);
		}

		else if(direction == Vector2.left || direction == Vector2.right)
		{
			enemyRoundedPosition = new Vector2(enemyRb.position.x, Convert.ToSingle(Math.Round(enemyRb.position.y)));
		}

		enemyRb.MovePosition(enemyRoundedPosition + (direction * speed * Time.deltaTime));
	}

	void Stalking()
	{

	}

	void Swooping()
	{
		if(swooperCountdown > 0)
		{
			swooperCountdown -= 1 * Time.deltaTime;

			if(swooperCountdown < 0)
			{
				swooperCountdown = 0;
			}
		}

		Vector3 enemyPosition = new Vector3(enemyRb.transform.position.x, enemyRb.transform.position.y, -2);

		if(swooperCountdown < 5)
		{
			Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, 0);

			Vector3 swoop = Vector3.MoveTowards(enemyPosition, playerPosition, 0.5f);

			enemyRb.MovePosition(swoop + (Vector3.one * speed * Time.deltaTime));
			swooperCountdown = 15;
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

	private void OnCollisionStay2D(Collision2D collision)
	{
		// If the enemy hits a wall it will randomly change to another direction
		if(collision.collider.CompareTag("Wall") && directionChangerCountdown == 0)
		{
			// If the enemy is moving along the y-axis
			Debug.Log("Touched Wall");
			if(direction == Vector2.up || direction == Vector2.down)
			{
				if(direction == Vector2.up)
				{
					if(randomDirectionWall == 0)
						direction = Vector2.down;

					else if(randomDirectionWall == 1)
						direction = Vector2.left;

					else if(randomDirectionWall == 2)
						direction = Vector2.right;
				}

				else if(direction == Vector2.down)
				{
					if(randomDirectionWall == 0)
						direction = Vector2.up;

					else if(randomDirectionWall == 1)
						direction = Vector2.left;

					else if(randomDirectionWall == 2)
						direction = Vector2.right;
				}
				enemyRoundedPosition = new Vector2(Convert.ToSingle(Math.Round(enemyRb.position.x)), enemyRb.position.y);
			}

			// If the is moving along the x-axis
			else if(direction == Vector2.left || direction == Vector2.right)
			{
				if(direction == Vector2.left)
				{
					if(randomDirectionWall == 0)
						direction = Vector2.up;

					else if(randomDirectionWall == 1)
						direction = Vector2.down;

					else if(randomDirectionWall == 2)
						direction = Vector2.right;
				}

				else if(direction == Vector2.right)
				{
					if(randomDirectionWall == 0)
						direction = Vector2.up;

					else if(randomDirectionWall == 1)
						direction = Vector2.down;

					else if(randomDirectionWall == 2)
						direction = Vector2.left;
				}
				enemyRoundedPosition = new Vector2(enemyRb.position.x, Convert.ToSingle(Math.Round(enemyRb.position.y)));
			}
			directionChangerCountdown = 0.05f;
		}
	}
}
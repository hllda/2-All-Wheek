using UnityEngine;

public class Enemy:MonoBehaviour
{
    private Rigidbody2D enemyRb;
	public float speed;
	private GameObject player;
	public GameManager gameManager;

	// Bounds of the screen
    private float xBound = 8.6f;
    private float yBound = 4.7f;
	
    // Start is called before the first frame update
    void Start()
    {
		enemyRb = GetComponent<Rigidbody2D>();
		player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {		
		if(gameManager.gameIsActive == true)
		{
			MoveEnemy();
			ConstrainPosition();
		}
    }

	void MoveEnemy()
	{
		enemyRb.AddForce((player.transform.position - transform.position).normalized * speed);
	}
	
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
}

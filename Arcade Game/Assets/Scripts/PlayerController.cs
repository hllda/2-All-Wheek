using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float verticalInput;
    public float horizontalInput;
    public Rigidbody2D playerRb;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector2.up * verticalInput, ForceMode2D.Impulse);


        horizontalInput = Input.GetAxis("Horizontal");

        playerRb.AddForce(Vector2.right * horizontalInput, ForceMode2D.Impulse);


        playerRb.transform(Vector2.right, playerRb.rotation)
    }
}

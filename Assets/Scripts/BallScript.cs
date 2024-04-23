using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    private bool isStarted = false;
    private float destroyZone = -12.0f;
    private Vector3 storedVelocity;

    private Rigidbody rb;
    private GameObject paddle;
    private GameManager gameManager;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        paddle = GameObject.Find("Paddle");
        audioSource = GameObject.Find("Ball").GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 ballPositionOnPaddle = new Vector3(paddle.transform.position.x, paddle.transform.position.y + .8f, paddle.transform.position.z);
        if (Input.GetKeyDown(KeyCode.Space) && !isStarted)
        {
            LaunchBall();
            isStarted = true;
        }

        if (!isStarted)
        {
            transform.position = ballPositionOnPaddle;
        }

        if (transform.position.y < destroyZone)
        {
            gameManager.lives -= 1;

            if (gameManager.lives > 0)
            {
                isStarted = false;
                gameObject.transform.position = ballPositionOnPaddle;
            }
        }

        if (gameManager.pauseGameScreen.activeSelf)
        {
            if (isStarted && rb.velocity != Vector3.zero)
            {
                storedVelocity = rb.velocity;
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            if (rb.velocity == Vector3.zero && storedVelocity != Vector3.zero)
            {
                rb.velocity = storedVelocity;
            }
        }
    }

    void LaunchBall()
    {
        float xValue = Random.Range(-1.0f, 1.0f);
        float yValue = 1.0f;

        Vector3 direction = new Vector3(xValue, yValue, 0).normalized;
        rb.velocity = direction * gameManager.ballSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();

        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
            gameManager.bricksAmount -= 1;
            gameManager.score += 1;
        }

        if (rb.velocity.magnitude != gameManager.ballSpeed)
        {
            rb.velocity = rb.velocity.normalized * gameManager.ballSpeed;
        }
    }
}

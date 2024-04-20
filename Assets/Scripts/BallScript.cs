using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    public float speed = 10.0f;
    public bool isStarted = false;

    Rigidbody rb;
    public GameObject paddle;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isStarted)
        {
            LaunchBall();
            isStarted = true;
        }

        if (!isStarted)
        {
            transform.position = new Vector3(paddle.transform.position.x, paddle.transform.position.y + .8f, paddle.transform.position.z);
        }
    }

    void LaunchBall()
    {
        float xValue = Random.Range(-1.0f, 1.0f);
        float yValue = 1.0f;

        Vector3 direction = new Vector3(-1.0f, yValue, 0).normalized;
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
            speed += 1;
        }

        if (rb.velocity.magnitude != speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }
}

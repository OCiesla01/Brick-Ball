using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{

    private float speed = 10.0f;
    private float horizontalBound = 16.45f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePaddle();
    }

    void MovePaddle()
    {
        float inputX = Input.GetAxis("Horizontal");

        Vector3 position = transform.position;
        position.x += inputX * speed * Time.deltaTime;

        position.x = Mathf.Clamp(position.x, -horizontalBound, horizontalBound);

        transform.position = position;
    }
}

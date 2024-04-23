using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{

    [SerializeField]
    private Color color;

    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

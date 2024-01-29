using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_Rigidbody;

    [SerializeField]
    private float m_Speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float movementH = Input.GetAxis("Horizontal");
        float movementV = Input.GetAxis("Vertical");

        m_Rigidbody.velocity = new Vector2(movementH * m_Speed, movementV * m_Speed);
    }

    
}

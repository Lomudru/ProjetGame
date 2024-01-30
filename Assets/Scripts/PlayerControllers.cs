using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControllers : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_Rigidbody;

    [SerializeField]
    private float m_Speed;

    public int Health;

    private Vector2 moveAmount;

    [SerializeField]
    private GameObject pointer;



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
        Vector3 mousePos = Input.mousePosition;
		mousePos.z = 10;

		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
		
        Vector3 direction = mousePos - transform.position;
        pointer.transform.up = direction.normalized;
    }

    public void OnMove(InputValue value)
    {
        // read the value for the "move" action each event call
        if(Health > 0){
            moveAmount = value.Get<Vector2>();
            m_Rigidbody.velocity = new Vector2(moveAmount.x * m_Speed, moveAmount.y * m_Speed);
        }

    }
    
}

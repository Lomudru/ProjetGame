using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyControllers : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_Rigidbody;


    private GameObject target;

    [SerializeField]
    public float speed = 2f;

    private float range;
    
    private Vector2 direction;
    
    [SerializeField]
    public int damage;

    private PlayerControllers player;

    private bool quit = false;


    [SerializeField]
    private Sprite[] sprites;

    // Start is called before the first frame update
    private void Start()
    {
        target = GameObject.FindWithTag("Player");
        player = target.GetComponent<PlayerControllers>();
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate(){
        if(!player.inMenu){
            Vector2 direction = target.transform.position - transform.position;
            m_Rigidbody.velocity = direction.normalized * speed;
        }else{
            m_Rigidbody.velocity = new Vector2(0,0);
        }

    }

    

    

    
}

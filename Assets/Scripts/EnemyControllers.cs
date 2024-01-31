using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControllers : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_Rigidbody;

    private GameObject target;

    [SerializeField]
    private float speed = 2f;

    private float range;
    
    private Vector2 direction;
    
    [SerializeField]
    private int damage;

    private PlayerControllers player;

    private bool quit = false;

    private bool attaquer = true;

    // Start is called before the first frame update
    private void Start()
    {
        target = GameObject.FindWithTag("Player");
        player = target.GetComponent<PlayerControllers>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate(){
        Vector2 direction = target.transform.position - transform.position;

        m_Rigidbody.velocity = direction.normalized * speed;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            if(attaquer && player.Health > 0){
                StartCoroutine(hit());
                attaquer = false;
            }
        }
    }

    private IEnumerator hit(){
        player.Health -= damage;
        Debug.Log(player.Health);
        float waitTime = 2;
        yield return wait(waitTime);
    }

    IEnumerator wait(float waitTime)
    {
        float counter = 0;
        while (counter < waitTime)
        {
            //Increment Timer until counter >= waitTime
            counter += Time.deltaTime;
            if (quit)
            {
                //Quit function
                attaquer = true;
                yield break;
            }
            //Wait for a frame so that Unity doesn't freeze
            yield return null;
        }
        attaquer = true;
    }   
}

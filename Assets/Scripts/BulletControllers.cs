using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletControllers : MonoBehaviour
{

    private PlayerControllers player;
    // Start is called before the first frame update
    void Start()
    {
        GameObject target = GameObject.FindWithTag("Player");
        player = target.GetComponent<PlayerControllers>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        Destroy(other.gameObject);
        Destroy(gameObject);
        player.Exp += 5;
    }
}

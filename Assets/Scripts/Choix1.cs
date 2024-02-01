using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Choix1 : MonoBehaviour
{
    [SerializeField]
    private GameObject canva;
    [SerializeField]
    private TMP_Text title;
    [SerializeField]
    private TMP_Text desc;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnEnable(){
        string[] Choice = canva.GetComponent<PowerUp>().Choix1;
        title.text = Choice[0];
        desc.text = Choice[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

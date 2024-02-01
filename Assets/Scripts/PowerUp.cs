using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private PlayerControllers player;
    [SerializeField]
    private EnemyControllers enemy;
    public string[] Choix1;
    public string[] Choix2;
    public string[] Choix3;
    private string[][] ListePower = {new string[]{"Fibre", "Augmenter votre vitesse de 10%"}, new string[]{"Tir rapide", "Réduit le delay entre de tir de 10%"}, new string[]{"Dezoom", "Augmente votre vision de 10%"}, new string[]{"Tir longue duree", "Augmente de 10% la durée de vie des balle"}, new string[]{"Augmentation du tickspeed", "Augmente la vitesse des balle mais aussi celle des monstre de 10%"}};
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnEnable(){
        List<string[]> tempList = new List<string[]>(ListePower);
        int random = Random.Range(0, tempList.Count);
        Choix1 = tempList[random];
        tempList.RemoveAt(random);
        random = Random.Range(0, tempList.Count);
        Choix2 = tempList[random];
        tempList.RemoveAt(random);
        random = Random.Range(0, tempList.Count);
        Choix3 = tempList[random];
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void Choisis1(){
        ApplyEffect(Choix1);
    }
    public void Choisis2(){
        ApplyEffect(Choix2);
    }
    public void Choisis3(){
        ApplyEffect(Choix3);
    }
    private void ApplyEffect(string[] Choix){
        player.inMenu = false;
        player.Exp = 0;
        player.NextLevel *= 1.10f;
        switch(Choix[0]){
            case "Fibre":
                player.m_Speed *= 1.10f;
                break;
            case "Tir rapide":
                player.shootSpeed -= player.shootSpeed * 10 / 100;
                break;
            case "Dezoom":
                Camera.main.orthographicSize *= 1.10f;
                break;
            case "Tir longue duree":
                player.bullet_LifeTime *= 1.10f;
                break;
            case "Augmentation du tickspeed":
                player.bullet_Speed *= 1.10f;
                enemy.speed *= 1.10f;
                break;
        }
        gameObject.SetActive(false);
    }
}

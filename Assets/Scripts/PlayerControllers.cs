using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using TMPro;


public class PlayerControllers : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D m_Rigidbody;

    [SerializeField]
    private GameObject PowerUp;
    [SerializeField]
    private GameObject GameOver;
    [SerializeField]
    private TMP_Text LifeZone;
    [SerializeField]
    private TMP_Text XpZone;
    [SerializeField]
    private TMP_Text TimerZone;
    [SerializeField]
    private TMP_Text ScoreZone;
    
    [SerializeField]
    public float m_Speed;
    [SerializeField]
    public float shootSpeed;
    [SerializeField]
    public float bullet_Speed;
    [SerializeField]
    public float bullet_LifeTime;

    public int Health;

    private Vector2 moveAmount;
    [SerializeField]
    private GameObject pointer;

    private Vector2 Aim = new Vector2(0,0);

    [SerializeField]
    private GameObject Enemy;



    [SerializeField]
    private GameObject Bullet;
    private bool quit = false;
    private bool spawn = true;
    private float CamWidth;
    private float CamHeight;
    private Camera cam;
    private bool shooting = true;
    private float waitTimeSpawn = 2;
    private float currentTime;
    public bool attaquer = true;
    public float Exp = 0;
    public bool inMenu = false;
    public float NextLevel = 100;
    private int score;
    private bool scored = false;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("reduceSpawnTime", 1, 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if(Health <= 0){
            if(!scored){
                score = (int)Time.timeSinceLevelLoad;
                scored = true;
            }
            inMenu = true;
            ScoreZone.text = "Vous avez tenus "+ score +" secondes";
            GameOver.SetActive(true);
        }else{
            LifeZone.text = "Vie : " + Health;
            XpZone.text = "Exp : " + Exp + " / " + (int)NextLevel;
            TimerZone.text = "Timer : " + (int)Time.timeSinceLevelLoad;
        }
        Gamepad game = Gamepad.current;

        Vector3 mousePos = Input.mousePosition;
		mousePos.z = 10;

		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
		
        Vector3 direction = mousePos - transform.position;
        if(game != null && Health > 0 && !inMenu){
            if(game.rightStick.ReadValue() != new Vector2(0,0) ){
                Aim = game.rightStick.ReadValue();
            }
            pointer.transform.up = Aim;
            if(shooting && Health > 0 && !inMenu){
            StartCoroutine(shoot(Aim));
                shooting = false;
            }
        }else if(inMenu == false){
            pointer.transform.up = direction.normalized;
            if(shooting && Health > 0 && !inMenu){
                StartCoroutine(shoot(direction));
                shooting = false;
            }   
        }
        
        if(spawn && Health > 0 && !inMenu){
            StartCoroutine(spawnEnemy());
            spawn = false;
        }
        if(Exp >= NextLevel){
            inMenu = true;
            PowerUp.SetActive(true);
        }

    }

    private void OnMove(InputValue value)
    {
        // read the value for the "move" action each event call
        if(Health > 0 && !inMenu){
            moveAmount = value.Get<Vector2>();
            m_Rigidbody.velocity = new Vector2(moveAmount.x * m_Speed, moveAmount.y * m_Speed);
        }
        if(Health == 0 || inMenu){
            m_Rigidbody.velocity = new Vector2(0, 0);
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Enemy"){
            if(attaquer && Health > 0){
                StartCoroutine(hit(collision.gameObject));
                attaquer = false;
            }
        }
    }
    private IEnumerator hit(GameObject enemy){
        Health -= enemy.GetComponent<EnemyControllers>().damage;
        float waitTime = 1;
        yield return wait(waitTime);
    }
    private IEnumerator spawnEnemy(){
        cam = Camera.main;
        float CamHeight = 2f * cam.orthographicSize;
        float CamWidth = CamHeight * cam.aspect;
        int whereToSpawn = Random.Range(1,5);
        if(whereToSpawn == 1){
            Instantiate(Enemy, new Vector3(CamWidth + transform.position.x, transform.position.y, 0), Quaternion.identity);
        }else if(whereToSpawn == 2){
            Instantiate(Enemy, new Vector3(-CamWidth + transform.position.x, transform.position.y, 0), Quaternion.identity);
        }else if(whereToSpawn == 3){
            Instantiate(Enemy, new Vector3(transform.position.x, CamHeight + transform.position.y, 0), Quaternion.identity);
        }else if(whereToSpawn == 4){
            Instantiate(Enemy, new Vector3(transform.position.x, -CamHeight + transform.position.y, 0), Quaternion.identity);
        }
        
        yield return waitEnemy(waitTimeSpawn);
    }
    private IEnumerator shoot(Vector2 direction){
        GameObject currentBullet = Instantiate(Bullet, new Vector3(transform.position.x + direction.normalized.x, transform.position.y + direction.normalized.y, 0), Quaternion.identity);
        currentBullet.GetComponent<Rigidbody2D>().velocity = new Vector2((direction.normalized.x * (1 + Mathf.Abs(moveAmount.x))) * bullet_Speed, (direction.normalized.y * (1 + Mathf.Abs(moveAmount.y))) * bullet_Speed);
        currentBullet.transform.up = direction.normalized;
        Destroy(currentBullet, bullet_LifeTime);
        float waitTime = shootSpeed;
        yield return waitBullet(waitTime);
    }
    void reduceSpawnTime(){
        waitTimeSpawn -= 0.05f;
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
    IEnumerator waitEnemy(float waitTime)
    {
        float counter = 0;
        while (counter < waitTime)
        {
            //Increment Timer until counter >= waitTime
            counter += Time.deltaTime;
            if (quit)
            {
                spawn = true;
                //Quit function
                yield break;
            }
            //Wait for a frame so that Unity doesn't freeze
            yield return null;
        }
        spawn = true;
    }   
    IEnumerator waitBullet(float waitTime)
    {
        float counter = 0;
        while (counter < waitTime)
        {
            //Increment Timer until counter >= waitTime
            counter += Time.deltaTime;
            if (quit)
            {
                shooting = true;
                //Quit function
                yield break;
            }
            //Wait for a frame so that Unity doesn't freeze
            yield return null;
        }
        shooting = true;
    }   
}

using System.Collections;
using System.Collections.Generic;
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

    private Vector2 Aim = new Vector2(0,0);

    [SerializeField]
    private GameObject Enemy;
    private bool quit = false;
    private bool spawn = true;
    private float CamWidth;
    private float CamHeight;
    private Camera cam;
    
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
        Gamepad game = Gamepad.current;

        Vector3 mousePos = Input.mousePosition;
		mousePos.z = 10;

		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
		
        Vector3 direction = mousePos - transform.position;
        if(game != null){
            
            if(game.rightStick.ReadValue() != new Vector2(0,0) ){
                Aim = game.rightStick.ReadValue();
            }
            pointer.transform.up = Aim;
        }else{
            pointer.transform.up = direction.normalized;
        }
        if(spawn && Health > 0){
            StartCoroutine(spawnEnemy());
            spawn = false;
        }

    }

    private void OnMove(InputValue value)
    {
        // read the value for the "move" action each event call
        if(Health > 0){
            moveAmount = value.Get<Vector2>();
            m_Rigidbody.velocity = new Vector2(moveAmount.x * m_Speed, moveAmount.y * m_Speed);
        }
        if(Health == 0){
            m_Rigidbody.velocity = new Vector2(0, 0);
        }

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
            Instantiate(Enemy, new Vector3(-CamWidth + transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
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
                spawn = true;
                //Quit function
                yield break;
            }
            //Wait for a frame so that Unity doesn't freeze
            yield return null;
        }
        spawn = true;
    }   
}

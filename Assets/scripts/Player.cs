using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed = 50f;
    public float jumpPower = 150f;
    public float maxSpeed = 3f;
    public bool onGround;
    public bool trip=false;
    public bool rolling=false;
    public static bool hitting = false;
    public bool hitUp = false;
    public bool hitMid = false;
    public bool hitDown = false;
    public static float healthHolder; //change healthHolder in order to change health

    private Animator anim;
    private float spedControl;
    private Rigidbody2D rb2d;
    [SerializeField]
    private Stat health;

    private void Awake()
    {
        health.Initialize();
    }

	void Start ()
    {
        healthHolder = health.CurrentVal; 
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
	}
	
    void Update()
    {
        health.CurrentVal = healthHolder;

        anim.SetBool("onGround", onGround);
        anim.SetFloat("speed", Mathf.Abs(spedControl));
        anim.SetBool("trip", trip);
        anim.SetBool("rolling", rolling);
        anim.SetBool("isHitting", hitting);
        anim.SetBool("hitUp", hitUp);
        anim.SetBool("hitDown", hitDown);
        anim.SetBool("hitMid", hitMid);

        Quaternion spawnRotation = Quaternion.identity;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = new Quaternion(0, -180, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        if(trip)
        {
            rolling = true;
        }
    }

	void FixedUpdate ()
    {
        /*
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb2d.AddForce(Vector2.up * jumpPower);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddForce(Vector2.left * speed);
            spedControl = 1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.AddForce(Vector2.right * speed);
            spedControl = 1f;
        }
        else
        {
            spedControl = 0;
        }
        */
        if(GameController.songStarted)
        {
            rb2d.AddForce(Vector2.right * speed);
            spedControl = 1f;
        }
        else
        {

        }

        if (rb2d.velocity.x>maxSpeed)
        {
            rb2d.velocity=new Vector2(maxSpeed,rb2d.velocity.y);
        }
        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Enemy1(Clone)" && GameController.enemySpawnAngle<75)
        {
            hitting = true;
            hitUp = true;
            hitMid = false;
            hitDown = false;
            //Debug.Log("ENTER");
        }
        else if(other.gameObject.name=="Enemy1(Clone)"&&GameController.enemySpawnAngle>105)
        {
            hitting = true;
            hitUp = false;
            hitMid = false;
            hitDown = true;
        }
        else if (other.gameObject.name == "Enemy1(Clone)" && (GameController.enemySpawnAngle <= 105||GameController.enemySpawnAngle>=75))
        {
            hitting = true;
            hitUp = false;
            hitMid = true;
            hitDown = false;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name=="Enemy1(Clone)")
        {
            hitting = false;
            //Debug.Log("EXIT");
        }
    }

}

using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    public float velocity=0;
    public float attack1;
    public float attack2;
    public float attack3;
    public Vector3 rotationVector;
    public bool wasHit=false;

    private Animator anim;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Rigidbody2D rb2d;
    Vector2 direction;

    void Start ()
    {
        anim = gameObject.GetComponent<Animator>();
        cam = gameObject.GetComponentInParent<Camera>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        attack1=Random.Range(0.0f,2.0f);
        attack2=Random.Range(0.0f,2.0f);
        //velocity = Screen.height * velocity / 534.67967320740412694276748972721f;
        velocity = cam.pixelHeight * velocity / 534.67967320740412694276748972721f;
        transform.localScale = transform.lossyScale;
        direction.x = -Mathf.Sin(GameController.enemySpawnAngle * Mathf.Deg2Rad);
        direction.y = -Mathf.Cos(GameController.enemySpawnAngle * Mathf.Deg2Rad);
        rb2d.velocity = direction * velocity;
        //Debug.Log(GameController.enemySpawnAngle);
        //transform.Rotate(0, 90, 0);
        //transform.rotation = new Quaternion(0,90,0,0);
        //rotationVector = transform.rotation.eulerAngles;
        //rotationVector.z = -GameController.enemySpawnAngle;
        //transform.Rotate(rotationVector,50f*Time.deltaTime);
        //transform.rotation = Quaternion.Euler(rotationVector);
        //transform.rotation = Quaternion.Euler(0, 0, 90);
        //rb2d.velocity = Vector2.up * velocity;
        //rb2d.velocity = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * velocity;
    }
	
	void Update ()
    {
        anim.SetFloat("attack1", attack1);
        anim.SetFloat("attack2", attack2);
        anim.SetFloat("attack3", attack3);
        anim.SetBool("wasHit", wasHit);
        //transform.rotation = new Quaternion(0, -60, 0, 0);
        //rotationVector = transform.rotation.eulerAngles;
        //rotationVector.z = 180-rotationVector.z;
        //rotationVector.y = 180;
        //transform.Rotate(rotationVector);
    }

    void FixedUpdate()
    {
        //rb2d.velocity = Vector2.down * velocity;
        //rb2d.velocity = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * velocity;
        if(Player.hitting&&Score.goodHit)
        {
            wasHit = true;
            rb2d.velocity = -direction * velocity;
            Score.goodHit = false;
        }
        else if(Player.hitting && !Score.goodHit)
        {
            Player.healthHolder -= 2;
        }
    }
}

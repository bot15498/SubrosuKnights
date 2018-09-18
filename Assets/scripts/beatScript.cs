using UnityEngine;
using System.Collections;

public class beatScript : MonoBehaviour {

    public float velocity; //~850 seems to be about right for slow songs

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start ()
    {
        velocity=Screen.height * velocity / 534.67967320740412694276748972721f;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        transform.localScale = transform.lossyScale;
        //Debug.Log(Screen.height);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void FixedUpdate()
    {
        //rb2d.AddForce(Vector2.down * velocity);
        rb2d.velocity = Vector2.down * velocity;
    }
}

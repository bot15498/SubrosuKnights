using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour
{
    private Player player;
	
	void Start ()
    {
        player = gameObject.GetComponentInParent<Player>();
	}
	
	
	void OnTriggerEnter2D(Collider2D other)
    {
        player.onGround = true;
        if (other.gameObject.tag == "tripTag")
        {
            //Destroy(other.gameObject);
            player.trip = true;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        player.onGround = true;
    }

    void OnCollsionEnter2D(Collision2D other)
    {
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        player.onGround = false;
    }
}

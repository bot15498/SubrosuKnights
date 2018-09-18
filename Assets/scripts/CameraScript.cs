using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public float xDisplace = 0;
    public float zDisplace = 0;

    [SerializeField]
    private Player player;
	
	void Start ()
    {

	}
	
	
	void Update ()
    {
        /*
        if(!player.onGround)
        {
            transform.position = new Vector3(target.transform.position.x - xDisplace, target.transform.position.y, target.transform.position.z - zDisplace);
        }
        else
        {
            transform.position = new Vector3(target.transform.position.x - xDisplace, transform.position.y, target.transform.position.z - zDisplace);
        }
        */
        transform.position = new Vector3(target.transform.position.x - xDisplace, target.transform.position.y, target.transform.position.z - zDisplace);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name=="Enemy1(Clone)")
        {
            Destroy(other.gameObject);
            Debug.Log(other.gameObject.name);
        }
    }
}

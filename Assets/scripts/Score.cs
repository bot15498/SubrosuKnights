using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

    public int excellentValue;
    public int greatValue;
    public int goodValue;
    public int badValue;
    public int missValue;
    public string key;
    public float excellentRange;
    public float greatRange;
    public float goodRange;
    public float badRange;
    public static bool goodHit = false;

    /*
    [SerializeField]
    private Collider2D miss;
    [SerializeField]
    private Collider2D bad;
    [SerializeField]
    private Collider2D good;
    [SerializeField]
    private Collider2D great;
    [SerializeField]
    private Collider2D excellent;
    [SerializeField]
    private Collider2D great2;
    [SerializeField]
    private Collider2D good2;
    [SerializeField]
    private Collider2D bad2;
    [SerializeField]
    private Collider2D miss2;
    */
    [SerializeField]
    private Text scoreText;
    //[SerializeField]
    //private int score;
    [SerializeField]
    private float distance;

	void Start ()
    {
        //score = 0;
        //UpdateScore();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(Input.GetKey(key))
        {
            //distance = Vector3.Distance(other.gameObject.transform.position, transform.position);
            distance = Vector3.Magnitude(other.gameObject.transform.position) - Vector3.Magnitude(transform.position);
            //distance = Vector3.Magnitude(transform.position);
            if(Mathf.Abs(distance)<=excellentRange)
            {
                GameController.score += excellentValue;
                goodHit = true;
            }
            else if(Mathf.Abs(distance)<=greatRange && Mathf.Abs(distance) >= excellentRange)
            {
                GameController.score += greatValue;
                goodHit = true;
            }
            else if(Mathf.Abs(distance)<=goodRange && Mathf.Abs(distance) >= greatRange)
            {
                GameController.score += goodValue;
                goodHit = true;
            }
            else if(Mathf.Abs(distance)<=badRange && Mathf.Abs(distance) >= goodRange)
            {
                GameController.score += badValue;
                goodHit = false;
            }
            else
            {
                GameController.score += missValue;
                goodHit = false;
            }
        }
    }
}

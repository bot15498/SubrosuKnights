using UnityEngine;
using System.Collections;


public class DestroyBeat : MonoBehaviour
{
    [SerializeField]
    private bool floor; //true= will destory everything it touches
    /*
    [SerializeField]
    private Collider2D Bad;
    [SerializeField]
    private Collider2D Good;
    [SerializeField]
    private Collider2D Great;
    [SerializeField]
    private Collider2D Excellent;
    [SerializeField]
    private Collider2D Great2;
    [SerializeField]
    private Collider2D Good2;
    [SerializeField]
    private Collider2D Bad2;
    [SerializeField]
    private Collider2D Miss;
    [SerializeField]
    private Collider2D Miss2;
    */
    //[SerializeField]
    //private bool startIntersect=false;

    public string key;

    void Start()
    { 

    }

    void Upadate()
    {
        //if(Input.GetKey(key) && (Vector3.Distance(holder.transform.position, transform.position) <= 10))
        //{
        //    Destroy(holder.gameObject);
        //}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //other = holder;
        if(floor)
        {
            Destroy(other.gameObject);
        }
        else
        {
            if(Input.GetKey(key))
            {
                Destroy(other.gameObject);
            }
        }
    }
}

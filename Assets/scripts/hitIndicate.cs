using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class hitIndicate : MonoBehaviour
{
    //[SerializeField]
    private float fill;

    [SerializeField]
    private Image circle;

    public string key;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        hitShow();
	}

    private void hitShow()
    {
        if (Input.GetKey(key))
        {
            fill = 1;
        }
        else
        {
            fill = 0;
        }
        circle.fillAmount = fill;
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {

    //public static float currentHealth;
    //public float maxHealth;
    public bool lerpColors=false;
    public float fillAmount;
    public float MaxValue { get; set; }
    public float Value
    {
        set
        {
            fillAmount = Map(value, 0, MaxValue);
        }
    }

    [SerializeField]
    private Image healthBarImage;
    [SerializeField]
    private Color fullColor;
    [SerializeField]
    private Color midColor;
    [SerializeField]
    private Color lowColor;

	// Use this for initialization
	void Start ()
    {
        if(lerpColors)
        {
            healthBarImage.color = fullColor;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        HealthBar();
	}

    private void HealthBar()
    {
        healthBarImage.fillAmount = fillAmount;
        if(lerpColors)
        {
            if(fillAmount>0.5)
            {
                healthBarImage.color = Color.Lerp(midColor, fullColor, 1-((1-fillAmount)*2)); //converts range of 1 - 0.5 to 1 - 0
            }
            else
            {
                healthBarImage.color = Color.Lerp(lowColor, midColor, fillAmount*2);  //converts range of 0.5 - 0 to 1 - 0
            }
        }
    }

    private float Map(float value, float min, float max)
    {
        return (value / max);
    }
}

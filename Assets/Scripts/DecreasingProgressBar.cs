using UnityEngine;
using System.Collections;

public class DecreasingProgressBar : MonoBehaviour
{
    public GameObject progressBar;

    private float timer;

    private bool alive;
    private int effectiveness;

    private bool increase = true;
    private bool decrease = false;
    // Use this for initialization
    void Start ()
    {
        timer = 0.9f;
	}

    void Update()
    {
        if(timer > 0.0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0.0f;
        }
        progressBar.transform.localScale = new Vector3(timer, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
    }

    public void increaseForPush()
    {
        
    }

    IEnumerator checkEffectiveness()
    {
        while(alive)
        {
            if (increase)
            {
                effectiveness += 1;
                if(effectiveness >= 10)
                {
                    increase = false;
                    decrease = true;
                }
            }

            if (decrease)
            {
                effectiveness -= 1;
                if (effectiveness <= -4)
                {
                    increase = true;
                    decrease = false;
                }
            }

            yield return new WaitForSeconds(1/15);
            Debug.Log("effectiveness = "+ effectiveness);
        } 
    }
}

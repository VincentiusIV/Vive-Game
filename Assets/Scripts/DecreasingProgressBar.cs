using UnityEngine;
using System.Collections;

public class DecreasingProgressBar : MonoBehaviour
{
    public float progressValue;
    public float decreasePerMs;

<<<<<<< HEAD
    private bool alive;
=======
    private bool runBar;
>>>>>>> 532ffb6ebecf7400c8602c7a1fb7a697bd594281
    private int effectiveness;

    private bool increase = true;
    private bool decrease = false;
    // Use this for initialization
    void Start ()
    {
<<<<<<< HEAD
        alive = true;
        effectiveness = 0;
=======
        runBar = true;
>>>>>>> 532ffb6ebecf7400c8602c7a1fb7a697bd594281

        Debug.Log("runBar in Start() = " + runBar);
        StartCoroutine(DecreasePerSecond());
        StartCoroutine(checkEffectiveness());
	}

	IEnumerator DecreasePerSecond()
    {
<<<<<<< HEAD
        while (progressValue != 0 && alive)
=======
        // runs continously while runBar == true
        while (progressValue > 0 || progressValue < 100)
>>>>>>> 532ffb6ebecf7400c8602c7a1fb7a697bd594281
        {
            progressValue -= decreasePerMs;
            Debug.Log("progress bar is at: " + progressValue + "%");

            if (progressValue <= 0)
            {
                Debug.Log("patient died! you lose");
<<<<<<< HEAD
                alive = false;
=======
                //runBar = false;
>>>>>>> 532ffb6ebecf7400c8602c7a1fb7a697bd594281
            }
            if (progressValue >= 100)
            {
                Debug.Log("patient is alive! you win");
                //runBar = false;
            }
            yield return new WaitForSeconds(1.0f);
        }
        
    }

    public void increaseForPush()
    {
<<<<<<< HEAD
        if(alive)
=======
        if (progressValue > 0 || progressValue < 100)
>>>>>>> 532ffb6ebecf7400c8602c7a1fb7a697bd594281
        {
            progressValue += effectiveness;
            Debug.Log("You pushed the chest and improved health by: "+effectiveness);

<<<<<<< HEAD
            if (progressValue > 100)
            {
                Debug.Log("Patient is breathing again! you win");
                progressValue = 100;
                alive = false;
            }
        }
        else
        {
            Debug.Log("Patient no longer needs pushing");
=======
            effectiveness = -5;
            increase = true;
            decrease = false;
>>>>>>> 532ffb6ebecf7400c8602c7a1fb7a697bd594281
        }
        
    }

    IEnumerator checkEffectiveness()
    {
<<<<<<< HEAD
        while(alive)
=======
        while(progressValue > 0 || progressValue < 100)
>>>>>>> 532ffb6ebecf7400c8602c7a1fb7a697bd594281
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
                if (effectiveness <= -6)
                {
                    increase = true;
                    decrease = false;
                }
            }
            yield return new WaitForSeconds(1/15);
        } 
    }
}

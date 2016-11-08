using UnityEngine;
using System.Collections;

public class PatientScript : MonoBehaviour
{
    public GameObject progressBar;
    public bool isOnStretcher;

    private float timer;

    private int effectiveness;

    private bool increase = true;
    private bool decrease = false;
    
    // Use this for initialization
    void Start ()
    {
        timer = 100.0f;
        isOnStretcher = false;
	}

    void Update()
    {
        if(timer > 0.05f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0.0f;
        }
        if(isOnStretcher)
        {
            progressBar.transform.localScale = new Vector3(timer / 100, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
        }
        
        if(Input.GetButtonUp("Jump"))
        {
            increaseForPush();
        }
    }

    public void increaseForPush()
    {
        Debug.Log("You increased for push ");
        if(timer <= 10)
        {
            timer += effectiveness;
        }
    }

    // Function that should run when the patient is on the stretcher
    void OnStretcher()
    {

    }

    IEnumerator checkEffectiveness()
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

﻿using UnityEngine;
using System.Collections;

public class PatientScript : MonoBehaviour
{
    public GameObject progressBar;
    public GameObject EffectText;

    public bool isOnStretcher;

    private float timer;

    private int effectiveness;

    private bool increase = true;
    private bool decrease = false;
    private bool inCondition;

    // Use this for initialization
    void Start ()
    {
        inCondition = true;
        timer = 50.0f;
        isOnStretcher = false;
        effectiveness = 5;
	}

    void Update()
    {
        if(inCondition)
        {
            if (timer > 0.05f)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 0.0f;
            }
            if (isOnStretcher)
            {
                progressBar.transform.localScale = new Vector3(timer / 100, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
            }

            if (Input.GetButtonUp("Jump"))
            {
                increaseForPush();
            }
            EffectText.GetComponent<TextMesh>().text = "Effectiveness = " + effectiveness;
        }

        if(timer >= 90)
        {
            inCondition = false;
            progressBar.transform.localScale = new Vector3(0.9f, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
            EffectText.GetComponent<TextMesh>().text = "Patient no longer needs CPR";
        }
    }

    public void increaseForPush()
    {
        Debug.Log("You increased for push ");
        if(timer <= 90)
        {
            timer += effectiveness;
        }
        StartCoroutine(checkEffective());
    }

    // Function that should run when the patient is on the stretcher
    void OnStretcher()
    {

    }

    IEnumerator checkEffective()
    {
        effectiveness = -5;

        yield return new WaitForSeconds(1.0f );

        effectiveness = 0;

        yield return new WaitForSeconds(1.0f);

        effectiveness = 50;
    }
}

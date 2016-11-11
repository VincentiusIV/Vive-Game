using UnityEngine;
using System.Collections;

public class PatientScript : MonoBehaviour
{
    public GameObject progressBar;
    public GameObject EffectText;

    public Transform pushArea;
    public Transform pinchArea;

    public bool isOnStretcher;

    private float timer;
    private int timeCompres;
    private int timeSucCompres;
    private int timeUnsucCompres;

    private int effectiveness;
    private bool inCondition;
    private bool pushed;
    // Use this for initialization
    void Start()
    {
        inCondition = true;
        timer = 50.0f;
        isOnStretcher = false;
        effectiveness = 5;
	}

    void Update()
    {
        if (timer > 0.05f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0.0f;
        }

        if (inCondition)
        {
            if (isOnStretcher)
            {
                progressBar.transform.localScale = new Vector3(timer / 100, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
                GetComponent<Rigidbody>().isKinematic = true;
                
            }

            if (pushArea.GetComponent<AddForce>().uncompressed == true && pushed == true)
            {
                pushed = false;
            }
            if(pushArea.GetComponent<AddForce>().compressed && pushed == false)
            {
                increaseForPush();
                pushed = true;
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
        timeCompres += 1;
        Debug.Log("You increased for push for the "+timeCompres+"th time");

        if(timer <= 90)
        {
            timer += effectiveness;
            if(effectiveness < 1)
            {
                timeUnsucCompres += 1;
            }
            if(effectiveness > 1)
            {
                timeSucCompres += 1;
            }
        }

        StopAllCoroutines();
        StartCoroutine(checkEffective());
    }

    IEnumerator checkEffective()
    {
        float timeBetween = 0.33f;
        effectiveness = -5;
        yield return new WaitForSeconds(timeBetween);
        effectiveness = 0;
        yield return new WaitForSeconds(timeBetween);
        effectiveness = 10;
        yield return new WaitForSeconds(timeBetween);
        effectiveness = 5;
    }
}

using UnityEngine;
using System.Collections;

public class PatientScript : MonoBehaviour
{
    public GameObject progressBar;
    public GameObject respirationBar;

    public GameObject EffectText;
    public GameObject greenLight;
    public GameObject redLight;

    public Transform pushArea;
    public Transform pinchArea;

    public bool isOnStretcher;

    private float timer;
    private int timeCompres;
    private int timeSucCompres;
    private int timeUnsucCompres;
    private float respirationStatus;
    private IEnumerator effectCoroutine;

    private int effectiveness;
    private int chance;
    private int chanceInrease;
    private bool inCondition;
    private bool pushed;

    private AudioSource IsAliveSound;

    // Use this for initialization
    void Start()
    {
        greenLight.SetActive(false);
        redLight.SetActive(false);
        
        inCondition = true;
        isOnStretcher = false;

        timer = 50.0f;
        effectCoroutine = checkEffective();
        effectiveness = 5;

        IsAliveSound = GetComponent<AudioSource>();
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
            timer = 90;
            chance = Random.Range(chanceInrease, 5);
            chanceInrease++;

            if (chance == 3)
            {
                PatientIsHealthy();
            }
        }
    }

    public void increaseForPush()
    {
        timeCompres += 1;

        if(timer <= 90)
        {
            timer += effectiveness;

            if(effectiveness < 6)
            {
                Debug.Log("push unsuccesful");
                timeUnsucCompres += 1;
                redLight.SetActive(true);
                StartCoroutine(OffAfterSeconds(0.5f, redLight));
            }
            if(effectiveness > 5)
            {
                Debug.Log("push succesful");
                timeSucCompres += 1;
                greenLight.SetActive(true);
                StartCoroutine(OffAfterSeconds(0.5f, greenLight));
            }
        }

        StopCoroutine(effectCoroutine);
        StartCoroutine(effectCoroutine);
    }
    public void respiration()
    {
        Debug.Log("You kissed me god damnit");
        if (respirationStatus >= 0.9)
        {
            respirationStatus = 0.9f;
        }
        else
        {
            respirationStatus += Time.deltaTime;
        }
        respirationBar.transform.localScale = new Vector3(respirationStatus, respirationBar.transform.localScale.y, respirationBar.transform.localScale.z);
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

    IEnumerator OffAfterSeconds(float sec, GameObject _light)
    {
        yield return new WaitForSeconds(sec);
        _light.SetActive(false);
    }

    void PatientIsHealthy()
    {
        inCondition = false;
        timer = 90;
        EffectText.GetComponent<TextMesh>().text = "Patient alive";
        
        IsAliveSound.Play();
        IsAliveSound.loop = true;
    }
}

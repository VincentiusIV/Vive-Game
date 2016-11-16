using UnityEngine;
using System.Collections;

public class PatientScript : MonoBehaviour
{
    public GameObject progressBar;
    public GameObject respirationBar;

    public GameObject effectText;
    public GameObject greenLight;
    public GameObject redLight;

    public GameObject goodPushText;
    public GameObject badPushText;
    public GameObject totalPushText;

    public Transform pushArea;
    public Transform pinchArea;

    public AudioSource heartMonitorSound;
    public AudioClip deathSound;

    public bool isOnStretcher;

    private float currentHealth;
    private int timeCompres;
    private int timeSucCompres;
    private int timeUnsucCompres;
    private float respirationStatus;
    private IEnumerator effectCoroutine;
    private int effectiveness;
    private int chance;
    private int chanceInrease;
    private float totalHealth;

    private bool inCondition;
    private bool pushed;
    private bool needsRespiration;
    private bool isNosePinched;

    private Animator bodyController;

    // Use this for initialization
    void Start()
    {
        greenLight.SetActive(false);
        redLight.SetActive(false);
        
        inCondition = true;
        isOnStretcher = false;
        needsRespiration = false;

        currentHealth = 50.0f;
        totalHealth = 90f;
        effectCoroutine = checkEffective();
        effectiveness = 5;
        
        bodyController = GetComponent<Animator>();
        SwitchActive(false);
	}

    void Update()
    {
        goodPushText.GetComponent<TextMesh>().text = "Good Pushes: " + timeSucCompres;
        badPushText.GetComponent<TextMesh>().text = "Bad Pushes: " + timeUnsucCompres;
        totalPushText.GetComponent<TextMesh>().text = "Total Pushes: " + timeCompres;

        if (inCondition)
        {
            if(needsRespiration == false)
            {
                effectText.GetComponent<TextMesh>().text = "Effectiveness: " + effectiveness;
            }
            if (isOnStretcher)
            {
                progressBar.transform.localScale = new Vector3(currentHealth / totalHealth, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
                GetComponent<Rigidbody>().isKinematic = true;
                SwitchActive(true);
            }
            if (pushArea.GetComponent<AddForce>().uncompressed && pushed == true)
            {
                SwitchAnimation("Compression", false);
                pushed = false;
            }
            if (pushArea.GetComponent<AddForce>().compressed && pushed == false)
            {
                SwitchAnimation("Compression", true);
                pushed = true;
                increaseForPush();
            }
        }

        if (currentHealth > totalHealth)
        {
            currentHealth = totalHealth;
            chance = Random.Range(chanceInrease, 5);
            chanceInrease++;

            if (chance == 4)
            {
                needsRespiration = true;
                effectText.GetComponent<TextMesh>().text = "Respire now";
            }
        }
        else if(currentHealth <= 0)
        {
            PatientIsDead();
        }
        else
        {
            if (currentHealth > 0.05f)
            {
                currentHealth -= Time.deltaTime;
            }
            else
            {
                currentHealth = 0.0f;
            }
            
        }

        if(Input.GetButtonDown("Cancel"))
        {
            PatientIsHealthy();
        }
    }

    public void increaseForPush()
    {
        if(needsRespiration)
        {
            currentHealth -= 5;
        }
        else
        {
            timeCompres += 1;

            if (currentHealth <= 90)
            {
                currentHealth += effectiveness;

                if (effectiveness < 1)
                {
                    timeUnsucCompres += 1;
                    redLight.SetActive(true);
                    StartCoroutine(OffAfterSeconds(0.5f, redLight));
                }
                if (effectiveness == 5)
                {
                    heartMonitorSound.Play();
                    timeSucCompres += 1;
                    greenLight.SetActive(true);
                    StartCoroutine(OffAfterSeconds(0.5f, greenLight));
                }
            }
            StopCoroutine(effectCoroutine);
            effectCoroutine = checkEffective();
            StartCoroutine(effectCoroutine);
        }
    }

    public void respiration()
    {
        if(needsRespiration && isNosePinched)
        {
            if (respirationStatus >= 9)
            {
                respirationStatus = 9.0f;
                PatientIsHealthy();
            }
            else
            {
                respirationStatus += Time.deltaTime;
            }
            respirationBar.transform.localScale = new Vector3(respirationStatus /10, respirationBar.transform.localScale.y, respirationBar.transform.localScale.z);
        }
    }

    public void pinchNose(bool value)
    {
        isNosePinched = value;
    }

    IEnumerator checkEffective()
    {
        float timeBetween = 0.33f;
        effectiveness = -5;
        yield return new WaitForSeconds(timeBetween);
        effectiveness = 0;
        yield return new WaitForSeconds(timeBetween);
        effectiveness = 5;
        yield return new WaitForSeconds(timeBetween);
        effectiveness = 3;
    }

    IEnumerator OffAfterSeconds(float sec, GameObject _obj)
    {
        yield return new WaitForSeconds(sec);
        _obj.SetActive(false);
    }

    IEnumerator HeartRateSound()
    {
        for (int i = 0; i < 10; i++)
        {
            heartMonitorSound.Play();
            yield return new WaitForSeconds(1.0f);
        }
    }

    void SwitchAnimation(string varName, bool value)
    {
        bodyController.SetBool(varName, value);
    }

    void SwitchActive(bool value)
    {
        if(pushArea.gameObject.activeInHierarchy != value)
        {
            pushArea.gameObject.SetActive(value);
            pinchArea.gameObject.SetActive(value);
        }
    }

    void PatientIsHealthy()
    {
        inCondition = false;
        currentHealth = 90;
        effectText.GetComponent<TextMesh>().text = "Patient alive";

        SwitchAnimation("Breathe", true);

        StartCoroutine(HeartRateSound());
    }

    void PatientIsDead()
    {
        currentHealth = 0;
        inCondition = false;
        effectText.GetComponent<TextMesh>().text = "Patient is dead";

        if(heartMonitorSound.isPlaying == false && heartMonitorSound.clip != deathSound)
        {
            heartMonitorSound.clip = deathSound;
            heartMonitorSound.loop = true;
            heartMonitorSound.Play();
        }
    }
}
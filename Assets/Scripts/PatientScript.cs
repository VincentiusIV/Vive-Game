using UnityEngine;
using System.Collections;

/* This class handles the behaviour for the Patient
 * 
 * Included functions are pushing chest, squeezing nose&breathing,
 * effects on certain occasions, visual feedback with the progressbar, etc.
 * */
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

    // A lot of variables are assigned to when the script starts
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
            // Updates text only when the patient does not need respiration
            if(needsRespiration == false)
            {
                effectText.GetComponent<TextMesh>().text = "Effectiveness: " + effectiveness;
            }
            // progressbar is only updated when patient is on stretcher
            if (isOnStretcher)
            {
                progressBar.transform.localScale = new Vector3(currentHealth / totalHealth, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
                GetComponent<Rigidbody>().isKinematic = true;
                SwitchActive(true);
            }
            /* Handles the body chest compression animations and increaseForPush() function
             * boolean pushed is used so the player cannot keep their hand inside the patient
             * and still revive it
             * */
            if (pushArea.GetComponent<AddForce>().uncompressed)
            {
                bodyController.SetBool("Compression", false);

                if (pushed)
                {
                    pushed = false;
                }
            }
            if (pushArea.GetComponent<AddForce>().uncompressed == false)
            {
                bodyController.SetBool("Compression", true);

                if(pushed == false)
                {
                    pushed = true;
                    increaseForPush();
                }
            }
        }

        // Handles the Chin lift animation when nose is pinched
        if(isNosePinched)
        {
            bodyController.SetBool("Chin Lift", true);
        }
        else
        {
            bodyController.SetBool("Chin Lift", false);
        }

        // Checks the state of the patient
        // when health is above total health, player has a chance to respire the patient
        if (currentHealth > totalHealth && inCondition)
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
        // Handles state of the patient
        else if(currentHealth <= 0 && inCondition)
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

        // Shortcut for testing without Vive
        if(Input.GetButtonDown("Cancel"))
        {
            PatientIsHealthy();
        }
    }

    // Handles the calculation for each push
    // the effectiveness is done in a coroutine to wait for seconds
    // between each value
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
                else if (effectiveness == 5)
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

    // Function for respiration runs when its needed 
    // and the nose is pinched
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

    // called from the InputScript when grip button is pressed in an interactable area (tag)
    public void pinchNose(bool value)
    {
        isNosePinched = value;
    }

    // Determines the effectiveness value
    // after exactly 2/3 of a second the push is the most effective which is the right rhythym
    IEnumerator checkEffective()
    {
        float timeBetween = 0.33f;
        effectiveness = -5;
        yield return new WaitForSeconds(timeBetween);
        effectiveness = -1;
        yield return new WaitForSeconds(timeBetween);
        effectiveness = 5;
        yield return new WaitForSeconds(timeBetween);
        effectiveness = 0;
    }

    // Turns off the light on the hand after a given amount of seconds
    IEnumerator OffAfterSeconds(float sec, GameObject _obj)
    {
        yield return new WaitForSeconds(sec);
        _obj.SetActive(false);
    }

    // Makes the heart rate sound play 10 times after the patient is determined alive
    IEnumerator HeartRateSound()
    {
        for (int i = 0; i < 10; i++)
        {
            heartMonitorSound.Play();
            yield return new WaitForSeconds(1.0f);
        }
    }

    // Switches the push and pinch area active when patient is on stretcher
    void SwitchActive(bool value)
    {
        if(pushArea.gameObject.activeInHierarchy != value)
        {
            pushArea.gameObject.SetActive(value);
            pinchArea.gameObject.SetActive(value);
        }
    }

    // Function when patient is alive, and player wins
    void PatientIsHealthy()
    {
        inCondition = false;
        currentHealth = 90;
        effectText.GetComponent<TextMesh>().text = "Patient alive";
        bodyController.SetBool("Breathe", true);

        StartCoroutine(HeartRateSound());
    }

    // Function when patient is dead, and player loses
    void PatientIsDead()
    {
        currentHealth = 0;
        inCondition = false;
        effectText.GetComponent<TextMesh>().text = "Patient is dead";

        if(heartMonitorSound.isPlaying == false && heartMonitorSound.clip != deathSound)
        {
            heartMonitorSound.clip = deathSound;
            heartMonitorSound.loop = false;
            heartMonitorSound.Play();
        }
    }
}
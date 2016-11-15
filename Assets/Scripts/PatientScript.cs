using UnityEngine;
using System.Collections;

public class PatientScript : MonoBehaviour
{
    public GameObject progressBar;
    public GameObject respirationBar;

    public GameObject effectText;
    public GameObject greenLight;
    public GameObject redLight;

    public Transform pushArea;
    public Transform pinchArea;

    public AudioSource heartMonitorSound;
    public AudioClip deathSound;

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

        timer = 50.0f;
        effectCoroutine = checkEffective();
        effectiveness = 5;
        
        bodyController = GetComponent<Animator>();
        SwitchActive(false);
	}

    void Update()
    {
        if (inCondition)
        {
            if (isOnStretcher)
            {
                progressBar.transform.localScale = new Vector3(timer / 100, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
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

        if (timer > 90)
        {
            timer = 90;
            chance = Random.Range(chanceInrease, 5);
            chanceInrease++;

            if (chance == 4)
            {
                needsRespiration = true;
                effectText.GetComponent<TextMesh>().text = "Respire now";
            }
        }
        else if(timer <= 0)
        {
            PatientIsDead();
        }
        else
        {
            if (timer > 0.05f)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 0.0f;
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
            timer -= 5;
        }
        else
        {
            timeCompres += 1;

            if (timer <= 90)
            {
                timer += effectiveness;

                if (effectiveness < 6)
                {
                    timeUnsucCompres += 1;
                    redLight.SetActive(true);
                    StartCoroutine(OffAfterSeconds(0.5f, redLight));
                }
                if (effectiveness == 10)
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
        effectiveness = 10;
        yield return new WaitForSeconds(timeBetween);
        effectiveness = 5;
    }

    IEnumerator OffAfterSeconds(float sec, GameObject _obj)
    {
        yield return new WaitForSeconds(sec);
        _obj.SetActive(false);
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
        timer = 90;
        effectText.GetComponent<TextMesh>().text = "Patient alive";

        heartMonitorSound.Play();
        heartMonitorSound.loop = true;

        SwitchAnimation("Breathe", true);

        if(heartMonitorSound.isPlaying == false)
        {
            heartMonitorSound.loop = true;
            heartMonitorSound.Play();
        } 
    }

    void PatientIsDead()
    {
        timer = 0;
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
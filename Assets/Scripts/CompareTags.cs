using UnityEngine;
using System.Collections;

public class CompareTags : MonoBehaviour
{
    /* Script that allows an object to snap into place when it collides
    * with a SnapPosition with the right tag
    */
    public string snapableObject;
    public bool canSnap;
    public bool isRespirationArea;

    void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
    void OnTriggerStay(Collider col)
    {
        GetComponent<MeshRenderer>().enabled = true;
        
        if(isRespirationArea)
        {
            transform.parent.GetComponent<PatientScript>().respiration();
        }
        if(snapableObject == col.tag && canSnap)
        {
            SnapToPosition(col);
        }
    }

    void OnTriggerExit(Collider col)
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
    
    private void SnapToPosition(Collider col)
    {
        GetComponent<MeshRenderer>().enabled = false;
        col.transform.position = this.transform.position;
        col.transform.rotation = this.transform.rotation;
        col.transform.SetParent(this.transform.parent);

        if (col.CompareTag("Patient"))
        {
            col.GetComponent<PatientScript>().isOnStretcher = true;
            gameObject.SetActive(false);
        }
    }
}

using UnityEngine;
using System.Collections;

public class CompareTags : MonoBehaviour
{
    /* Script that allows an object to snap into place when it collides
    * with a SnapPosition with the right tag
    */
    public string snapableObject;

    void OnTriggerStay(Collider col)
    {
        this.GetComponent<MeshRenderer>().enabled = true;
        
        if(snapableObject == col.tag)
        {
            SnapToPosition(col);
        }
    }

    void OnTriggerExit(Collider col)
    {
        this.GetComponent<MeshRenderer>().enabled = false;
    }
    
    private void SnapToPosition(Collider col)
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        col.transform.position = this.transform.position;
        col.transform.rotation = this.transform.rotation;
        col.transform.SetParent(this.transform.parent);

        if (col.CompareTag("Patient"))
        {
            col.GetComponent<PatientScript>().isOnStretcher = true;
            this.gameObject.SetActive(false);
        }
    }
}

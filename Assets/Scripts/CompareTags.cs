using UnityEngine;
using System.Collections;

public class CompareTags : MonoBehaviour
{
    /* Script that allows an object to snap into place when it collides
    * with a SnapPosition and the Trigger button on the vive controller
    * is released (patient only). Unfortunately a link must be made with the PatientScript
    * to make this happen for the patient.
    */

    // List of bools for special SnapPositions

    void OnTriggerStay(Collider col)
    {
        PreSnapping(col);
        
        if(col.CompareTag("Patient"))
        {
            col.GetComponent<PatientScript>().isOnStretcher = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Patient"))
        {
            col.GetComponent<PatientScript>().isColSnap = false;
        }
        this.GetComponent<MeshRenderer>().enabled = false;
    }
    
    void PreSnapping(Collider _col)
    {
        this.GetComponent<MeshRenderer>().enabled = true;

        if (_col.CompareTag("Patient"))
        {
            _col.GetComponent<PatientScript>().isColSnap = true;
            if (_col.GetComponent<PatientScript>().canSnap)
            {
                SnapToPosition(_col);
            }
        }
        
    }
    
    private void SnapToPosition(Collider col)
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        col.transform.position = this.transform.position;
        col.transform.rotation = this.transform.rotation;
        col.transform.SetParent(this.transform.parent);
    }
}

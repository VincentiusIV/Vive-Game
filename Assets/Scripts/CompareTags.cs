using UnityEngine;
using System.Collections;

public class CompareTags : MonoBehaviour
{
    /* Script that allows an object to snap into place when it collides
    * with a SnapPosition and the Trigger button on the vive controller
    * is released
    */

    // List of bools for special SnapPositions
    public bool isStretcher;

    void OnTriggerStay(Collider col)
    {
        PreSnapping(col);
        
        if(isStretcher && col.CompareTag("Patient"))
        {
            col.GetComponent<PatientScript>().isOnStretcher = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        col.GetComponent<PatientScript>().isColSnap = false;
        this.GetComponent<MeshRenderer>().enabled = false;
    }
    
    void PreSnapping(Collider _col)
    {
        _col.GetComponent<PatientScript>().isColSnap = true;
        this.GetComponent<MeshRenderer>().enabled = true;

        if (_col.GetComponent<PatientScript>().canSnap)
        {
            SnapToPosition(_col);
            this.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    
    private void SnapToPosition(Collider col)
    {
        col.transform.position = this.transform.position;
        col.transform.rotation = this.transform.rotation;
        col.transform.SetParent(this.transform.parent);
    }
}

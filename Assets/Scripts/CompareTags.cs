using UnityEngine;
using System.Collections;

public class CompareTags : MonoBehaviour
{
    /* Script that allows an object to snap into place when it collides
    * with a SnapPosition and the Trigger button on the vive controller
    * is released
    */
    
    // Bool that lets the InputScript know if this object collided with SnapPosition
    public bool isColSnap;
    
    // Bool that is set in the InputScript when the trigger button is released
    public bool canSnap;

    // List of bools for special SnapPositions
    public bool isStretcher;

    void OnTriggerStay(Collider col)
    {
        PreSnapping(col);

        if(isStretcher)
        {
            col.GetComponent<PatientScript>().isOnStretcher = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        isColSnap = false;
    }
    
    void PreSnapping(Collider _col)
    {
        isColSnap = true;

        this.GetComponent<MeshRenderer>().enabled = true;

        if (canSnap)
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

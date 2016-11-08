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

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("SnapPosition"))
        {
            Debug.Log("Other tag is SnapPosition");
            
            isColSnap = true;

            Stretcher(col);
            
            if(canSnap)
            {
                SnapToPosition(col);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        isColSnap = false;
    }

    private void SnapToPosition(Collider col)
    {
        col.GetComponent<MeshRenderer>().enabled = false;
        this.transform.position = col.transform.position;
        this.transform.rotation = col.transform.rotation;
        this.transform.SetParent(null);
    }

    void Stretcher(Collider _col)
    {
        if (_col.CompareTag("Stretcher"))
        {
            _col.GetComponent<MeshRenderer>().enabled = true;
            _col.GetComponent<PatientScript>().isOnStretcher = true;
            this.transform.SetParent(_col.transform.parent);
        }
    }
}

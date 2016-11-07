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

            col.GetComponent<MeshRenderer>().enabled = true;
            this.transform.SetParent(col.transform.parent);

            if(canSnap)
            {
                SnapToPosition(col);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        isColSnap = false;
        col.GetComponent<MeshRenderer>().enabled = false;
        this.transform.SetParent(null);
    }

    private void SnapToPosition(Collider col)
    {
        this.transform.position = col.transform.position;
        this.transform.rotation = col.transform.rotation;
        this.transform.SetParent(null);
    }
}

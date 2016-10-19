using UnityEngine;
using System.Collections;

public class CompareTags : MonoBehaviour
{
    /* Script that allows an object to snap into place when it collides
    * with a SnapPosition and the Trigger button on the vive controller
    * is released
    */
    
    // Bool that lets the PickUp script know if this object collided with SnapPosition
    public bool isColSnap;

    // Bool that is set in the PickUp script when the trigger button is released
    public bool canSnap;

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("SnapPosition"))
        {
            Debug.Log("Other tag is SnapPosition");
            
            isColSnap = true;

            // TO DO: Instantiate object to give player visual feedback that it can snap the held object into place
            if(canSnap)
            {
                SnapToPosition(col);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        isColSnap = false;
        // TO DO: Destroy the instantiated object to let the player know it can no longer snap the held object into that place
    }

    private void SnapToPosition(Collider col)
    {
        this.transform.position = col.transform.position;
        this.transform.rotation = col.transform.rotation;
        this.transform.SetParent(null);
    }
}

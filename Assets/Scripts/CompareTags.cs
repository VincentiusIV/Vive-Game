using UnityEngine;
using System.Collections;

public class CompareTags : MonoBehaviour
{
    public bool isColSnap;
    public bool canSnap;

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("SnapPosition"))
        {
            Debug.Log("Other tag is SnapPosition");
            
            isColSnap = true;

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
        this.transform.position = col.transform.position;
        this.transform.rotation = col.transform.rotation;
        this.transform.SetParent(null);
    }
}

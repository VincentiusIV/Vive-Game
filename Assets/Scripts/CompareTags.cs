using UnityEngine;
using System.Collections;

public class CompareTags : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("SnapPosition"))
        {
            Debug.Log("Other tag is SnapPosition");
            col.GetComponent<SnapScript>().SnapToPosition(col.gameObject);
        }
    }
}

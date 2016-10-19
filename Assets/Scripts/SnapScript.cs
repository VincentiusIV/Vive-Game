using UnityEngine;
using System.Collections;

public class SnapScript : MonoBehaviour
{
    private Transform snapPosition;
    public GameObject parent;

    void Start()
    {
        snapPosition = this.transform;

        if(parent = null)
        {
            Debug.Log("No parent set for: " + this.gameObject.name);
        }
    }

    public void SnapToPosition(GameObject _obj)
    {
        _obj.transform.position = snapPosition.position;
        _obj.transform.rotation = snapPosition.rotation;
        _obj.transform.SetParent(parent.transform);
    }
}

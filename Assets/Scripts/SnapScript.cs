using UnityEngine;
using System.Collections;

public class SnapScript : MonoBehaviour
{
    private Transform snapPosition;
    public GameObject parent;

    public void SnapToPosition(GameObject _obj)
    {
        snapPosition = this.transform;

        _obj.transform.position = snapPosition.position;
        _obj.transform.rotation = snapPosition.rotation;
        _obj.transform.SetParent(null);
    }
}

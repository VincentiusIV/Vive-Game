using UnityEngine;
using System.Collections;

public class SnapScript : MonoBehaviour
{
    private Transform snapPosition;

    void Start()
    {
        snapPosition = this.transform;
    }

    public void SnapToPosition(GameObject _obj)
    {
        _obj.transform.position = snapPosition.position;
        _obj.transform.rotation = snapPosition.rotation;
    }
}

using UnityEngine;
using System.Collections;

public class RotationScript : MonoBehaviour
{
    public float speed;

	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * speed, 0f));
	}
}

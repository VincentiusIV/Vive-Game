using UnityEngine;

// Simple script that allows an object to be rotated at a fixed speed
public class RotationScript : MonoBehaviour
{
    public float speed;

	void Update ()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * speed, 0f));
	}
}

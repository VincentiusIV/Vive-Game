using UnityEngine;

// Script to preview menu items such as a scene or patient
public class MenuPreviewer : MonoBehaviour
{
    // Array of all the possible preview options
    public GameObject[] previewObjects;

    public bool hasAnimation;
    public string idleBool;
    
    void Awake()
    {
        turnOffPreviews();
    }

    // Called from a button when its touched, hands the item id as a parameter
    public void SwitchPreview(int id)
    {
        turnOffPreviews();
        previewObjects[id].SetActive(true);

        if(hasAnimation)
        {
            previewObjects[id].GetComponent<Animator>().SetBool(idleBool, true);
        }
    }

    // All previews are turned off
    void turnOffPreviews()
    {
        foreach (GameObject item in previewObjects)
        {
            item.SetActive(false);
        }
    }
}

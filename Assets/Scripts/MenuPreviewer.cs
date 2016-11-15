using UnityEngine;
using System.Collections;

public class MenuPreviewer : MonoBehaviour
{
    public GameObject[] previewObjects;

    public bool hasAnimation;
    public string idleBool;
    
    void Awake()
    {
        turnOffPreviews();
    }

    public void SwitchPreview(int id)
    {
        turnOffPreviews();
        previewObjects[id].SetActive(true);

        if(hasAnimation)
        {
            previewObjects[id].GetComponent<Animator>().SetBool(idleBool, true);
        }
    }

    void turnOffPreviews()
    {
        foreach (GameObject item in previewObjects)
        {
            item.SetActive(false);
        }
    }
}

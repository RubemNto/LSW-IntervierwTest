using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StoreCanvas : MonoBehaviour
{
    public Image[] bodyPart;
    public Interactable interactable;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < bodyPart.Length; i++)
        {
            bodyPart[i].sprite = GameObject.Find("Player").transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            interactable.interact = false;
            interactable.interacting = false;
            GameObject.Find("Player").GetComponent<playerMovement>().movable = true;
            GameObject.Find("StoreManager").GetComponent<StoreManager>().removePreview();
            gameObject.SetActive(false);
        }
    }
}

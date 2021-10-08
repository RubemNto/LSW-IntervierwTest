using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool interact = false;
    public bool interacting = false;
    [SerializeField]
    private GameObject interactBuble;
    [SerializeField]
    private GameObject interactableUI;

    // Start is called before the first frame update
    void Start()
    {
        interactBuble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interact == true)
        {
            interact = false;
            interacting = true;
            interactBuble.SetActive(false);
            GameObject.Find("Player").GetComponent<playerMovement>().movable = false;
            interactableUI.SetActive(true);
            GameObject.Find("StoreManager").GetComponent<StoreManager>().definePrevious();
            if (GameObject.Find("StoreManager").GetComponent<StoreManager>().Sell == true)
            {
                GameObject.Find("StoreManager").GetComponent<StoreManager>().MakeSellItems();
            }
            else if (GameObject.Find("StoreManager").GetComponent<StoreManager>().Buy == true)
            {
                GameObject.Find("StoreManager").GetComponent<StoreManager>().MakeBuyItems();
            }
        }
        if (interacting == false)
        {
            if (interact == true)
            {
                interactBuble.SetActive(true);
            }
            else
            {
                interactBuble.SetActive(false);
            }
        }
    }
}

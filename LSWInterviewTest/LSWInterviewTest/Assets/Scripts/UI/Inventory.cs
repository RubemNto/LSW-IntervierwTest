using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image[] playerParts = new Image[3];
    public Button[] inventoryHeaderButtons = new Button[3];
    public Transform Content;
    [Range(1, 3)] public int inventoryState = 1;
    public GameObject player;
    public GameObject ItemLineTemplate;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = gameManager.player;
        //Set Inventory Buttons Listener Functions
        inventoryHeaderButtons[0].onClick.AddListener(() => { setInventoryState(1); });
        inventoryHeaderButtons[1].onClick.AddListener(() => { setInventoryState(2); });
        inventoryHeaderButtons[2].onClick.AddListener(() => { setInventoryState(3); });

        GenerateInventory();

        //set the default inventory view for the player => heads
        gameObject.SetActive(false);
    }

    public void GenerateInventory()
    {
        for (int i = 0; i < Content.childCount; i++)
        {
            Destroy(Content.GetChild(i).gameObject);
        }

        //Create all inventory Lines
        List<ShopItem> heads = new List<ShopItem>();
        List<ShopItem> torsos = new List<ShopItem>();
        List<ShopItem> legs = new List<ShopItem>();

        foreach (ShopItem item in gameManager.PlayerItems)
        {
            if (item.Head == true)
            {
                heads.Add(item);
            }
            else if (item.Torso == true)
            {
                torsos.Add(item);
            }
            else
            {
                legs.Add(item);
            }
        }
        //create inventory lines
        for (int line = 0; line < 3; line++)
        {
            GameObject tempLine = Instantiate(ItemLineTemplate, Content.position, Content.rotation);
            if (line == 0) tempLine.tag = "HeadLine"; else if (line == 1) tempLine.tag = "TorsoLine"; else tempLine.tag = "LegsLine";
            tempLine.transform.SetParent(Content);
            tempLine.transform.GetChild(0).gameObject.SetActive(true);
            if (line == 0)
            {
                tempLine.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = heads[0].itemImage;
                // tempLine.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { equipItem(heads[0].gameImage, gameManager.player.transform.GetChild(0).gameObject); });
            }
            else if (line == 1)
            {
                tempLine.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = torsos[0].itemImage;
                // tempLine.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { equipItem(torsos[0].gameImage, gameManager.player.transform.GetChild(2).gameObject); });
            }
            else if (line == 2)
            {
                tempLine.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = legs[0].itemImage;
                // tempLine.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { equipItem(legs[0].gameImage, gameManager.player.transform.GetChild(1).gameObject); });
            }


            int i,b;
            for (i = 1, b = 0; line == 0 ? i < heads.Count : line == 1 ? i < torsos.Count : i < legs.Count; i++)
            {
                // if (line == 0) Debug.Log(heads.Count); else if (line == 1) Debug.Log(torsos.Count); else Debug.Log(legs.Count);
                if (i % 4 == 0)
                {
                    b = 0;
                    tempLine = Instantiate(ItemLineTemplate, Content.position, Content.rotation);
                    if (line == 0) tempLine.tag = "HeadLine"; else if (line == 1) tempLine.tag = "TorsoLine"; else tempLine.tag = "LegsLine";
                    tempLine.transform.SetParent(Content);
                    tempLine.transform.GetChild(0).gameObject.SetActive(true);
                    if (line == 0)
                    {
                        tempLine.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = heads[0].itemImage;
                        // tempLine.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { equipItem(heads[0].gameImage, gameManager.player.transform.GetChild(0).gameObject); });
                    }
                    else if (line == 1)
                    {
                        tempLine.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = torsos[0].itemImage;
                        // tempLine.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { equipItem(torsos[0].gameImage, gameManager.player.transform.GetChild(2).gameObject); });
                    }
                    else if (line == 2)
                    {
                        tempLine.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = legs[0].itemImage;
                        // tempLine.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { equipItem(legs[0].gameImage, gameManager.player.transform.GetChild(1).gameObject); });
                    }
                }
                else
                {
                    b += 1;
                    tempLine.transform.GetChild(b).gameObject.SetActive(true);
                    if (line == 0)
                    {
                        tempLine.transform.GetChild(b).transform.GetChild(0).GetComponent<Image>().sprite = heads[i].itemImage;
                        //tempLine.transform.GetChild(b).transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { equipItem(heads[i].gameImage, gameManager.player.transform.GetChild(0).gameObject,i); });
                    }
                    else if (line == 1)
                    {
                        tempLine.transform.GetChild(b).transform.GetChild(0).GetComponent<Image>().sprite = torsos[i].itemImage;
                        //tempLine.transform.GetChild(b).transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { equipItem(torsos[i].gameImage, gameManager.player.transform.GetChild(2).gameObject,i); });
                    }
                    else if (line == 2)
                    {
                        tempLine.transform.GetChild(b).transform.GetChild(0).GetComponent<Image>().sprite = legs[i].itemImage;
                        //tempLine.transform.GetChild(b).transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { equipItem(legs[i].gameImage, gameManager.player.transform.GetChild(1).gameObject,i); });
                    }
                }
            }
        }
        setInventoryState(inventoryState);
    }

    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            playerParts[i].sprite = player.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;
        }

        switch (inventoryState)
        {
            case 1:
                inventoryHeaderButtons[0].interactable = false;
                inventoryHeaderButtons[1].interactable = true;
                inventoryHeaderButtons[2].interactable = true;
                break;
            case 2:
                inventoryHeaderButtons[1].interactable = false;
                inventoryHeaderButtons[0].interactable = true;
                inventoryHeaderButtons[2].interactable = true;
                break;
            case 3:
                inventoryHeaderButtons[2].interactable = false;
                inventoryHeaderButtons[0].interactable = true;
                inventoryHeaderButtons[1].interactable = true;
                break;
        }
    }

    //public void equipItem(Sprite sprite, GameObject BodyPart,int i = 0)
    //{
    //    Debug.Log(i);
    //    BodyPart.GetComponent<SpriteRenderer>().sprite = sprite;
    //}

    private void setInventoryState(int state)
    {
        if (state <= 0 || state >= 4)
        {
            throw new System.Exception("State provided is out of available Range");
        }
        inventoryState = state;

        for (int i = 0; i < Content.childCount; i++)
        {
            switch (inventoryState)
            {
                //get head lines
                case 1:
                    if (Content.GetChild(i).tag == "HeadLine")
                    {
                        Content.GetChild(i).gameObject.SetActive(true);
                    }
                    else
                    {
                        Content.GetChild(i).gameObject.SetActive(false);
                    }
                    break;
                //get  torso lines
                case 2:
                    if (Content.GetChild(i).tag == "TorsoLine")
                    {
                        Content.GetChild(i).gameObject.SetActive(true);
                    }
                    else
                    {
                        Content.GetChild(i).gameObject.SetActive(false);
                    }
                    break;
                //get leg lines
                case 3:
                    if (Content.GetChild(i).tag == "LegsLine")
                    {
                        Content.GetChild(i).gameObject.SetActive(true);
                    }
                    else
                    {
                        Content.GetChild(i).gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }
}

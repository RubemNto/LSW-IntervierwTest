using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private string playerName;
    [SerializeField]
    private float playerMoney;
    [SerializeField]
    private List<ShopItem> items;
    public List<ShopItem> Items => items;

    public List<ShopItem> playerItems;
    public List<ShopItem> PlayerItems => playerItems;
    [SerializeField]
    private TextAsset itemsPurchasableInfo;
    [SerializeField]
    private TextAsset purchasedItems;
    [SerializeField]
    private TextAsset playerData;
    public TextAsset ItemsPurchasableInfo => itemsPurchasableInfo;
    public TextAsset PurchasedItems => purchasedItems;
    public TextAsset PlayerData => playerData;

    public GameObject inventory;
    public GameObject player;
    private void Awake()
    {
        player = GameObject.Find("Player");
        string pInfo = "DataFiles/Items purchasable info.txt";//AssetDatabase.GetAssetPath(itemsPurchasableInfo);
        string pItems = "DataFiles/Purchased Items.txt";//AssetDatabase.GetAssetPath(purchasedItems);
        string pData = "DataFiles/Player Data.txt";//AssetDatabase.GetAssetPath(playerData);

        //check if item have been sold to player and modify it's value
        checkPlayerData(pData);
        checkItemsInfo(pInfo);
        checkPlayerItems(pItems);

        inventory = Instantiate(inventory, transform.position, transform.rotation);
        SetPlayer();
        DontDestroyOnLoad(GameObject.Find("backgroundMusic"));

        GameObject.Find("gameUI").transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Money: " + playerMoney.ToString();

        DontDestroyOnLoad(GameObject.Find("gameUI"));

    }    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        GameObject.Find("gameUI").transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Money: " + playerMoney.ToString();
        //Check Inventory
        if (Input.GetKeyDown(KeyCode.I) && inventory.activeInHierarchy == false)
        {
            inventory.SetActive(true);
            inventory.GetComponent<Inventory>().GenerateInventory();
            player.GetComponent<playerMovement>().movable = false;
            for (int i = 0; i < 3; i++)
            {
                player.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = -1;
            }

        }
        else if (Input.GetKeyDown(KeyCode.I) && inventory.activeInHierarchy == true)
        {
            inventory.SetActive(false);
            player.GetComponent<playerMovement>().movable = true;
            for (int i = 0; i < 3; i++)
            {
                player.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 3;
            }
        }
    }

    public void checkItemsInfo(string pInfo)
    {
        if (File.Exists(pInfo) == true)
        {
            string[] fData = File.ReadAllLines(pInfo);
            foreach (string line in fData)
            {
                string[] info = line.Split(' ');
                bool purchasedInfo = System.Convert.ToBoolean(int.Parse(info[1]));
                items[int.Parse(info[0])].purchased = purchasedInfo;
            }
        }
    }

    public void checkPlayerItems(string pItems)
    {
        playerItems = new List<ShopItem>();
        // Generate Player items list
        if (File.Exists(pItems) == true)
        {
            string[] fData = File.ReadAllLines(pItems);
            foreach (string line in fData)
            {
                int itemIndexer = int.Parse(line);
                playerItems.Add(items[itemIndexer]);
            }
        }
    }

    public void checkPlayerData(string pData)
    {
        if (File.Exists(pData) == true)
        {
            string[] fData = File.ReadAllLines(pData);
            playerName = fData[0];
            playerMoney = float.Parse(fData[1]);
        }
    }

    public void SetPlayer()
    {
        foreach (ShopItem item in playerItems)
        {
            if (item.equiped == true)
            {
                if (item.Head == true)
                {
                    player.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = item.gameImage;
                    player.transform.GetChild(0).gameObject.GetComponent<Animator>().runtimeAnimatorController = item.animatorController;
                }
                else if (item.Torso == true)
                {
                    player.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite = item.gameImage;
                    player.transform.GetChild(2).gameObject.GetComponent<Animator>().runtimeAnimatorController = item.animatorController;
                }
                else
                {
                    player.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = item.gameImage;
                    player.transform.GetChild(1).gameObject.GetComponent<Animator>().runtimeAnimatorController = item.animatorController;
                }
            }
        }

    }

    public bool spendMoney(float value)
    {
        if (value < playerMoney)
        {
            playerMoney -= value;
            if (File.Exists("DataFiles/PlayerData.txt"))
            {
                string[] data = File.ReadAllLines("DataFiles/PlayerData.txt");
                data[1] = playerMoney.ToString();
                File.WriteAllLines("DataFiles/PlayerData.txt", data);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GainMoney(float value)
    {
        playerMoney += value;
        if (File.Exists("DataFiles/PlayerData.txt"))
        {
            string[] data = File.ReadAllLines("DataFiles/PlayerData.txt");
            data[1] = playerMoney.ToString();
            File.WriteAllLines("DataFiles/PlayerData.txt", data);
        }
    }


}

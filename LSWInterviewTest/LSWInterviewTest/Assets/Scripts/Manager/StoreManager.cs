using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    private List<ShopItem> items = new List<ShopItem>();
    private List<ShopItem> playerItems = new List<ShopItem>();
    //private List<int> itemsIndex = new List<int>();
    private string dataPath;
    private string purchasedPath;
    private Sprite previousHead, previousTorso, previousLeg;
    private RuntimeAnimatorController previousHeadAnimatorController, previousTorsoAnimatorController, previousLegAnimatorController;
        
    [SerializeField]
    private GameObject itemContainerPrefab;
    public GameObject itemContainerParent;

    [SerializeField]
    private GameObject PlayerUI;
    [SerializeField]
    private GameObject StoreCanvas;

    public bool Sell, Buy;

    // Start is called before the first frame update
    void Start()
    {
        items = GameObject.Find("GameManager").GetComponent<GameManager>().Items;
        playerItems = GameObject.Find("GameManager").GetComponent<GameManager>().PlayerItems;
        

        string[] lines;
        if (File.Exists(dataPath) == true)
        {
            lines = File.ReadAllLines(dataPath);
            foreach (string item in lines)
            {
                string[] data = item.Split(' ');
                //itemsIndex.Add(int.Parse(data[0]));
                items[int.Parse(data[0])].purchased = System.Convert.ToBoolean(int.Parse(data[1]));
            }
        }
        //if (Buy == true)
        //{
        //    MakeBuyItems();
        //}
        //else if (Sell == true)
        //{
        //    MakeSellItems();
        //}

        StoreCanvas.SetActive(false);

    }

    //Preview Functions
    public void PreviewHead(int itemIndex)
    {
        Debug.Log("Preview Head");
        GameObject.Find("GameManager").GetComponent<GameManager>().player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = items[itemIndex].gameImage;
        GameObject.Find("GameManager").GetComponent<GameManager>().player.transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = items[itemIndex].animatorController;
    }
    public void PreviewTorso(int itemIndex)
    {
        Debug.Log("Preview Torso");
        GameObject.Find("GameManager").GetComponent<GameManager>().player.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = items[itemIndex].gameImage;
        GameObject.Find("GameManager").GetComponent<GameManager>().player.transform.GetChild(2).GetComponent<Animator>().runtimeAnimatorController = items[itemIndex].animatorController;

    }
    public void PreviewLeg(int itemIndex)
    {
        Debug.Log("Preview Leg");
        GameObject.Find("GameManager").GetComponent<GameManager>().player.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = items[itemIndex].gameImage;
        GameObject.Find("GameManager").GetComponent<GameManager>().player.transform.GetChild(1).GetComponent<Animator>().runtimeAnimatorController = items[itemIndex].animatorController;

    }

    public void definePrevious()
    {
        previousHead = GameObject.Find("GameManager").GetComponent<GameManager>().player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        previousTorso = GameObject.Find("GameManager").GetComponent<GameManager>().player.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite;
        previousLeg = GameObject.Find("GameManager").GetComponent<GameManager>().player.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;

        previousHeadAnimatorController = GameObject.Find("GameManager").GetComponent<GameManager>().player.transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController;
        previousTorsoAnimatorController = GameObject.Find("GameManager").GetComponent<GameManager>().player.transform.GetChild(2).GetComponent<Animator>().runtimeAnimatorController;
        previousLegAnimatorController = GameObject.Find("GameManager").GetComponent<GameManager>().player.transform.GetChild(1).GetComponent<Animator>().runtimeAnimatorController;
    }

    public void removePreview()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = previousHead;
        GameObject.Find("GameManager").GetComponent<GameManager>().player.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = previousTorso;
        GameObject.Find("GameManager").GetComponent<GameManager>().player.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = previousLeg;

        GameObject.Find("GameManager").GetComponent<GameManager>().player.transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = previousHeadAnimatorController;
        GameObject.Find("GameManager").GetComponent<GameManager>().player.transform.GetChild(2).GetComponent<Animator>().runtimeAnimatorController = previousTorsoAnimatorController;
        GameObject.Find("GameManager").GetComponent<GameManager>().player.transform.GetChild(1).GetComponent<Animator>().runtimeAnimatorController = previousLegAnimatorController;
    }

    public void MakeSellItems()
    {
        foreach (Transform child in itemContainerParent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ShopItem item in playerItems)
        {
            if (item.isDefaultItem == false || item.equiped)
            {
                GameObject temp = Instantiate(itemContainerPrefab, itemContainerParent.transform.position, itemContainerParent.transform.rotation);
                temp.transform.GetChild(0).GetComponent<Image>().sprite = item.itemImage;
                temp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "$ " + (item.cost / 2).ToString();
                temp.transform.GetChild(2).gameObject.SetActive(false);
                temp.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => SellItem(items.IndexOf(item), temp.transform.GetChild(3).gameObject));
                temp.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Sell Item";

                temp.transform.SetParent(itemContainerParent.transform);
            }
        }
    }

    public void MakeBuyItems() 
    {
        foreach (Transform child in itemContainerParent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ShopItem item in items)
        {
            GameObject temp = Instantiate(itemContainerPrefab, itemContainerParent.transform.position, itemContainerParent.transform.rotation);
            temp.transform.GetChild(0).GetComponent<Image>().sprite = item.itemImage;
            temp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "$ " + item.cost.ToString();

            temp.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => Purchase(items.IndexOf(item), temp.transform.GetChild(3).gameObject));

            if (item.Head == true)
            {
                temp.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => PreviewHead(items.IndexOf(item)));
            }
            else if (item.Torso == true)
            {
                temp.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => PreviewTorso(items.IndexOf(item)));
            }
            else if (item.Leg == true)
            {
                temp.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => PreviewLeg(items.IndexOf(item)));
            }

            if (item.purchased == true)
            {
                temp.transform.GetChild(3).gameObject.SetActive(false);
            }
            else
            {
                temp.transform.GetChild(3).gameObject.SetActive(true);
            }

            temp.transform.SetParent(itemContainerParent.transform);
        }
    }

    //Purchase Functions
    public void Purchase(int itemIndex, GameObject button)
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().spendMoney(items[itemIndex].cost) == true)
        {
            Debug.Log("Purchase");
            items[itemIndex].purchased = true;
            button.SetActive(false);
            string tempDataFile = "";
            foreach (ShopItem item in items)
            {
                tempDataFile = tempDataFile + items.IndexOf(item).ToString() + " " + System.Convert.ToInt32(item.purchased) + "\n";
            }
            File.WriteAllText(dataPath, tempDataFile);

            tempDataFile = File.ReadAllText(purchasedPath);
            tempDataFile = tempDataFile + itemIndex + "\n";
            File.WriteAllText(purchasedPath, tempDataFile);

            GameObject.Find("GameManager").GetComponent<GameManager>().checkPlayerItems("DataFiles/Purchased Items.txt");
        }

    }

    public void SellItem(int itemIndex, GameObject button)
    {
        Debug.Log("Sell Item");
        if (items[itemIndex].equiped == false)
        {
            items[itemIndex].purchased = false;
            button.SetActive(false);
            string tempDataFile = "";
            foreach (ShopItem item in items)
            {
                tempDataFile = tempDataFile + items.IndexOf(item).ToString() + " " + System.Convert.ToInt32(item.purchased) + "\n";
            }
            File.WriteAllText(dataPath, tempDataFile);

            string[] purchasedItemsLines = File.ReadAllLines(purchasedPath);
            List<string> purchasedItemsLinesList = new List<string>(purchasedItemsLines);
            for (int i = 0; i < purchasedItemsLines.Length; i++)
            {
                if (itemIndex == int.Parse(purchasedItemsLines[i]))
                {
                    purchasedItemsLinesList.RemoveAt(i);
                }
            }

            //tempDataFile = tempDataFile + itemIndex + "\n";
            File.WriteAllLines(purchasedPath, purchasedItemsLinesList.ToArray());
            GameObject.Find("GameManager").GetComponent<GameManager>().GainMoney(items[itemIndex].cost / 2f);
            //GameObject.Find("GameManager").GetComponent<GameManager>().checkPlayerItems(AssetDatabase.GetAssetPath(GameObject.Find("GameManager").GetComponent<GameManager>().PurchasedItems));
        }
        else Debug.Log("canot sell equiped item");
    }

}

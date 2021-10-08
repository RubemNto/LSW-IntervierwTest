using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryEquip : MonoBehaviour
{
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private string parentTag;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        parent = transform.parent;
        parentTag = parent.parent.tag;
        Sprite buttonItemImage = parent.GetChild(0).gameObject.GetComponent<Image>().sprite;
        GetComponent<Button>().onClick.AddListener(() => equipItem(buttonItemImage, parentTag));
    }

    private void equipItem(Sprite sprite, string tag)
    {
        int i = 0;
        foreach (ShopItem item in gameManager.PlayerItems)
        {
            if (item.itemImage.name == sprite.name)
            {
                break;
            }
            i++;
        }

        foreach (ShopItem item in gameManager.PlayerItems)
        {
            item.equiped = false;
        }

        if (tag == "HeadLine")
        {
            gameManager.player.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = gameManager.PlayerItems[i].gameImage;
            gameManager.player.transform.GetChild(0).gameObject.GetComponent<Animator>().runtimeAnimatorController = gameManager.PlayerItems[i].animatorController;
        }
        else if (tag == "TorsoLine")
        {
            gameManager.player.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite = gameManager.PlayerItems[i].gameImage;
            gameManager.player.transform.GetChild(2).gameObject.GetComponent<Animator>().runtimeAnimatorController = gameManager.PlayerItems[i].animatorController;
        }
        else
        {
            gameManager.player.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = gameManager.PlayerItems[i].gameImage;
            gameManager.player.transform.GetChild(1).gameObject.GetComponent<Animator>().runtimeAnimatorController = gameManager.PlayerItems[i].animatorController;
        }
        gameManager.PlayerItems[i].equiped = true;
        //Debug.Log(i);
        //BodyPart.GetComponent<SpriteRenderer>().sprite = sprite;
    }


}

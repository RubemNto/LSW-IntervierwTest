using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop Item")]
public class ShopItem : ScriptableObject
{
    public Sprite itemImage;
    public Sprite gameImage;

    public RuntimeAnimatorController animatorController;

    public float cost;
    public bool purchased;
    public bool Head, Torso, Leg;
    public bool isDefaultItem;
    public bool equiped;
}

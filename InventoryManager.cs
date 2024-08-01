using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{

    public GameObject InventoryMenu;
    public GameObject EquipmentMenu;
    //private bool InventoryIsOpen;
    public ItemSlot[] ItemSlots;
    public EquipmentSlot[] EquipmentSlots;
    public EquippedSlot[] EquippedSlots;
    public string ItemDescription;

    public ItemSO[] Items;


    // Start is called before the first frame update
    void Start()
    {
        //InventoryIsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Bag"))
        {
            OpenBag();
        }else if (Input.GetButtonDown("EquipmentBag"))
        {
            OpenEquipmentBag();
        }

    }

    //设置打开物品背包
    public void OpenBag()
    {
        if (!InventoryMenu.activeSelf)
        {
            InventoryMenu.SetActive(true);
            EquipmentMenu.SetActive(false);
            //InventoryIsOpen = true;
            Time.timeScale = 0;
        }
        else if (InventoryMenu.activeSelf)
        {
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);
            //InventoryIsOpen = false;
            Time.timeScale = 1;
        }
    }

    //设置打开装备背包
    public void OpenEquipmentBag()
    {
        if (!EquipmentMenu.activeSelf)
        {
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else if (EquipmentMenu.activeSelf)
        {
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public int AddItem(string ItemName, int ItemQuantity, Sprite ItemSprite, string ItemDescription, ItemType ItemType = ItemType.None)
    {
        //Debug.Log("Name is " + ItemName + " || Quantity is " + ItemQuantity + " || Sprite is " + ItemSprite);
        if (ItemType == ItemType.Consumable || ItemType == ItemType.Collectible || ItemType == ItemType.Crafting)
        {
            for (int i = 0; i < ItemSlots.Length; i++)
            {

                if (ItemSlots[i].SlotIsFull == false && ItemSlots[i].ItemName == ItemName || ItemSlots[i].ItemQuantity == 0)
                {
                    int LeftItems = ItemSlots[i].AddItem(ItemName, ItemQuantity, ItemSprite, ItemDescription, ItemType);
                    if (LeftItems > 0)
                    {
                        AddItem(ItemName, LeftItems, ItemSprite, ItemDescription, ItemType);
                    }
                    return LeftItems;
                }

            }
            return ItemQuantity;
        }
        else
        {
            for (int i = 0; i < EquipmentSlots.Length; i++)
            {

                if (EquipmentSlots[i].SlotIsFull == false && EquipmentSlots[i].ItemName == ItemName || EquipmentSlots[i].ItemQuantity == 0)
                {
                    int LeftItems = EquipmentSlots[i].AddItem(ItemName, ItemQuantity, ItemSprite, ItemDescription, ItemType);
                    if (LeftItems > 0)
                    {
                        AddItem(ItemName, LeftItems, ItemSprite, ItemDescription, ItemType);
                    }
                    return LeftItems;
                }

            }
            return ItemQuantity;
        }
        
    }

    public void DeSelectPanel()
    {
        if (InventoryMenu.activeSelf)
        {
            for (int i = 0; i < ItemSlots.Length; i++)
            {
                ItemSlots[i].SelectedPanel.SetActive(false);
                ItemSlots[i].PanelIsSelected = false;
            }
        }
        else if(EquipmentMenu.activeSelf)
        {
            for (int i = 0; i < EquipmentSlots.Length; i++)
            {
                EquipmentSlots[i].SelectedPanel.SetActive(false);
                EquipmentSlots[i].PanelIsSelected = false;
            }
            for(int i = 0; i < EquippedSlots.Length; i++)
            {
                EquippedSlots[i].SelectedPanel.SetActive(false);
                EquippedSlots[i].PanelIsSelected = false;
            }
        }

    }

    public bool UseItems(string ItemName)
    {
        for(int i = 0; i < Items.Length; i++)
        {
            if (Items[i].name == ItemName)
            {
                bool IAmNotFull = Items[i].UseItem();
                //Debug.Log("InventoryManager Normal");
                return IAmNotFull;
            }
        }
        return false;
    }

    public void DropStuff(string ItemName)
    {

        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].name == ItemName)
            {
                Vector3 V3 = GameObject.FindWithTag("Player").transform.position + new Vector3(2f ,2f ,0f );
                Instantiate(Items[i].ItemPrefab, V3, GameObject.FindWithTag("Player").transform.rotation);
            }
        }
    }
}

public enum ItemType
{
    Consumable,
    Crafting,
    Collectible,
    Head,
    Shirt,
    Body,
    Legs,
    MainHand,
    OffHand,
    Relic,
    Feet,
    None
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField]
    public string ItemName;
    [SerializeField]
    public int ItemQuanity;
    [SerializeField]
    public Sprite ItemSprite;
    [SerializeField]
    public string ItemDescription;

    private InventoryManager inventoryManager;
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("Canvas").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int LeftItems = inventoryManager.AddItem(ItemName, ItemQuanity, ItemSprite, ItemDescription);
            if(LeftItems <= 0) 
            {
                Destroy(gameObject);
            }
            else
            {
                LeftItems = ItemQuanity;
            }
            
        }
    }
}

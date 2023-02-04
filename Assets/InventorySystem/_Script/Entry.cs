using inventory;
using inventory_item;
using inventory_module;
using UnityEngine;

public class Entry : MonoBehaviour
{
    public Rigidbody item;

    void Start()
    {
        HUDSettings.Instance.Init();
        InventoryModule.Instance.InventoryInit(GameObject.Find("Capsule"));

        //外部无需知道捡到的具体是什么，只需要交给责任链处理
        //TODO:责任链
        ItemBase[] items = (ItemBase[])FindObjectsOfType(typeof(ItemBase));

        foreach (var i in items)
        {
            InventoryModule.Instance.AddItem(i.gameObject);
            Debug.Log("pick");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryModule.Instance.openInventory.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InventoryModule.Instance.closeInventory.Invoke();
        }

        float scrollWhell = Input.GetAxis("Mouse ScrollWheel");
        {
            if(scrollWhell != 0)
            {
                InventoryModule.Instance.ChangeCurrentItem();
            }
        }

        if(Input.GetMouseButtonDown(2))
        {
            InventoryModule.Instance.ChangeHoldState();
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            InventoryModule.Instance.DropCurrentItem();
        }


    }


}

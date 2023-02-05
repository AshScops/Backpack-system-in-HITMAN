using inventory;
using inventory_item;
using inventory_module;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entry : MonoBehaviour
{
    public Rigidbody item;
    public Dictionary<string, object> dic;
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

        dic = new Dictionary<string, object>();
        dic["direction"] = (Vector3.up + Vector3.forward).normalized;
        dic["velocitySize"] = 1.5f;
    }

    void Update()
    {
        if(Input.GetMouseButtonUp(1))
        {
            InventoryModule.Instance.CancelPrepareCurrentItem();
        }

        if (Input.GetMouseButton(1))
        {
            InventoryModule.Instance.PrepareCurrentItemAction(dic);
        }

        if (Input.GetMouseButtonDown(0))
        {
            InventoryModule.Instance.DoCurrentItemAction(dic);
        }


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

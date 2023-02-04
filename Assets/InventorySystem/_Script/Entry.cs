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
        InventoryModule.Instance.InventoryInit();

        //�ⲿ����֪���񵽵ľ�����ʲô��ֻ��Ҫ��������������
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
            InventoryModule.Instance.ShowInventory();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            InventoryModule.Instance.ChangeCurrentItem();
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            InventoryModule.Instance.DropCurrentItem();
        }
        if(Input.GetMouseButtonDown(2))
        {
            InventoryModule.Instance.ChangeHoldState();
        }

    }


}

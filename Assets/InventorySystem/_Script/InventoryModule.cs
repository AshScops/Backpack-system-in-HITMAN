using inventory;
using UnityEngine;
using inventory_item;
using UnityEngine.Events;

namespace inventory_module
{
    public class InventoryModule : Singleton<InventoryModule>
    {
        public UnityEvent openInventory = new UnityEvent();
        public UnityEvent closeInventory = new UnityEvent();

        public void InventoryInit(GameObject playerHandRoot)
        {
            Inventory.Instance.Init(playerHandRoot);
            InventoryUISettings.Instance.Init();

            openInventory.AddListener(() =>
            {
                InventoryUISettings.Instance.UpdateItemsList();
                InventoryUISettings.Instance.menu_root.visible = true;
                InventoryUISettings.Instance.inventory.visible = true;
            });

            closeInventory.AddListener(() =>
            {
                InventoryUISettings.Instance.menu_root.visible = false;
                InventoryUISettings.Instance.inventory.visible = false;
            });
        }

        public void AddItem(GameObject gameObject)
        {
            Inventory.Instance.AddItem(gameObject);
            InventoryUISettings.Instance.UpdateItemDispalyHUD();
        }

        /// <summary>
        /// ���л���Ʒ�������ı䵱ǰ��hold״̬
        /// </summary>
        public void ChangeCurrentItem()
        {
            Inventory.Instance.ChangeCurrentItem();
            InventoryUISettings.Instance.UpdateItemDispalyHUD();
        }

        /// <summary>
        /// ��hold_in_hand���򷵻ض�Ӧ��Ʒ�����򷵻�null
        /// </summary>
        /// <returns></returns>
        public void ChangeHoldState()
        {
            Inventory.Instance.ChangeHoldState();
            InventoryUISettings.Instance.UpdateHoldStateHUD();
        }

        public void DropCurrentItem()
        {
            Inventory.Instance.RemoveCurrentItem();
            InventoryUISettings.Instance.UpdateItemDispalyHUD();
        }



    }

}


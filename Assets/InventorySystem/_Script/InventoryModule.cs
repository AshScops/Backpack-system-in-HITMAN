using inventory;
using UnityEngine;
using inventory_item;

namespace inventory_module
{
    public class InventoryModule : Singleton<InventoryModule>
    {
        public void InventoryInit()
        {
            Inventory.Instance.Init(GameObject.Find("Capsule"));
            InventoryUISettings.Instance.Init();
        }

        public void ShowInventory()
        {
            InventoryUISettings.Instance.inventory.visible = true;
        }

        public void AddItem(GameObject gameObject)
        {
            Inventory.Instance.AddItem(gameObject);
            InventoryUISettings.Instance.UpdateItemDispalyHUD();
        }

        /// <summary>
        /// 仅切换物品，而不改变当前的hold状态
        /// </summary>
        public void ChangeCurrentItem()
        {
            Inventory.Instance.ChangeCurrentItem();
            InventoryUISettings.Instance.UpdateItemDispalyHUD();
        }

        /// <summary>
        /// 若hold_in_hand，则返回对应物品；否则返回null
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
        }



    }

}


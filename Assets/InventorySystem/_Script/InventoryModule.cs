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
        }



    }

}


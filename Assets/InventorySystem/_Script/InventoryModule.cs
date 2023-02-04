using inventory;
using UnityEngine;
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
        }

        public void AddItem(GameObject gameObject)
        {
            Inventory.Instance.AddItem(gameObject);
        }

        /// <summary>
        /// ���л���Ʒ�������ı䵱ǰ��hold״̬
        /// </summary>
        public void ChangeCurrentItem()
        {
            Inventory.Instance.ChangeCurrentItem();
        }

        /// <summary>
        /// ��hold_in_hand���򷵻ض�Ӧ��Ʒ�����򷵻�null
        /// </summary>
        /// <returns></returns>
        public void ChangeHoldState()
        {
            Inventory.Instance.ChangeHoldState();
        }

        public void DropCurrentItem()
        {
            Inventory.Instance.RemoveCurrentItem();
        }
    }

}


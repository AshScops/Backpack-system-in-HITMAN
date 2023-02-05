using inventory;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

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
        /// 仅切换物品，而不改变当前的hold状态
        /// </summary>
        public void ChangeCurrentItem()
        {
            Inventory.Instance.ChangeCurrentItem();
        }

        /// <summary>
        /// 若hold_in_hand，则返回对应物品；否则返回null
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

        public void CancelPrepareCurrentItem()
        {
            Inventory.Instance.CancelPrepareCurrentItem();
        }


        /// <summary>
        /// 
        /// </summary>
        public void PrepareCurrentItemAction(Dictionary<string, object> dic)
        {
            Inventory.Instance.PrepareCurrentItemAction(dic);
        }


        /// <summary>
        /// 
        /// </summary>
        public void DoCurrentItemAction(Dictionary<string, object> dic)
        {
            Inventory.Instance.DoCurrentItemAction(dic);
        }


    }

}


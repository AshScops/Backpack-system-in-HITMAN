using FairyGUI;
using inventory_item;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor.Search;
using UnityEngine;
using static UnityEditor.Progress;

namespace inventory
{
    public class Inventory : Singleton<Inventory>
    {
        public Dictionary<string, Queue<ItemBase>> items = new Dictionary<string, Queue<ItemBase>>();
        public ItemBase current_item = null;
        public bool hold_in_hand = false;
        private GameObject parentGO = null;

        public void Init(GameObject p)
        {
            if (p == null) Debug.Log("parentGO is set null.");
            this.parentGO = p;
        }

        /// <summary>
        /// 若当前物品为空，则入库并拿在手里；否则直接入库
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(GameObject gameObject)
        {
            if (gameObject == null || gameObject.GetComponent<ItemBase>() == null) return;

            ItemBase item = gameObject.GetComponent<ItemBase>();
            
            if (current_item == null)
            {
                current_item = item;
                hold_in_hand = true;
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }

            string itemType = item.GetType().ToString();
            if ( ! items.ContainsKey(itemType))
                items[itemType] = new Queue<ItemBase>();
            items[itemType].Enqueue(item);
            
            gameObject.transform.position = Vector3.zero;
            gameObject.transform.SetParent(parentGO.transform);
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            if(rb) rb.isKinematic = true;
        }

        /// <summary>
        /// 仅切换物品，而不改变当前的hold状态
        /// </summary>
        public void ChangeCurrentItem()
        {
            if (items.Count == 0) return;

            if (current_item == null)
            {
                current_item = items.ElementAt(0).Value.Peek();
            }
            else
            {
                current_item.gameObject.SetActive(false);
                string itemType = current_item.GetType().ToString();
                List<string> keys = new List<string>(items.Keys);
                int indexOfCurrentKey = keys.IndexOf(itemType);
                if (indexOfCurrentKey != -1 && indexOfCurrentKey != keys.Count - 1)
                {
                    string nextKey = keys[indexOfCurrentKey + 1];
                    current_item = items[nextKey].Peek();
                }
                else if(indexOfCurrentKey == keys.Count - 1)
                {
                    current_item = items.ElementAt(0).Value.Peek();
                }
                else
                {
                    Debug.Log("Current Key not found.");
                }
            }

            current_item.gameObject.SetActive(hold_in_hand);
            Debug.Log(current_item.gameObject);
        }

        /// <summary>
        /// 改变当前的hold状态
        /// </summary>
        /// <returns></returns>
        public void ChangeHoldState()
        {
            if (current_item == null) return ;
            hold_in_hand = !hold_in_hand;
            current_item.gameObject.SetActive(hold_in_hand);
        }

        /// <summary>
        /// 仅扔下并移除物品，而不改变当前的hold状态
        /// </summary>
        public void RemoveCurrentItem()
        {
            if (current_item == null || ! hold_in_hand) return ;

            string itemType = current_item.GetType().ToString();
            items[itemType].Dequeue();
            if (items[itemType].Count <= 0)
            {
                items.Remove(itemType);
            }
            current_item.gameObject.SetActive(false);
            current_item = null;
        }


    }

}

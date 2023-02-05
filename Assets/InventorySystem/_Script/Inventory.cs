using inventory_item;
using inventory_item_function;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace inventory
{
    public class Inventory : Singleton<Inventory>
    {
        private Dictionary<string, Queue<ItemBase>> items = new Dictionary<string, Queue<ItemBase>>();
        public ItemBase current_item = null;
        public bool hold_in_hand = false;
        private GameObject parentGO = null;

        public UnityEvent onHoldChange = new UnityEvent();
        public UnityEvent<ItemBase> onItemChange = new UnityEvent<ItemBase>();

        public void Init(GameObject p)
        {
            if (p == null) Debug.Log("parentGO is set null.");
            this.parentGO = p;
        }

        /// <summary>
        /// ����ǰ��ƷΪ�գ�����Ⲣ���У�����ֱ�����
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

                onItemChange?.Invoke(current_item);
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
        /// ���л���Ʒ�������ı䵱ǰ��hold״̬
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
            Debug.Log(current_item.item_name);

            onItemChange?.Invoke(current_item);
        }

        /// <summary>
        /// ����һ����Ʒ���࣬����ǰ��Ʒ�޸�Ϊ�������Ʒ�����ı䵱ǰ��hold״̬
        /// </summary>
        public void ChangeCurrentItem(ItemBase targetItem)
        {
            if (items.Count == 0 || targetItem == null) return;

            if (current_item != null)
            {
                current_item.gameObject.SetActive(false);
            }
            current_item = targetItem;
            current_item.gameObject.SetActive(hold_in_hand);
            Debug.Log(current_item.item_name);

            onItemChange?.Invoke(current_item);
        }

        /// <summary>
        /// �ı䵱ǰ��hold״̬
        /// </summary>
        /// <returns></returns>
        public void ChangeHoldState()
        {
            if (current_item == null) return ;
            hold_in_hand = !hold_in_hand;
            current_item.gameObject.SetActive(hold_in_hand);

            onHoldChange?.Invoke();
        }

        /// <summary>
        /// �����²��Ƴ���Ʒ�������ı䵱ǰ��hold״̬
        /// </summary>
        public ItemBase RemoveCurrentItem()
        {
            if (current_item == null || ! hold_in_hand) return null;

            string itemType = current_item.GetType().ToString();
            items[itemType].Dequeue();
            if (items[itemType].Count <= 0)
            {
                items.Remove(itemType);
            }
            current_item.transform.SetParent(null);
            Rigidbody rb = current_item.GetComponent<Rigidbody>();
            if (rb) rb.isKinematic = false;
            ItemBase res = current_item;
            current_item = null;

            onItemChange?.Invoke(null);
            return res;
        }



        private bool IsAvailableBeforeAction()
        {
            if (current_item == null || !hold_in_hand) return false;

            Type itemType = current_item.GetType();
            return typeof(IAction).IsAssignableFrom(itemType);
        }

        private IAction GetCurrentItemIAction()
        {
            return current_item as IAction;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CancelPrepareCurrentItem()
        {
            if (IsAvailableBeforeAction())
                GetCurrentItemIAction().CancelPrepare();
        }

        /// <summary>
        /// 
        /// </summary>
        public void PrepareCurrentItemAction(Dictionary<string, object> dic)
        {
            if (IsAvailableBeforeAction())
            {
                GetCurrentItemIAction().PrepareAction(dic);
                //Debug.Log("PrepareCurrentItemAction");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void DoCurrentItemAction(Dictionary<string, object> dic)
        {
            if (IsAvailableBeforeAction())
            {
                GetCurrentItemIAction().DoAction(dic);
                Debug.Log("DoCurrentItemAction");
            }
        }


        public ItemBase GetItemByIndex(int index)
        {
            return items.ElementAt(index).Value.Peek();
        }

        public int GetCountByIndex(int index)
        {
            return items.ElementAt(index).Value.Count;
        }

        public int GetItemTypesCount()
        {
            return items.Count;
        }


    }

}

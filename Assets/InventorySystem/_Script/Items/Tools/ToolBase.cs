using inventory_item_function;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace inventory_item
{
    public abstract class ToolBase : ItemBase, IAction
    {
        public void CancelPrepare()
        {
            throw new System.NotImplementedException();
        }

        public void DoAction(Dictionary<string, object> dic)
        {
            throw new System.NotImplementedException();
        }

        public void PrepareAction(Dictionary<string, object> dic)
        {
            throw new System.NotImplementedException();
        }
    }

}

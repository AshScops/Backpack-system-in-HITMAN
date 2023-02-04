using inventory_item_function;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace inventory_item
{
    public abstract class ToolBase : ItemBase, IThrowable
    {
        public abstract void Throw(Vector3 direction, float forceSize);
    }

}

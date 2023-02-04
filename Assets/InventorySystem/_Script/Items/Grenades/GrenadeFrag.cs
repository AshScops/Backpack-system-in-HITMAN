using System;
using System.Collections;
using UnityEngine;

namespace inventory_item
{
    public class GrenadeFrag: GrenadeBase
    {

        public override void Throw(Vector3 direction, float forceSize)
        {
            this.action_after_explosion = () =>
            {
                Debug.Log("this delay : " + this.delay + " this range : " + this.range);
                Debug.Log("delay : " + delay + "range : " + range);
                Debug.Log("ÆÆÆ¬ÊÖÀ×±¬Õ¨");
            };

            base.Throw(direction, forceSize);
        }


    }

}

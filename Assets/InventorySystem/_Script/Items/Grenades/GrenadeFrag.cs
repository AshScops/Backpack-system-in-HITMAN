using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace inventory_item
{
    public class GrenadeFrag: GrenadeBase
    {

        public override void DoAction(Dictionary<string, object> dic)
        {
            Debug.Log("GrenadeFrag DoAction");

            this.action_on_explosion = () =>
            {
                Debug.Log("this delay : " + this.delay + " this range : " + this.range);
                Debug.Log("delay : " + delay + "range : " + range);
                Debug.Log("GrenadeFragDoAction");
            };

            base.DoAction(dic);
        }


    }

}

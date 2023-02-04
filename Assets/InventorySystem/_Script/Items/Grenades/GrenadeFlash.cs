using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace inventory_item
{
    public class GrenadeFlash : GrenadeBase
    {
        
        public override void Throw(Vector3 direction, float forceSize)
        {
            this.action_after_explosion = () =>
            {
                Debug.Log("this delay : " + this.delay + " this range : " + this.range);
                Debug.Log("delay : " + delay + "range : " + range);
                Debug.Log("иа╧Б");
            };

            base.Throw(direction , forceSize);
        }


    }

}

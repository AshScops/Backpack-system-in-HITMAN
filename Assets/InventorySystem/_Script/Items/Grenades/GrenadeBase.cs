using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace inventory_item
{

    public abstract class GrenadeBase : ThrowableItemBase
    {
        public float delay;
        public float range;
        public Action action_on_explosion;


        public override void DoAction(Dictionary<string, object> dic)
        {
            action_after_throw = () =>
            {
                StartCoroutine(Explosion());
            };

            base.DoAction(dic);
        }


        private IEnumerator Explosion()
        {
            yield return new WaitForSeconds(delay);
            this.action_on_explosion.Invoke();
            Destroy(this.gameObject);
        }

    }


}

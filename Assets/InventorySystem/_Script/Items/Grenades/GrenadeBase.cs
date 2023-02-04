using inventory_item_function;
using System;
using System.Collections;
using UnityEngine;

namespace inventory_item
{

    public abstract class GrenadeBase : ItemBase , IThrowable
    {
        public float delay;
        public float range;
        public Action action_after_explosion;



        /// <summary>
        /// 要在父类方法调用前写明action方法
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="forceSize"></param>
        public virtual void Throw(Vector3 direction , float forceSize)
        {
            var rb = this.GetComponent<Rigidbody>();

            if (rb == null)
            {
                Debug.Log("The Grenade has no Rigidbody");
                return;
            }

            rb.AddForce(direction * forceSize, ForceMode.Impulse);

            StartCoroutine(Explosion());
        }


        private IEnumerator Explosion()
        {
            yield return new WaitForSeconds(delay);
            this.action_after_explosion.Invoke();
            Destroy(this.gameObject);
        }

    }


}

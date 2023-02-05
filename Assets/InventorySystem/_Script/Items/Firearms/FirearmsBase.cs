using inventory_item_function;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace inventory_item
{
    public abstract class FirearmsBase : ItemBase , IAction
    {
        public int ATK;
        public int max_magazine_capacity;
        public int max_inventory_capacity;
        public float reload_time;

        public float fire_sound_range;
        public float hit_sound_range;

        private int current_magazine_ammo;
        private int current_inventory_ammo;

        public void GainAmmo(int ammo)
        {
            if(ammo + current_inventory_ammo >= max_inventory_capacity)
            {
                current_inventory_ammo = max_inventory_capacity;
            }
            else
            {
                current_inventory_ammo += ammo;
            }
        }

        public void ReloadMagazine()
        {
            int emptyCapacity = max_magazine_capacity - current_magazine_ammo;
            if (emptyCapacity <= 0 || current_inventory_ammo <= 0) return;

            StartCoroutine(Reload(emptyCapacity));
        }

        private IEnumerator Reload(int emptyCapacity)
        {
            //TODO:EventAnimatorStart
            yield return new WaitForSeconds(reload_time);

            if (emptyCapacity <= current_inventory_ammo)
            {
                current_magazine_ammo += emptyCapacity;
                current_inventory_ammo -= emptyCapacity;
            }
            else
            {
                current_magazine_ammo += current_inventory_ammo;
                current_inventory_ammo = 0;
            }
        }

        public void Fire()
        {
            //if()
        }

        public void CancelPrepare()
        {
            throw new System.NotImplementedException();
        }

        public void PrepareAction(Dictionary<string, object> dic)
        {
            throw new System.NotImplementedException();
        }

        public void DoAction(Dictionary<string, object> dic)
        {
            throw new System.NotImplementedException();
        }
    }

}

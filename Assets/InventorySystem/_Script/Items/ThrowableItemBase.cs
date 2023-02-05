using inventory;
using inventory_item_function;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace inventory_item
{
    public enum ThrowableItemState
    {
        available = 0,
        prepared,
    }

    public abstract class ThrowableItemBase : ItemBase ,IAction
    {
        private ThrowableItemState state = ThrowableItemState.available;
        public Action action_after_throw = null;
        private Rigidbody rb = null;
        private LineRenderer line_renderer = null;

        /// <summary>
        /// 
        /// </summary>s
        public void CancelPrepare()
        {
            if (line_renderer != null)
            {
                line_renderer.enabled = false;
            }
            state = ThrowableItemState.available;
        }


        /// <summary>
        /// 要在父类方法调用前写明action方法
        /// </summary>
        public virtual void DoAction(Dictionary<string, object> dic)
        {
            if (state != ThrowableItemState.prepared || dic == null || dic.Count == 0) return;
            Vector3 direction = (Vector3)dic["direction"];
            float velocitySize = (float)dic["velocitySize"];

            rb = this.GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.Log("The Grenade has no Rigidbody");
                return;
            }

            //解除Inventory的持有状态
            Inventory.Instance.RemoveCurrentItem(); 
            CancelPrepare();

            //为了方便计算抛物线，这里使用速度而非力
            //rb.AddForce(direction * velocitySize, ForceMode.Impulse);
            Debug.Log("direction * velocitySize : " + direction * velocitySize);
            rb.velocity = direction * velocitySize;
            Debug.Log(rb.velocity);
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            action_after_throw?.Invoke();
        }


        /// <summary>
        /// 绘制抛物线
        /// </summary>
        /// <param name="dic"></param>
        public void PrepareAction(Dictionary<string, object> dic)
        {

            if (dic == null || dic.Count == 0) return;
            Vector3 direction = (Vector3)dic["direction"];
            float velocitySize = (float)dic["velocitySize"];

            line_renderer = GetComponent<LineRenderer>();
            if (line_renderer == null)
            {
                line_renderer = this.gameObject.AddComponent<LineRenderer>();
                line_renderer.material = new Material(Shader.Find("Sprites/Default"));
                line_renderer.startColor = Color.red;
                line_renderer.endColor = Color.red;
                line_renderer.startWidth = 0.01f;
                line_renderer.endWidth = 0.01f;
                line_renderer.numCornerVertices = 90;
                line_renderer.numCapVertices = 90;
                line_renderer.positionCount = 60;
            }
            line_renderer.enabled = true;

            Vector3 position = transform.position;
            Vector3 velocity = direction * velocitySize;

            float time = 0f;
            int segments = line_renderer.positionCount;
            float timeStep = 1f / segments;
            Vector3 v = velocity;
            for (int i = 0; i < line_renderer.positionCount; i++)
            {
                float x = v.x * time;
                float y = v.y * time - 0.5f * Physics.gravity.magnitude * time * time;
                float z = v.z * time;
                line_renderer.SetPosition(i, position + new Vector3(x, y, z));
                time += timeStep;
            }

            //下面的写法误差过大，不如上面的加速度-距离公式
            //float time = 0f;
            //int segments = line_renderer.positionCount;
            //float timeStep = 1f / segments;
            //Vector3 v = velocity;
            //for (int i = 0; i < line_renderer.positionCount; i++)
            //{
            //    double x = v.x * time;
            //    double y = v.y * time;
            //    double z = v.z * time;
            //    line_renderer.SetPosition(i, position + new Vector3(x, y, z));
            //    v = velocity + Physics.gravity * time;
            //    time += timeStep;
            //}

            state = ThrowableItemState.prepared;
        }

        private int pointCount = 0;
        private int maxPoints = 5000;
        private Vector3[] points = new Vector3[5000];
        private void FixedUpdate()
        {
            Debug.Log(GetComponent<Rigidbody>().velocity);
            if (pointCount < maxPoints)
            {
                points[pointCount] = transform.position;
                pointCount++;
            }
            else
            {
                for (int i = 0; i < maxPoints - 1; i++)
                {
                    points[i] = points[i + 1];
                }
                points[maxPoints - 1] = transform.position;
            }
        }

        private void OnDrawGizmos()
        {
            for (int i = 0; i < pointCount - 1; i++)
            {
                Gizmos.DrawLine(points[i], points[i + 1]);
            }
        }
    }

}

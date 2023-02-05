using System.Collections.Generic;
using UnityEngine;

namespace inventory_item_function
{
    interface IAction
    {
        /// <summary>
        /// 取消准备状态
        /// </summary>
        void CancelPrepare();

        /// <summary>
        /// 每帧调用
        /// </summary>
        /// <param name="dic"></param>
        void PrepareAction(Dictionary<string , object> dic);

        /// <summary>
        /// 仅调用一次
        /// </summary>
        /// <param name="dic"></param>
        void DoAction(Dictionary<string, object> dic);
    }

}
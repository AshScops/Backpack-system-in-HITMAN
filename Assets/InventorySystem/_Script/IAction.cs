using System.Collections.Generic;
using UnityEngine;

namespace inventory_item_function
{
    interface IAction
    {
        /// <summary>
        /// ȡ��׼��״̬
        /// </summary>
        void CancelPrepare();

        /// <summary>
        /// ÿ֡����
        /// </summary>
        /// <param name="dic"></param>
        void PrepareAction(Dictionary<string , object> dic);

        /// <summary>
        /// ������һ��
        /// </summary>
        /// <param name="dic"></param>
        void DoAction(Dictionary<string, object> dic);
    }

}
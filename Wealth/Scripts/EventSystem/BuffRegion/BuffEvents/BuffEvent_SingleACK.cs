using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BuffRegion
{
    public class BuffEvent_SingleACK : BuffEventBaseLine
    {
        int num;
        public override void FunInitParams()
        {
            num= BuffInParameters==null?2:0;
        }
        public override void FunInitMethods()
        {
            //eventQueue = new Queue<BuffMethodBase>();
            //eventQueue.Enqueue(new MethodCheck_HP());
            //eventQueue.Enqueue(new MethodCheck_Engine());
            //eventQueue.Enqueue(new MethodCheck_Distance());
        }

        public override void FunReset()
        {
            base.FunReset();
        }
    }
}

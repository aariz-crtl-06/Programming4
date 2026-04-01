using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Conditions
{
    public class SharkNearbyCT : ConditionTask
    {
        public BBParameter<NavMeshAgent> Shark;
        public BBParameter<float> detectRange;
        public BBParameter<bool> canEvade;

        protected override bool OnCheck()
        {
            if (agent == null || Shark.value == null)
                return false;

            if (!canEvade.value)
                return false;

            //Check if the shark is within the detection range of the turtle
            float dist = Vector3.Distance(agent.transform.position, Shark.value.transform.position);
            return dist <= detectRange.value;
        }
    }
}
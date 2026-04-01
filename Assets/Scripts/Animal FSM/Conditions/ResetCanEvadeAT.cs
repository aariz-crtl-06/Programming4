using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions
{
    public class ResetCanEvadeAT : ActionTask
    {
        //Resets the canEvade bool to check if the shark is in range of danger to re activate the sub FSM
        public BBParameter<NavMeshAgent> Shark;
        public BBParameter<float> safeRange;
        public BBParameter<bool> canEvade;

        protected override void OnUpdate()
        {
            if (agent == null || Shark.value == null)
            {
                EndAction(false);
                return;
            }

            float dist = Vector3.Distance(agent.transform.position, Shark.value.transform.position);

            //If the distance is greater the safe range, the turtle can evade again once in danger
            if (dist >= safeRange.value)
            {
                canEvade.value = true;
            }
        }
    }
}
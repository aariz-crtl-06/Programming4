using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{
    public class ShoreAT : ActionTask
    {
        public BBParameter<GameObject> shorePoint;
        public float moveSpeed = 3f;
        public BBParameter<float> shoreTime;

        private bool hasArrived = false;

        protected override void OnExecute()
        {
            hasArrived = false;
        }

        protected override void OnUpdate()
        {
            if (shorePoint.value == null || agent == null)
            {
                EndAction(false);
                return;
            }

            if (hasArrived)
                return;

            agent.transform.position = Vector3.MoveTowards(
                agent.transform.position,
                shorePoint.value.transform.position,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(agent.transform.position, shorePoint.value.transform.position) < 0.1f)
            {
                hasArrived = true;
                shoreTime.value = 50f;  
                EndAction(true);
            }
        }
    }
}
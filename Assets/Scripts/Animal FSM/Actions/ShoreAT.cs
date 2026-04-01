using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions
{
    public class ShoreAT : ActionTask<NavMeshAgent>
    {
        public BBParameter<GameObject> shorePoint;
        public BBParameter<float> timeAtShore = 5f;
        public BBParameter<float> shoreTime;

        private Vector3 startPosition;
        private float timer;
        private int phase; // 0 = go to shore, 1 = wait, 2 = return

        protected override void OnExecute()
        {
            // Safety check
            if (agent == null || !agent.isOnNavMesh || shorePoint.value == null)
            {
                EndAction(false);
                return;
            }

            //Set all starting values
            startPosition = agent.transform.position;
            timer = 0f;
            phase = 0;

            agent.SetDestination(shorePoint.value.transform.position);
        }

        protected override void OnUpdate()
        {
            if (agent == null || !agent.isOnNavMesh)
            {
                EndAction(false);
                return;
            }

            // Phase 0: go to shore
            if (phase == 0)
            {
                if (!agent.pathPending &&
                    agent.remainingDistance <= agent.stoppingDistance + 0.2f)
                {
                    phase = 1;
                    timer = 0f;
                    agent.ResetPath();
                }
            }
            // Phase 1: wait
            else if (phase == 1)
            {
                timer += Time.deltaTime;

                if (timer >= timeAtShore.value)
                {
                    phase = 2;
                    agent.SetDestination(startPosition);
                }
            }
            // Phase 2: return
            else if (phase == 2)
            {
                if (!agent.pathPending &&
                    agent.remainingDistance <= agent.stoppingDistance + 0.2f)
                {
                    shoreTime.value += 40f;
                    EndAction(true);
                }
            }
        }

        protected override void OnStop()
        {
            //stop moving and clear destination path
            if (agent != null && agent.isOnNavMesh)
            {
                agent.ResetPath();
            }
        }
    }
}
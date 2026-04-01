using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions
{
    public class SharkSwimAT : ActionTask
    {
        public BBParameter<NavMeshAgent> navAgent;
        public BBParameter<float> wanderRadius = 15f;
        public BBParameter<float> repathInterval = 4f;
        public BBParameter<float> smallSlack = 0.2f;
        public BBParameter<float> turnSpeed = 3f;
        public BBParameter<bool> rotateVelocity = true;

        private float timer;

        protected override string OnInit()
        {
            // Auto-fetch agent if not assigned in the blackboard/inspector
            if (navAgent.value == null)
            {
                navAgent.value = agent.GetComponent<NavMeshAgent>();
            }
            return null;
        }

        protected override void OnExecute()
        {
            // Force an immediate destination pick on the first update
            timer = repathInterval.value;
        }

        protected override void OnUpdate()
        {
            // Safety check
            if (navAgent.value == null)
            {
                EndAction(false);
                return;
            }

            timer += Time.deltaTime;

            // Check if arrived at destination
            bool arrived = !navAgent.value.pathPending && navAgent.value.remainingDistance <= navAgent.value.stoppingDistance + smallSlack.value;

            //If arrived or time has passed repath value, pick a new random destination
            if (arrived || timer >= repathInterval.value)
            {
                Vector3 center = agent.transform.position;

                // Try multiple times to find a valid NavMesh point
                bool found = false;
                Vector3 destination = center;

                // Try up to 20 times to find a valid point
                for (int i = 0; i < 20; i++)
                {
                    Vector3 randomPoint = center + Random.insideUnitSphere * wanderRadius.value;

                    if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 5f, navAgent.value.areaMask))
                    {
                        destination = hit.position;
                        found = true;
                        break;
                    }
                }
                // Set the new destination if found
                if (found)
                {
                    navAgent.value.SetDestination(destination);
                }

                timer = 0f;
            }


        }

        protected override void OnStop()
        {
            // Reset the path when the action stops
            if (navAgent.value != null && navAgent.value.isOnNavMesh)
            {
                navAgent.value.ResetPath();
            }
        }

    }
}
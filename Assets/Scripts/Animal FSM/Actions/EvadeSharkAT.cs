using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions
{
    public class EvadeSharkAT : ActionTask<NavMeshAgent>
    {
        public BBParameter<float> stayTime = 10f;
        public BBParameter<bool> canEvade;

        public BBParameter<NavMeshAgent> turtleAgent;

        //store sea and shore area
        public string walkableAreaName = "Walkable";
        public string sandAreaName = "Sand";

        private int walkableArea;
        private int sandArea;

        private Vector3 returnPoint;
        private float timer;
        private int phase;

        protected override void OnExecute()
        {
            // safety check
            if (turtleAgent.value == null)
                turtleAgent.value = agent;

           
            canEvade.value = false;

            //safety check
            if (turtleAgent.value == null || !turtleAgent.value.isOnNavMesh)
            {
                EndAction(false);
                return;
            }

            walkableArea = NavMesh.GetAreaFromName(walkableAreaName);
            sandArea = NavMesh.GetAreaFromName(sandAreaName);

            // reset agent settings and path
            turtleAgent.value.isStopped = false;
            turtleAgent.value.updatePosition = true;
            turtleAgent.value.updateRotation = true;
            turtleAgent.value.ResetPath();

            // save return point in sea
            if (NavMesh.SamplePosition(turtleAgent.value.transform.position, out NavMeshHit hit, 5f, 1 << walkableArea))
            {
                returnPoint = hit.position;
            }
            else
            {
                EndAction(false);
                return;
            }

            // allow movement between both areas
            turtleAgent.value.areaMask = (1 << walkableArea) | (1 << sandArea);

            phase = 0;
            timer = 0f;

            //if can't find a sand destination, end action
            if (!PickSandDestination())
            {
                EndAction(false);
                return;
            }
        }

        protected override void OnUpdate()
        {
            //safety check
            if (turtleAgent.value == null || !turtleAgent.value.isOnNavMesh)
            {
                EndAction(false);
                return;
            }


            // phase 0: go to sand
            if (phase == 0)
            {
                if (HasArrived())
                {
                    phase = 1;
                    timer = 0f;

                    turtleAgent.value.ResetPath();
                }
            }

            // phase 1: stay in sand
            else if (phase == 1)
            {
                timer += Time.deltaTime;

                //Once in the sand for long enough, return to sea
                if (timer >= stayTime.value)
                {
                    phase = 2;

                    turtleAgent.value.isStopped = false;
                    turtleAgent.value.areaMask = (1 << walkableArea) | (1 << sandArea);
                    turtleAgent.value.SetDestination(returnPoint);
                }
            }

            // phase 2: return to sea
            else if (phase == 2)
            {
                if (HasArrived())
                {
                    EndAction(true);
                }
            }
        }

        private bool PickSandDestination()
        {
            Vector3 center = turtleAgent.value.transform.position;

            // Try multiple times to find a valid NavMesh point on the sand area
            for (int i = 0; i < 20; i++)
            {
                Vector3 random = center + new Vector3(
                    Random.Range(-12f, 12f),
                    0f,
                    Random.Range(-12f, 12f)
                );

                //If a valid point is found, set it as the destination
                if (NavMesh.SamplePosition(random, out NavMeshHit hit, 8f, 1 << sandArea))
                {
                    bool success = turtleAgent.value.SetDestination(hit.position);

                    return success;
                }
            }

            return false;
        }

        private bool HasArrived()
        {
            //if path is still pending, turtle hasn't arrived
            if (turtleAgent.value.pathPending)
                return false;

            return turtleAgent.value.remainingDistance <= turtleAgent.value.stoppingDistance + 0.2f;
        }

        protected override void OnStop()
        {
            //reset path once complete
            if (turtleAgent.value != null && turtleAgent.value.isOnNavMesh)
            {
                turtleAgent.value.ResetPath();
            }
        }
    }
}
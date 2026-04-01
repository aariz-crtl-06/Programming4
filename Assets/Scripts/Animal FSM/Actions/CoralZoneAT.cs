using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions
{
    [Category("NavMesh")]
    public class CoralZoneAT : ActionTask<NavMeshAgent>
    {
        public BBParameter<float> timeInCoral;
        public BBParameter<float> wanderRadius;
        public BBParameter<float> coralTime;

        //seagrass and coral area names for navmesh
        public string normalAreaName = "Walkable";
        public string coralAreaName = "Coral";

        private int normalArea;
        private int coralArea;

        private float timer;
        private int phase = 0; // 0 = go to coral, 1 = swim in coral, 2 = return

        private Vector3 returnPoint;
        private bool hasReturnPoint = false;

        protected override void OnExecute()
        {
            //Get the area names from navmesh surface
            normalArea = NavMesh.GetAreaFromName(normalAreaName);
            coralArea = NavMesh.GetAreaFromName(coralAreaName);

            timer = 0f;
            phase = 0;

            // Find a valid return point in the seagrass zone
            if (NavMesh.SamplePosition(agent.transform.position, out NavMeshHit startHit, 5f, 1 << normalArea))
            {
                returnPoint = startHit.position;
                hasReturnPoint = true;
            }
            else
            {
                hasReturnPoint = false;
                EndAction(false);
                return;
            }

            //Set area mask to allow movement only on Walkable and Coral areas
            agent.areaMask = (1 << normalArea) | (1 << coralArea);

            if (!PickCoralDestination())
            {
                EndAction(false);
                return;
            }
        }

        protected override void OnUpdate()
        {
            if (agent == null || !agent.isOnNavMesh)
            {
                EndAction(false);
                return;
            }

            // phase 0: go to coral
            if (phase == 0)
            {
                if (HasArrived())
                {
                    phase = 1;
                    timer = 0f;

                    agent.areaMask = (1 << coralArea);

                    if (!PickCoralDestination())
                    {
                        EndAction(false);
                        return;
                    }
                }
            }

            // phase 1: swim in coral
            else if (phase == 1)
            {
                timer += Time.deltaTime;

                if (HasArrived())
                {
                    if (!PickCoralDestination())
                    {
                        EndAction(false);
                        return;
                    }
                }

                if (timer >= timeInCoral.value)
                {
                    if (!hasReturnPoint)
                    {
                        EndAction(false);
                        return;
                    }

                    phase = 2;
                    agent.areaMask = (1 << normalArea) | (1 << coralArea);
                    agent.SetDestination(returnPoint);
                }
            }

            // phase 2: return to seagrass zone
            else if (phase == 2)
            {
                if (HasArrived())
                {
                    if (NavMesh.SamplePosition(agent.transform.position, out NavMeshHit hit, 2f, 1 << normalArea))
                    {
                        coralTime.value += 60f;
                        EndAction(true);
                    }
                }
            }
        }

        private bool HasArrived()
        {
            if (agent.pathPending)
                return false;

            return agent.remainingDistance <= agent.stoppingDistance + 0.2f;
        }

        private bool PickCoralDestination()
        {
            // Pick a random point within wanderRadius on the coral area
            Vector3 center = agent.transform.position;

            for (int i = 0; i < 20; i++)
            {
                Vector3 random = center + new Vector3(
                    Random.Range(-wanderRadius.value, wanderRadius.value),
                    0f,
                    Random.Range(-wanderRadius.value, wanderRadius.value)
                );

                if (NavMesh.SamplePosition(random, out NavMeshHit hit, 5f, 1 << coralArea))
                {
                    return agent.SetDestination(hit.position);
                }
            }

            return false;
        }

        protected override void OnStop()
        {
            if (agent != null && agent.isOnNavMesh)
            {
                agent.ResetPath();
            }
        }
    }
}
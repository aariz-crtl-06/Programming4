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

        public string normalAreaName = "Walkable";
        public string coralAreaName = "Coral";

        private int normalArea;
        private int coralArea;

        private float timer;
        private int phase = 0; // 0 = go to coral, 1 = stay, 2 = leave

        protected override void OnExecute()
        {
            normalArea = NavMesh.GetAreaFromName(normalAreaName);
            coralArea = NavMesh.GetAreaFromName(coralAreaName);

            timer = 0f;
            phase = 0;

            // allow both so it can reach coral
            agent.areaMask = (1 << normalArea) | (1 << coralArea);

            PickCoralDestination();
        }

        protected override void OnUpdate()
        {
            // ---------- PHASE 0: GO TO CORAL ----------
            if (phase == 0)
            {
                if (!agent.pathPending &&
                    agent.remainingDistance <= agent.stoppingDistance + 0.2f)
                {
                    // lock to coral only
                    agent.areaMask = (1 << coralArea);

                    phase = 1;
                    timer = 0f;

                    PickCoralDestination();
                }
            }

            // ---------- PHASE 1: SWIM IN CORAL ----------
            else if (phase == 1)
            {
                timer += Time.deltaTime;

                // keep wandering inside coral
                if (!agent.pathPending &&
                    agent.remainingDistance <= agent.stoppingDistance + 0.2f)
                {
                    PickCoralDestination();
                }

                // after 15 seconds → leave
                if (timer >= timeInCoral.value)
                {
                    agent.areaMask = (1 << normalArea);
                    phase = 2;

                    PickNormalDestination();
                }
            }

            // ---------- PHASE 2: RETURN ----------
            else if (phase == 2)
            {
                if (!agent.pathPending &&
                    agent.remainingDistance <= agent.stoppingDistance + 0.2f)
                {
                    coralTime.value+= 50;
                    EndAction(true);
                }
            }
        }

        // -------------------------
        // Pick coral destination
        // -------------------------
        void PickCoralDestination()
        {
            Vector3 center = agent.transform.position;

            for (int i = 0; i < 20; i++)
            {
                Vector3 random = center + new Vector3(
                    Random.Range(-wanderRadius.value, wanderRadius.value),
                    0f,
                    Random.Range(-wanderRadius.value, wanderRadius.value)
                );

                if (NavMesh.SamplePosition(random, out NavMeshHit hit, 5f, (1 << coralArea)))
                {
                    agent.SetDestination(hit.position);
                    return;
                }
            }
        }

        // -------------------------
        // Pick normal destination
        // -------------------------
        void PickNormalDestination()
        {
            Vector3 center = agent.transform.position;

            for (int i = 0; i < 20; i++)
            {
                Vector3 random = center + new Vector3(
                    Random.Range(-wanderRadius.value, wanderRadius.value),
                    0f,
                    Random.Range(-wanderRadius.value, wanderRadius.value)
                );

                if (NavMesh.SamplePosition(random, out NavMeshHit hit, 5f, (1 << normalArea)))
                {
                    agent.SetDestination(hit.position);
                    return;
                }
            }
        }
    }
}
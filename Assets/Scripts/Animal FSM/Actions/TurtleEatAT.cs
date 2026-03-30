using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions
{

    public class TurtleEatAT : ActionTask
    {

        public BBParameter<float> hunger;
        public BBParameter<NavMeshAgent> navAgent;
        public BBParameter<float> stamina;

        private GameObject targetAlgae;

        protected override void OnExecute()
        {
            GameObject[] allAlgae = GameObject.FindGameObjectsWithTag("Algae");

            float closestDistance = Mathf.Infinity;
            targetAlgae = null;

            for (int i = 0; i < allAlgae.Length; i++)
            {
                float distance = Vector3.Distance(agent.transform.position, allAlgae[i].transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetAlgae = allAlgae[i];
                }
            }

            if (targetAlgae != null)
            {
                navAgent.value.SetDestination(targetAlgae.transform.position);
            }
            else
            {
                EndAction(false);
            }
        }

        protected override void OnUpdate()
        {
            stamina.value -= Time.deltaTime;

            if (targetAlgae == null)
            {
                EndAction(false);
                return;
            }

            if (!navAgent.value.pathPending &&
                navAgent.value.remainingDistance <= navAgent.value.stoppingDistance + 0.2f)
            {
                hunger.value += Time.deltaTime;
            }

            if (hunger.value > 50)
            {
                EndAction(true);
            }
        }
    }
}
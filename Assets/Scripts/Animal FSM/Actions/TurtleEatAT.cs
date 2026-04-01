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

        private GameObject targetAlgae;

        protected override void OnExecute()
        {
            //There is algae in both coral and seagrass zones, so use the tag to pick the closer one
            GameObject[] allAlgae = GameObject.FindGameObjectsWithTag("Algae");

            float closestDistance = Mathf.Infinity;
            targetAlgae = null;

            //Check the distance to each algae and pick the closest one as the target
            for (int i = 0; i < allAlgae.Length; i++)
            {
                float distance = Vector3.Distance(agent.transform.position, allAlgae[i].transform.position);

                //Select the closest algae as the target
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetAlgae = allAlgae[i];
                }
            }

            if (targetAlgae != null)
            {
                //once target is picked, set nav mesh destination to the algae
                navAgent.value.SetDestination(targetAlgae.transform.position);
            }
            else
            {
                EndAction(false);
            }
        }

        protected override void OnUpdate()
        {
            //Check if theres a target
            if (targetAlgae == null)
            {
                EndAction(false);
                return;
            }
            //IF there's a target, and is the distance remaining is less that stopping distance, turtle can eat
            if (!navAgent.value.pathPending && navAgent.value.remainingDistance <= navAgent.value.stoppingDistance + 0.2f)
            {
                hunger.value += Time.deltaTime *2;
            }

            //Once hunger is above 30, the turtle is full and action ends
            if (hunger.value > 30)
            {
                EndAction(true);
            }
        }
    }
}
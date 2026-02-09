using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class TurtleEatAT : ActionTask {

		public BBParameter<float> hunger;
		public BBParameter<NavMeshAgent> navAgent;
		public BBParameter<GameObject> algae;
		protected override string OnInit() {
			return null;
		}

		
		protected override void OnExecute() {
            //Set turtle destination to algae
            navAgent.value.SetDestination(algae.value.transform.position);
		}

		protected override void OnUpdate() {
            //When turtle reaches algae, hunger replenshes
            if (!navAgent.value.pathPending && navAgent.value.remainingDistance <= navAgent.value.stoppingDistance + 0.2f)
            {
                hunger.value += Time.deltaTime;
            }

            //Once hunger is above 50, end action
            if (hunger.value > 50)
			{
				EndAction(true);
			}
		}

	}
}
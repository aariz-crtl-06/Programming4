using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class NavigationAT : ActionTask {

		public BBParameter<float> timeSinceLastSampleBBP;
		public BBParameter<Vector3> targetPositionBBP;
		public BBParameter<bool> isMovingBBP;

		public float sampleRate;
		public float sampleRadius;

		public NavMeshAgent navAgent;
		private Vector3 lastTargetDestination;

		protected override string OnInit() {
			navAgent=agent.GetComponent<NavMeshAgent>();

			if(navAgent == null)
			{
				return $"{agent.name} - NavigationAT: Unable to get Navmesh Agent Reference!";
			}

			else
				return null;
		}

		

		protected override void OnUpdate()
		{

			timeSinceLastSampleBBP.value += Time.deltaTime;
			if (timeSinceLastSampleBBP.value > sampleRate)
			{
				timeSinceLastSampleBBP = 0;


				if (lastTargetDestination != targetPositionBBP.value)
				{
					lastTargetDestination = targetPositionBBP.value;


					if (NavMesh.SamplePosition(targetPositionBBP.value, out NavMeshHit hitInfo, sampleRadius, NavMesh.AllAreas))
					{
						navAgent.SetDestination(hitInfo.position);
					}

					isMovingBBP.value =
						navAgent.remainingDistance != 0
						&& navAgent.remainingDistance != Mathf.Infinity
						|| navAgent.pathPending;
				}
			}
		}

	
	}
}
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class MoveAndRotateTowardsAT : ActionTask {
		public float moveSpeed=5f;
		public float turnSpeed=180f; //in degrees per second
		public Transform target;
		public float stoppingDistance = 0.1f;

		protected override string OnInit() {
			return null;
		}

		protected override void OnUpdate() 
		{
			Vector3 direction = target.position - agent.transform.position;
			Quaternion rotation = Quaternion.LookRotation(direction);

			agent.transform.SetPositionAndRotation(
				agent.transform.position + moveSpeed * Time.deltaTime * agent.transform.forward,
				Quaternion.RotateTowards(agent.transform.rotation, rotation, turnSpeed * Time.deltaTime)
				);

			if(Vector3.Distance(agent.transform.position, target.position) > stoppingDistance )
			{
				EndAction(true);
			}

		}

	}
}
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions{

	public class AwaitActionTask : ActionTask{
		//public float maxWaitTime;
		//public float minWaitTime;
		public BBParameter<Vector3> startingPosition;

		//private float totalTimeToWait;
		//private float timeWaiting = 0f;

		protected override string OnInit(){
            startingPosition.value = agent.transform.position;
            return null;
		}

	
		//protected override void OnExecute()
		//{
		//	timeWaiting = 0f;
		//	totalTimeToWait = UnityEngine.Random.Range(minWaitTime, maxWaitTime);

		//}

		//protected override void OnUpdate()
		//{
		//	Choose a random time between two values
		//	Wait that amount of time
		//	timeWaiting += Time.deltaTime;
		//	if (timeWaiting > totalTimeToWait)
		//	{
		//		EndAction(true);
		//	}
		//}


		//protected override void OnStop()
		//{

		//}


		//protected override void OnPause()
		//{

		//}
	}
}
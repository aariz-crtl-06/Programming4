using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions {

	public class WaitCT : ConditionTask {
		public float waitDuration;

		private float timeWaiting = 0f;

		protected override string OnInit(){
			return null;
		}

		protected override void OnEnable() {
            // Reset the timer when the condition is enabled
            timeWaiting = 0f;
		}

		protected override void OnDisable() {
			
		}

		protected override bool OnCheck() {
            // Increment the timer
            timeWaiting += Time.deltaTime;
			return timeWaiting > waitDuration;
		}
	}
}
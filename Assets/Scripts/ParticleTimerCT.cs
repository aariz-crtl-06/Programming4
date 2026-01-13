using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class ParticleTimerCT : ConditionTask {

        //Custom conditonal task to wait for a set duration
        public float waitDuration;
		float timeInState = 0f;

		protected override string OnInit(){
			return null;
		}

		protected override void OnEnable() {
			timeInState = 0f;
        }


		protected override void OnDisable() {
			
		}

        //Once time in state exceeds wait duration, return true
        protected override bool OnCheck() {
			timeInState += Time.deltaTime;
            return timeInState > waitDuration;
		}
	}
}
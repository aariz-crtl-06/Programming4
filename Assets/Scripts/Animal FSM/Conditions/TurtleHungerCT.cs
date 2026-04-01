using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class TurtleHungerCT : ConditionTask {

		public BBParameter<float> hunger;
        protected override string OnInit(){
			return null;
		}

		protected override bool OnCheck() {
            //hunger grows over time
            hunger.value -= Time.deltaTime;

            //If hunger is above 10, return false to trigger eating state
            if (hunger.value >= 10) {
				return false;
            }
			else
			{
			    return true;
            }
           
		}
	}
}
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class FlameGoesOut : ConditionTask {

        public BBParameter<float> flame;
        public float burnRate = 1f;
        protected override string OnInit(){
            //Check if flame variable is assigned
            if (flame == null)
            {
                return "Flame variable is not assigned";
            }
            return null;
        }

		protected override bool OnCheck() {


            //Flame depletes over time
            flame.value -= burnRate * Time.deltaTime;

            //Once flame is out, return true
            if (flame.value <= 0f)
            {
                flame.value = 0f;
                return true; // flame out
            }

            return false;
        }
	}
}
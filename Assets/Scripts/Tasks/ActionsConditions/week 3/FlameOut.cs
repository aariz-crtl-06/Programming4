using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.Rendering;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class FlameOut : ActionTask {

        public BBParameter<float> flame;
        public float fireRate = 1f;
        public float maxFlame = 5f;

        protected override string OnInit() {
            //Check if flame variable is assigned
            if (flame == null)
            {
                return "Flame variable is not assigned";
            }
            return null;

            
		}
		protected override void OnUpdate() {
            //Flame increases over time
            flame.value += fireRate * Time.deltaTime;

            //Once flame is full, end action
            if (flame.value >= maxFlame)
            {
                flame.value = maxFlame;
                EndAction(true);
            }
        }

	}
}
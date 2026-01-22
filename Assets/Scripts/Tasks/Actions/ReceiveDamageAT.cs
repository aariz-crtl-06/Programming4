using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class ReceiveDamageAT : ActionTask {

        public BBParameter<int> health;

        protected override string OnInit() {
			return null;
		}

		protected override void OnExecute() {
            //If health is less than or equal to 0, reset it
            if (health.value <= 0)
                health.value = 2;


        }


		protected override void OnUpdate()
		{

            //If health is 1 or less, end action
            if (health.value <= 1)
			{
				Debug.Log("Target Attacked!");
				EndAction(true);

			}

		}

		protected override void OnStop() {
			
		}


		protected override void OnPause() {
			
		}
	}
}
using JetBrains.Annotations;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class DamageAT : ActionTask {

		public Blackboard targetBoard;

   
        protected override string OnInit() {
			return null;
		}

		
		protected override void OnExecute() {

            //Get value of the other health variable on targets blackboard
            int health = targetBoard.GetVariableValue<int>("health");

            //Decrease health by 1
            health -= 1;
            health = Mathf.Max(health, 0); // clamp

            //Set the updated health value back to the target's blackboard
            targetBoard.SetVariableValue("health", health);

            EndAction(true);


        }

		protected override void OnUpdate() {
			
		}

		protected override void OnStop() {
			
		}

		protected override void OnPause() {
			
		}
	}
}
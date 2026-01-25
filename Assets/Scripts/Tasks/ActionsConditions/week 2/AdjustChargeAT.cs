using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class AdjustChargeAT : ActionTask {

		public BBParameter<float >currentCharge;
		public float adjustmentValue = 0.1f;

		private Blackboard agentBlackBoard;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			agentBlackBoard = agent.GetComponent<Blackboard>();

            if (agentBlackBoard != null)
            {
                return null;
            }

            else return $"Adjust Charge - {agent.name}: unble to get blackboard reference!!";
        
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {

			//currentCharge = agentBlackBoard.GetVariableValue<float>("currentCharge");
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			currentCharge.value += adjustmentValue * Time.deltaTime;
			//agentBlackBoard.SetVariableValue("currentCharge", currentCharge);
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}
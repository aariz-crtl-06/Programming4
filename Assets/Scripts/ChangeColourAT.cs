using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class ChangeColourAT : ActionTask {
		//Reference to set a new colour once on state 3
		public Material newMaterial;


		protected override string OnInit() {
			return null;
		}

		
		protected override void OnExecute() {
			Renderer renderer = agent.GetComponent<Renderer>();

            //If rendered isnt null, change the material to the new material
            if (renderer != null) {
				renderer.material = newMaterial;
				EndAction(true);
            }
        }

		
		protected override void OnUpdate() {
			
		}

		
		protected override void OnStop() {
			
		}

		
		protected override void OnPause() {
			
		}
	}
}
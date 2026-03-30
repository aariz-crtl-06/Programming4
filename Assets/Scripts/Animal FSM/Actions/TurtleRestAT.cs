using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class TurtleRestAT : ActionTask {
		public BBParameter<float> stamina;
		public float restDuration = 10f;
		public float timer;
        protected override string OnInit() {
			return null;
		}

	
		protected override void OnExecute() {
			timer = restDuration;
       }

		
		protected override void OnUpdate() {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                stamina.value = 100f;
                EndAction(true);
            }


        }

        

		protected override void OnStop() {
			
		}

		protected override void OnPause() {
			
		}
	}
}
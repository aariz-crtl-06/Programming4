using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class TurtleTiredCT : ConditionTask {

		public BBParameter<float> stamina;
	
        protected override string OnInit(){
			return null;
		}

		
		protected override void OnEnable() {
			
		}


		protected override void OnDisable() {
			
		}

		protected override bool OnCheck() {
			if (stamina.value >= 0)
			{
				return false; 
            }
			else
			{
				return true; 
            }


        }
	}
}
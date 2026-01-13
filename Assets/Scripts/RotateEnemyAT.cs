using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEditor;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class RotateEnemyAT : ActionTask {

        //Reference to enemy game object to rotate
        public GameObject enemy;
		float rotated;

	
		protected override string OnInit() {
			return null;

		}

		protected override void OnExecute() {

            
        }

	
		protected override void OnUpdate()
		{
            //Rotate enemy 90 degrees over 1 second
            float rotation = 90f * Time.deltaTime; 
			enemy.transform.Rotate(0f, 0f, rotation); 
			rotated += rotation;
            //Once rotated, end action
            if (rotated >= 90f) 
			{ 
				EndAction(true); 
			}

        }

		protected override void OnStop() {
			
		}

		protected override void OnPause() {
			
		}
	}
}
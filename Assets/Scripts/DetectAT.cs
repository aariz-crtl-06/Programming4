using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.Rendering;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions
{

	public class DetectAT : ActionTask
	{
        public BBParameter<bool> detected;

       
        protected override string OnInit()
		{
			
            return null;
		}

		
		protected override void OnExecute()
		{
		}

	
		protected override void OnUpdate()
		{
            if (detected.value)
            {
                EndAction(true);
            }
        }

		protected override void OnStop()
		{

		}

		protected override void OnPause()
		{

		}

	
	}

}
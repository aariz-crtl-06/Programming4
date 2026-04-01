using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;


namespace NodeCanvas.Tasks.Conditions
{

    public class TurtleCoralZone : ConditionTask
    {
        //Checks if the turtle's coral time has reached, allowing it to go to the coral zone
        public BBParameter<float> coralTime;

        protected override string OnInit()
        {
            return null;
        }


        protected override void OnEnable()
        {

        }


        protected override void OnDisable()
        {

        }

        protected override bool OnCheck()
        {
            coralTime.value -= Time.deltaTime;
            if (coralTime.value >= 0)
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
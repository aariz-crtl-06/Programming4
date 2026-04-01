using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.Conditions
{
    // Checks if the turtle's shore time has depleted, allowing it to go to the shore
    public class TurtleShoreCT : ConditionTask
    {
        public BBParameter<float> shoreTime;

        protected override bool OnCheck()
        {
            return shoreTime.value <= 0f;
        }
    }
}
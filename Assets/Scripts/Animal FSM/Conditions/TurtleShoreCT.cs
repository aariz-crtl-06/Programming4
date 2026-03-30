using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.Conditions
{
    public class TurtleShoreCT : ConditionTask
    {
        public BBParameter<float> shoreTime;

        protected override bool OnCheck()
        {
            return shoreTime.value <= 0f;
        }
    }
}
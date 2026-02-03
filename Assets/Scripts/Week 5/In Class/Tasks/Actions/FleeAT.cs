using NodeCanvas.Framework;
using UnityEngine;

public class FleeAT : ActionTask
{
    public BBParameter<Vector3> targetPositionBBP;
    public BBParameter<bool> hasTargetBBP;

    public BBParameter<Transform>hostileTarget;
    public float safeRadius;

    protected override void OnUpdate()
    {
       
        if(hasTargetBBP.value == false)
        {
            EndAction();
        }

        else
        {
            float distance = Vector3.Distance(agent.transform.position, hostileTarget.value.position);
            if (distance < safeRadius)
            {
                Vector3 safeDirection = (agent.transform.position - hostileTarget.value.position).normalized;
                Vector3 desiredPoint = agent.transform.position + safeDirection * safeRadius;

                targetPositionBBP.value = desiredPoint;
            }
            }
    }
}

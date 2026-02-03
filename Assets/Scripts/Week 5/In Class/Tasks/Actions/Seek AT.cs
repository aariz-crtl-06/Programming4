using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class SeekAT : ActionTask
{
    public BBParameter<Vector3> targetPositionBBP;
    public BBParameter<bool> hasTargetBBP;

    public BBParameter<Transform>friendlyTarget;
    public float seekRadius;

    protected override void OnUpdate()
    {
        hasTargetBBP.value = friendlyTarget != null;
        if (hasTargetBBP.value == false)
            EndAction();

        else
        {
            float distance = Vector3.Distance(agent.transform.position, friendlyTarget.value.position);

            if (distance < seekRadius)
            {
                targetPositionBBP.value = friendlyTarget.value.position;

            }
        }
    }
}
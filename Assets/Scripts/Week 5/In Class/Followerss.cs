using NodeCanvas.Framework;
using UnityEngine;

public class Followerss : MonoBehaviour
{
    public Blackboard followerBlackboard;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Friendly"))
        {
            followerBlackboard.SetVariableValue("friendlyTarget", other.transform);
        }

        else if(other.CompareTag("Hostile"))
        {
            followerBlackboard.SetVariableValue("hostileTarget", other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Friendly"))
        {
            followerBlackboard.SetVariableValue("friendlyTarget", null);
        }

        if (other.CompareTag("Hostile"))
        {
            followerBlackboard.SetVariableValue("hostileTarget", null);
        }
    }
}

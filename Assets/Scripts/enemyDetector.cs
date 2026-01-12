using UnityEngine;
using NodeCanvas.Framework;
public class enemyDetector : MonoBehaviour
{
    public Blackboard blackboard;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            blackboard.SetVariableValue("detected", true);
        }
    }
}

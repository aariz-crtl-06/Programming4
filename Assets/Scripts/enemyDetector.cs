using UnityEngine;
using NodeCanvas.Framework;
public class enemyDetector : MonoBehaviour
{
    public Blackboard blackboard;

    //Script attached to character to check for enemies entering trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            blackboard.SetVariableValue("detected", true);
        }
    }
}

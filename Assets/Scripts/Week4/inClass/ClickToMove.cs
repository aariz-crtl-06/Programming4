using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    public NavMeshAgent navAgent;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(mouseRay, out RaycastHit hitInfo))
            {
                navAgent.SetDestination(hitInfo.point);
            }
        }
    }
}

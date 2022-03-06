using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent characterAgent;
    [SerializeField] private Camera mainCamera;
    
    private void Awake()
    {
        SetNavMeshAgent();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 destination = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            destination.z = 0;
            MoveCharacterToDestination(destination);
        }
    }

    private void SetNavMeshAgent()
    {
        characterAgent.updateRotation = false;
        characterAgent.updateUpAxis = false;
    }

    private void MoveCharacterToDestination(Vector3 destination)
    {
        characterAgent.SetDestination(destination);
    }
}

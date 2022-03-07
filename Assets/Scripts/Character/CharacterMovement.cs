using PlayerData;
using UnityEngine;
using UnityEngine.AI;

namespace Character
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent characterAgent;
        [SerializeField] private Camera mainCamera;
        [Space]
        [SerializeField] private bool sendDataToServer = true;

        private Vector3 _destination;
        private ServerDataManager _serverDataManager;
    
        private void Awake()
        {
            InitialiseVariables();
            SetNavMeshAgent();
        }
    
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _destination = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                _destination.z = 0;
                MoveCharacterToDestination(_destination);
            }
            
            if (characterAgent.velocity.sqrMagnitude > 0 && sendDataToServer)
                _serverDataManager.SendDataToServer($"moving to destination {_destination}");
        }

        private void OnDestroy()
        {
            _serverDataManager.Stop();
        }

        private void InitialiseVariables()
        {
            _serverDataManager = new ServerDataManager();
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
}

using System.Collections;
using UnityEngine;
using PlayerData;

namespace Character
{
    public class CharacterSaveData : MonoBehaviour
    {
        [SerializeField] private Transform character;
        [SerializeField] private float saveTimePeriod = 2;

        private Coroutine _saveCoroutine;
        private WaitForSeconds _waitPeriod;
        private SaveAndLoad _saveSystem;
        private SaveAndLoad.SaveData _saveData;

        private void Awake()
        {
            InitialiseVariables();
            
            GetPlayerData();
            StartSavePlayerData();
        }

        private void OnDestroy()
        {
            if (_saveCoroutine != null)
                StopCoroutine(_saveCoroutine);
        }

        private void StartSavePlayerData()
        {
            _saveCoroutine = StartCoroutine(SaveOnPeriod());
        }

        IEnumerator SaveOnPeriod()
        {
            while (true)
            {
                _saveData.position = character.position;
                _saveSystem.SaveUserData(_saveData);
                yield return _waitPeriod;
            }
        }

        private void InitialiseVariables()
        {
            _saveSystem = new SaveAndLoad();
            _saveData = new SaveAndLoad.SaveData();
            _waitPeriod = new WaitForSeconds(saveTimePeriod);
        }

        private void GetPlayerData()
        {
            _saveData = _saveSystem.LoadUserData();
            character.position = _saveData.position;
        }
    }
}

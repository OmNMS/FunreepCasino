using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViewModel;
using UniRx;
using System.Linq;
using System;

namespace Components
{
    public class RoulleteBallDisplay : MonoBehaviour
    {
        public CharacterTable characterTable;
        public GameRoullete gameRoullete;
        public GameObject[] _anchorNumbers;
        public GameObject sphereContainer;
        public GameObject sphereDefault;

        private GameObject _sphere;
        private Vector3 _ballPosition;

        public List<GameObject> FinalPositions;
        
        void Start()
        {
            characterTable.OnRound
                .Subscribe(OnRound)
                .AddTo(this);

            gameRoullete.OnNumber
                .Subscribe(SetBallInRoullete)
                .AddTo(this);
        }

        private void OnRound(bool isRound)
        {
            if(isRound)
            {
                DestroyLastSphere();
                sphereDefault.SetActive(true);
                
                for(int i=0;i<FinalPositions.Count;i++)
                {
                    FinalPositions[i].SetActive(false);
                }
            }
        }

        public void SetBallInRoullete(int num)
        {    
            sphereDefault.SetActive(false);

            for (int i = 0; i < FinalPositions.Count; i++)
            {

                if (int.Parse(FinalPositions[i].name) == num)
                {
                   
                    FinalPositions[i].SetActive(true);
                }
            }
            DestroyLastSphere();

            // Activate ball and position in the indicated number
            IEnumerable<GameObject> anchor = _anchorNumbers.Where(anc => anc.name == $"handler_{num}");
            _ballPosition = anchor.ToArray()[0].gameObject.transform.position;
            
            _sphere = Instantiate(gameRoullete.sphere);
            _sphere.transform.position = _ballPosition;
            _sphere.SetActive(true);
            _sphere.transform.SetParent(sphereContainer.transform);

            Debug.Log($"Roullete positioning ball in number {num}!");
        }

        void DestroyLastSphere()
        {
            if(sphereContainer.transform.childCount > 0)
                Destroy(_sphere);
        }
    }
}

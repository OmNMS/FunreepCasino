using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roulette.Gameplay
{
    public class RoulleteRotateDisplay : MonoBehaviour
    {
        public static RoulleteRotateDisplay Instance;
        public GameObject ballRotator;
        public GameObject wheelRotator;
        public float rotateSpeed;
        public float wheel_rotateSpeed;
        public GameObject SphereDefault;
        public List<GameObject> FinalPositions;

        void Awake()
        {
            Instance = this;
            wheel_rotateSpeed = 1f;
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        void FixedUpdate()
        {
            // wheelRotator.transform.Rotate(Vector3.forward * rotateSpeed * wheel_rotateSpeed * Time.deltaTime);
            // ballRotator.transform.Rotate(Vector3.back * rotateSpeed * 3 * Time.deltaTime);
        }

        public void SetBallInRoullete(string num)
        {
            SphereDefault.SetActive(false);

            for (int i = 0; i < FinalPositions.Count; i++)
            {
                if (FinalPositions[i].name == num)
                {
                    FinalPositions[i].SetActive(true);
                }
            }
        }

        public void DisabledLastSphere()
        {
            SphereDefault.SetActive(true);

            for (int i = 0; i < FinalPositions.Count; i++)
            {
                FinalPositions[i].SetActive(false);
            }
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;
using AndarBahar.Utility;
using AndarBahar.Gameplay;

namespace AndarBahar.Gameplay
{
    class AndarBahar_InputHandler:MonoBehaviour
    {
        public Spots spot;
        public Camera camera;
        public GameObject[] cards;
        [SerializeField] int number;
       [SerializeField] AndarBahar_ChipController chipController;
        // public void Start()
        // {
        //     chipController = GetComponent<AndarBahar_ChipController>();
        // }
        // private void Update()
        // {
        //     // if (isTimeUp) return;
        //     // if (Input.GetMouseButtonDown(0))
        //     // {
        //     //     ProjectRay();
        //     // }
        // }


        //void ProjectRay()
        //{
        //    Vector3 origin = camera.ScreenToWorldPoint(Input.mousePosition);
        //    RaycastHit2D hit = Physics2D.Raycast(origin, Vector3.forward * 100);
        //    if (hit.collider != null)
        //    {
        //        print("onclicked");
        //        chipController.OnUserInput(hit.transform, hit.point);
        //    }
        //}
        
        private void OnMouseDown()
        {
            ProjectRay();
            zoom();
        }
        void ProjectRay()
        {
            Vector3 origin = camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector3.zero);
            if (hit.collider != null)
            {
                // Debug.LogError("handle  " + uiHandler.currentChip);
                // Debug.Log("hit: " + hit);
                chipController.OnUserInput(hit.transform, hit.point);
                
            }

            // RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector3.forward);
            // if( hit.collider != null )
            // {
            //     // Debug.LogError("hit collider ");
            //     chipController.OnUserInput(hit.transform, hit.point);
            // }

        }
        void zoom()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if(i == number)
                {
                    cards[i].transform.localScale =new Vector3(1.7f,1.4f,1f);
                }
                else
                {
                    cards[i].transform.localScale =new Vector3(1.5f,1.2f,1f);
                }
            }
        }



    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class dummyscript : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            this.GetComponent<RectTransform>().transform.RotateAround( transform.position, Vector3.forward, 20 * Time.deltaTime);
        }
    }
}

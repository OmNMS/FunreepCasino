using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BingoTitleAnimation : MonoBehaviour
{
    [SerializeField] List<GameObject> titleSprites = new List<GameObject>();
    [SerializeField] float interval = 0.5f;

    void Start()
    {
        StartCoroutine(CycleSprites());
    }

    IEnumerator CycleSprites()
    {
        while (true)
        {
            foreach(GameObject obj in titleSprites)
            {
                obj.SetActive(true);
                yield return new WaitForSeconds(interval);
                obj.SetActive(false);
            }
        }
    }
}

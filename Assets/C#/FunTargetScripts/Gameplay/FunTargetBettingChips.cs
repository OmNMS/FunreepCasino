using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FunTarget.GamePlay
{
    public class FunTargetBettingChips : MonoBehaviour
    {
        [SerializeField] Button[] chips;
        [SerializeField] GameObject[] selectedChips;

        void Start()
        {
            foreach (Button chip in chips)
            {
                chip.onClick.AddListener(() => OnToggleValueChanged(chip));
                //Debug.Log("Chip: " + chip.name);
            }
        }

        public void OnToggleValueChanged(Button selectedChip)
        {
            Debug.Log("Chip : " + selectedChip.name);
            FunTargetGamePlay.Instance.currentlySectedChip = int.Parse(selectedChip.GetComponentInChildren<TextMeshProUGUI>().text);

            for(int i = 0; i < chips.Length; i++)
            {
                if(chips[i] == selectedChip)
                {
                    selectedChips[i].SetActive(true);
                }
                else
                {
                    selectedChips[i].SetActive(false);
                }
            }
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Commands
{
    public class AnimationScript : MonoBehaviour
    {
        public static AnimationScript instance;
        public GameObject RouletteObj;
        public GameObject ChipBars;
        public GameObject Table;
        public GameObject CancelSpecificBet_OldImage,CancelSpecificBet_NewImage;
        public GameObject CancelBets_OldImage, CancelBets_NewImage;
        public Transform ChipNewPos, ChipOldPos;
        public Transform TableNewPos, TablepOldPos;
        public float speed;
        public bool _hideRoulette, _startShift_Pos;

        void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            _hideRoulette = false;
            _startShift_Pos = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(_startShift_Pos == true)
            {
                if(_hideRoulette == true)
                {
                    ChipBars.transform.position = Vector2.MoveTowards(ChipBars.transform.position, ChipNewPos.position, speed * Time.deltaTime);
                    Table.transform.position = Vector2.MoveTowards(Table.transform.position, TableNewPos.position, speed * Time.deltaTime);
                    CancelBets_NewImage.SetActive(true);
                    CancelBets_OldImage.SetActive(false);
                    CancelSpecificBet_NewImage.SetActive(true);
                    CancelSpecificBet_OldImage.SetActive(false);
                    RouletteObj.SetActive(false);
                }
                else
                {
                    ChipBars.transform.position = Vector2.MoveTowards(ChipBars.transform.position, ChipOldPos.position, speed * Time.deltaTime);
                    Table.transform.position = Vector2.MoveTowards(Table.transform.position, TablepOldPos.position, speed * Time.deltaTime);
                    CancelBets_NewImage.SetActive(false);
                    CancelBets_OldImage.SetActive(true);
                    CancelSpecificBet_NewImage.SetActive(false);
                    CancelSpecificBet_OldImage.SetActive(true);
                    RouletteObj.SetActive(true);
                }
            }
        }

        public void NewAnim_Pos()
        {
            _hideRoulette = !_hideRoulette;
            _startShift_Pos = true;
        }

    }
}

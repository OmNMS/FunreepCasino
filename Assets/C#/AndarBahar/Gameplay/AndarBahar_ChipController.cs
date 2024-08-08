using UnityEngine.UI;
using UnityEngine;
using AndarBahar.Utility;
using Shared;
using System.Collections;
using AndarBahar.UI;
using System;
using AndarBahar.ServerStuff;
using System.Collections.Generic;

namespace AndarBahar.Gameplay
{
    class AndarBahar_ChipController : MonoBehaviour
    {
        public static AndarBahar_ChipController Instance;
        public AndarBahar_UiHandler uiHandler;
        public AndarBahar_Timer timer;
        public AndarBahar_Spawner chipSpawner;
        public Action<Transform, Vector3> OnUserInput;
        public AndarBhar_AiBot_Data AndarBhar_Ai;
        bool isTimeUp;
        private void Start()
        {
            OnUserInput += AddUserBet;
            timer.onTimerStart += () => isTimeUp = false;
            timer.onTimeUp += () => isTimeUp = true;

        }
        void Awake()
        {
            Instance = this;
        }
        public Transform Card_A_Parent, Card_2_Parent, Card_3_Parent, Card_4_Parent, Card_5_Parent, Card_6_Parent, Card_7_Parent, Card_8_Parent, Card_9_Parent, Card_10_Parent, Card_J_Parent, Card_Q_Parent, Card_K_Parent, Card_Diamond_Parent, Card_Heart_Parent, Card_Club_Parent, Card_Spade_Parent, Card_Red_Parent, Card_Black_Parent, Card_A_6_Parent, Card_Seven_Parent, Card_8_K_Parent, Card_Andar_Parent, Card_Bahar_Parent;
        public Transform GetChipParent(Spots betType)
        {
            switch (betType)
            {
                case Spots.A: return Card_A_Parent;
                case Spots._2: return Card_2_Parent;
                case Spots._3: return Card_3_Parent;
                case Spots._4: return Card_4_Parent;
                case Spots._5: return Card_5_Parent;
                case Spots._6: return Card_6_Parent;
                case Spots._7: return Card_7_Parent;
                case Spots._8: return Card_8_Parent;
                case Spots._9: return Card_9_Parent;
                case Spots._10: return Card_10_Parent;
                case Spots.J: return Card_J_Parent;
                case Spots.Q: return Card_Q_Parent;
                case Spots.K: return Card_K_Parent;
                case Spots.Diamond: return Card_Diamond_Parent;
                case Spots.Heart: return Card_Heart_Parent;
                case Spots.Club: return Card_Club_Parent;
                case Spots.Spade: return Card_Spade_Parent;
                case Spots.Red: return Card_Red_Parent;
                case Spots.Black: return Card_Black_Parent;
                case Spots.A_6: return Card_A_6_Parent;
                case Spots.Seven: return Card_Seven_Parent;
                case Spots._8_k: return Card_8_K_Parent;
                case Spots.Andar: return Card_Andar_Parent;
                case Spots.Bahar: return Card_Bahar_Parent;
                
            }
            return null;
        }

        int getSpawnNo(Chip chip)
        {
            switch (chip)
            {
                case Chip.Chip10: return 0;
                case Chip.Chip25: return 1;
                case Chip.Chip50: return 2;
                case Chip.Chip100: return 3;
                case Chip.Chip500: return 4;
                case Chip.Chip1000: return 5;
                case Chip.Chip5000: return 6;
                case Chip.Chip10000: return 7;
            }
            return 0;
        }

        void AddUserBet(Transform bettingSpot, Vector3 target)
        {
            Debug.Log("TIMERRR"+isTimeUp);
            Debug.Log("BETS PLACEMENT" +uiHandler.canPlaceBet );
            if (isTimeUp || !uiHandler.canPlaceBet|| AndarBahar_RoundWinnerHandlercs.Instance.winPoint !=0 || uiHandler.balance < (int)uiHandler.currentChip) return;
            // if (!uiHandler.IsEnoughBalanceAvailable()) return;
                        
            Chip chip = uiHandler.currentChip;          

            Spots spots = bettingSpot.GetComponent<AndarBahar.Gameplay.AndarBahar_InputHandler>().spot;
            
            // if (spots == Spots.Andar && uiHandler.Andar_bet_Allowed == 2 ) return;
            // if (spots == Spots.Bahar && uiHandler.Andar_bet_Allowed == 1 ) return;

            uiHandler.AddBets(spots);

            int spawnNo = ((int)chip);
            if(uiHandler.limit == true)
            {
                return;
            }
            // AndarBahar_ServerRequest.instance.OnChipMove(target, chip, spot);
            // CreateChip( target, chip, spots, getSpawnNo(chip));
            Debug.Log("this is the ready value" +uiHandler.ready );
            CreateChip( GetChipParent(spots).gameObject.transform.position, chip, spots, getSpawnNo(chip));
            // }
            // if (uiHandler.ready)
            // {
            //     CreateChip( GetChipParent(spots).gameObject.transform.position, chip, spots, getSpawnNo(chip));
            // }
            //CreateChip( GetChipParent(spots).gameObject.transform.position, chip, spots, getSpawnNo(chip));
            SoundManager.instance.PlayClip("addchip");

        }

        public void CreateChip(Vector3 target, Chip chip, Spots spot, int spawnNo)
        {
            // uiHandler.UpdateBets(spot, chip);
            Debug.Log("Chip Desti Vector:"+target );

            GameObject chipInstance = chipSpawner.Spawn(spawnNo, chip, GetChipParent(spot));
            chips.Add(chipInstance.transform);
            Bot bot = new Bot()
            {
                spot = spot,
                position = target,
                chip = chip
            };
            AndarBhar_Ai.AddData(bot);
            //if (AndarBahar_UiHandler.Instance.ready == true)//AndarBahar_UiHandler.Instance.balance >= (AndarBahar_UiHandler.Instance.currentChip)
            //{
            StartCoroutine(MoveChip(chipInstance, target));
            //}
        }
        public void CreateChip(object o)
        {
        }

        public float chipMovetime, speed;
        public iTween.EaseType easeType;


        IEnumerator MoveChip(GameObject chip, Vector3 target)
        {
            iTween.MoveTo(chip, iTween.Hash("position", target, "time", chipMovetime, "easetype", easeType));
            yield return new WaitForSeconds(speed);
            // float distance = Vector3.Distance(chip.transform.position, target);
            // //var rotat = new Vector3(0f, 0f, UnityEngine.Random.Range(0, 180));
            // //iTween.RotateTo(chip, iTween.Hash("rotation", rotat, "time", chipMovetime, "easetype", easeType));

            // while (distance > .1f)
            // {
            //     chip.transform.position = Vector3.MoveTowards(chip.transform.position, target, speed );
            //     distance = Vector3.Distance(chip.transform.position, target);
            //     yield return new WaitForEndOfFrame();
            // }
            // iTween.PunchScale(chip, iTween.Hash("x", .3, "y", 0.3f, "default", .1));
            //SoundManager.instance.PlayClip("addchip");

        }

        public void DestroyChips()
        {
             if (chips.Count > 0)
                MoveChipAndDestroy();

        }

        List<Transform> chips = new List<Transform>();
        public Transform chipDistroyerSpot;
        public float chipMoveTime = 1f;
        void MoveChipAndDestroy()
        {
            foreach (var chip in chips)
            {
                StartCoroutine(MoveChip(chip.gameObject));
            }
            chips.Clear();
        }
        IEnumerator MoveChip(GameObject chip)
        {
            var target = chipDistroyerSpot.position;

            float distance = Vector3.Distance(chip.transform.position, target);
            while (distance > .1f)
            {
                chip.transform.position = Vector3.MoveTowards(chip.transform.position, target, speed/2);
                distance = Vector3.Distance(chip.transform.position, target);
                yield return new WaitForEndOfFrame();
            }
            Destroy(chip);

        }


    }
}

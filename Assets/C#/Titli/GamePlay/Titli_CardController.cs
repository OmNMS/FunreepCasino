using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shared;
using KhushbuPlugin;
using Titli.UI;
using Titli.Utility;
// using Titli.ServerStuff;

namespace Titli.Gameplay
{
    public class Titli_CardController : MonoBehaviour
    {
        public static Titli_CardController Instance;
        public List<Image> _cardsImage;
        public Titli_BetManager betManager;
        public Transform ChipParent;
        float chipMovetime = .5f;
        [SerializeField] float chipMoveTime;
        [SerializeField] iTween.EaseType easeType;
        public Action<Transform, Vector3> OnUserInput;
        public Dictionary<Spots, Transform> chipHolder = new Dictionary<Spots, Transform>();
        public bool _startCardBlink, _canPlaceBet, _winNo;
        public GameObject UmbrellaChipPos, GoatChipPos, Pigeonchippos, Ballchippos, Diyachippos, Rabbitchippos, Dogchippos,
        Rosechippos, Flowerchippos, Kitechippos, Butterflychippos, Deerchippos,Fungamechippos;
        [SerializeField] Transform chipSecondLastSpot;
        [SerializeField] Transform chipLastSpot;
        public AudioClip AddChip;
        public AudioSource CoinMove_AudioSource;
        public IEnumerator CardBlink_coroutine;
        public List<GameObject> TableObjs;
        

        void Awake()
        {
            Instance = this;
            _canPlaceBet = true;
        }
        // Start is called before the first frame update
        void Start()
        {
            OnUserInput += CreateChip;
            chipHolder.Add(Spots.Umbrella, UmbrellaChipPos.transform);
            chipHolder.Add(Spots.Goat, GoatChipPos.transform);
            chipHolder.Add(Spots.Pigeon, Pigeonchippos.transform);
            chipHolder.Add(Spots.Ball, Ballchippos.transform);
            chipHolder.Add(Spots.Diya, Diyachippos.transform);
            chipHolder.Add(Spots.Rabbit, Rabbitchippos.transform);
            chipHolder.Add(Spots.Dog, Dogchippos.transform);
            chipHolder.Add(Spots.Rose, Rosechippos.transform);
            chipHolder.Add(Spots.Flower, Flowerchippos.transform);
            chipHolder.Add(Spots.Kite, Kitechippos.transform);
            chipHolder.Add(Spots.Butterfly, Butterflychippos.transform);
            chipHolder.Add(Spots.Deer, Deerchippos.transform);
            chipHolder.Add(Spots.FunGame, Fungamechippos.transform);
            _startCardBlink = true;
            _winNo = false;
        }

        public IEnumerator CardsBlink()
        {
            //if(_startCardBlink == true)
            //{

                foreach(var item in _cardsImage)
                {
                    var tempColor = item.color;
                    tempColor.a = 0.5f;
                    item.color = tempColor;
                    item.transform.GetChild(5).gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.25f);
                    tempColor.a = 1.0f;
                    item.color = tempColor;
                    item.transform.GetChild(5).gameObject.SetActive(false);
                    //
                }
                

                CardBlink_coroutine = CardsBlink();
                //StartCoroutine(CardsBlink());

            //}
        }

        public IEnumerator StopCardsBlink(int winn)
        {
            //int i=0;
            yield return StartCoroutine(CardsBlink());
            //foreach(var item in _cardsImage)
            for(int i=0;i<_cardsImage.Count;i++)
                {
                    if( i> winn)
                        continue;
                    var tempColor = _cardsImage[i].color;
                    tempColor.a = 0.5f;
                    _cardsImage[i].color = tempColor;
                    _cardsImage[i].transform.GetChild(5).gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.25f);
                    tempColor.a = 1.0f;
                    _cardsImage[i].color = tempColor;
                    if(i == winn)
                    {
                        //Debug.Log("vvvalue of i" +i + "value of winno"+ winn);
                        yield return new WaitForSeconds(2f);
                        _cardsImage[i].transform.GetChild(5).gameObject.SetActive(false);
                        break;
                    }
                    _cardsImage[i].transform.GetChild(5).gameObject.SetActive(false);
                   
                    
                }
                
            //_startCardBlink = false;
            // StopCoroutine(CardBlink_coroutine);
        }
        public List<GameObject> chipclone;

        void CreateChip(Transform bettingSpot, Vector3 target)
        {
            if (!Titli_UiHandler.Instance.IsEnoughBalancePresent()) return;
            Chip chip = Titli_UiHandler.Instance.currentChip;
            Spots spot = bettingSpot.GetComponent<BettingSpot>().spotType;
            Titli_UiHandler.Instance.AddBets(spot);
            // ServerRequest.instance.OnChipMove(target, chip, spot);
            switch (spot)
            {
                case Spots.Umbrella:
                    target = UmbrellaChipPos.transform.position;
                    break;
                case Spots.Goat:
                    target = GoatChipPos.transform.position;
                    break;
                case Spots.Pigeon:
                    target = Pigeonchippos.transform.position;
                    break;
                case Spots.Ball:
                    target = Ballchippos.transform.position;
                    break;
                case Spots.Diya:
                    target = Diyachippos.transform.position;
                    break;
                case Spots.Rabbit:
                    target = Rabbitchippos.transform.position;
                    break;
                case Spots.Dog:
                    target = Dogchippos.transform.position;
                    break;
                case Spots.Rose:
                    target = Rosechippos.transform.position;
                    break;
                case Spots.Flower:
                    target = Flowerchippos.transform.position;
                    break;
                case Spots.Kite:
                    target = Kitechippos.transform.position;
                    break;
                case Spots.Butterfly:
                    target = Butterflychippos.transform.position;
                    break;
                case Spots.Deer:
                    target = Deerchippos.transform.position;
                    break;
                case Spots.FunGame:
                    target = Fungamechippos.transform.position;
                    break;
                default:
                    break;
            }
            betManager.AddBets(spot, Titli_UiHandler.Instance.currentChip);
            // Titli_UiHandler.Instance.UpDateBets(spot, chip);         // not need as it gives bot data
            //GameObject 
            GameObject chipInstance = Titli_ChipSpawner.Instance.Spawn(0, chip, GetChipParent(spot));
            chipclone.Add(chipInstance);
            StartCoroutine(MoveChip(chipInstance, target));
            
        }

        Transform GetChipParent(Spots betType)
        {
            switch (betType)
            {
                case Spots.Umbrella: return UmbrellaChipPos.transform;
                case Spots.Goat: return GoatChipPos.transform;
                case Spots.Pigeon: return Pigeonchippos.transform;
                case Spots.Ball: return Ballchippos.transform;
                case Spots.Diya: return Diyachippos.transform;
                case Spots.Rabbit: return Rabbitchippos.transform;
                case Spots.Dog: return Dogchippos.transform;
                case Spots.Rose: return Rosechippos.transform;
                case Spots.Flower: return Flowerchippos.transform;
                case Spots.Kite: return Kitechippos.transform;
                case Spots.Butterfly: return Butterflychippos.transform;
                case Spots.Deer: return Deerchippos.transform;
                case Spots.FunGame: return Fungamechippos.transform;
            }
            return null;
        }

        public IEnumerator MoveChip(GameObject chip, Vector3 target)
        {
            iTween.MoveTo(chip, iTween.Hash("position", target, "time", chipMovetime, "easetype", easeType));
            //var rotat = new Vector3(0f, 0f, UnityEngine.Random.Range(0, 180));
            //iTween.RotateBy(gameObject, iTween.Hash("z", UnityEngine.Random.Range(0, 180), "easeType", "easeInOutBack", "loopType", "pingPong", "delay", chipMovetime));
            //iTween.RotateTo(chip, iTween.Hash("rotation", rotat, "time", chipMovetime, "easetype", easeType));
            yield return new WaitForSeconds(chipMovetime);
            // UtilitySound.Instance.addchipsound();
            StartCoroutine(PlayAudioClip());
            iTween.PunchScale(chip, iTween.Hash("x", .3, "y", 0.3f, "default", .1));
            
            
        }

        public IEnumerator PlayAudioClip()
        {
            yield return new WaitForSeconds(0.1f);
            CoinMove_AudioSource.clip = AddChip;
            CoinMove_AudioSource.Play();
            // CoinMove_AudioSource.Stop();
        }

        public void TakeChipsBack(Spots winner)
        {
            StartCoroutine(DestroyChips(winner));
        }
        public IEnumerator DestroyChips(Spots winnerSpot)
        {
            foreach (var item in chipHolder)
            {
                if (item.Key == winnerSpot) continue;
                foreach (Transform child in item.Value)
                {
                    StartCoroutine(MoveChips(child, chipSecondLastSpot));
                }
            }
            yield return new WaitForSeconds(1);
            foreach (Transform child in chipHolder[winnerSpot])
            {
                StartCoroutine(MoveChips(child, chipLastSpot));
            }
            //Titli_UiHandler.Instance.ResetUi();
        }

        public float waitTime = 2.5f;
        IEnumerator MoveChips(Transform chip, Transform destinatio)
        {
            iTween.MoveTo(chip.gameObject, iTween.Hash("position", destinatio.position, "time", 2.5f, "easetype", easeType));
            yield return new WaitForSeconds(waitTime);
            Destroy(chip.gameObject);
            //Debug.Log("This is the array of cardholer" + chipHolder);
        }


    }
}

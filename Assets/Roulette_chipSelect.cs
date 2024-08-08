using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Components;
using ViewModel;

public class Roulette_chipSelect : MonoBehaviour
{
    public GameObject Chip1;
    public GameObject Chip5;
    public GameObject Chip10;
    public GameObject Chip50;
    public GameObject Chip100;
    public GameObject Chip500;
    public GameObject Chip1000;
    public GameObject Chip5000;


    public CharacterTable CharacterTable;
    // Start is called before the first frame update
    void Start()
    {
        AddListeners();
    }

   private void AddListeners()
    {
        Chip1.GetComponent<Button>().onClick.AddListener(() =>
        {
            CharacterTable.currentChipSelected = Chip1.GetComponent<ChipSelected>().chipData;
        });

        Chip5.GetComponent<Button>().onClick.AddListener(() =>
        {
            
            CharacterTable.currentChipSelected = Chip5.GetComponent<ChipSelected>().chipData;
        });

        Chip10.GetComponent<Button>().onClick.AddListener(() =>
        {
            CharacterTable.currentChipSelected = Chip10.GetComponent<ChipSelected>().chipData;
        });

        Chip50.GetComponent<Button>().onClick.AddListener(() =>
        {
            CharacterTable.currentChipSelected = Chip50.GetComponent<ChipSelected>().chipData;
        });

        Chip100.GetComponent<Button>().onClick.AddListener(() =>
        {
            CharacterTable.currentChipSelected = Chip100.GetComponent<ChipSelected>().chipData;
        });

        Chip500.GetComponent<Button>().onClick.AddListener(() =>
        {
            CharacterTable.currentChipSelected = Chip500.GetComponent<ChipSelected>().chipData;
        });

        Chip1000.GetComponent<Button>().onClick.AddListener(() =>
        {
            CharacterTable.currentChipSelected = Chip1000.GetComponent<ChipSelected>().chipData;
        });
        Chip5000.GetComponent<Button>().onClick.AddListener(() =>
        {
            CharacterTable.currentChipSelected = Chip5000.GetComponent<ChipSelected>().chipData;
        });


    }
}

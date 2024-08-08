﻿using Shared;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AndarBahar.Gameplay
{
    class AndarBahar_Spawner:MonoBehaviour
    {
        [SerializeField] GameObject[] chips;
        [SerializeField] Transform[] spawnPostions;

        Dictionary<Chip, GameObject> chipContainer =
            new Dictionary<Chip, GameObject>();


        int chipOrderInLayer = 10;

        public void Start()
        {
            chipContainer.Add(Chip.Chip10, chips[0]);
            chipContainer.Add(Chip.Chip25, chips[1]);
            chipContainer.Add(Chip.Chip50, chips[2]);
            chipContainer.Add(Chip.Chip100, chips[3]);
            chipContainer.Add(Chip.Chip500, chips[4]);
            chipContainer.Add(Chip.Chip1000, chips[5]);
            chipContainer.Add(Chip.Chip5000, chips[6]);
            chipContainer.Add(Chip.Chip10000, chips[7]);
        }
        public GameObject Spawn(int positinIndex, Chip chipType, Transform parent)
        {
            var chip = Instantiate(chipContainer[chipType], parent);
            chip.GetComponent<SpriteRenderer>().sortingOrder = chipOrderInLayer++;
            chip.SetActive(true);
            chip.transform.position = spawnPostions[positinIndex].position;
            return chip;
        }
    }
}

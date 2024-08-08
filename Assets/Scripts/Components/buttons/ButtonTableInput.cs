using System.Collections;
using System.Collections.Generic;
using Commands;
using UnityEngine;
using ViewModel;
using UniRx;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Components;
using Managers;

namespace Commands
{
    [RequireComponent (typeof(Button))]
    public class ButtonTableInput : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {
        public CharacterTable characterTable;
        public ButtonTable buttonData;
        
        private IStatusButton _statusButton;
        private IReseteableButton _resetableButton;
        private IInteractableButton _interactableButton;
        public ILongPress _longPress;
        Chip MyChip;



        void Awake() 
        {
            _statusButton = GetComponent<IStatusButton>();    
            _resetableButton = GetComponent<IReseteableButton>();    
            _interactableButton = GetComponent<IInteractableButton>();    
            _longPress = GetComponent<ILongPress>();    
        }
        
        void Start()
        {
           // CSel = new ChipSelected();
            _resetableButton.ResetButton(buttonData);

            characterTable.currentTableActive
                .Subscribe(OnActiveButton)
                .AddTo(this);
        }
        
        private void OnActiveButton(bool isActive)
        {
            _statusButton._isActive = isActive;
            if(_statusButton._isActive) _resetableButton.ResetButton(buttonData);       
        }

        

        public void Click()
        {
            if (!_statusButton._isActive)
                return;

            Debug.Log("button data  " + buttonData.currentChipsOnTop);
            _interactableButton.InstantiateChip(characterTable, buttonData);
            Debug.Log("click");
            GameManager.Instance.BetsPlaced(characterTable.currentChipSelected.chipValue);
       //   PlacedBetsTxt.text =   characterTable.currentChipSelected.chipValue.ToString();
        }

        public void OnPointerDown (PointerEventData eventData) 
        {
            _longPress.SetPointerDown(true);
            Debug.Log("pinter down");
        }

        private void Update ()
        {
            _longPress.LongPressCheck(characterTable, buttonData);

            CheckChip();


        }
        
        public void CheckChip()
        {
            
        }

        public  void OnPointerUp (PointerEventData eventData) 
        {
            _longPress.LongPress(characterTable, buttonData, false);
            _longPress.ResetPointer();
            Click();
        }
    }
}

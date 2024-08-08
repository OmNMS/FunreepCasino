using UnityEngine;
using ViewModel;
using Components;

namespace Commands
{
    public interface ITableInteractable
    {
         void PushChipInButton(CharacterTable characterTable, ButtonTable buttonData, ChipGame chipGame, Chip chipData, GameObject chipInstance, Vector2 spritePivot, Vector2 offsetPosition, bool fichasOnTop);
    }
}

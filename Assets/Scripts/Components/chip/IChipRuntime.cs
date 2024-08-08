using UnityEngine;
using ViewModel;

namespace Components
{
    public interface IChipRuntime
    {
         Chip currentChipData {get; set;}
         Vector2 currentPosition {get; set;}
         ButtonTable currentButton {get; set;}
         void StartChip(Chip chipData, Vector2 position, ButtonTable buttonPressed, SpriteRenderer currentSprite);
    }
}

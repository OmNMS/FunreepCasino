using System;
using System.Collections;
using UniRx;
using UnityEngine;
using ViewModel;

namespace Infrastructure
{
    public interface IRound 
    {
        IObservable<Unit> PlayTurn();
         int randomNumber {get; set;}    
    }
}

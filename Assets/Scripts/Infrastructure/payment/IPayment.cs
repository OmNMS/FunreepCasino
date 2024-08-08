using System;
using UniRx;
using ViewModel;

public interface IPayment 
{
     IObservable<Unit> PaymentSystem(CharacterTable characterTable);
     int PaymentValue
    {
        get;
        set;
    }
}
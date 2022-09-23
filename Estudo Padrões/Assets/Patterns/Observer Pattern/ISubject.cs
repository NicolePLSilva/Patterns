using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public interface ISubject 
    {
        void Attach(IObserver observer); //Add observer 
        void Detach(IObserver observer); //remove observer
        void Notify(); //Notify Observers
    }
}

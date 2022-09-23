using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern_DelegatesAndEvents
{
    //public delegate void ChangeStatusDelegate();
    public interface IObserver
    {
        // void Attach(ISubject s);
        // void Detach(ISubject s);

        void UpdateObserver(ISubject s, bool state);

    }
}
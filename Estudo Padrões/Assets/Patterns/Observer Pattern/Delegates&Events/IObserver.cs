using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern_DelegatesAndEvents
{
    public interface IObserver
    {
        void UpdateObserver(ISubject s, bool state);

    }
}
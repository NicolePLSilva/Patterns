using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern_DelegatesAndEvents
{
    public delegate void ChangeStatusDelegate(ISubject s, bool b);
    public interface ISubject 
    {
        static event ChangeStatusDelegate OnNotify; //Notify Observers
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public interface IObserver 
    {
        void UpdateObserver(ISubject subject, bool state); //Update observers...optional parametre
    }
}

using UnityEngine;
using System.Collections.Generic;

public class ManagerObserverSystem : MonoBehaviour 
{
    public List<Observer> observers;

    public Subject subject;


    private void Update() 
    {
        Link();    
    }

    public void Link()
    {
        if(subject == null) return;
        if(observers == null) return;

        foreach (Observer o in observers)
        {
            subject.Attach(o);
        }
    }     
}
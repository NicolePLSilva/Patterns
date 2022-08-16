using System.Collections; //Permite uso do List<>
using System; //Permite uso do IComparable interface
using UnityEngine;
using System.Collections.Generic;

public class Subject : MonoBehaviour, ISubject 
{
    List<IObserver> observers;//Armazena observadores dentro de uma coleção

    // [SerializeField]enum Estado{verde, amarelo, azul};

    private void Start() 
    {
            
    }

    public void Attach(IObserver o)
    {
        observers.Add(o);
    }

    public void Detach(IObserver o)
    {
        observers.Remove(o);
    }

    public void DoSomething()//realizar uma ação
    {
        Debug.Log("Realizando ação: ");
        Notify();
    }

    public void ChangeStatus()
    {
        Debug.Log("Mudou status para:");
        Notify();
    }

    public void Notify()
    {
        foreach (var o in observers)
        {
            o.UpdateObserver(this);
        }
    }
}
using System.Collections; //Permite uso do List<>
using System; //Permite uso do IComparable interface
using UnityEngine;
using System.Collections.Generic;

namespace ObserverPattern
{
    public class Subject : MonoBehaviour, ISubject 
    {
        List<IObserver> observers = new List<IObserver>();//Armazena observadores dentro de uma coleção

        
        enum State{verde, amarelo,  vermelho};// Define Enum

        [SerializeField] State status; // Show in inspector
        [SerializeField] GameObject[] lightSprite;

        bool stateBool;

        private void Awake() 
        {
                //status = State.vermelho;
                //stateBool = false;
                ClickGreen();
        }

        public void Attach(IObserver o)
        {
            observers.Add(o);
        }

        public void Detach(IObserver o)
        {
            observers.Remove(o);
        }

        public void ChangeStatus()
        {
            Debug.Log("Notificando: mudou status para:" + status);
            Notify();
        }

        IEnumerator ChangingStatus(State newState)
        {
            status = State.amarelo;
            Debug.Log("Amarelo");
            lightSprite[2].SetActive(false);
            lightSprite[1].SetActive(true);
            ChangeStatus();

            yield return new WaitForSeconds(3);  
            Debug.Log("New " + newState);
            lightSprite[1].SetActive(false);
            status = newState;
            Debug.Log("Current " + status);
            lightSprite[0].SetActive(true);
            ChangeStatus();
        }

        public void ClickRed()
        {
            StartCoroutine(ChangingStatus(State.vermelho));
            stateBool = false;
            Debug.Log("Loading para Vermelho");
        }

        public void ClickGreen()
        {
            //StartCoroutine(ChangingStatus(State.verde));
            status = State.verde;
            stateBool = true;
            lightSprite[2].SetActive(true);
            lightSprite[0].SetActive(false);
            
            ChangeStatus();
            Debug.Log("Loading para Verde");
        }


        public void Notify()
        {
            if(observers == null) return;
            foreach (var o in observers)
            {
                o.UpdateObserver(this, stateBool);
            }
        }
    }
}
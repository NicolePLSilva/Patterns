using System.Collections; //Permite uso do List<>
using System; //Permite uso do IComparable interface
using UnityEngine;
using System.Collections.Generic;

namespace ObserverPattern
{
    public class Subject : MonoBehaviour, ISubject 
    {
        List<IObserver> observers = new List<IObserver>();//stores observers within a collection

        
        enum State{green, yellow,  red};// Define Enum

        [SerializeField] State status; // Show in inspector
        [SerializeField] GameObject[] lightSprite;

        bool stateBool;

        private void Awake() 
        {
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
            Notify();
        }

        IEnumerator ChangingStatus(State newState)
        {
            status = State.yellow;
    
            lightSprite[2].SetActive(false);
            lightSprite[1].SetActive(true);

            ChangeStatus();

            yield return new WaitForSeconds(3);  

            lightSprite[1].SetActive(false);
            status = newState;
  
            lightSprite[0].SetActive(true);
            ChangeStatus();
        }

        public void ClickRed()
        {
            StartCoroutine(ChangingStatus(State.red));
            stateBool = false;
        }

        public void ClickGreen()
        {

            status = State.green;
            stateBool = true;
            lightSprite[2].SetActive(true);
            lightSprite[0].SetActive(false);
            
            ChangeStatus();
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
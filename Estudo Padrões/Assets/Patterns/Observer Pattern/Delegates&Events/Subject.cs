using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern_DelegatesAndEvents
{
    public class Subject : MonoBehaviour, ISubject
    {
        public static event ChangeStatusDelegate OnNotify;

        enum State{verde, amarelo,  vermelho};// Define Enum

        [SerializeField] State status; // Show in inspector
        [SerializeField] GameObject[] lightSprite;

        bool stateBool;

        private void Awake() 
        {
                status = State.vermelho;
                stateBool = false;
        }

// call OnNotify to notify observers about changes of states 
        public void DoSomething()
        {
            Debug.Log("Realizando ação: ");
            OnNotify?.Invoke(this, stateBool); //if (OnNotify != null) OnNotify(this, stateBool);
        }

        public void ChangeStatus()
        {
            Debug.Log("Notificando: mudou status para:" + status);
            OnNotify?.Invoke(this, stateBool); //if (OnNotify != null) OnNotify(this, stateBool);
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


        // public void Notify()
        // {
        //     if(observers == null) return;
        //     foreach (var o in observers)
        //     {
        //         o.UpdateObserver(this, stateBool);
        //     }
        // }
    
        
    }
}
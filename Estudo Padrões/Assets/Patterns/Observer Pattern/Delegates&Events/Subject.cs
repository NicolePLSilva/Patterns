using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ObserverPattern_DelegatesAndEvents
{
    public class Subject : MonoBehaviour, ISubject
    {
        public static event ChangeStatusDelegate OnNotify;

        enum State{green, yellow,  red};// Define Enum

        [SerializeField] State status; // Show in inspector
        [SerializeField] GameObject[] lightSprite;

        [SerializeField] Button[] button;

        bool stateBool;

        private void Awake() 
        {
            ClickGreen();
        }

        // call OnNotify to notify observers about changes of states
        public void ChangeStatus()
        {
            OnNotify?.Invoke(this, stateBool); //if (OnNotify != null) OnNotify(this, stateBool);
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
            //LockClick(button[1]);
        }

        public void ClickGreen()
        {

            status = State.green;
            stateBool = true;
            lightSprite[2].SetActive(true);
            lightSprite[0].SetActive(false);
            
            ChangeStatus();
            //LockClick(button[0]);
        }

        private void LockClick(Button b)
        { 
            if(b.Equals(button[0]))
            {
                b.interactable = false;
                button[1].interactable = true;
            }
            else if(b.Equals(button[1]))
            {
                b.interactable = false;
                button[0].interactable = true;
            }
        }

    }
}
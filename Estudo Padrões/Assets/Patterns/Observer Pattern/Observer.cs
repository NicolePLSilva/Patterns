using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ObserverPattern
{
    public class Observer : MonoBehaviour, IObserver
    {
        [SerializeField]float horizontal = 1f; //Define direction
        [SerializeField]float speed = 1.5f; //Define movement speed
        [SerializeField]float initialSpeed = 1.5f; //Define initial movement speed
        [SerializeField]float maxVelocity = 3f;
        [SerializeField] Transform startPosition;

        Rigidbody2D myRigidbody;
        public bool canMove = false;
        public bool reductionZone = false;
        
        private void OnEnable() 
        {
            //StartCoroutine(DisableRotine());
            myRigidbody = GetComponent<Rigidbody2D>();
            GetComponentInChildren<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            
            
            initialSpeed = speed;
            //Find subjects in the scene to subscribe this observer
            foreach (var s in FindObjectsOfType<Subject>())
            {
                s.Attach(this);
            }
            reductionZone = false;
            canMove = true;
            Debug.Log("OnEnable");
        }

        IEnumerator DisableRotine()
        {
            yield return new WaitForSeconds(1.4f);
            gameObject.SetActive(false);
        }
    
        private void FixedUpdate() 
        {
            if(canMove)
            {
                CarMove();
            }else if(!canMove & reductionZone)
            {
                CarDontMove();
            }   
        }
        
        public void UpdateObserver(ISubject subject, bool state)
        {
            if (subject == null) { return; }
            canMove = state;     
        }

        private void CarMove()
        {
            if(initialSpeed>speed)
            {
                //Acceleration to initial speed
                speed = Mathf.SmoothStep(speed, initialSpeed, 4 * Time.deltaTime);
            }
            Vector2 carVelocity = new Vector2(horizontal * speed, myRigidbody.velocity.y);
            myRigidbody.velocity = carVelocity;
        }

        private void CarDontMove()
        {
            //Speed reduction before stop
            speed = Mathf.SmoothStep(speed, 0, 4 * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.tag != "Respawn" || other.tag != "Disable")
            {
                reductionZone = true;
            }
                
        }
        private void OnTriggerExit2D(Collider2D other) 
        {
            if(other.tag == "Disable")
            {
                gameObject.SetActive(false);
                transform.position = GetComponentInParent<Transform>().position;
                Debug.Log("Desabilitando");
                return;
            }
            reductionZone = false;    
            //Car stop completely 
            Vector2 carVelocity = new Vector2(horizontal * 0, myRigidbody.velocity.y);
            myRigidbody.velocity = carVelocity;
        }

        private void OnCollisionEnter2D(Collision2D other) 
        {
            CarDontMove();    
        }

    }
}
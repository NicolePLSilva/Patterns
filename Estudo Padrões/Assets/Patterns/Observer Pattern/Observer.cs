using System;
using UnityEngine;

namespace ObserverPattern
{
    public class Observer : MonoBehaviour, IObserver
    {
        [SerializeField]float horizontal = 1f; //Define direction
        [SerializeField]float speed = 1.2f; //Define movement speed
        [SerializeField]float initialSpeed; //Define initial movement speed
        [SerializeField]float maxVelocity = 3f;

        Rigidbody2D myRigidbody;
        public bool canMove = false;
        public bool reductionZone = false;
        
        private void Start() 
        {   
            myRigidbody = GetComponent<Rigidbody2D>();
            initialSpeed = speed;
            //Find subjects in the scene to subscribe this observer
            foreach (var s in FindObjectsOfType<Subject>())
            {
                s.Attach(this);
            }
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
            reductionZone = true;    
        }
        private void OnTriggerExit2D(Collider2D other) 
        {
            reductionZone = false;    
            //Car stop completely 
            Vector2 carVelocity = new Vector2(horizontal * 0, myRigidbody.velocity.y);
            myRigidbody.velocity = carVelocity;
        }

    }
}
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{    
    public class Car : MonoBehaviour, IObserver, ITraffic 
    {
        [SerializeField] float horizontal = 1f;
        [SerializeField] float currentSpeed = 1.4f;
        [SerializeField] float initialSpeed = 1.4f;

        [SerializeField] float distanceToTheOther = 0.05f;
        [SerializeField] LayerMask mask;

        Rigidbody2D myRigidbody;
        Transform crosswalkPosition;
        Vector2 carSize;

        bool isGreen = false;
        bool isStoppingZone;
        bool isTouched;


        private void Awake() 
        {
            myRigidbody = GetComponent<Rigidbody2D>();  
            carSize = GetComponent<BoxCollider2D>().size; 

            GameObject crosswalk = GameObject.Find("Crosswalk");
            crosswalkPosition = crosswalk.GetComponent<Transform>();

            isGreen = true;
            isStoppingZone = false;
        }
        private void OnEnable() 
        {
            GetComponentInChildren<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            
            //Find subjects in the scene to subscribe this observer
            foreach (var s in FindObjectsOfType<Subject>())
            {
                s.Attach(this);
            }
            if (!isGreen)
            {
                Moves();
            }
        }

        private void FixedUpdate()
        {
            if (isGreen)
            {
                Moves();
            }
            else if (!isGreen )
            {
                if (isStoppingZone || isTouched)
                {
                    Stops();
                }
            }
            else
            {
                CheckRaycast();
            } 
        }

        public void Moves()
        {
            Vector2 carVelocity = new Vector2(horizontal * currentSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = carVelocity;
        }

        public void Stops()
        {
            Vector2 carVelocity = new Vector2(horizontal * 0, myRigidbody.velocity.y);
            myRigidbody.velocity = carVelocity;
        }

        public void UpdateObserver(ISubject subject, bool state)
        {
            if (subject == null) { return; }
            isGreen = state;     

        }

        private void OnTriggerStay2D(Collider2D other) 
        {
           if(other.tag.Equals("Stopping"))
           {
                isStoppingZone = true;
           }
        }

        private void OnTriggerExit2D(Collider2D other) 
        {   
            CollisionRoutine(other.tag.ToString());
        }

        public void CheckRaycast()
        {
            Vector2 rayRight = (Vector2)transform.position + Vector2.right * carSize.x * 0.5f;
            isTouched = Physics2D.Raycast(rayRight, Vector2.right, distanceToTheOther, mask);  
        }

        public void CollisionRoutine(string other)
        {
            if (other.Equals("Stopping"))
            {
                isStoppingZone = false;
            }
            if (other.Equals("Disable"))
            {
                currentSpeed = initialSpeed;
                gameObject.SetActive(false);
                transform.position = GetComponentInParent<Transform>().position;
                return;
            }    
        }
    }
}
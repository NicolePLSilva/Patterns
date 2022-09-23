 using UnityEngine;

namespace ObserverPattern_DelegatesAndEvents
{ 
    public class Observer : MonoBehaviour , IObserver
    {
        enum Classification{car, people, stationary};// Define Enum
        [SerializeField] Classification classification; // Show in inspector
        [SerializeField]float horizontal = 1f; //Define direction
        [SerializeField]float vertical = 1f; //Define direction
        [SerializeField]float speed = 1.2f; //Define movement speed
        [SerializeField]float initialSpeed; //Define movement speed
        [SerializeField]float maxVelocity = 3f;

        Rigidbody2D myRigidbody;
        public bool canMove = false;
        public bool reductionZone = false;
        
        private void Start() 
        {    
            myRigidbody = GetComponent<Rigidbody2D>();
            initialSpeed = speed;
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
        private void OnEnable()
        {
                Subject.OnNotify +=UpdateObserver;
        }
        private void OnDisable()
        {
                Subject.OnNotify -=UpdateObserver;
        }

        public void UpdateObserver(ISubject s, bool state)
        {
            if (s == null) { return; }
            canMove = state;
        }

         private void CarMove()
        {
            if(classification == Classification.car)
            {   
                if(initialSpeed>speed)
                {
                    Debug.Log("Test acceleration");
                    speed = Mathf.SmoothStep(speed, initialSpeed, 4 * Time.deltaTime);
                }
                Debug.Log("Continue Drive");
                Vector2 playerVelocity = new Vector2(horizontal * speed, myRigidbody.velocity.y);
                myRigidbody.velocity = playerVelocity;
            }
            if(classification == Classification.people)
            {
                Debug.Log("Stop");
            }
        }

        private void CarDontMove()
        {
            if(classification == Classification.car)
            {
                Debug.Log("Stop");
                speed = Mathf.SmoothStep(speed, 0, 4 * Time.deltaTime);
            }
            if(classification == Classification.people)
            {
                Debug.Log("Cross crosswalk");
            }
        }

        private void OnTriggerEnter2D(Collider2D other) 
        {
            Debug.Log("Testando Trigger");
            reductionZone = true;    
        }
        private void OnTriggerExit2D(Collider2D other) 
        {
            reductionZone = false;    
            Vector2 playerVelocity = new Vector2(horizontal * 0, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVelocity;
        }

        void SpeedReduction()
        {
            Vector2 playerVelocity = new Vector2(horizontal * 0, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVelocity;
        }

    }
}
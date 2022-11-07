using UnityEngine;

namespace ObserverPattern_DelegatesAndEvents
{    
    public class Walker : MonoBehaviour, IObserver, ITraffic 
    {
        [SerializeField] float vertical = 1f;
        [SerializeField] float currentSpeed = 1.4f;
        [SerializeField] float initialSpeed = 1.4f;


        [SerializeField] float distanceToTheOther = 0.05f;
        [SerializeField] LayerMask mask;

        Rigidbody2D myRigidbody;
        Vector2 walkerSize;

        bool isGreen;
        bool isStoppingZone;
        bool isTouching;

        private void Awake() 
        {
            myRigidbody = GetComponent<Rigidbody2D>();
            walkerSize = GetComponent<BoxCollider2D>().size; 
            isStoppingZone = false;
            Moves();
        }

        private void OnEnable() 
        {
            Subject.OnNotify += UpdateObserver;
            foreach (var child in GetComponentsInChildren<SpriteRenderer>())
            {
                child.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }
            
            if (isGreen)
            {
                Moves();
            }
        }

        private void OnDisable() 
        {
            //Subject.OnNotify -= UpdateObserver;
        }

        private void FixedUpdate() 
        {
            if (!isGreen)
            {
                Moves();
            }
            else if (isGreen )
            {
                if (isStoppingZone || isTouching)
                {
                    Stops();
                }
            }
           
                CheckRaycast();
            
        }

        public void Moves()
        {
           Vector2 walkerVelocity = new Vector2(myRigidbody.velocity.x, vertical * currentSpeed);
            myRigidbody.velocity = walkerVelocity;
        }

        public void Stops()
        {
            Vector2 walkerVelocity = new Vector2(myRigidbody.velocity.x, vertical * 0);
            myRigidbody.velocity = walkerVelocity;
        }

        public void UpdateObserver(ISubject subject, bool state)
        {
            if (subject == null) { return; }
            isGreen = state;     
        }

        private void OnTriggerStay2D(Collider2D other) 
        {
           if(other.CompareTag("Stopping"))
           {
                isStoppingZone = true;
           }
        }

        private void OnTriggerExit2D(Collider2D other) 
        {   
            CollisionRoutine(other.tag.ToString());
        }

       
        private void OnCollisionEnter2D(Collision2D other) 
        {
            if(other.gameObject.tag.Equals("Car"))
            {
                CollisionRoutine(other.gameObject.tag.ToString());   
            }
        }

        public void CollisionRoutine(string other)
        {
            if (other.Equals("Stopping"))
            {
                isStoppingZone = false;
            }
            if (other.Equals("Disable") || other.Equals("Car"))
            {
                isStoppingZone = false;
                currentSpeed = initialSpeed;
                gameObject.SetActive(false);

                transform.position = GetComponentInParent<Transform>().position;
                return;
            }    
        }


        public void CheckRaycast()
        {
            Vector2 rayUp = (Vector2)transform.position + Vector2.up * walkerSize.y * 0.5f;
            isTouching = Physics2D.Raycast(rayUp, Vector2.up, distanceToTheOther, mask);  
        }


    }
}
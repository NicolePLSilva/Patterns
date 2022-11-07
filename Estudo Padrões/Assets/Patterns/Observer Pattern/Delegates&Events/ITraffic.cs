using UnityEngine;

namespace ObserverPattern_DelegatesAndEvents
{
    public interface ITraffic 
    {
        void Moves();
        void Stops();
        void CheckRaycast();
        void CollisionRoutine(string s);
    }
}
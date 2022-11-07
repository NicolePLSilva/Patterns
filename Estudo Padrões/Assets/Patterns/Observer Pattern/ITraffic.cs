using UnityEngine;

namespace ObserverPattern
{
    public interface ITraffic 
    {
        void Moves();
        void Stops();
        void CheckRaycast();
        void CollisionRoutine(string s);
    }
}

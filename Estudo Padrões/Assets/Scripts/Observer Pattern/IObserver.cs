using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver 
{
    void UpdateObserver(ISubject subject, bool state); //Atualizar observadores...passagem de parametro opcional
}

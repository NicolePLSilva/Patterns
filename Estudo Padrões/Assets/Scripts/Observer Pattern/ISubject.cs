using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject 
{
    void Attach(IObserver observer); //Adicionar observador 
    void Detach(IObserver observer); //Remover observador
    void Notify(); //Notificar observadores
}

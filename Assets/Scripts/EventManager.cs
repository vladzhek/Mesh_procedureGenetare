using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent MoveObject = new UnityEvent();

    private void FixedU()
    {
        //InvokeMeshGenerate();
    }

    public static void InvokeMeshGenerate()
    {
       // MeshGenerate.Invoke();
    }

    public void InvokeMoveObject(GameObject gm)
    {
        //MoveObject.Invoke(gm);
    }

}

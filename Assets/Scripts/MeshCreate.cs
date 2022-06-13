using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeshCreate : MonoBehaviour
{
    [SerializeField]
    GameObject _Player;

    public void SetMesh()
    {
        _Player.GetComponent<Player>().SetParamsMesh();
        GetComponent<MeshFilter>().mesh = Manager.Instance.NewMesh;
    }    
}


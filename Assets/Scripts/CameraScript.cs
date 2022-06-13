using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    GameObject Player, _Mesh;

    [SerializeField]
    public static bool EndCameraAnimation = false;
    public static bool StartCameraAnimation = false;
    public static float AngleBetweenPoints;

    void Start()
    {
        MeshBackGroundRecreate();        
        GetComponent<Camera>().farClipPlane = 1000;
        transform.position = new Vector3(
                    transform.position.x,
                    40,
                    -500);
    }
    void FixedUpdate()
    {
        if (EndCameraAnimation)
        {
            Quaternion needRotation = Quaternion.Euler(0.0f, 0.0f, AngleBetweenPoints);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, needRotation, 300 * Time.deltaTime);
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -90);
        }
        else if (StartCameraAnimation)
        {
            CameraStartAnimation();
        }
    }

    public void MeshBackGroundRecreate()
    {
        _Mesh.GetComponent<MeshFilter>().mesh.Clear();
        Vector3 ver0 = new Vector3(-Manager.SizeSite, 0, 0);
        Vector3 ver1 = new Vector3(-Manager.SizeSite, Manager.QuadForY * Manager.CountPoints, 0);
        Vector3 ver2 = new Vector3(Manager.SizeSite + Manager.SizeBreaking, 0, 0);
        Vector3 ver3 = new Vector3(Manager.SizeSite + Manager.SizeBreaking, Manager.QuadForY * Manager.CountPoints, 0);     
        var mesh = new Mesh();
        mesh = Manager.Instance.Quad(ver0, ver1, ver2, ver3);
        _Mesh.GetComponent<MeshFilter>().mesh = mesh;
        _Mesh.GetComponent<SkinnedMeshRenderer>().sharedMesh = mesh;
    }

    public void CameraStartAnimation()
    {
        Vector3 MoveVector = new Vector3(Player.transform.position.x, Player.transform.position.y, -90);
        transform.position = Vector3.MoveTowards(transform.position, MoveVector, 250f*Time.deltaTime);
        if (transform.position == MoveVector)
        {
            EndCameraAnimation = true;
            StartCameraAnimation = false;
        }
    }
    public void StartCameraAnimateBool()
    {
        StartCameraAnimation = true;
    }
    public void HideMeshBackGround()
    {
        _Mesh.SetActive(false);
    }
}

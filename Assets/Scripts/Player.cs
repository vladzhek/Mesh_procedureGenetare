using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    bool CurretPositonLeft = true;
    public List<Vector3> listPoints;
    public List<Vector3> listPointsSecond;
    int CountPoints, MovePoint = 0;
    float AngleRotate;

    [SerializeField]
    GameObject VisualObjectForForwarding;

    Vector3 MoveVectorChield = new Vector3(0,0,0);

    public void SetParamsMesh()
    {
        listPoints = Manager.Instance.listPoints;
        listPointsSecond = Manager.Instance.listPointsSecond;
        CountPoints = Manager.CountPoints;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CameraScript.EndCameraAnimation)
        {
           OffsetPlayer();
        }
    }

    void FixedUpdate()
    {
        Quaternion needRotation = Quaternion.Euler(0.0f, 0.0f, CameraScript.AngleBetweenPoints);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, needRotation, 100 * Time.deltaTime);
        VisualObjectForForwarding.transform.localPosition = Vector3.MoveTowards(VisualObjectForForwarding.transform.localPosition, MoveVectorChield, 0.5f);
    }       

    public void MovePlayerFromManager()
    {
        MoveObj(this.gameObject);
    }

    public void MoveObj(GameObject obj)
    {

        Vector3 MoveVector = new Vector3(listPoints[MovePoint].x + (Manager.SizeBreaking / 2), listPoints[MovePoint].y, 0);
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, MoveVector, 0.3f);        
        if (obj.transform.position == MoveVector)
        {
            if (MovePoint == CountPoints - 1)
            {
                SceneManager.LoadScene("SampleScene");
                CameraScript.EndCameraAnimation = false;
                CameraScript.StartCameraAnimation = false;
                Manager.CurrentPositionCombine = 0;
                MovePoint = 0;
            }
            else
            {
                if (MovePoint < CountPoints - 1)
                {
                    MovePoint++;
                }

                if (Manager.Instance.SetObjSide(MovePoint) < 0)
                {
                    Vector3 VectorAB = listPoints[MovePoint] - listPoints[MovePoint - 1];
                    AngleRotate = Vector3.SignedAngle(VectorAB, listPoints[MovePoint], Vector3.left);
                }
                if (Manager.Instance.SetObjSide(MovePoint) > 0)
                {

                    Vector3 VectorAB = listPoints[MovePoint] - listPoints[MovePoint - 1];
                    AngleRotate = Vector3.SignedAngle(VectorAB, listPoints[MovePoint], Vector3.left);
                    AngleRotate *= -1;
                }
                CameraScript.AngleBetweenPoints = AngleRotate;
            }
        }
    }

    void OffsetPlayer()
    {
        if (CurretPositonLeft)
        {
            MoveVectorChield = new Vector3(Manager.SizeBreaking / 2.5f, 0f, 0f);
            CurretPositonLeft = false;
        }
        else
        {
            MoveVectorChield = new Vector3(-(Manager.SizeBreaking / 2.5f), 0f, 0f);
            CurretPositonLeft = true;
        }
    }
}

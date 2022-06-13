using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Manager : MonoBehaviour
{
    public static UnityEvent MoveObject = new UnityEvent();
    public static Manager Instance { get; private set; } // Экземпляр объекта


    public static int CurrentPositionCombine = 0;
    public CombineInstance[] combine;

    [HideInInspector]
    public Mesh NewMesh;

    [SerializeField]
    public static int CountPoints = 7;
    [SerializeField]
    public static int SizeBreaking = 15;
    [SerializeField]
    public static int SizeSite = 50;

    public static int QuadForX = 10;
    public static int QuadForY = 10;

    public List<Vector3> listPoints;
    public List<Vector3> listPointsSecond;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        if(CameraScript.EndCameraAnimation)
            MoveObject.Invoke();
    }   

    public void GenerateMesh()
    {
        combine = new CombineInstance[CountPoints * 2];
        SideBreaking(CountPoints);
        var mesh = new Mesh();
        mesh.CombineMeshes(combine, true, false);
        NewMesh = mesh;
    }
   
    public void GameObjectAngleRotationZ(GameObject gameObj, float AngleRotate)
    {
        Quaternion myRotation = Quaternion.identity;
        myRotation.eulerAngles = new Vector3(0, 0, AngleRotate);
        gameObj.transform.rotation = myRotation;
    }
    public Mesh Quad(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2, Vector3 vertex3)
    {
        var mesh = new Mesh
        {
            vertices = new[] { vertex0, vertex1, vertex2, vertex3 },
            triangles = new[] { 0, 1, 2, 1, 3, 2 }
        };
        mesh.RecalculateNormals();
        mesh.RecalculateUVDistributionMetrics();
        return mesh;
    }

    void combineMesh(Vector3 vertical0, Vector3 vertical1, Vector3 vertical2, Vector3 vertical3)
    {
        combine[CurrentPositionCombine].mesh = Quad(vertical0, vertical1, vertical2, vertical3);
        CurrentPositionCombine++;
    }

    void VerticalisRightSide(Vector3 vertNext, Vector3 vertPrev)
    {
        var vertical0 = vertPrev;
        var vertical1 = vertNext;
        var vertical2 = new Vector3(SizeSite + SizeBreaking, vertPrev.y, 0);
        var vertical3 = new Vector3(SizeSite + SizeBreaking, vertNext.y, 0);
        combineMesh(vertical0, vertical1, vertical2, vertical3);
    }

    void VerticalisLeftSide(Vector3 vertNext, Vector3 vertPrev)
    {
        var vertical0 = new Vector3(-SizeSite, vertPrev.y, 0);
        var vertical1 = new Vector3(-SizeSite, vertNext.y, 0);
        var vertical2 = vertPrev;
        var vertical3 = vertNext;
        combineMesh(vertical0, vertical1, vertical2, vertical3);
    }

    public int SetObjSide(int i)
    {
        int ChoseSpacePosition = 1;
        if (i % 2 == 0)
            ChoseSpacePosition = 1;
        else ChoseSpacePosition = -1;

        return ChoseSpacePosition;
    }

    public void SideBreaking(int countPoints)
    {
        Vector3 listXY;
        listPoints = new List<Vector3>();
        listPointsSecond = new List<Vector3>();
        float rndX = 0, rndY = 0;
        int CoordinateStart = 0, z = 0; ;
        for (int i = 0, j = 1; i < countPoints; i++)
        {
            if (i == CoordinateStart)
            {
                rndX = CoordinateStart;
                rndY = CoordinateStart;
            }
            else
            {
                rndX = Random.Range(QuadForX, QuadForX+5) * SetObjSide(i);
                rndY = Random.Range(rndY + QuadForY, rndY + QuadForX+5);
            }

            listXY = new Vector3(rndX, rndY, z);
            listPoints.Add(listXY);

            listXY = new Vector3(rndX + SizeBreaking, rndY, z);
            listPointsSecond.Add(listXY);
            if (i > 0)
            {
                VerticalisLeftSide(listPoints[j], listPoints[j - 1]);
                VerticalisRightSide(listPointsSecond[j], listPointsSecond[j - 1]);
                j++;
            }
        }
    }
}

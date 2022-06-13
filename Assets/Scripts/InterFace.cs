using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterFace : MonoBehaviour
{
    [SerializeField]
    Text SizeSideText ,BreakingSizeText,
         LengthSegmentsText ,TextInclinationText, TextCountPOints;
    [SerializeField]
    Scrollbar ScrollbarSizeSide, ScrollbarBreakingSize,
          ScrollbarLengthSegments, ScrollbarInclination, ScrollbarCountPoints;
    [SerializeField]
    GameObject PanelFirstMenu, PanelExpertMenu, _Mesh, _Camera, _Player;

    void Start()
    {
        PanelFirstMenu.SetActive(true);
        PanelExpertMenu.SetActive(false);
    }

    public void BtnStartGame()
    {
        PanelFirstMenu.SetActive(false);
        PanelExpertMenu.SetActive(false);
        Manager.MoveObject.AddListener(_Player.GetComponent<Player>().MovePlayerFromManager);
        Manager.Instance.GenerateMesh();
        _Mesh.GetComponent<MeshCreate>().SetMesh();
        _Camera.GetComponent<CameraScript>().HideMeshBackGround();
        _Camera.GetComponent<CameraScript>().StartCameraAnimateBool();
    }

    public void BtnExpert()
    {
        PanelFirstMenu.SetActive(false);
        PanelExpertMenu.SetActive(true);
    }

    public void OnChangeSizeSide()
    {
        SizeSideText.text = "Размер игры:\n" + (ScrollbarSizeSide.value * 100).ToString("0");
        Manager.SizeSite = (int)(ScrollbarSizeSide.value * 100);
        _Camera.GetComponent<CameraScript>().MeshBackGroundRecreate();
    }
    public void OnChangeBreakingSize()
    {
        BreakingSizeText.text = "Размер разреза:" + (ScrollbarBreakingSize.value * 100).ToString("0");
        Manager.SizeBreaking = (int)(ScrollbarBreakingSize.value * 100);
        _Camera.GetComponent<CameraScript>().MeshBackGroundRecreate();
    }
    public void OnChangeLengthSegments()
    {
        LengthSegmentsText.text = "Длина отрезков:" + (ScrollbarLengthSegments.value * 100).ToString("0");
        Manager.QuadForY = (int)(ScrollbarLengthSegments.value * 100);
        _Camera.GetComponent<CameraScript>().MeshBackGroundRecreate();
    }
    public void OnChangeInclination()
    {
        TextInclinationText.text = "Наклон отрезков:" + (ScrollbarInclination.value * 100).ToString("0");
        Manager.QuadForY = (int)(ScrollbarLengthSegments.value * 100);
        _Camera.GetComponent<CameraScript>().MeshBackGroundRecreate();
    }
    public void OnChangeCountPoints()
    {
        if (ScrollbarCountPoints.value <= 0.2f) 
        {
            ScrollbarCountPoints.value = 0.2f;
        }
        TextCountPOints.text = "Кол-во вершин:" + (System.Math.Round(ScrollbarCountPoints.value,1)*10).ToString();
        Manager.CountPoints = (int)(System.Math.Round(ScrollbarCountPoints.value, 1) * 10);
        _Camera.GetComponent<CameraScript>().MeshBackGroundRecreate();
    }
}

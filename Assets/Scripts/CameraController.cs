using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Camera _squashFrontCam;
    [SerializeField] private Camera _squashSideCam;
    [SerializeField] private Camera _zombieCam;
    [SerializeField] private Camera _trajectoryCam;
    [SerializeField] private Camera _resultCam;

    private void Start()
    {
        SquashLocomotion.InPeak.AddListener(ChangeTrajectoryView);
        SquashLocomotion.DescentHalfTime.AddListener(ChangeResultView);
    }

    private void SetCameras()
    {
        _squashFrontCam.gameObject.SetActive(true);
        _squashSideCam.gameObject.SetActive(false);
        _zombieCam.gameObject.SetActive(true);
        _trajectoryCam.gameObject.SetActive(false);
        _resultCam.gameObject.SetActive(false);
    }

    private void ChangeTrajectoryView()
    {
        _squashFrontCam.gameObject.SetActive(false);
        _squashSideCam.gameObject.SetActive(true);
        _zombieCam.gameObject.SetActive(false);
        _trajectoryCam.gameObject.SetActive(true);
        _resultCam.gameObject.SetActive(false);
    }

    private void ChangeResultView()
    {
        _squashFrontCam.gameObject.SetActive(false);
        _squashSideCam.gameObject.SetActive(false);
        _zombieCam.gameObject.SetActive(false);
        _trajectoryCam.gameObject.SetActive(false);
        _resultCam.gameObject.SetActive(true);
    }

}

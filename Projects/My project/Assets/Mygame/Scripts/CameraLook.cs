using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraLook : MonoBehaviour
{
    private Playermain input;
    private CinemachineFreeLook LookCam;
    public float lookSpeedx;
    public float lookSpeedy;

    private void Awake()
    {
        input = new Playermain();
        LookCam = GetComponent<CinemachineFreeLook>();
    }

    private void OnEnable()
    {

        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }

    void Update()
    {

        Vector2 delta = input.PlayerMain.Look.ReadValue<Vector2>();
        LookCam.m_XAxis.Value += delta.x  * Time.deltaTime * lookSpeedx;
        LookCam.m_YAxis.Value += delta.y * Time.deltaTime * -lookSpeedy;
    }
}

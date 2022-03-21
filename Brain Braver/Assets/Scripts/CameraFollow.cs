using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Camera cam = null;
    [SerializeField] OptionsSO optionsSO;
    [SerializeField] Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] float camSizeMult = 5f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Player.Instance.transform.position + offset;
        //cam.orthographicSize = 1f;
    }

    private void Update()
    {
        SizeInput();
        if (Input.GetKeyDown(KeyCode.Tab))
            ChangeCameraOffset();
        ResizeCamera();
    }

    void SizeInput()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            optionsSO.cameraSize = 1;
        else if (Input.GetKeyDown(KeyCode.F2))
            optionsSO.cameraSize = 2;
        else if (Input.GetKeyDown(KeyCode.F3))
            optionsSO.cameraSize = 3;
        else if (Input.GetKeyDown(KeyCode.F4))
            optionsSO.cameraSize = 4;
    }

    void ChangeCameraOffset()
    {
        optionsSO.frontalCameraOffset = optionsSO.frontalCameraOffset == 0 ? 4 : 0;
    }

    void ResizeCamera()
    {
        cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, 
            optionsSO.cameraSize * camSizeMult, optionsSO.cameraResizeSpeed);
    }

    void FixedUpdate()
    {
        Vector3 target = Player.Instance.transform.position + offset + 
            Player.Instance.transform.up * optionsSO.frontalCameraOffset *optionsSO.cameraSize;
        transform.position = Vector3.MoveTowards
            (transform.position, target, 
            /*optionsSO.cameraSpeed * 3f * Time.deltaTime*/ SetCamSpeed(target));
    }
    float SetCamSpeed(Vector3 target)
    {
        return Vector3.Distance(transform.position, target) * (5f +  optionsSO.cameraSpeed) * 0.01f;
    }
}

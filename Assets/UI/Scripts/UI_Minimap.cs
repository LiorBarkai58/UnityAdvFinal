using UnityEngine;

public class UI_Minimap : MonoBehaviour
{
    public Camera miniMapCam;
    public Transform playerCamera;
    public Transform playerTransform;

    private void LateUpdate()
    {
        Vector3 newPosition = playerTransform.position;
        newPosition.y = transform.position.y;
        miniMapCam.transform.position = newPosition;

        miniMapCam.transform.rotation = Quaternion.Euler(90f, playerCamera.eulerAngles.y, 0);
    }
}

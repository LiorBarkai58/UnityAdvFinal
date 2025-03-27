using UnityEngine;

public class UI_Minimap : MonoBehaviour
{
    [SerializeField] private Camera miniMapCam;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform playerTransform;

    private void Start()
    {
        miniMapCam.transform.position = playerTransform.transform.position;
    }
    private void LateUpdate()
    {
        Vector3 newPosition = playerTransform.position;
        newPosition.y = transform.position.y;
        miniMapCam.transform.position = newPosition;

        miniMapCam.transform.rotation = Quaternion.Euler(90f, playerCamera.eulerAngles.y, 0);
        //Debug.Log($"current player position: {playerTransform.transform.position} \ncurrent camera position: {miniMapCam.transform.position}");
    }
}

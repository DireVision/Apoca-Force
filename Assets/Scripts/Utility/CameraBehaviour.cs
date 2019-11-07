using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public float minFov = 15f;
    public float maxFov = 90f;
    float sensitivity = 20f;

    public float camDistance;
    public float rotationSpeed;
    public float moveSpeed;

    public Transform cameraHolder;
    public Transform cameraRotation;

    void Update()
    {
        //Controlling the camera via increasing/decreasing the fov (which creates the zoom effect since stuff fills more of the screen)
        float fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }

    void LateUpdate()
    {
        #region Rotation
        /* Grab the pivot point (center of the screen) by using transform.forward * length.
         * Since transform.forward is relative to the local rotation of the camera, it is effectively straight down the center, and
         * since transform.forward is a unit vector, it is easily interpolateable with length */

        if (Input.GetAxis("Horizontal") > 0)
            //transform.RotateAround(Vector3.zero, Vector3.up, -rotationSpeed * Time.deltaTime);
        transform.RotateAround((transform.position + transform.forward * 2 * camDistance), Vector3.up, -rotationSpeed * Time.deltaTime);
        else if (Input.GetAxis("Horizontal") < 0)
            transform.RotateAround((transform.position + transform.forward * 2 * camDistance), Vector3.up, rotationSpeed * Time.deltaTime);
        //transform.RotateAround(Vector3.zero, Vector3.up, rotationSpeed * Time.deltaTime);

        /* To solve a Euler conversion issue (basically 300 degrees is technically the same as -60 degrees), we check for the angle 
         * between no rotation (Quaternion.Identity) and the current rotation to 
         * determine whether the angle is negative and then determine the angle to bound base on that. */

        if (Input.GetAxis("Vertical") > 0)
        {
            float currentXAngle = transform.rotation.eulerAngles.x;
            if (currentXAngle < 90)
            {
                //transform.RotateAround((transform.position +  transform.forward * camDistance), transform.right, rotationSpeed * Time.deltaTime);
                transform.RotateAround(Vector3.zero, transform.right, rotationSpeed * Time.deltaTime);
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            float currentXAngle = transform.rotation.eulerAngles.x;
            if (currentXAngle > 45)
            {
                //transform.RotateAround((transform.position + transform.forward * camDistance), transform.right, -rotationSpeed * Time.deltaTime);
                transform.RotateAround(Vector3.zero, transform.right, -rotationSpeed * Time.deltaTime);
            }
        }
        
        //Cleanup the rotation by rounding the Euler values after interpolation to avoid drifting
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0 )
        {
            transform.localEulerAngles = RotateAxisToNearestSide(transform.localEulerAngles);
        }

        Vector3 RotateAxisToNearestSide(Vector3 eulerAngles)
        {
            Vector3 roundedEulerAngles = RoundToNearest90Degree(eulerAngles);
            return Vector3.Slerp(eulerAngles, roundedEulerAngles, Time.deltaTime * 5);
        }

        Vector3 RoundToNearest90Degree(Vector3 eulerAngles)
        {
            for (int i = 0; i < 3; i++)
            {
                eulerAngles[i] = Mathf.Round(eulerAngles[i] / 45f) * 45f;
            }
            return eulerAngles;
        }
        #endregion

        /*
        //Moving 
        if (Input.GetAxis("Move Horizontal") > 0)
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }
        else if (Input.GetAxis("Move Horizontal") < 0)
        {
            transform.Translate(-Vector3.right * Time.deltaTime * moveSpeed);
        }   

        if (Input.GetAxis("Move Vertical") > 0)
        {
            cameraHolder.Translate(cameraRotation.forward * Time.deltaTime * moveSpeed);
        }
        else if (Input.GetAxis("Move Vertical") < 0)
        {
            cameraHolder.Translate(-cameraRotation.forward * Time.deltaTime * moveSpeed);
        }
        */
    }
}

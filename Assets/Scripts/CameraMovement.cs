using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject followTarget;

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    private Vector2 bgvelocity = Vector2.zero;
    private float cameraZ = -10;
    [SerializeField] private MeshRenderer background;
    private Camera cam;

    void Start()
    {
        cameraZ = transform.position.z;
        cam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        if (followTarget)
        {
            Vector3 delta = followTarget.transform.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cameraZ));
            Vector3 destination = transform.position + delta;
            destination.z = cameraZ;
            Vector3 pos = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            transform.position = pos;
            background.sharedMaterial.mainTextureOffset = -pos / Player.Instance.walkSpeed;
        }
    }
}

using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public Transform player0, player1;
    public float minSizeY = 5f;
    Camera cam;
    bool player0Ready = false;
    bool player1Ready = false;


    [SerializeField] float zoomFactor = 1.5f;
    [SerializeField] float followTimeDelta = 0.8f;
    [SerializeField] float distMin = 2;
    [SerializeField] float distMax = 6;
    [SerializeField] float magnitudeDelta = 0.1f;


    private void Start()
    {
        cam = this.GetComponent<Camera>();
        Player.ThePlayerSpawns += OnReceptionOfSignal;
    }



    // Follow Two Transforms with a Fixed-Orientation Camera
    public void FixedCameraFollowSmooth(Camera cam, Transform player0, Transform player1)
    {
        // How many units should we keep from the players
        

        // Midpoint we're after
        Vector3 midpoint = (player0.position + player1.position) / 2f;

        // Distance between objects
        float distance = (player0.position - player1.position).magnitude;
        distance = Mathf.Max(distMin, Mathf.Min(distance, distMax));

        // Move camera a certain distance
        Vector3 cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;

        // Adjust ortho size if we're using one of those
        if (cam.orthographic)
        {
            // The camera's forward vector is irrelevant, only this size will matter
            cam.orthographicSize = distance;
        }
        // You specified to use MoveTowards instead of Slerp
        cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);

        // Snap when close enough to prevent annoying slerp behavior
        if ((cameraDestination - cam.transform.position).magnitude <= magnitudeDelta)
            cam.transform.position = cameraDestination;
    }


    private void OnReceptionOfSignal(int indexOfPrefab)
    {
        
        if (indexOfPrefab == 0)
        {
            player0 = GameObject.Find("Player0(Clone)").GetComponent<Transform>();
            player0Ready = true;
        }
        if (indexOfPrefab == 1)
        {
            player1 = GameObject.Find("Player1(Clone)").GetComponent<Transform>();
            player1Ready = true;
        }
        
    }

    void Update()
    {
        if (player0Ready && player1Ready)
        {
            FixedCameraFollowSmooth(cam, player0, player1);
        }
    }
}
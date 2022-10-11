using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float Power = 2.5f;

    public TrajectoryRenderer Trajectory;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }
    
    void Update()
    {
        float enter;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        new Plane(-Vector3.forward, transform.position).Raycast(ray, out enter);
        Vector3 mouseInWorld = ray.GetPoint(enter);

        Vector3 speed = (mouseInWorld - transform.position) * Power;
        transform.rotation = Quaternion.LookRotation(speed);
        Trajectory.ShowTrajectory(transform.position, speed);

        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            bullet.AddForce(speed, ForceMode.VelocityChange);
        }
    }
}

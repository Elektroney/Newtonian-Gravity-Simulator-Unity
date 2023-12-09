using System;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    [SerializeField]
    internal float timeScale = 0;
    [SerializeField]
    internal float gravitationalConstant = 1;

    [SerializeField]
    internal List<PhysicsBody> physicsBodys = new List<PhysicsBody>();

    public static PhysicsManager instance;

    public event Action PhysicsEvent;

    [SerializeField]
    private Camera mainCamera;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;
    }

    public void AddPhysicsBody(PhysicsBody physicsBody)
    {
        physicsBodys.Add(physicsBody);
    }

    public void CheckForDeletedPhysicsBodies()
    {
        physicsBodys.RemoveAll(body => body == null);
    }

    public void InvokePhysicsEvent()
    {
        CheckForDeletedPhysicsBodies();
        PhysicsEvent?.Invoke();
    }

    private void Update()
    {
        Time.timeScale = timeScale / 100;
    }

    private void FixedUpdate()
    {
        InvokePhysicsEvent();

        // Move the camera to the center of all physics bodies' bounds
        if (mainCamera != null && physicsBodys.Count > 0)
        {
            Vector3 center = GetCenterOfAllBounds();
            mainCamera.transform.position = center;
        }
    }

    private Vector3 GetCenterOfAllBounds()
    {
        Bounds totalBounds = new Bounds(physicsBodys[0].transform.position, Vector3.zero);

        for (int i = 1; i < physicsBodys.Count; i++)
        {
            totalBounds.Encapsulate(physicsBodys[i].transform.position);
        }

        return totalBounds.center;
    }
}

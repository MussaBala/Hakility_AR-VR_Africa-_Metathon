using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardManager : MonoBehaviour
{
    public Transform cam;
    public float SyncSpeed;

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward * Time.deltaTime * SyncSpeed);
    }
}

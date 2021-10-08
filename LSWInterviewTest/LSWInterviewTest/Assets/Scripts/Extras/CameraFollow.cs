using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(new Vector3(target.position.x,target.position.y,-10), target.position, moveSpeed * Time.deltaTime);
    }
}

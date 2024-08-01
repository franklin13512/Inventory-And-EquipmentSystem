using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MoveSlightly : MonoBehaviour
{
    public float MoveSpeed;
    public float SinMove;
    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        SinMove = math.sin(Time.time);
        Vector2 CurrentPosition = transform.position;
        CurrentPosition.y = transform.position.y + SinMove * MoveSpeed * Time.deltaTime;
        transform.position = CurrentPosition;
    }
}

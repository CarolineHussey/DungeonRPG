using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float[] minionSpeed = { 3.0f, -3.0f};
    public Transform[] minions;
    public float distance = 0.35f;

    private void Update()
    {
        for (int i = 0; i < minions.Length; i++)
        {
            minions[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * minionSpeed[i]) * distance, Mathf.Sin(Time.time * minionSpeed[i]) * distance, 0);
        }
    }
}

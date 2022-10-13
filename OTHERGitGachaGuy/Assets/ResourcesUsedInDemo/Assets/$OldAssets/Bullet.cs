using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Credit Code Monkey on YouTube for some starting code.
public class Bullet : MonoBehaviour
{
    private Vector3 shootDir;

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(shootDir));
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    private void Update()
    {
        float moveSpeed = 100f;
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }

}

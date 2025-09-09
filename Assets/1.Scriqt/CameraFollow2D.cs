using UnityEngine;



    // Start is called once before the first execution of Update after the MonoBehaviour is created


public class CameraFollow2D : MonoBehaviour
{
    public Transform target;     // Player
    public float smooth = 10f;   // 클수록 빠르게 따라감

    void LateUpdate()
    {
        if (!target) return;
        Vector3 pos = transform.position;
        pos = Vector3.Lerp(pos, new Vector3(target.position.x, target.position.y, pos.z), Time.deltaTime * smooth);
        transform.position = pos;
    }
}



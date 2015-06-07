using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform stopPosition;

    [SceneName]
    public string nextLevel;

    private Camera m_camera;

    void Awake()
    {
        m_camera = GetComponent<Camera>();
        StopPositionSet();
    }

    void LateUpdate()
    {
        var right = m_camera.ViewportToWorldPoint(Vector2.right);
        var center = m_camera.ViewportToWorldPoint(Vector2.one * 0.5f);
        
        if (target != null) {
            if (center.x < target.position.x)
            {
                var pos = m_camera.transform.position;
    
                if (Math.Abs(pos.x - target.position.x) >= 0.0000001f)
                {
                    m_camera.transform.position = new Vector3(target.position.x, pos.y, pos.z);
                }
            }
    
            if (stopPosition.position.x - right.x < 0)
            {
                StartCoroutine(INTERNAL_Clear());
                enabled = false;
            }
        } else {
            if (GameObject.FindGameObjectWithTag("Player") != null) {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
    }

    private IEnumerator INTERNAL_Clear()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            player.SendMessage("Clear", SendMessageOptions.DontRequireReceiver);
        }

        yield return new WaitForSeconds(3);

        Application.LoadLevel(nextLevel);
    }
    
    public void StopPositionSet() {
        stopPosition = GameObject.Find("StopPosition").transform;
    }
}

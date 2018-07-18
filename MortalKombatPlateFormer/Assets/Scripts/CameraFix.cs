using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFix : MonoBehaviour
{
    [SerializeField]
    private CameraFollow m_CameraFollow;
    private void OnTriggerEnter2D(Collider2D aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            m_CameraFollow.SetCameraFix();
        }
    }
}

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SchoolMetaverse
{
    public class CameraController : MonoBehaviourPunCallbacks, ISetUp
    {
        public void SetUp()
        {
            //float yRot;
            //float xRot;
            //float currentYRot;
            //float currentXRot;
            //float yRotVelocity;
            //float xRotVelocity;
            //void Update()
            //{
            //    yRot += Input.GetAxis("Mouse X") * lookSensitivity;  // �}�E�X�̉��ړ� 
            //    xRot -= Input.GetAxis("Mouse Y") * lookSensitivity;  // �}�E�X�̏c�ړ� 
            //                                                         // �i�|�C���g�j�uClamp�v�̈Ӗ��Ǝg�������l�b�g�Œ��ׂ悤�I 
            //    xRot = Mathf.Clamp(xRot, MinMaxAngle.x, MinMaxAngle.y); // �㉺�̊p�x�ړ��̍ő�A�ŏ� 
            //                                                            // �i�|�C���g�j�uSmoothDamp�v�̈Ӗ��Ǝg�������l�b�g�Œ��ׂ悤�I 
            //    currentXRot = Mathf.SmoothDamp(currentXRot, xRot, ref xRotVelocity, lookSmooth);
            //    currentYRot = Mathf.SmoothDamp(currentYRot, yRot, ref yRotVelocity, lookSmooth);
            //    transform.rotation = Quaternion.Euler(currentXRot, currentYRot, 0);
            //}
        }
    }
}

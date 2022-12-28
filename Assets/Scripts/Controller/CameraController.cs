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
            //    yRot += Input.GetAxis("Mouse X") * lookSensitivity;  // マウスの横移動 
            //    xRot -= Input.GetAxis("Mouse Y") * lookSensitivity;  // マウスの縦移動 
            //                                                         // （ポイント）「Clamp」の意味と使い方をネットで調べよう！ 
            //    xRot = Mathf.Clamp(xRot, MinMaxAngle.x, MinMaxAngle.y); // 上下の角度移動の最大、最小 
            //                                                            // （ポイント）「SmoothDamp」の意味と使い方をネットで調べよう！ 
            //    currentXRot = Mathf.SmoothDamp(currentXRot, xRot, ref xRotVelocity, lookSmooth);
            //    currentYRot = Mathf.SmoothDamp(currentYRot, yRot, ref yRotVelocity, lookSmooth);
            //    transform.rotation = Quaternion.Euler(currentXRot, currentYRot, 0);
            //}
        }
    }
}

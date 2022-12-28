using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace SchoolMetaverse
{
    public class CameraController : MonoBehaviourPunCallbacks
    {
        /// <summary>
        /// カメラの初期設定を行う
        /// </summary>
        public void SetUp()
        {
            //このカメラの所有者が自分でなければ、以降の処理を行わない
            if (!photonView.IsMine) return;

            //マウスからの入力
            float yRot = 0f;
            float xRot = 0f;

            //現在の適切な角度
            float currentYRot = 0f;
            float currentXRot = 0f;

            //速度の保持用
            float yRotVelocity = 0f;
            float xRotVelocity = 0f;

            //カメラの制御
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    //マウスの移動を取得する
                    yRot += Input.GetAxis("Mouse X") * ConstData.LOOK_SENSITIVITY;
                    xRot -= Input.GetAxis("Mouse Y") * ConstData.LOOK_SENSITIVITY;

                    //現在の適切な角度を取得する
                    currentXRot = Mathf.SmoothDamp(currentXRot, xRot, ref xRotVelocity, ConstData.LOOK_SMOOTH);
                    currentYRot = Mathf.SmoothDamp(currentYRot, yRot, ref yRotVelocity, ConstData.LOOK_SMOOTH);

                    //カメラを回転させる
                    transform.rotation = Quaternion.Euler(currentXRot, currentYRot, 0);
                })
                .AddTo(this);
        }
    }
}

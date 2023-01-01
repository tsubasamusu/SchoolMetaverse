using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace SchoolMetaverse
{
    public class CameraController : MonoBehaviourPunCallbacks, ISetUp
    {
        /// <summary>
        /// カメラの初期設定を行う
        /// </summary>
        public void SetUp()
        {
            //このカメラの所有者が自分でなければ、以降の処理を行わない
            if (!photonView.IsMine) return;

            //目標角度
            float yRot = 0f;
            float xRot = 0f;

            //現在の適切な角度
            float currentYRot = 0f;
            float currentXRot = 0f;

            //速度（保持用）
            float yRotVelocity = 0f;
            float xRotVelocity = 0f;

            //現在の角度（保持用）
            Vector3 currentEulerAngles = Vector3.zero;

            //カメラの制御
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    //マウスの移動を取得する
                    xRot -= Input.GetAxis("Mouse Y") * ConstData.LOOK_SENSITIVITY;
                    yRot += Input.GetAxis("Mouse X") * ConstData.LOOK_SENSITIVITY;

                    //現在の適切な角度を取得する
                    currentXRot = Mathf.SmoothDamp(currentXRot, xRot, ref xRotVelocity, ConstData.LOOK_SMOOTH);
                    currentYRot = Mathf.SmoothDamp(currentYRot, yRot, ref yRotVelocity, ConstData.LOOK_SMOOTH);

                    //取得した角度xに制限を加える
                    currentXRot = Mathf.Clamp(currentXRot, -ConstData.MAX_CAMERA_ANGLE_X, ConstData.MAX_CAMERA_ANGLE_X);

                    //視点移動キーが押されているなら
                    if (Input.GetKey(ConstData.VIEWPOINT_MOVE_KEY))
                    {
                        //カメラを回転させる
                        transform.eulerAngles = new(currentXRot, currentYRot, 0);

                        //現在の角度の記録を初期化する
                        currentEulerAngles = Vector3.zero;

                        //以降の処理を行わない
                        return;
                    }

                    //現在の角度が記録されていないなら
                    if (currentEulerAngles == Vector3.zero)
                    {
                        //現在の角度を記録する
                        currentEulerAngles = transform.eulerAngles;
                    }

                    //現在の角度を維持する
                    transform.eulerAngles = currentEulerAngles;
                })
                .AddTo(this);
        }
    }
}

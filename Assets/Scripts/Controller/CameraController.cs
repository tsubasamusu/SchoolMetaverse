using Photon.Pun;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

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

            //マウスの座標
            Vector3 mousePos = Vector3.zero;

            //カメラの角度
            Vector3 cameraAngle = Vector3.zero;

            //速度（保持用）
            float yRotVelocity = 0f;
            float xRotVelocity = 0f;

            //カメラの制御
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    //マウス移動を取得する
                    mousePos.y += Input.GetAxis("Mouse X") * ConstData.LOOK_SENSITIVITY * Time.deltaTime;
                    mousePos.x -= Input.GetAxis("Mouse Y") * ConstData.LOOK_SENSITIVITY * Time.deltaTime;

                    //滑らかに値を更新する
                    cameraAngle.x = Mathf.SmoothDamp(cameraAngle.x, mousePos.x, ref xRotVelocity, ConstData.LOOK_SMOOTH);
                    cameraAngle.y = Mathf.SmoothDamp(cameraAngle.y, mousePos.y, ref yRotVelocity, ConstData.LOOK_SMOOTH);

                    //角度xに制限を加える
                    cameraAngle.x = Mathf.Clamp(cameraAngle.x, -ConstData.MAX_CAMERA_ANGLE_X, ConstData.MAX_CAMERA_ANGLE_X);

                    //カメラの角度を設定する
                    transform.eulerAngles = new(cameraAngle.x, cameraAngle.y, 0f);
                })
                .AddTo(this);
        }
    }
}


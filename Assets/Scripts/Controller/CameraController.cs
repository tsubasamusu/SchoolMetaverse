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

            //現在の角度
            Vector3 currentAngle = Vector3.zero;

            //マウスの最後の座標
            Vector3 lastMousePos = Vector3.zero;

            //カメラの角度
            Vector3 cameraAngle = Vector3.zero;

            //カメラの制御
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    //視点移動キーが押されたら、マウスカーソルを中央に移動させる
                    if(Input.GetKeyDown(ConstData.VIEWPOINT_MOVE_KEY))Cursor.lockState = CursorLockMode.Locked;

                    //視点移動キーが押されていないなら
                    if (!Input.GetKey(ConstData.VIEWPOINT_MOVE_KEY))
                    {
                        //現在の角度が記録されていないなら、記録する
                        if (currentAngle == Vector3.zero) { currentAngle = transform.eulerAngles; }

                        //カメラの角度を維持し続ける
                        transform.eulerAngles = currentAngle;

                        //マウスカーソルを表示する
                        Cursor.visible = true;

                        //以降の処理を行わない
                        return;
                    }

                    //マウスカーソルを非表示にする
                    Cursor.visible = false;

                    //カーソルの移動に制限が掛かっていないなら
                    if (Cursor.lockState == CursorLockMode.None)
                    {
                        //カメラの適切な角度を取得する
                        cameraAngle.y += ((Input.mousePosition.x - lastMousePos.x) * ConstData.LOOK_SENSITIVITY);
                        cameraAngle.x -= ((Input.mousePosition.y - lastMousePos.y) * ConstData.LOOK_SENSITIVITY);

                        //取得した角度xに制限を加える
                        cameraAngle.x = Mathf.Clamp(cameraAngle.x, -ConstData.MAX_CAMERA_ANGLE_X, ConstData.MAX_CAMERA_ANGLE_X);

                        //カメラの角度を設定する
                        transform.eulerAngles = cameraAngle;

                        //現在の角度の記録を初期化する
                        currentAngle = Vector3.zero;
                    }

                    //マウスカーソルのロックモードを設定する
                    Cursor.lockState =
                        Mathf.Abs(Screen.width / 2 - Input.mousePosition.x) > ConstData.MAX_CUSOR_LENGTH_FROM_CENTER
                        || Mathf.Abs(Screen.height / 2 - Input.mousePosition.y) > ConstData.MAX_CUSOR_LENGTH_FROM_CENTER ?
                        CursorLockMode.Locked : Cursor.lockState = CursorLockMode.None;

                    //マウスの最後の座標を更新する
                    lastMousePos = Input.mousePosition;
                })
                .AddTo(this);
        }
    }
}

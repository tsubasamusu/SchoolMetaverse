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

            //カメラを操作するメソッドを呼び出す
            Invoke(Application.platform == RuntimePlatform.WebGLPlayer ? nameof(ControlCameraForWebGL) : nameof(ControlCameraForWindows), 0f);
        }

        /// <summary>
        /// カメラを操作する（Windows用）
        /// </summary>
        private void ControlCameraForWindows()
        {
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
                    if (Input.GetKeyDown(ConstData.VIEWPOINT_MOVE_KEY)) Cursor.lockState = CursorLockMode.Locked;

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
                        cameraAngle.y += (Input.mousePosition.x - lastMousePos.x) * ConstData.LOOK_SENSITIVITY_FOR_WINDOWS * Time.deltaTime;
                        cameraAngle.x -= (Input.mousePosition.y - lastMousePos.y) * ConstData.LOOK_SENSITIVITY_FOR_WINDOWS * Time.deltaTime;

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

        /// <summary>
        /// カメラを操作する（WebGL用）
        /// </summary>
        private void ControlCameraForWebGL()
        {
            //現在の角度
            Vector3 currentAngle = Vector3.zero;

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
                    //視点移動キーが押されていないなら
                    if (!Input.GetKey(ConstData.VIEWPOINT_MOVE_KEY))
                    {
                        //現在の角度が記録されていないなら、記録する
                        if (currentAngle == Vector3.zero) { currentAngle = transform.eulerAngles; }

                        //カメラの角度を維持し続ける
                        transform.eulerAngles = currentAngle;

                        //以降の処理を行わない
                        return;
                    }

                    //マウス移動を取得する
                    mousePos.y += Input.GetAxis("Mouse X") * ConstData.LOOK_SENSITIVITY_FOR_WEBGL * Time.deltaTime;
                    mousePos.x -= Input.GetAxis("Mouse Y") * ConstData.LOOK_SENSITIVITY_FOR_WEBGL * Time.deltaTime;

                    //滑らかに値を更新する
                    cameraAngle.x = Mathf.SmoothDamp(cameraAngle.x, mousePos.x, ref xRotVelocity, ConstData.LOOK_SMOOTH_FOR_WEBGL);
                    cameraAngle.y = Mathf.SmoothDamp(cameraAngle.y, mousePos.y, ref yRotVelocity, ConstData.LOOK_SMOOTH_FOR_WEBGL);

                    //角度xに制限を加える
                    cameraAngle.x = Mathf.Clamp(cameraAngle.x, -ConstData.MAX_CAMERA_ANGLE_X, ConstData.MAX_CAMERA_ANGLE_X);

                    //カメラの角度を設定する
                    transform.eulerAngles = new(cameraAngle.x, cameraAngle.y, 0f);

                    //現在の角度の記録を初期化する
                    currentAngle = Vector3.zero;
                })
                .AddTo(this);
        }
    }
}


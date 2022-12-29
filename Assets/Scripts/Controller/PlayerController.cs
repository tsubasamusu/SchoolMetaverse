using Photon.Pun;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace SchoolMetaverse
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviourPunCallbacks
    {
        /// <summary>
        /// PlayerControllerの初期設定を行う
        /// </summary>
        /// <param name="cameraTran">カメラの位置情報</param>
        public void SetUp(Transform cameraTran)
        {
            //所有者が自分ではないなら、以降の処理を行わない
            if (!photonView.IsMine) return;

            //CharacterControllerを取得する
            CharacterController characterController = GetComponent<CharacterController>();

            //移動・アニメーション
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    //キャラクターの向きをカメラに合わせる
                    transform.eulerAngles = new(0f,cameraTran.eulerAngles.y,0f);

                    //移動方向を取得する
                    Vector3 movement = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                    //移動方向を修正する
                    movement = Vector3.Scale(cameraTran.forward * movement.z + cameraTran.transform.right * movement.x,
                        new Vector3(1f, 0f, 1f));

                    //移動する
                    characterController.Move(movement * Time.fixedDeltaTime * ConstData.MOVE_SPEED);
                })
                .AddTo(this);
        }
    }
}

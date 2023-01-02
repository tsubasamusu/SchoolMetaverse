using Cysharp.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Threading;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace SchoolMetaverse
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public class PlayerController : MonoBehaviourPunCallbacks
    {
        /// <summary>
        /// プレイヤーの初期設定を行う
        /// </summary>
        public void SetUp()
        {
            //所有者が自分ではないなら、以降の処理を行わない
            if (!photonView.IsMine) return;

            //カスタムプロパティを作成する
            var hashtable = new ExitGames.Client.Photon.Hashtable
            {
                //プレイヤーの名前を持たせる
                ["PlayerName"] = GameData.instance.playerName
            };

            //作成したカスタムプロパティを登録する
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);

            //プレイヤーの名前を表示する準備を行う
            PrepareDisplayPlayerNameAsync(this.GetCancellationTokenOnDestroy()).Forget();

            //自分の体を非表示にする
            transform.GetChild(0).gameObject.SetActive(false);

            //CharacterControllerを取得する
            CharacterController characterController = GetComponent<CharacterController>();

            //Animatorを取得する
            Animator animator = GetComponent<Animator>();

            //移動・アニメーション
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    //キャラクターの向きをカメラに合わせる
                    transform.eulerAngles = new(0f, Camera.main.transform.eulerAngles.y, 0f);

                    //移動方向を取得する
                    Vector3 movement = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                    //移動方向を修正する
                    movement = Vector3.Scale(Camera.main.transform.forward * movement.z + Camera.main.transform.right * movement.x,
                        new Vector3(1f, 0f, 1f));

                    //移動する
                    characterController.Move(ConstData.MOVE_SPEED * Time.deltaTime * movement);

                    //アニメーションの名前を取得する
                    AnimationName animationName = GetPressedKey() switch
                    {
                        ConstData.WALK_F_KEY => AnimationName.isWalking_F,
                        ConstData.WALK_R_KEY => AnimationName.isWalking_R,
                        ConstData.WALK_B_KEY => AnimationName.isWalking_B,
                        ConstData.WALK_L_KEY => AnimationName.isWalking_L,
                        _ => AnimationName.Null,
                    };

                    //アニメーションの名前の数だけ繰り返す
                    foreach (AnimationName animName in Enum.GetValues(typeof(AnimationName)))
                    {
                        //繰り返し処理で得たアニメーションの名前が「Null」なら、次の繰り返し処理へ飛ばす
                        if (animName == AnimationName.Null) break;

                        //アニメーションを設定する
                        animator.SetBool(animName.ToString(), animationName == animName);
                    }
                })
                .AddTo(this);

            //押されたキーを取得する
            KeyCode GetPressedKey()
            {
                //キーの数だけ繰り返す
                foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
                {
                    //繰り返し処理で取得したキーが押されているなら、そのキーを返す
                    if (Input.GetKey(code)) return code;
                }

                //何も押されていないなら、Noneを返す
                return KeyCode.None;
            }
        }

        /// <summary>
        /// プレーヤーがルームに参加した際に呼び出される
        /// </summary>
        /// <param name="newPlayer">参加したプレイヤー</param>
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            //プレイヤーの名前を表示する準備を行う
            PrepareDisplayPlayerNameAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }

        /// <summary>
        /// プレイヤーの名前を表示する準備を行う
        /// </summary>
        /// <param name="token">CancellationToken</param>
        /// <returns>待ち時間</returns>
        private async UniTaskVoid PrepareDisplayPlayerNameAsync(CancellationToken token)
        {
            //カスタムプロパティが設定されるまで待つ
            await UniTask.WaitUntil(() =>
            PhotonNetwork.LocalPlayer.CustomProperties["PlayerName"] is string, cancellationToken: token);

            //プレイヤーの名前を表示する
            photonView.RPC(nameof(DisplayPlayerName), RpcTarget.All, PhotonNetwork.LocalPlayer.CustomProperties["PlayerName"]);
        }

        /// <summary>
        /// プレイヤーの名前を表示する
        /// </summary>
        /// <param name="playerName">プレイヤーの名前</param>
        [PunRPC]
        private void DisplayPlayerName(string playerName) { transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Text>().text = playerName; }
    }
}
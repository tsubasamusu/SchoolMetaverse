using Photon.Pun;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace SchoolMetaverse
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviourPunCallbacks
    {
        //Animatorの各ステートへの参照
        static int idleState = Animator.StringToHash("Base Layer.Idle");
        static int locoState = Animator.StringToHash("Base Layer.Locomotion");
        static int restState = Animator.StringToHash("Base Layer.Rest");

        /// <summary>
        /// PlayerControllerの初期設定を行う
        /// </summary>
        [System.Obsolete]
        public void SetUp()
        {
            CapsuleCollider cap;//カプセルコライダー

            Rigidbody rb;//Rigidbody

            Vector3 velocity;//カプセルコライダーの移動量

            float capFirstHeight;//カプセルコライダーの高さの初期値

            Vector3 capCenter;//カプセルコライダーの中央の初期値

            Animator animator;//Animator	

            AnimatorStateInfo currentBaseState;//Animatorの現在の状態

            //リセットする
            Reset();

            //移動・アニメーション
            this.FixedUpdateAsObservable()
                .Subscribe(_ =>
                {
                    //プレイヤーからの入力を取得する
                    float h = Input.GetAxis("Horizontal");
                    float v = Input.GetAxis("Vertical");

                    //無操作状態での移動を防止する
                    rb.isKinematic = h == 0f && v == 0f;

                    //アニメーションの各値を設定する
                    animator.SetFloat("Speed", v);
                    animator.SetFloat("Direction", h);
                    animator.speed = ConstData.ANIM_SPEED;
                    currentBaseState = animator.GetCurrentAnimatorStateInfo(0);

                    //移動速度を設定する
                    velocity = new Vector3(h, 0, v);
                    velocity = transform.TransformDirection(velocity);

                    //移動速度zの絶対値が0.1より大きいなら、移動速度を掛ける
                    //if (Mathf.Abs(v) > 0.1f) velocity *= ConstData.MOVE_SPEED;

                    //キャラクターを移動させる
                    transform.localPosition += velocity*ConstData.MOVE_SPEED * Time.fixedDeltaTime;

                    //現在のベースレイヤーがlocoStateなら
                    if (currentBaseState.nameHash == locoState)
                    {
                        //カーブでコライダ調整をしている時は、念のためにリセットする
                        if (ConstData.USE_CURVES) resetCollider();

                    }
                    //現在のベースレイヤーがidleStateなら
                    else if (currentBaseState.nameHash == idleState)
                    {
                        //カーブでコライダ調整をしている時は、念のためにリセットする
                        if (ConstData.USE_CURVES) resetCollider();
                    }
                    //現在のベースレイヤーがrestStateなら
                    else if (currentBaseState.nameHash == restState)
                    {
                        //ステートが遷移中でないなら、Rest bool値をリセットする（ループしないようにする）
                        if (!animator.IsInTransition(0)) animator.SetBool("Rest", false);
                    }
                })
                .AddTo(this);

            //リセットする
            void Reset()
            {
                //Animatorを取得する
                animator = GetComponent<Animator>();

                //CapsuleColliderを取得する
                cap = GetComponent<CapsuleCollider>();

                //Rigidbodyを取得する
                rb = GetComponent<Rigidbody>();

                //CapsuleColliderの初期値を取得する
                capFirstHeight = cap.height;
                capCenter = cap.center;
            }

            //カプセルコライダーを初期値にリセットする
            void resetCollider()
            {
                //カプセルコライダーの各値を初期値に戻す
                cap.height = capFirstHeight;
                cap.center = capCenter;
            }
        }
    }
}
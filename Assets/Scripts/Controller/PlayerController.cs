﻿using Photon.Pun;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

namespace SchoolMetaverse
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
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

            //Animatorを取得する
            Animator animator = GetComponent<Animator>();

            //移動・アニメーション
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    //キャラクターの向きをカメラに合わせる
                    transform.eulerAngles = new(0f, cameraTran.eulerAngles.y, 0f);

                    //移動方向を取得する
                    Vector3 movement = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                    //移動方向を修正する
                    movement = Vector3.Scale(cameraTran.forward * movement.z + cameraTran.transform.right * movement.x,
                        new Vector3(1f, 0f, 1f));

                    //移動する
                    characterController.Move(movement * Time.fixedDeltaTime * ConstData.MOVE_SPEED);

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
    }
}


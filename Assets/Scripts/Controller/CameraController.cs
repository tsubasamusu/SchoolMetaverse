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
        /// �J�����̏����ݒ���s��
        /// </summary>
        public void SetUp()
        {
            //���̃J�����̏��L�҂������łȂ���΁A�ȍ~�̏������s��Ȃ�
            if (!photonView.IsMine) return;

            //�}�E�X����̓���
            float yRot = 0f;
            float xRot = 0f;

            //���݂̓K�؂Ȋp�x
            float currentYRot = 0f;
            float currentXRot = 0f;

            //���x�̕ێ��p
            float yRotVelocity = 0f;
            float xRotVelocity = 0f;

            //�J�����̐���
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    //�}�E�X�̈ړ����擾����
                    yRot += Input.GetAxis("Mouse X") * ConstData.LOOK_SENSITIVITY;
                    xRot -= Input.GetAxis("Mouse Y") * ConstData.LOOK_SENSITIVITY;

                    //���݂̓K�؂Ȋp�x���擾����
                    currentXRot = Mathf.SmoothDamp(currentXRot, xRot, ref xRotVelocity, ConstData.LOOK_SMOOTH);
                    currentYRot = Mathf.SmoothDamp(currentYRot, yRot, ref yRotVelocity, ConstData.LOOK_SMOOTH);

                    //�J��������]������
                    transform.rotation = Quaternion.Euler(currentXRot, currentYRot, 0);
                })
                .AddTo(this);
        }
    }
}

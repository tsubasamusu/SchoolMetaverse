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
        /// �J�����̏����ݒ���s��
        /// </summary>
        public void SetUp()
        {
            //���̃J�����̏��L�҂������łȂ���΁A�ȍ~�̏������s��Ȃ�
            if (!photonView.IsMine) return;

            //�ڕW�p�x
            float yRot = 0f;
            float xRot = 0f;

            //���݂̓K�؂Ȋp�x
            float currentYRot = 0f;
            float currentXRot = 0f;

            //���x�i�ێ��p�j
            float yRotVelocity = 0f;
            float xRotVelocity = 0f;

            //���݂̊p�x�i�ێ��p�j
            Vector3 currentEulerAngles = Vector3.zero;

            //�J�����̐���
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    //�}�E�X�̈ړ����擾����
                    xRot -= Input.GetAxis("Mouse Y") * ConstData.LOOK_SENSITIVITY;
                    yRot += Input.GetAxis("Mouse X") * ConstData.LOOK_SENSITIVITY;

                    //���݂̓K�؂Ȋp�x���擾����
                    currentXRot = Mathf.SmoothDamp(currentXRot, xRot, ref xRotVelocity, ConstData.LOOK_SMOOTH);
                    currentYRot = Mathf.SmoothDamp(currentYRot, yRot, ref yRotVelocity, ConstData.LOOK_SMOOTH);

                    //�擾�����p�xx�ɐ�����������
                    currentXRot = Mathf.Clamp(currentXRot, -ConstData.MAX_CAMERA_ANGLE_X, ConstData.MAX_CAMERA_ANGLE_X);

                    //���_�ړ��L�[��������Ă���Ȃ�
                    if (Input.GetKey(ConstData.VIEWPOINT_MOVE_KEY))
                    {
                        //�J��������]������
                        transform.eulerAngles = new(currentXRot, currentYRot, 0);

                        //���݂̊p�x�̋L�^������������
                        currentEulerAngles = Vector3.zero;

                        //�ȍ~�̏������s��Ȃ�
                        return;
                    }

                    //���݂̊p�x���L�^����Ă��Ȃ��Ȃ�
                    if (currentEulerAngles == Vector3.zero)
                    {
                        //���݂̊p�x���L�^����
                        currentEulerAngles = transform.eulerAngles;
                    }

                    //���݂̊p�x���ێ�����
                    transform.eulerAngles = currentEulerAngles;
                })
                .AddTo(this);
        }
    }
}

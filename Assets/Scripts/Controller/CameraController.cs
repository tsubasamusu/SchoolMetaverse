using Photon.Pun;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

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

            //�}�E�X�̍��W
            Vector3 mousePos = Vector3.zero;

            //�J�����̊p�x
            Vector3 cameraAngle = Vector3.zero;

            //���x�i�ێ��p�j
            float yRotVelocity = 0f;
            float xRotVelocity = 0f;

            //�J�����̐���
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    //�}�E�X�ړ����擾����
                    mousePos.y += Input.GetAxis("Mouse X") * ConstData.LOOK_SENSITIVITY * Time.deltaTime;
                    mousePos.x -= Input.GetAxis("Mouse Y") * ConstData.LOOK_SENSITIVITY * Time.deltaTime;

                    //���炩�ɒl���X�V����
                    cameraAngle.x = Mathf.SmoothDamp(cameraAngle.x, mousePos.x, ref xRotVelocity, ConstData.LOOK_SMOOTH);
                    cameraAngle.y = Mathf.SmoothDamp(cameraAngle.y, mousePos.y, ref yRotVelocity, ConstData.LOOK_SMOOTH);

                    //�p�xx�ɐ�����������
                    cameraAngle.x = Mathf.Clamp(cameraAngle.x, -ConstData.MAX_CAMERA_ANGLE_X, ConstData.MAX_CAMERA_ANGLE_X);

                    //�J�����̊p�x��ݒ肷��
                    transform.eulerAngles = new(cameraAngle.x, cameraAngle.y, 0f);
                })
                .AddTo(this);
        }
    }
}


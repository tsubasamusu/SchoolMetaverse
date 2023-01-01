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

            //���݂̊p�x
            Vector3 currentAngle = Vector3.zero;

            //�}�E�X�̍Ō�̍��W
            Vector3 lastMousePos = Vector3.zero;

            //�J�����̊p�x
            Vector3 cameraAngle = Vector3.zero;

            //�J�����̐���
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    //���_�ړ��L�[�������ꂽ��A�}�E�X�J�[�\���𒆉��Ɉړ�������
                    if(Input.GetKeyDown(ConstData.VIEWPOINT_MOVE_KEY))Cursor.lockState = CursorLockMode.Locked;

                    //���_�ړ��L�[��������Ă��Ȃ��Ȃ�
                    if (!Input.GetKey(ConstData.VIEWPOINT_MOVE_KEY))
                    {
                        //���݂̊p�x���L�^����Ă��Ȃ��Ȃ�A�L�^����
                        if (currentAngle == Vector3.zero) { currentAngle = transform.eulerAngles; }

                        //�J�����̊p�x���ێ���������
                        transform.eulerAngles = currentAngle;

                        //�}�E�X�J�[�\����\������
                        Cursor.visible = true;

                        //�ȍ~�̏������s��Ȃ�
                        return;
                    }

                    //�}�E�X�J�[�\�����\���ɂ���
                    Cursor.visible = false;

                    //�J�[�\���̈ړ��ɐ������|�����Ă��Ȃ��Ȃ�
                    if (Cursor.lockState == CursorLockMode.None)
                    {
                        //�J�����̓K�؂Ȋp�x���擾����
                        cameraAngle.y += ((Input.mousePosition.x - lastMousePos.x) * ConstData.LOOK_SENSITIVITY);
                        cameraAngle.x -= ((Input.mousePosition.y - lastMousePos.y) * ConstData.LOOK_SENSITIVITY);

                        //�擾�����p�xx�ɐ�����������
                        cameraAngle.x = Mathf.Clamp(cameraAngle.x, -ConstData.MAX_CAMERA_ANGLE_X, ConstData.MAX_CAMERA_ANGLE_X);

                        //�J�����̊p�x��ݒ肷��
                        transform.eulerAngles = cameraAngle;

                        //���݂̊p�x�̋L�^������������
                        currentAngle = Vector3.zero;
                    }

                    //�}�E�X�J�[�\���̃��b�N���[�h��ݒ肷��
                    Cursor.lockState =
                        Mathf.Abs(Screen.width / 2 - Input.mousePosition.x) > ConstData.MAX_CUSOR_LENGTH_FROM_CENTER
                        || Mathf.Abs(Screen.height / 2 - Input.mousePosition.y) > ConstData.MAX_CUSOR_LENGTH_FROM_CENTER ?
                        CursorLockMode.Locked : Cursor.lockState = CursorLockMode.None;

                    //�}�E�X�̍Ō�̍��W���X�V����
                    lastMousePos = Input.mousePosition;
                })
                .AddTo(this);
        }
    }
}

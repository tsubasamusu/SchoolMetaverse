using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace SchoolMetaverse
{
    /// <summary>
    /// ���[���Q���O��UI�𐧌䂷��
    /// </summary>
    public class UIManagerBeforeJoinRoom : MonoBehaviour
    {
        [SerializeField]
        private Image imgBackground;//�w�i�̃C���[�W

        [SerializeField]
        private Image imgBtnMain;//���C���{�^���̃C���[�W

        [SerializeField]
        private Image imgBtnSub;//�T�u�{�^���̃C���[�W

        [SerializeField]
        private Text txtPlaceholder;//�X�y�[�X�z���_�̃e�L�X�g

        [SerializeField]
        private Text txtPlayerEntered;//�v���C���[�����͂����e�L�X�g

        [SerializeField]
        private Text txtBtnMain;//���C���{�^���̃e�L�X�g

        [SerializeField]
        private Text txtBtnSub;//�T�u�{�^���̃e�L�X�g

        [SerializeField]
        private Button btnMain;//���C���{�^��

        [SerializeField]
        private Button btnSub;//�T�u�{�^��

        [SerializeField]
        private InputField inputField;//InputField

        /// <summary>
        /// �Q�[���J�n����ɌĂяo�����
        /// </summary>
        private void Start()
        {
            //�w�i�����F�ɐݒ肷��
            imgBackground.color = Color.black;

            //�e�e�L�X�g��ݒ肷��
            txtPlaceholder.text = "�p�X�R�[�h�����...";
            txtBtnMain.text = "����";
            txtBtnSub.text = "�X�L�b�v";

            //SingleAssignmentDisposable���쐬����
            var disposable = new SingleAssignmentDisposable();

            //���C���{�^���������ꂽ�ۂ̏���
            disposable.Disposable = btnMain.OnClickAsObservable()
                .Where(_ => txtPlayerEntered.text == ConstData.PASSCODE)
                .Subscribe(_ =>
                {
                    //���O����͂����ʂɈڂ�
                    GoToEnterNameScene();

                    //�w�ǂ��~����
                    disposable.Dispose();
                });

            //�T�u�{�^���������ꂽ�ۂ̏���
            btnSub.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    //���O����͂����ʂɈڂ�
                    GoToEnterNameScene();
                })
                .AddTo(btnSub);

            //���O����͂����ʂɈڂ�
            void GoToEnterNameScene()
            {
                //�T�u�{�^��������
                Destroy(btnSub.gameObject);

                //�v���C���[�����͂����e�L�X�g����ɂ���
                inputField.text=string.Empty;

                //�e�L�X�g��ύX����
                txtPlaceholder.text = "���O�����...";
            }
        }
    }
}

using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using System;

namespace SchoolMetaverse
{
    /// <summary>
    /// ���[���Q���O��UI�𐧌䂷��
    /// </summary>
    public class UIManagerLobby : MonoBehaviour
    {
        [SerializeField]
        private Text txtPlaceholder;//�X�y�[�X�z���_�̃e�L�X�g

        [SerializeField]
        private Button btnMain;//���C���{�^��

        [SerializeField]
        private Button btnSub;//�T�u�{�^��

        [SerializeField]
        private InputField inputField;//InputField

        [SerializeField]
        private PhotonController photonController;//PhotonController

        /// <summary>
        /// �Q�[���J�n����ɌĂяo�����
        /// </summary>
        private void Start()
        {
            //���A���O����͂����ʂ��ǂ���
            bool isEnterNameScene = false;

            //�X�y�[�X�z���_�̃e�L�X�g��ݒ肷��
            txtPlaceholder.text = "�p�X�R�[�h�����...";

            btnMain.OnClickAsObservable()
            .Subscribe(_ =>
            {
                //���O����͂����ʂȂ�A�ȍ~�̏������s��Ȃ�
                if (isEnterNameScene) return;

                //���͂��ꂽ�p�X�R�[�h���������Ȃ��Ȃ�
                if (inputField.text != ConstData.PASSCODE)
                {
                    //���ʉ����Đ�����
                    SoundManager.instance.PlaySound(SoundDataSO.SoundName.�����ȃ{�^�������������̉�);

                    //�ȍ~�̏������s��Ȃ�
                    return;
                }

                //���ʉ����Đ�����
                SoundManager.instance.PlaySound(SoundDataSO.SoundName.�{�^�������������̉�);

                //���O����͂����ʂɈڂ�
                GoToEnterNameScene();
            });

            //�T�u�{�^���������ꂽ�ۂ̏���
            btnSub.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    //���ʉ����Đ�����
                    SoundManager.instance.PlaySound(SoundDataSO.SoundName.�{�^�������������̉�);

                    //���O����͂����ʂɑJ�ڂ���
                    GoToEnterNameScene();
                })
                .AddTo(btnSub);

            //���O����͂����ʂɈڂ�
            void GoToEnterNameScene()
            {
                //���O����͂����ʂɑJ�ڂ�����Ԃɐ؂�ւ���
                isEnterNameScene = true;

                //�T�u�{�^��������
                Destroy(btnSub.gameObject);

                //�v���C���[�����͂����e�L�X�g������������
                inputField.text = GameData.instance.playerName;

                //�e�L�X�g��ύX����
                txtPlaceholder.text = "���O�����...";

                //���C���{�^���������ꂽ�ۂ̏���
                btnMain.OnClickAsObservable()
                    .Subscribe(_ =>
                    {
                        //���O�����͂���Ă��Ȃ��Ȃ�
                        if (inputField.text == string.Empty)
                        {
                            //���ʉ����Đ�����
                            SoundManager.instance.PlaySound(SoundDataSO.SoundName.�����ȃ{�^�������������̉�);

                            //�ȍ~�̏������s��Ȃ�
                            return;
                        }

                        //���ʉ����Đ�����
                        SoundManager.instance.PlaySound(SoundDataSO.SoundName.�{�^�������������̉�);

                        //�v���C���[�̖��O���擾����
                        GameData.instance.playerName = inputField.text;

                        //�v���C���[�̖��O���f�o�C�X�ɕۑ�����
                        GameData.instance.SavePlayerNameInDevice();

                        //InputField������
                        Destroy(inputField.gameObject);

                        //���C���{�^��������
                        Destroy(btnMain.gameObject);

                        //���C���V�[���Ɉڂ�
                        StartCoroutine(GoToMain());
                    })
                    .AddTo(btnMain);
            }

            //���C���V�[���Ɉڂ�
            IEnumerator GoToMain()
            {
                //�}�X�^�[�T�[�o�[�Ɍq��
                photonController.ConnectMasterServer();

                //���[���ɎQ������܂ő҂�
                yield return new WaitUntil(() => photonController.JoinedRoom);

                //���C���V�[����ǂݍ���
                SceneManager.LoadScene("Main");
            }
        }
    }
}

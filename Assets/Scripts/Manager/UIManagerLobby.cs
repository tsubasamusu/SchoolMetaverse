using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        private Image imgLoad;//���[�h���̃C���[�W

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

            //���[�h���̃C���[�W���\���ɂ���
            imgLoad.DOFade(0f, 0f);

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

                //���[�h���̃A�j���[�V�������s��
                PlayLoadAnimation(this.GetCancellationTokenOnDestroy()).Forget();

                //���[���ɎQ������܂ő҂�
                yield return new WaitUntil(() => photonController.JoinedRoom);

                //���C���V�[����ǂݍ���
                SceneManager.LoadScene("Main");
            }

            //���[�h���̃A�j���[�V�������s��
            async UniTaskVoid PlayLoadAnimation(CancellationToken token)
            {
                //���[�h���̃C���[�W��\������
                imgLoad.DOFade(1f, 0f);

                //�����ɌJ��Ԃ�
                while(true)
                {
                    //��]����
                    imgLoad.transform.Rotate(0f,0f,-360f/8f);

                    //��莞�ԑ҂�
                    await UniTask.Delay(TimeSpan.FromSeconds(ConstData.IMG_LOAD_ROT_SPAN), cancellationToken: token);
                }
            }
        }
    }
}

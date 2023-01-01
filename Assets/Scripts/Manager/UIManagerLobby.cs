using System.Collections;
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
        private Text txtPlayerEntered;//�v���C���[�����͂����e�L�X�g

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
            //�X�y�[�X�z���_�̃e�L�X�g��ݒ肷��
            txtPlaceholder.text = "�p�X�R�[�h�����...";

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
                .Subscribe(_ =>GoToEnterNameScene())
                .AddTo(btnSub);

            //���O����͂����ʂɈڂ�
            void GoToEnterNameScene()
            {
                //�T�u�{�^��������
                Destroy(btnSub.gameObject);

                //�v���C���[�����͂����e�L�X�g������������
                inputField.text=GameData.instance.playerName;

                //�e�L�X�g��ύX����
                txtPlaceholder.text = "���O�����...";

                //���C���{�^���������ꂽ�ۂ̏���
                btnMain.OnClickAsObservable()
                    .Where(_=>txtPlayerEntered.text!=string.Empty)
                    .Subscribe(_ =>
                    {
                        //�v���C���[�̖��O���擾����
                        GameData.instance.playerName = txtPlayerEntered.text;

                        //�v���C���[�̖��O���f�o�C�X�ɕۑ�����
                        GameData.instance.SetDevicePlayerName();

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

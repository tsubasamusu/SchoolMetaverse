using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace SchoolMetaverse
{
    public class PhotonController : MonoBehaviourPunCallbacks
    {
        private bool isConnecting;//�}�X�^�[�T�[�o�[�ɐڑ����Ă��邩�ǂ���

        private bool joinedRoom;//���[���ɎQ���������ǂ���

        /// <summary>
        /// �u���[���ɎQ���������ǂ����v�̎擾�p
        /// </summary>
        public bool JoinedRoom { get => joinedRoom; }

        /// <summary>
        /// Start���\�b�h���O�ɌĂяo�����
        /// </summary>
        private void Awake()
        {
            //�}�X�^�[�N���C�A���g���V�[�������[�h������A����ȊO�̃N���C�A���g�������V�[�������[�h����
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        /// <summary>
        /// �Q�[���J�n���Ȍ�ɌĂяo�����
        /// </summary>
        private void Start()
        {
            //�}�X�^�[�T�[�o�[�ɐڑ������Ȃ�
            if (PhotonNetwork.IsConnected)
            {
                //�����_���ȃ��[���ɓ���
                PhotonNetwork.JoinRandomRoom();
            }
            //�}�X�^�[�T�[�o�[�ɐڑ��ł��Ȃ�������
            else
            {
                //�}�X�^�[�T�[�o�[�ւ̐ڑ����A���̌��ʂ��擾����
                isConnecting = PhotonNetwork.ConnectUsingSettings();
            }
        }

        /// <summary>
        /// �}�X�^�[�T�[�o�[�ւ̐ڑ������������ۂɌĂяo�����
        /// </summary>
        public override void OnConnectedToMaster()
        {
            //�}�X�^�[�T�[�o�[�ɐڑ����Ă���Ȃ�
            if (isConnecting)
            {
                //���[���̐ݒ���쐬����
                RoomOptions roomOptions = new();

                //���[���̍ő�l����ݒ肷��
                roomOptions.MaxPlayers = ConstData.MAX_PLAYERS;

                //�uroom�v�ɎQ�����邩�쐬����
                PhotonNetwork.JoinOrCreateRoom("room", roomOptions, TypedLobby.Default);

                //�}�X�^�[�T�[�o�[�ɐڑ����Ă��Ȃ���Ԃɐ؂�ւ���
                isConnecting = false;
            }
        }

        /// <summary>
        /// �Q�[���T�[�o�[�ւ̐ڑ������������ۂɌĂяo�����
        /// </summary>
        public override void OnJoinedRoom()
        {
            //���[���ɎQ��������Ԃɐ؂�ւ���
            joinedRoom = true;
        }
    }
}

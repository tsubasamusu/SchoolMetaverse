using Photon.Pun;
using Photon.Realtime;
using UniRx;

namespace SchoolMetaverse
{
    public class PhotonController : MonoBehaviourPunCallbacks, ISetUp
    {
        private bool isConnecting;//�}�X�^�[�T�[�o�[�ɐڑ����Ă��邩�ǂ���

        public ReactiveProperty<bool> JoinedRoom = new(false);//���[���ɎQ���������ǂ���

        /// <summary>
        /// Start���\�b�h���O�ɌĂяo�����
        /// </summary>
        private void Awake()
        {
            //�}�X�^�[�N���C�A���g���V�[�������[�h������A����ȊO�̃N���C�A���g�������V�[�������[�h����
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        /// <summary>
        /// PhotonController�̏����ݒ���s��
        /// </summary>
        public void SetUp()
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
            JoinedRoom.Value= true;
        }
    }
}

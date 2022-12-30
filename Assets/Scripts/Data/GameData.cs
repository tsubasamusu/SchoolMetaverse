using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace SchoolMetaverse
{
    [RequireComponent(typeof(PhotonView))]
    public class GameData : MonoBehaviour, IPunObservable
    {
        [HideInInspector]
        public string playerName;//�v���C���[�̖��O

        public static GameData instance;//�C���X�^���X

        private PhotonView photonView;//PhotonView

        /// <summary> 
        /// Start���\�b�h���O�ɌĂяo����� 
        /// </summary> 
        private void Awake()
        {
            //�ȉ��A�V���O���g���ɕK�{�̋L�q 
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// �Q�[���J�n����ɌĂяo�����
        /// </summary>
        private void Start()
        {
            //�v���C���[�̖��O�����ɕۑ�����Ă���Ȃ�A������擾����
            if (PlayerPrefs.HasKey("PlayerName")) playerName = PlayerPrefs.GetString("PlayerName");

            //PhotonView���擾����
            photonView = GetComponent<PhotonView>();

            //Request�ɕύX����
            photonView.OwnershipTransfer = OwnershipOption.Request;
        }

        /// <summary>
        /// �f�o�C�X�Ƀv���C���[�̖��O��ۑ�����
        /// </summary>
        public void SetDevicePlayerName()
        {
            //�v���C���[�̖��O��ۑ�����
            PlayerPrefs.SetString("PlayerName", playerName);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="stream">PhotonStream</param>
        /// <param name="info">PhotonMessageInfo</param>
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                //stream.SendNext(playerControllers);
            }
            else
            {
                //playerControllers = (PlayerController[])stream.ReceiveNext();
            }
        }

        /// <summary>
        /// ���L����v������
        /// </summary>
        public void RequestOwnership() { photonView.RequestOwnership(); }
    }
}

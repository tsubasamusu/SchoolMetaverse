using Photon.Pun;
using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// �ϓ��l���Ǘ�����
    /// </summary>
    public class GameData : MonoBehaviour
    {
        [HideInInspector]
        public string playerName;//�v���C���[�̖��O

        [HideInInspector]
        public string message;//���b�Z�[�W

        public static GameData instance;//�C���X�^���X

        /// <summary> 
        /// Start���\�b�h���O�ɌĂяo����� 
        /// </summary> 
        private void Awake() { if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); } else { Destroy(gameObject); } }

        /// <summary>
        /// �Q�[���J�n����ɌĂяo�����
        /// </summary>
        private void Start()
        {
            //�v���C���[�̖��O�����ɕۑ�����Ă���Ȃ�A������擾����
            if (PlayerPrefs.HasKey("PlayerName")) playerName = PlayerPrefs.GetString("PlayerName");
        }

        /// <summary>
        /// �f�o�C�X�Ƀv���C���[�̖��O��ۑ�����
        /// </summary>
        public void SavePlayerNameInDevice() { PlayerPrefs.SetString("PlayerName", playerName); }

        /// <summary>
        /// �T�[�o�[�Ƀv���C���[�̖��O��ۑ�����
        /// </summary>
        public void SavePlayerNameInServer() { PhotonNetwork.LocalPlayer.NickName = playerName; }
    }
}

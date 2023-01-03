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
        public float lookSensitivity;//���_���x�i0�`10�j

        [HideInInspector]
        public string[] messages = new string[ConstData.MAX_MESSAGE_LINES];//���b�Z�[�W�̔z��

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
            //�f�o�C�X�ɕۑ�����Ă���f�[�^���擾����
            if (PlayerPrefs.HasKey("PlayerName")) playerName = PlayerPrefs.GetString("PlayerName");
            if (PlayerPrefs.HasKey("LookSensitivity")) lookSensitivity = PlayerPrefs.GetFloat("LookSensitivity");
        }

        /// <summary>
        /// �f�o�C�X�Ƀv���C���[�̖��O��ۑ�����
        /// </summary>
        public void SavePlayerNameInDevice() { PlayerPrefs.SetString("PlayerName", playerName); }

        /// <summary>
        /// �f�o�C�X�Ɏ��_���x��ۑ�����
        /// </summary>
        public void SavelookSensitivityInDevice() { PlayerPrefs.SetFloat("LookSensitivity",lookSensitivity); }
    }
}

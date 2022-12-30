using System.Collections.Generic;
using UnityEngine;

namespace SchoolMetaverse
{
    public class GameData : MonoBehaviour
    {
        [HideInInspector]
        public string playerName;//�v���C���[�̖��O

        [HideInInspector]
        public List<PlayerController> playerControllers = new();//�v���C���[�̃��X�g

        public static GameData instance;//�C���X�^���X

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
        }

        /// <summary>
        /// �f�o�C�X�Ƀv���C���[�̖��O��ۑ�����
        /// </summary>
        public void SetDevicePlayerName()
        {
            //�v���C���[�̖��O��ۑ�����
            PlayerPrefs.SetString("PlayerName", playerName);
        }
    }
}

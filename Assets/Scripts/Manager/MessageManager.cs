using Photon.Pun;
using System.Collections;
using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// ���b�Z�[�W�𐧌䂷��
    /// </summary>
    public class MessageManager : MonoBehaviourPunCallbacks, ISetUp
    {
        [SerializeField]
        private UIManagerMain uiManagerMain;//UIManagerMain

        /// <summary>
        /// MessageManager�̏����ݒ���s��
        /// </summary>
        public void SetUp()
        {
            //�J�X�^���v���p�e�B���쐬����
            var hashtable = new ExitGames.Client.Photon.Hashtable
            {
                //���b�Z�[�W�̔z�����������
                ["Messages"] = GameData.instance.messages
            };

            //�J�X�^���v���p�e�B��o�^����
            PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
        }

        /// <summary>
        /// ���b�Z�[�W�̃f�[�^���X�V����
        /// </summary>
        /// <param name="message">���b�Z�[�W</param>
        /// <param name="senderName">���M�҂̖��O</param>
        public void UpdateMessageData(string message, string senderName)
        {
            //���b�Z�[�W�̔z��ɋ󂫂������Ȃ�A���b�Z�[�W�̔z��̍ŏ��̗v�f����ɂ���
            if (!CheckMessagesIsFull()) GameData.instance.messages[0] = string.Empty;

            //���b�Z�[�W�̔z��̗v�f�������J��Ԃ�
            for (int i = 0; i < GameData.instance.messages.Length; i++)
            {
                //1��ڂ̌J��Ԃ������Ȃ�A���̌J��Ԃ������Ɉڂ�
                if (i == 0) continue;

                //���b�Z�[�W�̔z��̗v�f��1���炷
                GameData.instance.messages[i - 1] = GameData.instance.messages[i];
            }

            //���b�Z�[�W�̔z��Ɏ擾�������b�Z�[�W��o�^����
            GameData.instance.messages[ConstData.MAX_MESSAGE_LINES - 1] = "�@" + senderName + "�F" + message + "\n";

            //�J�X�^���v���p�e�B�Ƀ��b�Z�[�W��o�^����
            PhotonNetwork.CurrentRoom.CustomProperties["Messages"] = GameData.instance.messages;

            //UI��ݒ肷��
            uiManagerMain.SetTxtMessage();

            //���b�Z�[�W�̔z��ɋ󂫂��Ȃ������ׂ�
            bool CheckMessagesIsFull()
            {
                //�g�p����Ă���v�f�̐�
                int usedBoxCount = 0;

                //���b�Z�[�W�̔z��̗v�f�������J��Ԃ�
                for (int i = 0; i < GameData.instance.messages.Length; i++)
                {
                    //�g�p����Ă���v�f�̐����J�E���g����
                    if (GameData.instance.messages[i] != string.Empty) usedBoxCount++;
                }

                //���ʂ�Ԃ�
                return usedBoxCount != GameData.instance.messages.Length;
            }
        }

        ///// <summary>
        ///// ���[���̃J�X�^���v���p�e�B���X�V���ꂽ�ۂɌĂяo�����
        ///// </summary>
        ///// <param name="propertiesThatChanged">�X�V���ꂽ�v���p�e�B</param>
        //public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        //{
        //    //�X�V���ꂽ���[���̃f�[�^���擾����
        //    GameData.instance.messages = (string[])PhotonNetwork.CurrentRoom.CustomProperties["Messages"];

        //    //UI��ݒ肷��
        //    uiManagerMain.SetTxtMessage();
        //}
    }
}

using Photon.Pun;
using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// ���b�Z�[�W�𐧌䂷��
    /// </summary>
    [RequireComponent(typeof(PhotonView))]
    public class MessageManager : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private UIManagerMain uiManagerMain;//UIManagerMain

        /// <summary>
        /// ���b�Z�[�W�̃f�[�^�̍X�V�̏������s��
        /// </summary>
        /// <param name="senderName">���M�҂̖��O</param>
        /// <param name="message">���b�Z�[�W</param>
        public void PrepareUpdateMessageData(string senderName, string message)
        { photonView.RPC(nameof(UpdateMessageData), RpcTarget.All, GameData.instance.playerName, message); }

        /// <summary>
        /// ���b�Z�[�W�̃f�[�^���X�V����
        /// </summary>
        /// <param name="senderName">���M�҂̖��O</param>
        /// <param name="message">���b�Z�[�W</param>
        [PunRPC]
        private void UpdateMessageData(string senderName, string message)
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

            //���b�Z�[�W�̃e�L�X�g���X�V����
            uiManagerMain.UpdateTxtMessage();

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
    }
}

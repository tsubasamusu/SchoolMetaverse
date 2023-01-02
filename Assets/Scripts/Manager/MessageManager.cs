using Cysharp.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using System.Threading;
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
        /// ���b�Z�[�W�̑��M�̏������s��
        /// </summary>
        /// <param name="senderName">���M�҂̖��O</param>
        /// <param name="message">���b�Z�[�W</param>
        public void PrepareSendMessage(string senderName, string message)
        { photonView.RPC(nameof(SendMessage), RpcTarget.All, senderName, message); }

        /// <summary>
        /// ���b�Z�[�W�𑗐M����
        /// </summary>
        /// <param name="senderName">���M�҂̖��O</param>
        /// <param name="message">���b�Z�[�W</param>
        [PunRPC]
        private void SendMessage(string senderName, string message)
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
            static bool CheckMessagesIsFull()
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

        /// <summary>
        /// ���̃v���C���[�����[���ɎQ�������ۂɌĂяo�����
        /// </summary>
        /// <param name="newPlayer">�Q�������v���C���[</param>
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            //�{�b�g���烁�b�Z�[�W�𑗐M����
            SendMessageFromBotAsync(this.GetCancellationTokenOnDestroy(), newPlayer, "���񂪎Q�����܂����B").Forget();
        }

        /// <summary>
        /// ���̃v���C���[�����[�����痣�ꂽ�ۂɌĂяo�����
        /// </summary>
        /// <param name="otherPlayer">���ꂽ�v���C���[</param>
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            //�{�b�g���烁�b�Z�[�W�𑗐M����
            SendMessageFromBotAsync(this.GetCancellationTokenOnDestroy(), otherPlayer, "���񂪑ޏo���܂����B").Forget();
        }

        /// <summary>
        /// �{�b�g�ɂ�郁�b�Z�[�W�̑��M���s��
        /// </summary>
        /// <param name="token">CancellationToken</param>
        /// <param name="player">Player</param>
        /// <param name="message">���b�Z�[�W</param>
        /// <returns>�҂�����</returns>
        private async UniTaskVoid SendMessageFromBotAsync(CancellationToken token, Player player, string message)
        {
            //�Ώۂ̃v���C���[�̃j�b�N�l�[�����ݒ肳���܂ő҂�
            await UniTask.WaitUntil(() => player.NickName != string.Empty, cancellationToken: token);

            //���b�Z�[�W�̑��M�̏������s��
            PrepareSendMessage("Bot", player.NickName + message);
        }
    }
}

using Cysharp.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using System.Threading;
using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// メッセージを制御する
    /// </summary>
    [RequireComponent(typeof(PhotonView))]
    public class MessageManager : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private UIManagerMain uiManagerMain;//UIManagerMain

        /// <summary>
        /// メッセージの送信の準備を行う
        /// </summary>
        /// <param name="senderName">送信者の名前</param>
        /// <param name="message">メッセージ</param>
        public void PrepareSendMessage(string senderName, string message)
        { photonView.RPC(nameof(SendMessage), RpcTarget.All, senderName, message); }

        /// <summary>
        /// メッセージを送信する
        /// </summary>
        /// <param name="senderName">送信者の名前</param>
        /// <param name="message">メッセージ</param>
        [PunRPC]
        private void SendMessage(string senderName, string message)
        {
            //メッセージの配列に空きが無いなら、メッセージの配列の最初の要素を空にする
            if (!CheckMessagesIsFull()) GameData.instance.messages[0] = string.Empty;

            //メッセージの配列の要素数だけ繰り返す
            for (int i = 0; i < GameData.instance.messages.Length; i++)
            {
                //1回目の繰り返し処理なら、次の繰り返し処理に移る
                if (i == 0) continue;

                //メッセージの配列の要素を1つずらす
                GameData.instance.messages[i - 1] = GameData.instance.messages[i];
            }

            //メッセージの配列に取得したメッセージを登録する
            GameData.instance.messages[ConstData.MAX_MESSAGE_LINES - 1] = "　" + senderName + "：" + message + "\n";

            //メッセージのテキストを更新する
            uiManagerMain.UpdateTxtMessage();

            //メッセージの配列に空きがないか調べる
            static bool CheckMessagesIsFull()
            {
                //使用されている要素の数
                int usedBoxCount = 0;

                //メッセージの配列の要素数だけ繰り返す
                for (int i = 0; i < GameData.instance.messages.Length; i++)
                {
                    //使用されている要素の数をカウントする
                    if (GameData.instance.messages[i] != string.Empty) usedBoxCount++;
                }

                //結果を返す
                return usedBoxCount != GameData.instance.messages.Length;
            }
        }

        /// <summary>
        /// 他のプレイヤーがルームに参加した際に呼び出される
        /// </summary>
        /// <param name="newPlayer">参加したプレイヤー</param>
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            //ボットからメッセージを送信する
            SendMessageFromBotAsync(this.GetCancellationTokenOnDestroy(), newPlayer, "さんが参加しました。").Forget();
        }

        /// <summary>
        /// 他のプレイヤーがルームから離れた際に呼び出される
        /// </summary>
        /// <param name="otherPlayer">離れたプレイヤー</param>
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            //ボットからメッセージを送信する
            SendMessageFromBotAsync(this.GetCancellationTokenOnDestroy(), otherPlayer, "さんが退出しました。").Forget();
        }

        /// <summary>
        /// ボットによるメッセージの送信を行う
        /// </summary>
        /// <param name="token">CancellationToken</param>
        /// <param name="player">Player</param>
        /// <param name="message">メッセージ</param>
        /// <returns>待ち時間</returns>
        private async UniTaskVoid SendMessageFromBotAsync(CancellationToken token, Player player, string message)
        {
            //対象のプレイヤーのニックネームが設定されるまで待つ
            await UniTask.WaitUntil(() => player.NickName != string.Empty, cancellationToken: token);

            //メッセージの送信の準備を行う
            PrepareSendMessage("Bot", player.NickName + message);
        }
    }
}

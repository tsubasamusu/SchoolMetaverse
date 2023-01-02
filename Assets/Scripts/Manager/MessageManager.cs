using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// メッセージを制御する
    /// </summary>
    public class MessageManager : MonoBehaviour
    {
        /// <summary>
        /// メッセージのデータを更新する
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="senderName">送信者の名前</param>
        public void UpdateMessageData(string message, string senderName)
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

            //メッセージの配列に空きがないか調べる
            bool CheckMessagesIsFull()
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
    }
}

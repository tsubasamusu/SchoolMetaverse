using Photon.Pun;
using System.Drawing;
using System.IO;
using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// 画像に関する処理を行う
    /// </summary>
    [RequireComponent(typeof(PhotonView))]
    public class PictureManager : MonoBehaviourPunCallbacks,ISetUp
    {
        [SerializeField]
        private UIManagerMain uiManagerMain;//UIManagerMain

        [SerializeField]
        private MessageManager messageManager;//MessageManager

        /// <summary>
        /// PictureManagerの初期設定を行う
        /// </summary>
        public void SetUp()
        {
            //既に他のプレイヤーが画像を表示しているなら
            if (PhotonNetwork.CurrentRoom.CustomProperties["PictureBites"] is byte[] bytes)
            {
                //バイナリデータから画像を黒板に設置する
                SetPictureFromBytes(bytes);
            }
        }

        /// <summary>
        /// 画像を送信する準備を行う
        /// </summary>
        /// <param name="picturePath"></param>
        public void PrepareSendPicture(string picturePath) { photonView.RPC(nameof(SendPicture), RpcTarget.All,picturePath); }

        /// <summary>
        /// 画像を送信する
        /// </summary>
        [PunRPC]
        private void SendPicture(string picturePath)
        {
            //イメージ（保持用）
            Image imgPicture;

            //イメージを取得する
            try { imgPicture = Image.FromFile(picturePath); }

            //ファイルが見つからなかったら
            catch (FileNotFoundException)
            {
                //メッセージを送信する
                messageManager.PrepareSendMessage("Bot", "正しい画像のパスを入力してください。");

                //以降の処理を行わない
                return;
            }

            //ImageConverterを作成する
            ImageConverter imageConverter = new();

            //イメージをバイナリデータに変換する
            byte[] bytes = (byte[])imageConverter.ConvertTo(imgPicture, typeof(byte[]));

            //バイナリデータから画像を黒板に設置する
            SetPictureFromBytes(bytes);
        }

        /// <summary>
        /// バイナリデータから画像を黒板に設置する
        /// </summary>
        /// <param name="bytes">バイナリデータ</param>
        private void SetPictureFromBytes(byte[] bytes)
        {
            //Texture2Dを作成する
            Texture2D texture = new(1, 1);

            //テクスチャを作成できなかったら
            if (!texture.LoadImage(bytes))
            {
                //メッセージを送信する
                messageManager.PrepareSendMessage("Bot", "画像のテクスチャを作成できませんでした。");

                //以降の処理を行わない
                return;
            }

            //スプライトを作成する
            Sprite sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero);

            //Hashtableを作成する
            var hashtable = new ExitGames.Client.Photon.Hashtable
            {
                //ゲームサーバーに画像のバイナリデータを持たせる
                ["PictureBites"] = bytes
            };

            //作成したカスタムプロパティを持たせる
            PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);

            //黒板のスプライトを設定する
            uiManagerMain.SetImgBlackBordSprite(sprite, texture.width, texture.height);
        }
    }
}


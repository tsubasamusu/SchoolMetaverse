using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// 画像に関する処理を行う
    /// </summary>
    public class PictureManager : MonoBehaviour
    {
        [SerializeField]
        private UIManagerMain uiManagerMain;//UIManagerMain

        [SerializeField]
        private MessageManager messageManager;//MessageManager

        [SerializeField]
        private Material blackBoardMaterial;//黒板のマテリアル

        /// <summary>
        /// 画像を送信する
        /// </summary>
        public void SendPicture()
        {
            //入力された画像のパスを取得する
            string picturePath = string.Empty;//"C:/Users/Hashimoto/Pictures/ゲーム素材/ゲージ/IMG_4472.png";

            //イメージ（保持用）
            Image imgPicture = null;

            //イメージを取得する
            try{imgPicture = Image.FromFile(picturePath);}

            //ファイルが見つからなかったら
            catch(FileNotFoundException)
            {
                //メッセージを送信する
                messageManager.PrepareSendMessage("Bot", "正しい画像のパスを入力してください。");
            }

            //ImageConverterを作成する
            ImageConverter imageConverter = new();

            //イメージをバイナリデータに変換する
            byte[] bytes = (byte[])imageConverter.ConvertTo(imgPicture, typeof(byte[]));

            //Texture2Dを作成する
            Texture2D texture = new(1, 1);

            //テクスチャを作成できなかったら
            if(! texture.LoadImage(bytes))
            {
                //メッセージを送信する
                messageManager.PrepareSendMessage("Bot", "画像のテクスチャを作成できませんでした。");

                //以降の処理を行わない
                return;
            }

            //黒板のテクスチャを設定する
            blackBoardMaterial.mainTexture= texture;
        }
    }
}

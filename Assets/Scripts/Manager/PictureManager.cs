using Photon.Pun;
using System.Drawing;
using System.IO;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// 画像に関する処理を行う
    /// </summary>
    public class PictureManager : MonoBehaviour, ISetUp
    {
        [SerializeField]
        private UIManagerMain uiManagerMain;//UIManagerMain

        /// <summary>
        /// PictureManagerの初期設定を行う
        /// </summary>
        public void SetUp()
        {
            //画像を同期する
            this.UpdateAsObservable()
                .Where(_ => PhotonNetwork.CurrentRoom.CustomProperties["PictureBites"] is byte[])
                .ThrottleFirst(System.TimeSpan.FromSeconds(ConstData.PICTURE_SYNCHRONIZE_SPAN))
                .Subscribe(_ => { SetPictureFromBytes((byte[])PhotonNetwork.CurrentRoom.CustomProperties["PictureBites"]); })
                .AddTo(this);
        }

        /// <summary>
        /// 画像を送信する
        /// </summary>
        public void SendPicture(string picturePath)
        {
            //他のプレイヤーが画像を設定中なら
            if (PhotonNetwork.CurrentRoom.CustomProperties["IsSettingPicture"] is bool isSettingPicture && isSettingPicture)
            {
                //効果音を再生する
                SoundManager.instance.PlaySound(SoundDataSO.SoundName.エラーを表示する時の音);

                //エラーを表示する
                uiManagerMain.SetTxtSendPictureError("他のプレイヤーが画像を送信中です。");

                //以降の処理を行わない
                return;
            }

            //Hashtableを作成する
            var hashtable = new ExitGames.Client.Photon.Hashtable
            {
                //ゲームサーバーに「画像設定中」という情報を持たせる
                ["IsSettingPicture"] = true
            };

            //作成したカスタムプロパティを登録する
            PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);

            //イメージ（保持用）
            Image imgPicture;

            //イメージを取得する
            try { imgPicture = Image.FromFile(picturePath); }

            //ファイルが見つからなかったら
            catch (FileNotFoundException)
            {
                //効果音を再生する
                SoundManager.instance.PlaySound(SoundDataSO.SoundName.エラーを表示する時の音);

                //画像のサイズ調節用のスライダーを非活性化する
                uiManagerMain.SetSldPictureSizeActive(false);

                //エラーを表示する
                uiManagerMain.SetTxtSendPictureError("正しい画像のパスを入力してください。");

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
                //効果音を再生する
                SoundManager.instance.PlaySound(SoundDataSO.SoundName.エラーを表示する時の音);

                //画像のサイズ調節用のスライダーを非活性化する
                uiManagerMain.SetSldPictureSizeActive(false);

                //エラーを表示する
                uiManagerMain.SetTxtSendPictureError("画像のテクスチャを作成できませんでした。\n開発者に問い合わせてください。\nhttps://tsubasamusu.com");

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

            //作成したカスタムプロパティを登録する
            PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);

            //黒板のスプライトを設定する
            uiManagerMain.SetImgBlackBordSprite(sprite, texture.width, texture.height);

            //Hashtableを作成する
            var hashtable1 = new ExitGames.Client.Photon.Hashtable
            {
                //ゲームサーバーに「画像設定中ではない」という情報を持たせる
                ["IsSettingPicture"] = false
            };

            //作成したカスタムプロパティを登録する
            PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable1);
        }
    }
}


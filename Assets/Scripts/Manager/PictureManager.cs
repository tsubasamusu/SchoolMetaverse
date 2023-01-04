using UnityEngine;
using System.Drawing;

namespace SchoolMetaverse
{
    /// <summary>
    /// 画像に関する処理を行う
    /// </summary>
    public class PictureManager : MonoBehaviour
    {
        /// <summary>
        /// 画像のバイナリデータを取得する
        /// </summary>
        /// <returns>画像のバイナリデータ</returns>
        public byte[] GetPictureData()
        {
            string picturePath = "";

            //イメージを取得する
            Image imgPicture=Image.FromFile(picturePath);

            //ImageConverterを作成する
            ImageConverter imageConverter = new();

            //イメージをバイナリデータに変換して返す
            return (byte[])imageConverter.ConvertTo(imgPicture, typeof(byte[]));
        }
    }
}

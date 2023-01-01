using System;
using System.Windows.Forms;
using System.IO;
using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// 画像に関する処理を行う
    /// </summary>
    public class PictureManager : MonoBehaviour
    {
        /// <summary>
        /// 画像を取得する
        /// </summary>
        public void GetPicture()
        {
            //ファイルのパスを取得する
            string filePath = GetFilePathByDialog();

            //ファイル名を取得する
            string fileName = Path.GetFileName(filePath);
        }

        /// <summary>
        /// ダイアログを用いてファイルのパスを取得する
        /// </summary>
        /// <returns>ファイルのパス</returns>
        private string GetFilePathByDialog()
        {
            //ファイルパスの保持用
            string filePath = string.Empty;

            //ファイル名を返す
            return OpenDialog(ref filePath) ? filePath : string.Empty;
        }

        /// <summary>
        /// ダイアログを開く
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns>結果</returns>
        private bool OpenDialog(ref string fileName)
        {
            //ダイアログを作成する
            SaveFileDialog dlg = new()
            {
                //ファイル名を設定する
                FileName = fileName,

                //初期フォルダを「MyPictures」に設定する
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),

                //「jpg」と「png」以外のファイルをダイアログに表示しない
                Filter = "画像ファイル(*.jpg;*.png)|*.jpg;*.png|すべてのファイル(*.*)|*.*",

                //Filterの1つめの画像ファイルを指定する
                FilterIndex = 1,

                //タイトルを設定する
                Title = "画像を選択",

                //カレントディレクトリを復元する
                RestoreDirectory = true
            };

            //ダイアログを開けないなら
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                //ファイル名を空にする
                fileName = string.Empty;

                //falseを返す
                return false;
            }

            //選択したファイル名を取得する
            fileName = dlg.FileName;

            //trueを返す
            return true;
        }
    }
}

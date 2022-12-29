using System;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

namespace SchoolMetaverse
{
    /// <summary>
    /// 画像に関する処理を行う
    /// </summary>
    public class PictureManager : MonoBehaviour
    {
        [SerializeField]
        private Text txtFileName;//ファイル名のテキスト

        /// <summary>
        /// ダイアログを開く
        /// </summary>
        public void OpenDialog()
        {
            //ファイル名の保持用
            string file = string.Empty;

            //画像ファイルを開くダイアログを呼び出し、ファイル名を取得する
            if (SaveImgFileDialog(ref file) == true) txtFileName.text = file;
        }

        /// <summary>
        /// ダイアログを開く
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns>結果</returns>
        private bool SaveImgFileDialog(ref string fileName)
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
                Title = "画像ファイル指定",

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

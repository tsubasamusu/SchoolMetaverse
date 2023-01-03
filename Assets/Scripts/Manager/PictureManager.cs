using System.Diagnostics;
using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// 画像に関する処理を行う
    /// </summary>
    public class PictureManager : MonoBehaviour
    {
        private bool isChoosingPicture;//画像選択中かどうか

        private Process exProcess;//起動する外部プロセス

        /// <summary>
        /// 画像選択中かどうか（取得用）
        /// </summary>
        public bool IsChoosingPicture { get => isChoosingPicture; }

        /// <summary>
        /// 画像を取得し、表示する
        /// </summary>
        public void GetAndDisplayPicture()
        {
            //画像選択中に変更する
            isChoosingPicture= true;

            //パスを取得する
            string path = Application.dataPath + "/Plugins/GetPictureData.exe";

            //外部プロセスをインスタンス化する
            exProcess = new Process();

            //パスを登録する
            exProcess.StartInfo.FileName = exProcess.StartInfo.Arguments = path;

            //外部プロセスの終了を検知してイベントを発生させる
            exProcess.EnableRaisingEvents = true;
            exProcess.Exited += OnProcessExited;

            //外部プロセスを実行する
            exProcess.Start();
        }

        /// <summary>
        /// 外部プロセスが終了した際に呼び出される
        /// </summary>
        /// <param name="sender">送信者</param>
        /// <param name="eventArgs">EventArgs</param>
        private void OnProcessExited(object sender, System.EventArgs eventArgs)
        {
            //ウィンドウを閉じる
            exProcess.CloseMainWindow();

            //外部プロセスを処分する
            exProcess.Dispose();

            //画像の選択をしていない状態に切り替える
            isChoosingPicture= false;
        }
    }
}
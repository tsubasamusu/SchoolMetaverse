using System;
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

        private Process process;//プロセス

        /// <summary>
        /// 画像選択中かどうか（取得用）
        /// </summary>
        public bool IsChoosingPicture { get => isChoosingPicture; }

        /// <summary>
        /// 外部プロセスを実行する
        /// </summary>
        public void LaunchExternalProsess()
        {
            //プロセスを作成する
            process = new Process
            {
                // ロセスを起動するときに使用する値のセットを指定する
                StartInfo = new ProcessStartInfo
                {
                    //起動するファイルのパスを指定する
                    FileName = Application.dataPath + "/Plugins/GetPictureData.exe",

                    //プロセスの起動にOSのシェルを使用しないように設定する
                    UseShellExecute = false,

                    //StandardInputから入力を読み取るように設定する
                    RedirectStandardInput = true,

                    //出力をStandardOutputに書き込むように設定する
                    RedirectStandardOutput = true,
                },
                //外部プロセスの終了を検知する
                EnableRaisingEvents = true
            };
            //メソッドを登録する
            process.Exited += OnEndedProcess;

            //外部プロセスを起動する
            process.Start();
            process.BeginOutputReadLine();
        }

        /// <summary>
        /// 外部プロセスが終了した際に呼び出される
        /// </summary>
        private void OnEndedProcess(object sender, EventArgs e)
        {
            //外部プロセスを取得できていないか、外部プロセスが実行中なら、以降の処理を行わない
            if (process == null || process.HasExited) return;

            //外部プロセスを終了する
            process.StandardInput.Close();
            process.CloseMainWindow();
            process.Dispose();
            process = null;
        }
    }
}
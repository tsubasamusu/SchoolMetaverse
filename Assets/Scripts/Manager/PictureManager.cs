using System;
using System.Diagnostics;
using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// �摜�Ɋւ��鏈�����s��
    /// </summary>
    public class PictureManager : MonoBehaviour
    {
        private bool isChoosingPicture;//�摜�I�𒆂��ǂ���

        private Process process;//�v���Z�X

        /// <summary>
        /// �摜�I�𒆂��ǂ����i�擾�p�j
        /// </summary>
        public bool IsChoosingPicture { get => isChoosingPicture; }

        /// <summary>
        /// �O���v���Z�X�����s����
        /// </summary>
        public void LaunchExternalProsess()
        {
            //�v���Z�X���쐬����
            process = new Process
            {
                // ���Z�X���N������Ƃ��Ɏg�p����l�̃Z�b�g���w�肷��
                StartInfo = new ProcessStartInfo
                {
                    //�N������t�@�C���̃p�X���w�肷��
                    FileName = Application.dataPath + "/Plugins/GetPictureData.exe",

                    //�v���Z�X�̋N����OS�̃V�F�����g�p���Ȃ��悤�ɐݒ肷��
                    UseShellExecute = false,

                    //StandardInput������͂�ǂݎ��悤�ɐݒ肷��
                    RedirectStandardInput = true,

                    //�o�͂�StandardOutput�ɏ������ނ悤�ɐݒ肷��
                    RedirectStandardOutput = true,
                },
                //�O���v���Z�X�̏I�������m����
                EnableRaisingEvents = true
            };
            //���\�b�h��o�^����
            process.Exited += OnEndedProcess;

            //�O���v���Z�X���N������
            process.Start();
            process.BeginOutputReadLine();
        }

        /// <summary>
        /// �O���v���Z�X���I�������ۂɌĂяo�����
        /// </summary>
        private void OnEndedProcess(object sender, EventArgs e)
        {
            //�O���v���Z�X���擾�ł��Ă��Ȃ����A�O���v���Z�X�����s���Ȃ�A�ȍ~�̏������s��Ȃ�
            if (process == null || process.HasExited) return;

            //�O���v���Z�X���I������
            process.StandardInput.Close();
            process.CloseMainWindow();
            process.Dispose();
            process = null;
        }
    }
}
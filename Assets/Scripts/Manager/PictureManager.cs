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

        private Process exProcess;//�N������O���v���Z�X

        /// <summary>
        /// �摜�I�𒆂��ǂ����i�擾�p�j
        /// </summary>
        public bool IsChoosingPicture { get => isChoosingPicture; }

        /// <summary>
        /// �摜���擾���A�\������
        /// </summary>
        public void GetAndDisplayPicture()
        {
            //�摜�I�𒆂ɕύX����
            isChoosingPicture= true;

            //�p�X���擾����
            string path = Application.dataPath + "/Plugins/GetPictureData.exe";

            //�O���v���Z�X���C���X�^���X������
            exProcess = new Process();

            //�p�X��o�^����
            exProcess.StartInfo.FileName = exProcess.StartInfo.Arguments = path;

            //�O���v���Z�X�̏I�������m���ăC�x���g�𔭐�������
            exProcess.EnableRaisingEvents = true;
            exProcess.Exited += OnProcessExited;

            //�O���v���Z�X�����s����
            exProcess.Start();
        }

        /// <summary>
        /// �O���v���Z�X���I�������ۂɌĂяo�����
        /// </summary>
        /// <param name="sender">���M��</param>
        /// <param name="eventArgs">EventArgs</param>
        private void OnProcessExited(object sender, System.EventArgs eventArgs)
        {
            //�E�B���h�E�����
            exProcess.CloseMainWindow();

            //�O���v���Z�X����������
            exProcess.Dispose();

            //�摜�̑I�������Ă��Ȃ���Ԃɐ؂�ւ���
            isChoosingPicture= false;
        }
    }
}
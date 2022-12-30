using System;
using System.Windows.Forms;
using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// �摜�Ɋւ��鏈�����s��
    /// </summary>
    public class PictureManager : MonoBehaviour
    {
        /// <summary>
        /// �_�C�A���O��p���ăt�@�C�������擾����
        /// </summary>
        /// <returns>�t�@�C����</returns>
        private string GetFileNameByDialog()
        {
            //�t�@�C�����̕ێ��p
            string fileName = string.Empty;

            //�t�@�C������Ԃ�
            return OpenDialog(ref fileName) ? fileName : string.Empty;
        }

        /// <summary>
        /// �_�C�A���O���J��
        /// </summary>
        /// <param name="fileName">�t�@�C����</param>
        /// <returns>����</returns>
        private bool OpenDialog(ref string fileName)
        {
            //�_�C�A���O���쐬����
            SaveFileDialog dlg = new()
            {
                //�t�@�C������ݒ肷��
                FileName = fileName,
                 
                //�����t�H���_���uMyPictures�v�ɐݒ肷��
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),

                //�ujpg�v�Ɓupng�v�ȊO�̃t�@�C�����_�C�A���O�ɕ\�����Ȃ�
                Filter = "�摜�t�@�C��(*.jpg;*.png)|*.jpg;*.png|���ׂẴt�@�C��(*.*)|*.*",

                //Filter��1�߂̉摜�t�@�C�����w�肷��
                FilterIndex = 1,

                //�^�C�g����ݒ肷��
                Title = "�摜��I��",

                //�J�����g�f�B���N�g���𕜌�����
                RestoreDirectory = true        
            };

            //�_�C�A���O���J���Ȃ��Ȃ�
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                //�t�@�C��������ɂ���
                fileName = string.Empty;

                //false��Ԃ�
                return false;
            }

            //�I�������t�@�C�������擾����
            fileName = dlg.FileName;

            //true��Ԃ�
            return true;
        }
    }
}

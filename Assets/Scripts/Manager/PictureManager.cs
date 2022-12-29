using System;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

namespace SchoolMetaverse
{
    /// <summary>
    /// �摜�Ɋւ��鏈�����s��
    /// </summary>
    public class PictureManager : MonoBehaviour
    {
        [SerializeField]
        private Text txtFileName;//�t�@�C�����̃e�L�X�g

        /// <summary>
        /// �_�C�A���O���J��
        /// </summary>
        public void OpenDialog()
        {
            //�t�@�C�����̕ێ��p
            string file = string.Empty;

            //�摜�t�@�C�����J���_�C�A���O���Ăяo���A�t�@�C�������擾����
            if (SaveImgFileDialog(ref file) == true) txtFileName.text = file;
        }

        /// <summary>
        /// �_�C�A���O���J��
        /// </summary>
        /// <param name="fileName">�t�@�C����</param>
        /// <returns>����</returns>
        private bool SaveImgFileDialog(ref string fileName)
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
                Title = "�摜�t�@�C���w��",

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

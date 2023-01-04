using UnityEngine;
using System.Drawing;

namespace SchoolMetaverse
{
    /// <summary>
    /// �摜�Ɋւ��鏈�����s��
    /// </summary>
    public class PictureManager : MonoBehaviour
    {
        /// <summary>
        /// �摜�̃o�C�i���f�[�^���擾����
        /// </summary>
        /// <returns>�摜�̃o�C�i���f�[�^</returns>
        public byte[] GetPictureData()
        {
            string picturePath = "";

            //�C���[�W���擾����
            Image imgPicture=Image.FromFile(picturePath);

            //ImageConverter���쐬����
            ImageConverter imageConverter = new();

            //�C���[�W���o�C�i���f�[�^�ɕϊ����ĕԂ�
            return (byte[])imageConverter.ConvertTo(imgPicture, typeof(byte[]));
        }
    }
}

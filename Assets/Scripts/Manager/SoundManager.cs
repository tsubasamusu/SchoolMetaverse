using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>  
    /// ���̏������s��
    /// </summary>  
    public class SoundManager : MonoBehaviour
    {
        [SerializeField]
        private SoundDataSO soundDataSO;//SoundDataSO

        private AudioSource[] audioSources;//���Đ��p��AudioSource�̔z�� 

        public static SoundManager instance;//�C���X�^���X  

        /// <summary>
        /// �C���X�^���X������ɌĂяo�����
        /// </summary>
        private void Awake() { if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); } else { Destroy(gameObject); } }

        /// <summary>
        /// �Q�[���J�n����ɌĂяo�����
        /// </summary>
        private void Start()
        {
            //���Đ��p��AudioSource�̔z��̗v�f����ݒ肷��
            audioSources = new AudioSource[soundDataSO.soundDataList.Count];

            //���Đ��p��AudioSource�̔z��̗v�f�������J��Ԃ�  
            for (int i = 0; i < audioSources.Length; i++)
            {
                //AudioSorce�R���|�[�l���g���쐬���A���g�ɃA�^�b�`������ɁA�z��Ɋi�[����  
                audioSources[i] = gameObject.AddComponent<AudioSource>();
            }
        }

        /// <summary> 
        /// ���̃N���b�v���擾���� 
        /// </summary> 
        /// <param name="name">���̖��O</param> 
        /// <returns>���̃N���b�v</returns> 
        public AudioClip GetAudioClip(SoundDataSO.SoundName name)
        {
            //���̃N���b�v��Ԃ� 
            return soundDataSO.soundDataList.Find(x => x.name == name).clip;
        }

        /// <summary>  
        /// �����Đ�����  
        /// </summary>  
        /// <param name="name">���̖��O</param>  
        /// <param name="volume">���̃{�����[��</param>  
        /// <param name="loop">�J��Ԃ����ǂ���</param>  
        public void PlaySound(SoundDataSO.SoundName name, float volume = 1f, bool loop = false)
        {
            //���Đ��p��AudioSource�̔z��̗v�f��1�����o��  
            foreach (AudioSource source in audioSources)
            {
                //���o����AudioSource���Đ����ł͂Ȃ��i�g�p����Ă��Ȃ��j�Ȃ�  
                if (source.isPlaying == false)
                {
                    //���̃N���b�v��o�^����  
                    source.clip = GetAudioClip(name);

                    //���̃{�����[����ݒ肷��  
                    source.volume = volume;

                    //�J��Ԃ����ǂ�����ݒ肷�� 
                    source.loop = loop;

                    //�����Đ�����  
                    source.Play();

                    //�J��Ԃ��������甲���o�� 
                    break;
                }
            }
        }
    }
}
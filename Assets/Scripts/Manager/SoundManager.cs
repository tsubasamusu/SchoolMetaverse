using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>  
    /// 音の処理を行う
    /// </summary>  
    public class SoundManager : MonoBehaviour
    {
        [SerializeField]
        private SoundDataSO soundDataSO;//SoundDataSO

        private AudioSource audBgmPlayer;//BGM再生用のAudioSource

        private AudioSource[] audioSources;//音再生用のAudioSourceの配列 

        public static SoundManager instance;//インスタンス  

        /// <summary>
        /// インスタンス化直後に呼び出される
        /// </summary>
        private void Awake() { if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); } else { Destroy(gameObject); } }

        /// <summary>
        /// ゲーム開始直後に呼び出される
        /// </summary>
        private void Start()
        {
            //BGM再生用のAudioSourceを取得する
            audBgmPlayer = transform.GetChild(0).GetComponent<AudioSource>();

            //音再生用のAudioSourceの配列の要素数を設定する
            audioSources = new AudioSource[soundDataSO.soundDataList.Count];

            //音再生用のAudioSourceの配列の要素数だけ繰り返す  
            for (int i = 0; i < audioSources.Length; i++)
            {
                //AudioSorceコンポーネントを作成し、自身にアタッチした後に、配列に格納する  
                audioSources[i] = gameObject.AddComponent<AudioSource>();
            }
        }

        /// <summary> 
        /// 音のクリップを取得する 
        /// </summary> 
        /// <param name="name">音の名前</param> 
        /// <returns>音のクリップ</returns> 
        public AudioClip GetAudioClip(SoundDataSO.SoundName name) { return soundDataSO.soundDataList.Find(x => x.name == name).clip; }

        /// <summary>  
        /// 音を再生する  
        /// </summary>  
        /// <param name="name">音の名前</param>   
        /// <param name="isBgm">BGMどうか</param>  
        /// <param name="volume">音のボリューム</param> 
        public void PlaySound(SoundDataSO.SoundName name, bool isBgm = false, float volume = 1f)
        {
            //再生する音がBGMなら
            if (isBgm)
            {
                //BGMのクリップを登録する
                audBgmPlayer.clip = GetAudioClip(name);

                //BGMの音量を設定する
                audBgmPlayer.volume = volume;

                //繰り返す
                audBgmPlayer.loop = true;

                //BGMを再生する
                audBgmPlayer.Play();

                //以降の処理を行わない
                return;
            }

            //音再生用のAudioSourceの配列の要素を1つずつ取り出す  
            foreach (AudioSource source in audioSources)
            {
                //取り出したAudioSourceが再生中ではない（使用されていない）なら  
                if (source.isPlaying == false)
                {
                    //音のクリップを登録する  
                    source.clip = GetAudioClip(name);

                    //音のボリュームを設定する  
                    source.volume = volume;

                    //音を再生する  
                    source.Play();

                    //繰り返し処理から抜け出す 
                    break;
                }
            }
        }

        /// <summary>
        /// BGmの音量を更新する
        /// </summary>
        public void UpdateBgmVolume() { audBgmPlayer.volume = GameData.instance.bgmVolume; }
    }
}
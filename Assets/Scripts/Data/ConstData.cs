using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// 固定値を管理する
    /// </summary>
    public static class ConstData
    {
        public const int MAX_PLAYERS = 5;//最大参加人数

        public const int MAX_MESSAGE_LINES = 13;//メッセージの表示可能な最大行数

        public const float LOOK_SMOOTH = 0.1f;//視点の滑らかさ

        public const float MAX_CUSOR_LENGTH_FROM_CENTER = 20f;//マウスカーソルの中央からの最大値

        public const float MOVE_SPEED = 1.3f;//移動速度

        public const float MAX_CAMERA_ANGLE_X = 40f;//カメラの最大角度x

        public const float CAMERA_HEIGHT = 1.3f;//カメラの高さ

        public const float BACKGROUND_FADE_OUT_TIME = 1f;//背景がフェードアウトする時間

        public const float BUTTON_ANIMATION_SIZE = 1.3f;//ボタンのアニメーションのサイズ

        public const float PICTURE_SIZE_RATIO = 2f;//画像のサイズの調節用変数

        public const string PASSCODE = "0308";//パスコード

        public const KeyCode WALK_F_KEY = KeyCode.UpArrow;//前へ進むキー

        public const KeyCode WALK_R_KEY = KeyCode.RightArrow;//右へ進むキー

        public const KeyCode WALK_B_KEY = KeyCode.DownArrow;//後ろへ進むキー

        public const KeyCode WALK_L_KEY = KeyCode.LeftArrow;//左へ進むキー
    }
}

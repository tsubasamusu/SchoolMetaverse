using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// 固定値を管理する
    /// </summary>
    public static class ConstData
    {
        public const int MAX_PLAYERS = 10;//最大参加人数

        public const float LOOK_SENSITIVITY = 10f;//視点感度

        public const float MAX_CUSOR_LENGTH_FROM_CENTER = 50f;//マウスカーソルの中央からの最大値

        public const float MOVE_SPEED = 1.3f;//移動速度

        public const float MAX_CAMERA_ANGLE_X = 40f;//カメラの最大角度x

        public const float CAMERA_HEIGHT = 1.3f;//カメラの高さ

        public const float BACKGROUND_FADE_OUT_TIME = 1f;//背景がフェードアウトする時間

        public const string PASSCODE = "0308";//パスコード

        public const KeyCode WALK_F_KEY = KeyCode.UpArrow;//前へ進むキー

        public const KeyCode WALK_R_KEY = KeyCode.RightArrow;//右へ進むキー

        public const KeyCode WALK_B_KEY = KeyCode.DownArrow;//後ろへ進むキー

        public const KeyCode WALK_L_KEY = KeyCode.LeftArrow;//左へ進むキー

        public const KeyCode VIEWPOINT_MOVE_KEY = KeyCode.Mouse1;//視点移動キー
    }
}

using System.Collections.Generic;
using SSWC.Common.Serialization;
using SSWC.Data.Config;
using SSWC.Data.Save;
using UnityEngine;

namespace SSWC.Data
{
    /// <summary>
    /// 数据管理器，用来在游戏开始时初始化数据。
    /// </summary>
    public class DataManager
    {
        private static DataManager _Instance;

        public static DataManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DataManager();
                }

                return _Instance;
            }
        }

        //音乐数据类
        public MusicData MusicData;

        //玩家数据类
        public PlayerData PlayerData;

        //角色配置类列表
        public List<RoleInfoData> RoleInfoDatas;

        //当前选择的角色配置类
        public RoleInfoData NowRoleInfoData;

        //场景配置类列表
        public List<SceneData> SceneDatas;

        //怪物配置类列表
        public List<MonsterInfoData> MonsterInfoDatas;

        //塔配置类列表
        public List<TowerInfo> TowerInfoDatas;

        //初始化数据
        public DataManager()
        {
            MusicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
            PlayerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
            RoleInfoDatas = JsonMgr.Instance.LoadData<List<RoleInfoData>>("RoleInfoData");
            SceneDatas = JsonMgr.Instance.LoadData<List<SceneData>>("SceneData");
            MonsterInfoDatas = JsonMgr.Instance.LoadData<List<MonsterInfoData>>("MonsterInfo");
            TowerInfoDatas = JsonMgr.Instance.LoadData<List<TowerInfo>>("TowerInfo");
        }

        //保存音乐数据
        public void SaveMusicData()
        {
            JsonMgr.Instance.SaveData(MusicData, "MusicData");
        }

        //保存玩家数据
        public void SavePlayerData()
        {
            JsonMgr.Instance.SaveData(PlayerData, "PlayerData");
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="name">MP3文件的名字</param>
        public void PlaySound(string name)
        {
            GameObject audioObj = new GameObject("SFX");
            AudioSource audioSource = audioObj.AddComponent<AudioSource>();
            audioSource.clip = Resources.Load<AudioClip>("Music/" + name);

            if (!MusicData.IsOpenSfx)
                return;

            audioSource.volume = MusicData.SfxVolume;
            audioSource.Play();

            Object.Destroy(audioObj, audioSource.clip.length);
        }
    }
}

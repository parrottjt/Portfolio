using System;

using UnityEngine;

namespace FrikinCore.Achievement
{
    [CreateAssetMenu]

    public class AchievementModel : ScriptableObject
    {
        [SerializeField] Achievements achievement;
        [SerializeField] bool achieved;
        [SerializeField] string displayName;
        [SerializeField] string description;
        [SerializeField] bool hidden;
        [SerializeField] Sprite achievedIcon;
        [SerializeField] Sprite unachievedIcon;
        [SerializeField] int achievedProgressGate;

        public string Achievement => achievement.ToString();

        public bool Achieved
        {
            get => achieved;
            set => achieved = value;
        }

        public bool Hidden
        {
            get => hidden;
            set => hidden = value;
        }

        public int AchievedProgressGate
        {
            get => achievedProgressGate;
            set => achievedProgressGate = value;
        }

        public int Id { get; set; }

        //Honestly do we really need to keep the time stored, as we can't set the time that an achievement pops,
        //it calculates the time on server side.
        public DateTime DateAchieved { get; set; }
        public int ProgressStat { get; set; }
        public string SetBy { get; set; } = "Client";
        public string AchievedIconName { get; set; }
        public string UnachievedIconName { get; set; }

        public string DisplayName => displayName;

        public string Description => string.IsNullOrWhiteSpace(description) ? displayName : description;
        public Sprite AchievedIcon => achievedIcon;
        public Sprite UnachievedIcon => unachievedIcon;


        public void SetModelValues(int id, int progessStat, bool achieved = false, string setBy = "")
        {
            Id = id;
            //DateAchieved = dateAchieved;
            ProgressStat = progessStat;
            SetBy = setBy;
            Achieved = achieved;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatMangementApi.Models.Entities
{
    public class Group
    {
        public string Id { get; set; }
        public string GroupName { get; set; }
        public int MemberCount { get; set; }
        public bool GroupAdded { get; set; }

        public ICollection<AdminGroup> AdminGroups { get; set; }
        public ICollection<LimitedPeople> LimitedPeople { get; set; }
        public botSetting BotSetting { get; set; }

    }

    public class AdminGroup
    {
        public string GroupId { get; set; }
        public string AdminId { get; set; }

        public Admin Admin { get; set; }
        public Group Group { get; set; }

    }

    public class Admin
    {       
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }

        public ICollection<AdminGroup> AdminGroups { get; set; }
    }

    public class botSetting
    {
        public  string SettingId { get; set; }
        public bool LockGap { get; set; }
        public bool LockVoice { get; set; }
        public bool LockVideo { get; set; }
        public bool LockSticker { get; set; }
        public string Group_Id { get; set; }   
        public Group Group { get; set; }
    }

    public class LimitedPeople
    {
        public string Id { get; set; }
        public bool Mute { get; set; }
        public DateTime? stratDate { get; set; }
        public DateTime? enddate { get; set; }
        public string Group_Id { get; set; }
        public Group Group { get; set; }
    }

}

using Entities;
using SkillBridge.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Managers
{
    public class TeamManager : Singleton<TeamManager>, IDisposable
    {
        public BindableProperty<NTeamInfo> Team = new BindableProperty<NTeamInfo>();
        public void UpdateTeam(NTeamInfo nTeam)
        {

            Team.Value = nTeam;

        }

        public void Dispose()
        {

        }
    }
}

using Common;
using GameServer.Entities;
using GameServer.Models;
using Network;
using SkillBridge.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Managers
{
    class TeamManager : Singleton<TeamManager>
    {
        private int TeamId = 0;

        Dictionary<int, Team> Teams = new Dictionary<int, Team>();

        public void AddTeamMember(Character sender, Character target)
        {
            if (sender.team == null)
            {
                sender.team = CreateTeam(sender);
            }
            sender.team.AddMember(target);
        }


        //public void RemoveTeamMember()

        public Team CreateTeam(Character character)
        {

            foreach (var team in Teams.Values)
            {
                if (team.Count == 0)
                {
                    team.AddMember(character);
                    return team;
                }
            }
            Team _team = new Team(character);
            _team.ID = TeamId++;
            Teams.Add(_team.ID, _team);
            return _team;

        }

    }
}

using Common;
using GameServer.Entities;
using GameServer.Models;
using GameServer.Services;
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


        public void RemoveTeamMember(Character target)
        {
            Team team = target.team;
            team.RemoveMember(target);
            SendUpdate(team);
        }
        public void SetLeader(Character target)
        {
            Team team = target.team;
            team.SetLeader(target);
            SendUpdate(team);
        }

        private void SendUpdate(Team team)
        {
            foreach (var character in team.GetMember())
            {
                MessageService.Instance.SendUpdate(character.Connection);
            }
        }
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

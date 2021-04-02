using Managers;
using Network;
using SkillBridge.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Services
{
    class TeamService : Singleton<TeamService>, IDisposable
    {
        public EventWraperV1<TeamInviteRequest> OnTeamInvite = new EventWraperV1<TeamInviteRequest>();
        public TeamService()
        {


            MessageDistributer.Instance.Subscribe<TeamInviteRequest>(this.OnTeamInviteRequest);
            MessageDistributer.Instance.Subscribe<TeamInviteResponse>(this.OnTeamInviteResponse);
            MessageDistributer.Instance.Subscribe<TeamLeaderResponse>(this.OnTeamLeaderResponse);
            MessageDistributer.Instance.Subscribe<TeamLeaveResponse>(this.OnTeamLeaveResponse);
            MessageDistributer.Instance.Subscribe<TeamInfoResponse>(this.OnTeamInfoResponse);

        }

        public void Dispose()
        {
            MessageDistributer.Instance.Unsubscribe<TeamInviteRequest>(this.OnTeamInviteRequest);
            MessageDistributer.Instance.Unsubscribe<TeamInviteResponse>(this.OnTeamInviteResponse);
            MessageDistributer.Instance.Unsubscribe<TeamLeaderResponse>(this.OnTeamLeaderResponse);
            MessageDistributer.Instance.Unsubscribe<TeamLeaveResponse>(this.OnTeamLeaveResponse);
            MessageDistributer.Instance.Unsubscribe<TeamInfoResponse>(this.OnTeamInfoResponse);
        }
        private void OnTeamInviteRequest(object sender, TeamInviteRequest message)
        {
            OnTeamInvite.Invoke(message);
        }


        public void SendTeamInviteRequest(int targetID)
        {

            Debug.Log($"SendTeamInvite {targetID}");
            NetMessage message = new NetMessage()
            {
                Request = new NetMessageRequest()
                {
                    teamInviteRequest = new TeamInviteRequest()
                    {
                        ToId = targetID
                    }
                }
            };
            NetClient.Instance.SendMessage(message);
        }




        private void OnTeamInviteResponse(object sender, TeamInviteResponse message)
        {

            if (message.Result == Result.Failed)
            {
                LuaBehaviour.Instance.SafeDoString($"UIManager:ShowDaleyTip('{message.Errormsg}')");
            }
            else
            {
                LuaBehaviour.Instance.SafeDoString($"UIManager:ShowDaleyTip('组队成功')");
            }
           
        }


        public void SendTeamInviteResponse(bool accept,TeamInviteRequest request)
        {
            NetMessage message = new NetMessage()
            {
                Request = new NetMessageRequest()
                {
                   teamInviteResponse= new TeamInviteResponse()
                   {
                       Request = request,
                       Result = accept?Result.Success:Result.Failed
                   }
                }
            };

            NetClient.Instance.SendMessage(message);
        }



        private void OnTeamLeaderResponse(object sender, TeamLeaderResponse message)
        {
            if (message.Result == Result.Failed)
            {
                LuaBehaviour.Instance.SafeDoString($"UIManager:ShowDaleyTip('{message.Errormsg}')");
            }
        }
        private void OnTeamLeaveResponse(object sender, TeamLeaveResponse message)
        {
            if (message.Result == Result.Failed)
            {
                LuaBehaviour.Instance.SafeDoString($"UIManager:ShowDaleyTip('{message.Errormsg}')");
            }
        }



        public void SendTeamLeaveRequest(int id)
        {
            NetMessage message = new NetMessage()
            {

                Request = new NetMessageRequest()
                {
                    teamLeave = new TeamLeaveRequest()
                    {
                        characterId = id
                    }
                }
            };
            NetClient.Instance.SendMessage(message);

        }

        public void SendTeamLeaderRequest( int id)
        {
            NetMessage message = new NetMessage()
            {

                Request = new NetMessageRequest()
                {
                    teamLeader = new TeamLeaderRequest()
                    {                      
                        characterId = id
                    }
                }
            };
            NetClient.Instance.SendMessage(message);

        }




        private void OnTeamInfoResponse(object sender, TeamInfoResponse message)
        {
            TeamManager.Instance.UpdateTeam(message.Team);
        }









    }
}

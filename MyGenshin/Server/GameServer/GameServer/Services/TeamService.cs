using Common;
using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillBridge.Message;
using GameServer.Managers;
using GameServer.Models;
namespace GameServer.Services
{
    class TeamService : Singleton<TeamService>, IDisposable
    {

        public TeamService()
        {

            MessageDistributer<NetConnection<NetSession>>.Instance.Subscribe<TeamInviteRequest>(this.OnTeamInviteRequest);
            MessageDistributer<NetConnection<NetSession>>.Instance.Subscribe<TeamInviteResponse>(this.OnTeamInviteResponse);
            MessageDistributer<NetConnection<NetSession>>.Instance.Subscribe<TeamInfoRequest>(this.OnTeamInfo);
            MessageDistributer<NetConnection<NetSession>>.Instance.Subscribe<TeamLeaveRequest>(this.OnTeamLeave);
            MessageDistributer<NetConnection<NetSession>>.Instance.Subscribe<TeamLeaderRequest>(this.OnTeamLeader);
        }

        public override void Init()
        {
            TeamManager.Instance.Init();
        }

        public void Dispose()
        {

            MessageDistributer<NetConnection<NetSession>>.Instance.Unsubscribe<TeamInviteRequest>(this.OnTeamInviteRequest);
            MessageDistributer<NetConnection<NetSession>>.Instance.Unsubscribe<TeamInviteResponse>(this.OnTeamInviteResponse);
            MessageDistributer<NetConnection<NetSession>>.Instance.Unsubscribe<TeamInfoRequest>(this.OnTeamInfo);
            MessageDistributer<NetConnection<NetSession>>.Instance.Unsubscribe<TeamLeaveRequest>(this.OnTeamLeave);
            MessageDistributer<NetConnection<NetSession>>.Instance.Unsubscribe<TeamLeaderRequest>(this.OnTeamLeader);


        }


        private void OnTeamLeader(NetConnection<NetSession> sender, TeamLeaderRequest message)
        {
            throw new NotImplementedException();
        }

        private void OnTeamLeave(NetConnection<NetSession> sender, TeamLeaveRequest message)
        {
            throw new NotImplementedException();
        }

        private void OnTeamInfo(NetConnection<NetSession> sender, TeamInfoRequest message)
        {
            throw new NotImplementedException();
        }

        private void OnTeamInviteRequest(NetConnection<NetSession> sender, TeamInviteRequest message)
        {
            Log.Info($"OnTeamInviteRequest ID:{message.ToId}");
            NetConnection<NetSession> target;
            if (!SessionManager.Instance.GetSession(message.ToId, out target))
            {
                sender.Session.Response.teamInviteResponse = new TeamInviteResponse()
                {
                    Errormsg = "玩家不存在或不在线",
                    Result = Result.Failed
                };
                sender.SendResponse();
                return;
            }
            message.FromId = sender.Session.Character.Id;
            message.FromName = sender.Session.Character.Info.Name;
            target.Session.Response.teamInviteRequest = message;
            target.SendResponse();
        }

        private void OnTeamInviteResponse(NetConnection<NetSession> sender, TeamInviteResponse message)
        {
            NetConnection<NetSession> target;
            if (SessionManager.Instance.GetSession(message.Request.FromId, out target))
            {
                if (message.Result == Result.Failed)
                {

                    target.Session.Response.teamInviteResponse = new TeamInviteResponse()
                    {
                        Errormsg = "对方拒绝了你的申请",
                        Result = Result.Failed,

                    };
                    target.SendResponse();
                }
                else
                {
                    sender.Session.Response.teamInviteResponse = new TeamInviteResponse();
                    target.Session.Response.teamInviteResponse = new TeamInviteResponse();
                    try
                    {
                        TeamManager.Instance.AddTeamMember(target.Session.Character, sender.Session.Character);
                        sender.Session.Response.teamInviteResponse.Result = Result.Success;                       
                        target.Session.Response.teamInviteResponse.Result = Result.Success;
                    }
                    catch(FullTeamException e)
                    {
                        sender.Session.Response.teamInviteResponse.Errormsg = e.Message;
                        sender.Session.Response.teamInviteResponse.Result = Result.Failed;
                        target.Session.Response.teamInviteResponse.Errormsg = e.Message;
                        target.Session.Response.teamInviteResponse.Result = Result.Failed;
                    }
                    sender.SendResponse();
                    target.SendResponse();

                }
            }
            else
            {
                if (message.Result == Result.Success)
                {
                    sender.Session.Response.teamInviteResponse = new TeamInviteResponse()
                    {
                        Errormsg = "对方已下线",
                        Result = Result.Failed
                    };
                }
            }
        }



    }
}



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
            sender.Session.Response.teamLeader = new TeamLeaderResponse();
            Log.Info($"OnTeamLequestRequest ID:{message.characterId}");
            try
            {   //如果没有队伍
                if (sender.Session.Character.team == null)
                {
                    throw new NoTeamException();
                }
                //如果不是队长
                if (sender.Session.Character.team.Leader != sender.Session.Character)
                {
                    throw new NotLeaderException();
                }
                //
                if (sender.Session.Character.Id == message.characterId)
                {
                    throw new AlreadyLeaderException();

                }
                //如果没有这个玩家
                NetConnection<NetSession> session;
                if (!SessionManager.Instance.GetSession(message.characterId, out session))
                {
                    throw new NoSessionException();
                }
                //如果队里没有这个人
                if (!sender.Session.Character.team.HasMember(session.Session.Character))
                {
                    throw new NoMemberException();
                }

                TeamManager.Instance.SetLeader(session.Session.Character);
               
            }

            catch (Exception e)
            {
                sender.Session.Response.teamLeader.Result = Result.Failed;
                sender.Session.Response.teamLeader.Errormsg = e.Message;
            }
            sender.SendResponse();
        }

        private void OnTeamLeave(NetConnection<NetSession> sender, TeamLeaveRequest message)
        {

            sender.Session.Response.teamLeave = new TeamLeaveResponse();
            Log.Info($"OnTeamLequestRequest ID:{message.characterId}");
            try
            {   //如果没有队伍
                if (sender.Session.Character.team == null)
                {
                    throw new NoTeamException();
                }
                //如果是自己退队
                if (sender.Session.Character.Id == message.characterId)
                {

                    TeamManager.Instance.RemoveTeamMember(sender.Session.Character);                   
                }
                //如果是队长踢人
                else
                {
                    //如果不是队长
                    if(sender.Session.Character.team.Leader!= sender.Session.Character)
                    {
                        throw new NotLeaderException();
                    }
                    //如果没有这个玩家
                    NetConnection<NetSession> session;
                    if (!SessionManager.Instance.GetSession(message.characterId,out session))
                    {
                        throw new NoSessionException();
                    }
                    //如果队里没有这个人
                    if (!sender.Session.Character.team.HasMember(session.Session.Character))
                    {
                        throw new NoMemberException();
                    }
                    TeamManager.Instance.RemoveTeamMember(session.Session.Character);
                    MessageService.Instance.SendUpdate(session);
                }

            }

            catch (Exception e)
            {
                sender.Session.Response.teamLeave.Result = Result.Failed;
                sender.Session.Response.teamLeave.Errormsg = e.Message;
            }
            sender.SendResponse();
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
                    catch (FullTeamException e)
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



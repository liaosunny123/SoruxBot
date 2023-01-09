﻿using System.Text.Json.Nodes;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Register;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.PluginsModels;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.Basic;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.FutureTest.QQ;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace EpicMo.Example.ChatPlugins.Controller;

public class PrivateMessageController : BotController
{
    private ILoggerService _loggerService;
    private IBasicAPI _bot;
    private ILongMessageCommunicate _longMessageCommunicate;
    public PrivateMessageController(ILoggerService loggerService,IBasicAPI bot,ILongMessageCommunicate longMessageCommunicate)
    {
        this._loggerService = loggerService;
        this._bot = bot;
        this._longMessageCommunicate = longMessageCommunicate;
        _loggerService.Info("ExamplePlugins", "ExamplePlugins has been enable private message controller module\n");
    }
    
    [Event(EventType.SoloMessage)]
    [Command(CommandAttribute.Prefix.None,"echoa")]
    [CoolDown(10,CoolDownAttribute.CoolDownLevel.SinglePerson)]
    public PluginFucFlag Echo(MessageContext context)
    {
        _loggerService.Info("ExamplePlugins","无参数的插件方法被调用了！");
        _bot.SendPrivateMessage(context,QqMessageExtension.QqCreatePoke(context.TriggerId));
        _bot.QqSendPrivateMessageCompute(context, context.TriggerId, null, "hello", false);
        //_bot.SendPrivateMessage(context,"你好, " + context.GetSenderNick() + " !你发送的消息是：" + context.Message.GetRawMessage());
        return PluginFucFlag.MsgFlag;
    }

    [Event(EventType.SoloMessage)]
    [Command(CommandAttribute.Prefix.None,"echoa")]
    [CoolDown(10,CoolDownAttribute.CoolDownLevel.SinglePerson)]
    public PluginFucFlag Echoa(MessageContext context)
    {
        _bot.QqSendPrivateMessageCompute(context, context.TriggerId, null, "hello -> 这是一个重复Route", false);
        //_bot.SendPrivateMessage(context,"你好, " + context.GetSenderNick() + " !你发送的消息是：" + context.Message.GetRawMessage());
        return PluginFucFlag.MsgFlag;
    }
    
    [Event(EventType.SoloMessage)]
    [Command(CommandAttribute.Prefix.None,"echob [msg] <optional>")]
    [PlatformConstraint("qq")]
    public PluginFucFlag Echo(MessageContext context,string msg,int? optional)
    {
        _loggerService.Info("ExamplePlugins","可选参数类型的方法接受到了一条消息：" + msg + (optional == null ? "null":optional));
        //_bot.SendPrivateMessage(context,"你好！你想要发送的消息是：" + m
        //
        //sg 
         //                                               + (optional == null ? " 且没有携带额外信息":" 且携带了一条额外信息:" + optional));
        return PluginFucFlag.MsgFlag;
    }
    
    [Event(EventType.SoloMessage)]
    [Command(CommandAttribute.Prefix.None, "echoc [msg]")]
    [PlatformConstraint("qq")]
    public PluginFucFlag Echo(MessageContext context,string msg)
    {
        _loggerService.Info("ExamplePlugins","接收到一个消息：" + msg);
        _bot.SendPrivateMessage(context,"你好！你发送了这样的一个消息：" + msg);
        string callbackmsg = _bot.SendPrivateMessageAsync(context, "你好！").Result;
        _bot.SendPrivateMessage(context, "我接受到了：" + callbackmsg);
        //_bot.SendPrivateMessage(context,"你好，" + context.GetSenderNick() + "!你想要发送：" + msg);
        return PluginFucFlag.MsgFlag;
    }

    [Event(EventType.SoloMessage)]
    [Command(CommandAttribute.Prefix.Single,"echoPrivileged [msg]")]
    [Permission("solomsg.echoprivilege")]
    [BeforeMethod]
    [AfterMethod]
    [PlatformConstraint("qq","friend-privatemessage")]
    //在框架内存储的节点为 “epicmo.example.chatplugins.solomsg.echoprivilege”
    //也就是会自动加上前缀
    public PluginFucFlag EchoPrivilege(MessageContext context,string msg)
    {
        //_bot.SendPrivateMessage(context,"你好，你发送的消息是" + msg);
        //_bot.SendPrivateMessage(context,"在你发送这个消息的时候，你具有的权限是\"epicmo.example.chatplugins.solomsg.echoprivilege\"");
        return PluginFucFlag.MsgFlag;
        
    }

    [Event(EventType.GroupMessage)]
    [Command(CommandAttribute.Prefix.Single,"say [msg]")]
    public PluginFucFlag GroupMessageType(MessageContext context, string msg)
    {
        _bot.SendGroupMessage(context,"你想让我说：" + msg + "对吗？");
        return PluginFucFlag.MsgIntercepted;
    }
    
    
    [Event(EventType.SoloMessage)]
    [Command(CommandAttribute.Prefix.Single,"longCommunicate")]
    public PluginFucFlag LongCommunicate(MessageContext context)
    {
        //_bot.SendPrivateMessage(context,"你好，现在开始长对话！你输入的每一句话会进行重复，直到你说了\"停止\"");
        //bool loop = true;
        //while (loop)
        //{
        //    var msg = _longMessageCommunicate.ReadNextMessage();
        //    if (msg.Message.GetRawMessage().Equals("停止"))
        //    {
        //        loop = false;
        //        _bot.SendPrivateMessage(context,"你结束了对话！");
        //    }
        //    else
        //    {
        //        _bot.SendPrivateMessage(context,msg.Message.GetRawMessage());
        //    }
        //}
        return PluginFucFlag.MsgIntercepted;
    }
    
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Args;
using ChatMangementApi.Models.Anwsers;
using ChatMangementApi.Models.Context;
using ChatMangementApi.Models.Entities;
using Telegram.Bot.Types;
using Microsoft.EntityFrameworkCore;

namespace ChatMangementApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BotPannel : ControllerBase
    {
        private readonly ITelegramBotClient _client;
        private readonly BotDbContext _context;

        public BotPannel(ITelegramBotClient client, BotDbContext context)
        {
            _client = client;
            _context = context;

        }



        public string Start() => "ddddd";


        public async Task<OkResult> GetUpdates([FromBody] Update updates)
        {
            if (updates.Type == UpdateType.Unknown)
                return Ok();
            else if (updates.Type == UpdateType.Message)
            {
                if (updates.Message.Chat.Type == ChatType.Private)
                {
                    if (updates.Type == UpdateType.Message)
                    {

                    }
                }
                else if (updates.Message.Chat.Type == ChatType.Supergroup || updates.Message.Chat.Type == ChatType.Group)
                {
                    if (updates.Message.Text == "فعال شدن")
                    {
                        var creator = await _client.GetChatMemberAsync(updates.Message.Chat.Id, updates.Message.From.Id);

                        if (creator.Status == ChatMemberStatus.Creator)
                        {
                            if (_context.Groups.Any(c => c.Id == updates.Message.Chat.Id.ToString()))
                            {
                                await _client.SendTextMessageAsync(updates.Message.Chat.Id, ErrorAnswerModel.GroupAddedbefor);
                            }
                            else
                            {
                                try
                                {
                                    _context.Groups.Add(new Group
                                    {
                                        Id = updates.Message.Chat.Id.ToString(),
                                        BotSetting = new botSetting { LockGap = false, LockSticker = false, LockVideo = false, LockVoice = false, SettingId = Guid.NewGuid().ToString() },
                                        GroupAdded = true,
                                        GroupName = updates.Message.Chat.FirstName + updates.Message.Chat.LastName,
                                        MemberCount = await _client.GetChatMembersCountAsync(updates.Message.Chat.Id),
                                    });

                                    _context.Admins.Add(new Admin
                                    {
                                        Id = updates.Message.From.Id.ToString(),
                                        Name = updates.Message.From.FirstName + updates.Message.From.LastName,
                                        UserName = updates.Message.From.Username ?? default
                                    });

                                    _context.AdminGroups.Add(new AdminGroup { AdminId = updates.Message.From.Id.ToString(), GroupId = updates.Message.Chat.Id.ToString() });

                                    _context.SaveChanges();

                                    await _client.SendTextMessageAsync(updates.Message.Chat.Id, TextAnswerModel.GapAdded(updates.Message.Chat.Id, updates.Message.From.Id));
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(ex.Message);
                                }
                            }
                        }

                        return Ok();
                    }

                    if (await _context.AdminGroups.AnyAsync(c => c.GroupId == updates.Message.Chat.Id.ToString() && c.AdminId == updates.Message.From.Id.ToString()))
                    {
                        if (updates.Message.Text.ToLower() == "راهنما")
                            await _client.SendTextMessageAsync(updates.Message.Chat.Id, TextAnswerModel.ChatHelpMessage);
                        else if(updates.Message.Text == "تنظیمات")
                        {
                            var setting = await _context.BotSettings.FirstOrDefaultAsync(c => c.Group_Id == updates.Message.Chat.Id.ToString());

                            string message = default;
                            if (setting.LockGap)
                                message += "قفل گروه : فعال \n ";
                            else
                                message += "قفل گروه : غیرفعال \n ";
                            if (setting.LockSticker)
                                message += "قفل استیکر : فعال \n ";
                            else
                                message += "قفل استیکر : غیرفعال \n ";
                            if (setting.LockVideo)
                                message += "قفل ویدیو : فعال \n ";
                            else
                                message += "قفل ویدیو : غیرفعال \n ";
                            if (setting.LockVoice)
                                message += "قفل ویس : فعال \n ";
                            else
                                message += "قفل ویس : غیرفعال \n ";

                            await _client.SendTextMessageAsync(updates.Message.Chat.Id, message);

                        }
                        else if (updates.Message.Text.ToLower() == "ارتقا")
                        {
                            if (updates.Message.ReplyToMessage != null)
                            {
                                try
                                {
                                    var replpayedperson = updates.Message.ReplyToMessage.From;
                                    _context.Admins.Add(new Admin
                                    {
                                        Id = replpayedperson.Id.ToString(),
                                        Name = replpayedperson.FirstName + " " + replpayedperson.LastName,
                                        UserName = replpayedperson.Username
                                    });

                                    _context.AdminGroups.Add(new AdminGroup
                                    {
                                        AdminId = replpayedperson.Id.ToString(),
                                        GroupId = updates.Message.Chat.Id.ToString()
                                    });

                                    await _context.SaveChangesAsync();

                                    await _client.SendTextMessageAsync(updates.Message.Chat.Id, TextAnswerModel.AdminAdded(replpayedperson.Id, updates.Message.Chat.Id));
                                    return Ok();

                                }
                                catch
                                {
                                    throw new Exception("Error While Adding entity");
                                }
                            }
                        }
                        else if (updates.Message.Text.ToLower() == "حذف")
                        {
                            if (updates.Message.ReplyToMessage != null)
                            {
                                try
                                {
                                    await _client.KickChatMemberAsync(updates.Message.Chat.Id, updates.Message.ReplyToMessage.From.Id);
                                    await _client.SendTextMessageAsync(updates.Message.Chat.Id, $"user With {updates.Message.ReplyToMessage.From.Id} Has been Removed");

                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(ex.Message);
                                }

                                return Ok();
                            }
                        }
                        else if (updates.Message.Text.ToLower() == "سکوت")
                        {
                            if (updates.Message.ReplyToMessage != null)
                            {
                                _context.LimitedPeoples.Add(new LimitedPeople
                                {
                                    Id = updates.Message.ReplyToMessage.From.Id.ToString(),
                                    enddate = null,
                                    stratDate = DateTime.Now,
                                    Group_Id = updates.Message.Chat.Id.ToString(),
                                    Mute =true,
                                });

                                await _context.SaveChangesAsync();
                                await _client.SendTextMessageAsync(updates.Message.Chat.Id, $"کاربر با شناسه {updates.Message.ReplyToMessage.From.Id} در لیست سکوت قرار گرفت");
                            }
                        }
                        else if (updates.Message.Text.ToLower() == "لغوسکوت")
                        {
                            try
                            {
                                var user = await _context.LimitedPeoples.FindAsync(updates.Message.ReplyToMessage.From.Id.ToString());

                                if (user != null)
                                {
                                    user.Mute = false;
                                    _context.Update(user);
                                    await _context.SaveChangesAsync();
                                    //  await _client.RestrictChatMemberAsync(updates.Message.Chat.Id,Convert.ToInt32(user.Id), new ChatPermissions { CanAddWebPagePreviews = false, CanChangeInfo = false, CanInviteUsers = false, CanPinMessages = false, CanSendMediaMessages = false, CanSendMessages = false, CanSendOtherMessages = false, CanSendPolls = false });
                                    await _client.SendTextMessageAsync(updates.Message.Chat.Id, $"کاربر با شناسه : {user.Id} از قفل خارج شد ");

                                }
                            }
                            catch
                            {

                                throw;
                            }
                        }
                        else if (updates.Message.Text.ToLower() == "قفل گروه")
                        {
                            var gapsetting = await _context.BotSettings.SingleOrDefaultAsync(c => c.Group_Id == updates.Message.Chat.Id.ToString());

                            if (gapsetting != null)
                                gapsetting.LockGap = true;

                            await _context.SaveChangesAsync();

                            await _client.SendTextMessageAsync(updates.Message.Chat.Id, "گروه قفل شد");
                        }
                        else if (updates.Message.Text.ToLower() == "لغو قفل")
                        {
                            var gapsetting = await _context.BotSettings.SingleOrDefaultAsync(c => c.Group_Id == updates.Message.Chat.Id.ToString());

                            if (gapsetting != null)
                                gapsetting.LockGap = false;

                            await _context.SaveChangesAsync();

                            await _client.SendTextMessageAsync(updates.Message.Chat.Id, "گروه از قفل خارج شد");
                        }
                        return Ok();
                    }
                    else
                    {
                        var setting = await _context.BotSettings.FirstOrDefaultAsync(c => c.Group_Id == updates.Message.Chat.Id.ToString());

                        if (setting != null)
                        {
                            if (setting.LockGap)
                            {
                                await _client.DeleteMessageAsync(updates.Message.Chat.Id.ToString(), updates.Message.MessageId);
                                return Ok();
                            }
                            else if (setting.LockVideo && updates.Message.Type == MessageType.Video)
                            {
                                await _client.DeleteMessageAsync(updates.Message.Chat.Id.ToString(), updates.Message.MessageId);
                                return Ok();
                            }
                            else if (setting.LockSticker && updates.Message.Type == MessageType.Sticker)
                            {
                                await _client.DeleteMessageAsync(updates.Message.Chat.Id.ToString(), updates.Message.MessageId);
                                return Ok();
                            }
                            else if (setting.LockVideo && updates.Message.Type == MessageType.Voice)
                            {
                                await _client.DeleteMessageAsync(updates.Message.Chat.Id.ToString(), updates.Message.MessageId);
                                return Ok();
                            }
                            else
                            {
                                var user = await _context.LimitedPeoples.FirstOrDefaultAsync(c => c.Group_Id == updates.Message.Chat.Id.ToString());

                                if (user != null && user.Mute)
                                    await _client.DeleteMessageAsync(updates.Message.Chat.Id,updates.Message.MessageId);
                            }

                        }
                    }


                    return Ok();
                }
            }
            return Ok();
        }


    }
}

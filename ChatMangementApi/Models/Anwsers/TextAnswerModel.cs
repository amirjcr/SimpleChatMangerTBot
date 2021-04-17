using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatMangementApi.Models.Anwsers
{
    public static class TextAnswerModel
    {
        public static string StartMessgae { get; } = "سلام به ربات مدیریت گرو خوش امدین \n با وارد کردن دستور /hlep  میتوانید راهنمایی ربات را تمشاشا کنید";

        public static string ChatHelpMessage { get; } = "تنظیمات --> مشاهده تنظیمات ربات \n حذف --> حذف کاربر \n سکوت-->سکوت کردن کاربر  \n ارتقا--> ارتقای کاربر به مدیرربات  \n لغوسکوت-->خارج کردن کاربر از سکوت \n  لغو قفل --> خارج کردن گروه از قفل \n قفل گروه --> قفل گروه \n راهنما -->راهنمایی استفاده از بات";

        //for groups that will be add
        public static string GapAdded(long chatid, int userid) => $"گروه شما با شناسه : {chatid} \n با مدیریت : {userid} افزوده شد";
        public static string AdminAdded(int userid, long chatid) => $"کاربر با شماسه : {userid}  به لیست مدیران گروه با شناسه  {chatid}";
   
    }
}

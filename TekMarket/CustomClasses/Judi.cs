using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Entities;
using TekMarket.Models;

namespace TekMarket.CustomClasses
{
    public class Judi
    {
        private OscovaBot bot;
        private DataBaseUtility dbutility;
        public Judi()
        {
            bot = new OscovaBot();
            bot.Dialogs.Add(new AppDialog());

            dbutility = new DataBaseUtility();
            dbutility.init();
            bot.MainUser.Context.SharedData.Add(dbutility);
            bot.CreateRecognizer("message", new[] { "this is a message" });
            bot.CreateRecognizer("price", new[] { "price" });
            bot.CreateRecognizer("action", new[] { "show", "display", "show me, give me" });
            bot.CreateRecognizer("qteQuestions", new[] { "do you have", "have you", "how many" });
            bot.CreateRecognizer("greeting", new[] { "hello", "hi", "good morning", "good afternoon", "good evening" });
            String[] st = dbutility.categoryDescriptions();
            String[] pl = dbutility.productLib();
            bot.CreateRecognizer("category", st);
            bot.CreateRecognizer("libelle", pl);

            bot.Trainer.StartTraining();
        }

        public String answer(String message)
        {
            EvaluationResult ev = bot.Evaluate(message);
            ev.Invoke();
            Response resp = bot.MainUser.Responses.First<Response>();
            String ans = resp.Text;
            return ans;

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;

namespace TekMarket.CustomClasses
{
    public class AppDialog : Dialog
    {
        public AppDialog()
        {

        }

        [Expression(@"[A-Za-z\s.,!?]+@message @greeting")]
        public void sayHello(Context context, Result result)
        {
            result.SendResponse("Hello sir!");
        }

        /*[Expression (@"[A-Za-z\s.,!?]+@message how are you?")]
        public void sayFine(Context context, Result result)
        {
            result.SendResponse("I'm fine, thanks!");
        }*/

        [Expression(@"[A-Za-z\s.,!?]+@message @libelle price")]
        public void getPrice(Context context, Result result)
        {
            var lib = result.Entities.OfType("libelle").Value;
            var utility = context.SharedData.OfType<DataBaseUtility>();
            String res = utility.PriceByName(lib);
            Response response = new Response();
            response.Text = res;
            response.Format = ResponseFormat.Html;
            result.SendResponse(response);
        }


        [Expression(@"[A-Za-z\s.,!?]+@message @qteQuestions @libelle")]
        public void getQte(Context context, Result result)
        {
            var lib = result.Entities.OfType("libelle").Value;
            var utility = context.SharedData.OfType<DataBaseUtility>();
            String res = utility.QuantityByName(lib);
            Response response = new Response();
            response.Text = res;
            response.Format = ResponseFormat.Html;
            result.SendResponse(response);
        }
        
        [Expression(@"[A-Za-z\s.,!?]+@message @action @category")]
        public void getResults(Context context, Result result)
        {
            var property = result.Entities.OfType("category").Value;
            var utility = context.SharedData.OfType<DataBaseUtility>();
            String res = utility.ProductByCategory(property);
            Response response = new Response();
            response.Text = res;
            response.Format = ResponseFormat.Html;
            result.SendResponse(response);
        }

        [Expression(@"[A-Za-z\s.,!?]+@message @action products")]
        public void getListProduct(Context context, Result result)
        {
            var utility = context.SharedData.OfType<DataBaseUtility>();
            String res = utility.ProductList();
            Response response = new Response();
            response.Text = res;
            response.Format = ResponseFormat.Html;
            result.SendResponse(response);
        }

        [Expression(@"[A-Za-z\s.,!?]+ colors @libelle ")]
        [Entity("property")]
        public void getColors(Context context, Result result)
        {
            var name = result.Entities.OfType("libelle").Value;
            var utility = context.SharedData.OfType<DataBaseUtility>();
            String res = utility.ColorListByName(name);
            Response response = new Response();
            response.Text = res;
            response.Format = ResponseFormat.Html;
            result.SendResponse(response);
        }

        [Expression(@"[A-Za-z\s.,!?]+@message @action @libelle description")]
        public void getDescription(Context context, Result result)
        {
            var name = result.Entities.OfType("libelle").Value;
            var utility = context.SharedData.OfType<DataBaseUtility>();
            String res = utility.DescriptionByName(name);
            Response response = new Response();
            response.Text = res;
            response.Format = ResponseFormat.Html;
            result.SendResponse(response);
        }

        [Expression(@"[A-Za-z\s.,!?]+@message what is the most sold product")]
        public void getInfolist(Context context, Result result)
        {
            var utility = context.SharedData.OfType<DataBaseUtility>();
            String res = utility.MostWanted();
            Response response = new Response();
            response.Text = res;
            response.Format = ResponseFormat.Html;
            result.SendResponse(response);
        }
    }
}
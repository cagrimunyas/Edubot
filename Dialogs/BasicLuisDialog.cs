using System;
using System.Configuration;
using System.Threading.Tasks;


using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;


namespace Microsoft.Bot.Sample.LuisBot
{
    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [Serializable]
    public class BasicLuisDialog : LuisDialog<object>
    {
        public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(
            ConfigurationManager.AppSettings["LuisAppId"], 
            ConfigurationManager.AppSettings["LuisAPIKey"], 
            domain: ConfigurationManager.AppSettings["LuisAPIHostName"])))
        {
        }

        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"I'm afraid I didn't understand that. Write 'Help' for the question you can ask me");
        }

        // Go to https://luis.ai and create a new intent, then train/publish your luis app.
        // Finally replace "Gretting" with the name of your newly created intent in the following handler
        [LuisIntent("Greeting")]
        public async Task GreetingIntent(IDialogContext context, LuisResult result)
        {
            if (result.Entities.Count > 0) { 
                await context.PostAsync($"Hello  {result.Entities[0].Entity}. I am AC Elementary School bot. How can I help?"); 
            } else {
                await context.PostAsync($"Hello. I am AC Elementary School bot. How can I help?");
            }

        }
        [LuisIntent("Help")]
        public async Task HelpIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"I can answer your questions, about our campus, transportation, fees etc., . If you want more information please go: www.microsoft.com ");
        }

        [LuisIntent("Campus")]
        public async Task CampusIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Our campus has 2 buildings. One with classes and the other for facilities. We offer wide range of activities to our students. We also have cafetarias, libraries etc., in second building. We have open basketball,football and tenis courts. For more information, please go to our website www.microsoft.com ");
        }

        [LuisIntent("School Location")]
        public async Task LocationIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"The campus is located just outside of city away from all the noise and chaos. We offer easily accesible green campus to our students.   ");
        }
        
        [LuisIntent("transportation")]
        public async Task TransportationIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"The ride to campus took 20 minutes with a bus and 15 minutes with the subway from city centre. We also have school transportation option. For pricing of our school transportation check our website.");
        }
        
        [LuisIntent("fees")]
        public async Task FeesIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"We do offer scholarships to successfull students and students who can't afford to full price. To check your eligibility and give you a full campus tour, we would like to invite you to our campus.");
        }
        
        [LuisIntent("meetingRequest")]
        public async Task MeetingRequestIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Let us know when you're free so that we can save a possible slot for you :) ");
        }
        

        [LuisIntent("meetingSet")]
        public async Task MeetingSetIntent(IDialogContext context, LuisResult result)
        {
            
            if (result.Entities.Count > 0) { 
                await context.PostAsync($"Alright,{result.Entities[0].Entity} sounds good.That's also free for us. I am now adding a meeting to your calendar ");
                await context.PostAsync($"I saved the date for you. Would you like anyting else?");
            } else {
                await context.PostAsync($"Alright,{result.Entities[0].Entity} sounds good.That's also free for us. I am now adding a meeting to your calendar");
            }


           
        }



        private async Task ShowLuisResult(IDialogContext context, LuisResult result) 
        {
            await context.PostAsync($"You have reached {result.Intents[0].Intent}. You said: {result.Query}");
            context.Wait(MessageReceived);
        }
    }
}


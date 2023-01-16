using System;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.AspNet.Core;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;
using Twilio.TwiML.Voice;

namespace Dotnet6TwilioVoice.Controllers
{
    public class CallingController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CallingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Get: ReceiveCall
        public async Task<IActionResult> ReceiveCall()
        {
            var twiml = new VoiceResponse();
            var response = new TwiMLResult(twiml.Say("You are calling Marcos Placona").Dial(_configuration["Twilio:Number"]));
            return response;
        }

        // POST: MakeCall
        [HttpGet]
        public async Task<IActionResult> MakeCall()
        {
            // Instantiate new Rest API object

            TwilioClient.Init(_configuration["Twilio:AccountSid"], _configuration["Twilio:AuthToken"]);
            var call = CallResource.Create(
                twiml: new Twilio.Types.Twiml("<Response><Say>Ahoy, World!</Say></Response>"),
                to: new Twilio.Types.PhoneNumber(_configuration["Twilio:Number"]),
                from: new Twilio.Types.PhoneNumber("")
            );

            return Content("The call has been is initiated");
        }
    }
}


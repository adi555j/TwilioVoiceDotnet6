using Dotnet6TwilioVoice.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.AspNet.Core;
using Twilio.Rest.Api.V2010.Account;


namespace Dotnet6TwilioVoiceTests
{

    public class CallingControllerTests
    {
        private CallingController _controller;
        private IConfiguration _configuration;


        public CallingControllerTests()
        {
            // Create a mock configuration object and set Twilio:Number value
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                        { "Twilio:Number", "+1234567890" },
                        { "Twilio:AccountSid", "ACXXXXXXXXXXXXXXXXXXX" },
                        { "Twilio:AuthToken", "test_token" }
                })
                .Build();

            // Instantiate the controller with the mock configuration
            _controller = new CallingController(_configuration);
        }

        [Fact]
        public async void ReceiveCall_ReturnsTwiMLResult()
        {
            // Act
            var result = await _controller.ReceiveCall();

            // Assert
            result.Should().BeOfType<TwiMLResult>();
        }

        [Fact]
        public async void MakeCall_ReturnsContentResult()
        {
            // Arrange
            var mockCall = new Mock<CallResource>();

            // Act
            var result = await _controller.MakeCall();

            // Assert
            result.Should().BeOfType<ContentResult>()
                .Which.Content.Should().Be("The call has been is initiated");
        }
    }

}

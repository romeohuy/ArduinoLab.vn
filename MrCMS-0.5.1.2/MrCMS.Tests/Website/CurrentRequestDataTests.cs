﻿using System;
using System.Collections;
using System.Linq;
using System.Web;
using FluentAssertions;
using MrCMS.Entities.People;
using MrCMS.Settings;
using MrCMS.Website;
using Xunit;

namespace MrCMS.Tests.Website
{
    public class CurrentRequestDataTests : MrCMSTest
    {
        [Fact]
        public void CurrentRequestData_UserGuid_ReturnsTheUserGuidIfTheUserIsLoggedIn()
        {
            var currentUser = new User();
            CurrentRequestData.CurrentUser = currentUser;

            var userGuid = CurrentRequestData.UserGuid;

            userGuid.Should().Be(currentUser.Guid);
        }

        [Fact]
        public void CurrentRequestData_UserGuid_ReturnsTheSessionGuidIfTheUserIsNotLoggedIn()
        {
            CurrentRequestData.CurrentUser = null;
            var newGuid = Guid.NewGuid();
            CurrentRequestData.CurrentContext.Response.Cookies.Add(new HttpCookie("current.usersessionGuid",
                                                                                  newGuid.ToString()));

            var userGuid = CurrentRequestData.UserGuid;

            userGuid.Should().Be(newGuid);
        }

        [Fact]
        public void CurrentRequestData_UserGuid_IfNotSetBeforeHandShouldGenerateANewGuid()
        {
            CurrentRequestData.CurrentUser = null;

            var userGuid = CurrentRequestData.UserGuid;

            userGuid.Should().NotBeEmpty();
        }

        [Fact]
        public void CurrentRequestData_Now_ShouldBeUTCOffsetByTimeZone()
        {
            CurrentRequestData.SiteSettings = new SiteSettings { TimeZone = TimeZoneInfo.GetSystemTimeZones().First().Id };

            var time = DateTime.UtcNow.AddHours(-12);
            CurrentRequestData.Now.Should().BeAfter(time.AddSeconds(-1)).And.BeBefore(time.AddSeconds(1));
        }

        [Fact]
        public void CurrentRequestData_TimeZoneInfo_ShouldBeLocalTimeZoneIfNotSet()
        {
            CurrentRequestData.SiteSettings = new SiteSettings();

            CurrentRequestData.TimeZoneInfo.Should().Be(TimeZoneInfo.Local);
        }
    }

    public class FakeHttpSessionState : HttpSessionStateBase
    {
        public FakeHttpSessionState()
        {
            _store = new SortedList();
        }

        public override object this[string name]
        {
            get
            {
                return _store[name];
            }
            set
            {
                _store[name] = value;
            }
        }
        private readonly SortedList _store;
    }
}
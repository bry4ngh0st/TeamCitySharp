using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using NUnit.Framework;
using System.Collections.Generic;
using TeamCitySharp.DomainEntities;

namespace TeamCitySharp.IntegrationTests
{
    [TestFixture]
    public class when_interacting_to_get_change_information
    {
        private ITeamCityClient _client;

        [SetUp]
        public void SetUp()
        {
            _client = new TeamCityClient("limapedev41:8080");
            _client.Connect("admin", "123");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void it_returns_exception_when_no_host_specified()
        {
            var client = new TeamCityClient(null);

            //Assert: Exception
        }

        [Test]
        [ExpectedException(typeof(WebException))]
        public void it_returns_exception_when_host_does_not_exist()
        {
            var client = new TeamCityClient("test:81");
            client.Connect("admin", "123");

            var changes = client.Changes.All();

            //Assert: Exception
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void it_returns_exception_when_no_connection_made()
        {
            var client = new TeamCityClient("teamcity.codebetter.com");

            var changes = client.Changes.All();

            //Assert: Exception
        }

        [Test]
        public void it_returns_all_changes()
        {
            List<Change> changes = _client.Changes.All();

            Assert.That(changes.Any(), "Cannot find any changes recorded in any of the projects");
        }

        [TestCase("34")]
        public void it_returns_change_details_by_change_id(string changeId)
        {
            Change changeDetails = _client.Changes.ByChangeId(changeId);

            Assert.That(changeDetails != null, "Cannot find details of that specified change");
        }

        [TestCase("DirecTv_Releases_20151000")]
        public void it_returns_change_details_for_build_config(string buildConfigId)
        {
            Change changeDetails = _client.Changes.LastChangeDetailByBuildConfigId(buildConfigId);

            Assert.That(changeDetails != null, "Cannot find details of that specified change");
        }

        // TODO: Fix issues
        [TestCase("DirecTv_Dev_20151000", "15")]
        public void it_returns_change_list_for_build_config_and_build_number(string buildConfigId, string buildNumber)
        {
            List<Change> changeList = _client.Changes.ByBuildConfigIdAndBuildNumber(buildConfigId, buildNumber);

            // Assert.That(changeList != null, "Cannot find details of that specified change");
        }

        [TestCase("151")]
        public void it_returns_change_list_for_build_id(string buildId)
        {
            var changeList = _client.Changes.ByBuildId(buildId);

            foreach (var change in changeList)
            {
                Trace.WriteLine(string.Format("{0} - {1} - {2} files - {3} - {4}", change.Comment, change.Username, change.Files.File.Count, change.Version, change.Date));
            }

            Assert.That(changeList != null, "Cannot find details of that specified change");
        }
    }
}
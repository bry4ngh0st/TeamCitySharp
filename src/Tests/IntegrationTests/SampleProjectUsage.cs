using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NUnit.Framework;
using TeamCitySharp.DomainEntities;
using TeamCitySharp.Locators;

namespace TeamCitySharp.IntegrationTests
{
    [TestFixture]
    public class when_interacting_to_get_project_details
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
        public void it_throws_exception_when_not_passing_url()
        {
            var client = new TeamCityClient(null);

            //Assert: Exception
        }

        [Test]
        [ExpectedException(typeof(WebException))]
        public void it_throws_exception_when_host_does_not_exist()
        {
            var client = new TeamCityClient("test:81");
            client.Connect("admin", "qwerty");

            var allProjects = client.Projects.All();

            //Assert: Exception
        }


        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void it_throws_exception_when_no_connection_formed()
        {
            var client = new TeamCityClient("teamcity.codebetter.com");

            var projects = client.Projects.All();

            //Assert: Exception
        }

        [Test]
        public void it_returns_all_projects()
        {
            List<Project> projects = _client.Projects.All();

            Assert.That(projects.Any(), "No projects were found for this server");
        }

        [TestCase("project137")]
        public void it_returns_project_details_when_passing_a_project_id(string projectId)
        {
            Project projectDetails = _client.Projects.ById(projectId);

            Assert.That(projectDetails != null, "No details found for that specific project");
        }

        [TestCase("YouTrackSharp")]
        public void it_returns_project_details_when_passing_a_project_name(string projectName)
        {
            Project projectDetails = _client.Projects.ByName(projectName);

            Assert.That(projectDetails != null, "No details found for that specific project");
        }

        [Test]
        public void it_returns_project_details_when_passing_project()
        {
            var project = new Project { Id = "project137" };
            Project projectDetails = _client.Projects.Details(project);

            Assert.That(!string.IsNullOrWhiteSpace(projectDetails.Id));
        }


        [Test]
        public void it_returns_project_details_when_creating_project()
        {
            var client = new TeamCityClient("localhost:81");
            client.Connect("admin", "qwerty");
            var projectName = Guid.NewGuid().ToString("N");
            var project = client.Projects.Create(projectName);

            Assert.That(project, Is.Not.Null);
            Assert.That(project.Name, Is.EqualTo(projectName));
        }

        [Test]
        public void CreateProjectTest()
        {
            //_client.Projects.Create("DirecTv");
            /*_client.Projects.Create("DEV", "DirecTv");
            _client.Projects.Create("RELEASES", "DirecTv");*/
            _client.Projects.Create("Dev_2015.10.00", "DirecTv_Dev");
            _client.Projects.Create("Releases_2015.10.00", "DirecTv_Releases");
        }

        [Test]
        public void BuildConfigurationTest()
        {
            var projectId = "OS_GetOrder_Dev";
            var buildConfig = _client.BuildConfigs.CreateConfigurationByProjectId(projectId, "Second");
        }
    }
}

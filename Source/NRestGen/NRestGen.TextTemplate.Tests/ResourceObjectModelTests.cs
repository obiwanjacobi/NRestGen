using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NRestGen.TextTemplate.Tests
{
    [TestClass]
    [DeploymentItem(ResourceModelYaml)]
    public class ResourceObjectModelTests
    {
        public const string ResourceModelYaml = "ResourceObjectModelTests.TestData.yaml";

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Deserialize()
        {
            var path = Path.Combine(TestContext.DeploymentDirectory, ResourceModelYaml);
            var rescObj = ResourceObjectModel.FromFile(path);

            rescObj.Should().NotBeNull();
            rescObj.Entities.Should().HaveCount(2);
            rescObj.Relations.Should().HaveCount(1);

            rescObj.Settings.Odata.Should().NotBeNull();
            rescObj.Settings.Odata.Max.Should().Be(100);
            rescObj.Settings.Odata.Queryable.Should().BeTrue();

            rescObj.Settings.Mediatr.RegisterAssembly.Should().BeTrue();

            rescObj.Settings.Api.BaseUrl.Should().Be("api");
            rescObj.Settings.Api.Version.Should().Be("1");

            rescObj.Settings.Project.Controllers.Should().Be("Controllers");
            rescObj.Settings.Project.ResourceModel.Should().Be("ResourceModel");
        }

        [TestMethod]
        public void Serialize()
        {
            var model = new ResourceObjectModel
            {
                Settings = new GenSettings
                {
                    Mediatr = new GenSettings.MediatrSettings
                    {
                        RegisterAssembly = true
                    },
                    Odata = new GenSettings.ODataSettings
                    {
                        Count = true,
                        Expand = 1,
                        Max = 100,
                        Queryable = true,
                        Select = true
                    }
                },
                Entities = new List<ResourceEntity>
                {
                    new ResourceEntity
                    {
                        Name = "Entity1",
                        SetName = "Entity1s",
                        Properties = new List<ResourceEntityProperty>
                        {
                            new ResourceEntityProperty
                            {
                                Name = "Property1a",
                                Type = "System.Int32"
                            },
                            new ResourceEntityProperty
                            {
                                Name = "Property2a",
                                Type = "System.String"
                            }
                        }
                    },
                    new ResourceEntity
                    {
                        Name = "Entity2",
                        SetName = "Entity2s",
                        Properties = new List<ResourceEntityProperty>
                        {
                            new ResourceEntityProperty
                            {
                                Name = "Property1b",
                                Type = "System.Int32"
                            },
                            new ResourceEntityProperty
                            {
                                Name = "Property2b",
                                Type = "System.String"
                            }
                        }
                    }
                }
            };

            var yaml = model.ToString();
            yaml.Should().NotBeNullOrWhiteSpace();

            Console.WriteLine(yaml);
        }
    }
}

using System.Collections.Generic;
using System.IO;
using SharpYaml.Serialization;

namespace NRestGen.TextTemplate
{
    public class ResourceObjectModel
    {
        private readonly static Serializer Serializer = new Serializer(new SerializerSettings
        {
            NamingConvention = new SharpYaml.Serialization.FlatNamingConvention()
        });

        public GenSettings Settings { get; set; }

        public List<ResourceEntity> Entities { get; set; }

        [YamlIgnore]
        public Dictionary<string, List<ResourceEntity>> Relations { get; set; }

        public override string ToString()
        {
            return Serializer.Serialize(this);
        }

        public static ResourceObjectModel FromFile(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            {
                var model = Serializer.Deserialize<ResourceObjectModel>(stream);
                return ResourceObjectModelExtensions.BuildRelations(model);
            }
        }
    }

    public class ResourceEntity
    {
        public string Name { get; set; }
        public string SetName { get; set; }
        public List<ResourceEntityProperty> Properties { get; set; }
    }

    public class ResourceEntityProperty
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}

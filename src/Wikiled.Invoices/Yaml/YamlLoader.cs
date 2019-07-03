using System;
using System.Collections.Generic;
using System.IO;
using Wikiled.Common.Resources;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Wikiled.Invoices.Yaml
{
    public class YamlLoader : IYamlLoader
    {
        public IEnumerable<YamlDefinition> Load()
        {
            var assembly = GetType().Assembly;
            var files = assembly.GetManifestResourceNames();
            foreach (var file in files)
            {
                using (var stream = assembly.GetEmbeddedFile(file))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var deserializer = new DeserializerBuilder()
                                           .WithNamingConvention(new CamelCaseNamingConvention())
                                           .Build();
                        var yaml = new YamlStream();
                        yaml.Load(reader);
                        var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
                        mapping.
                    }
                }
            }
        }
    }
}

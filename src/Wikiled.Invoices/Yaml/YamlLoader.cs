using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using Wikiled.Invoices.Helpers;
using Wikiled.Invoices.Yaml.Data;
using YamlDotNet.Serialization;

namespace Wikiled.Invoices.Yaml
{
    public class YamlLoader : IYamlLoader
    {
        private readonly ILogger<YamlLoader> logger;

        public YamlLoader(ILogger<YamlLoader> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<InvoiceTemplate> Load()
        {
            var assembly = GetType().Assembly;
            var files = assembly.GetManifestResourceNames();
            foreach (var file in files)
            {
                using (var stream = assembly.GetManifestResourceStream(file))
                {
                    if (stream == null)
                    {
                        logger.LogError("Stream {0} is null", file);
                        continue;
                    }

                    using (var reader = new StreamReader(stream))
                    {
                        var deserializer = new DeserializerBuilder().Build();
                        var yamlObject = deserializer.Deserialize(reader);

                        var serializer = new SerializerBuilder()
                            .JsonCompatible()
                            .Build();

                        var json = serializer.Serialize(yamlObject);
                        var result = JsonConvert.DeserializeObject<InvoiceTemplate>(json);
                        if (result.Validate())
                        {
                            if (result.Options.DateFormats != null)
                            {
                                for (var i = 0; i < result.Options.DateFormats.Length; i++)
                                {
                                    var format = result.Options.DateFormats[i];
                                    result.Options.DateFormats[i] = PythonDateStyleConverter.Convert(format);
                                }
                            }

                            yield return result;
                        }
                        else
                        {
                            logger.LogError("Failed to construct object: {0}", json);
                        }
                    }
                }
            }
        }
    }
}

using ESLearn.Domain.AggregatesModel.PostsAggregate;
using Nest;

namespace ESLearn.Repository.Mappings
{
    public class PostTypeDescriptor: IConfigureIndexDescriptor<Post>, ITypeMappingDescriptor<Post>
    {
        public IndexSettingsDescriptor ConfigureIndex(IndexSettingsDescriptor indexDescriptor)
        {
            return indexDescriptor.Analysis(a => a
                .TokenFilters(tf => tf.Synonym("city_synonym", tfd => tfd.Synonyms("lol => laughing", "new york, nyc")))
                .Analyzers(aa =>
                        aa.Custom("cna", ca => ca
                            .CharFilters("html_strip")
                            .Tokenizer("standard")
                            .Filters("lowercase","stop", "city_synonym"))
            )
                );
        }

        public string IndexName => nameof(Post).ToLower();
        public PropertiesDescriptor<Post> ConfigureMappings(PropertiesDescriptor<Post> descriptor)
        {
            return descriptor.Text(t => t.Name(n => n.Title).Analyzer("cna"));
        }
    }
}
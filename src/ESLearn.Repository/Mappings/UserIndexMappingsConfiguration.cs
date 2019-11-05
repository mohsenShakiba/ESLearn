using ESLearn.Domain.AggregatesModel.UsersAggregate;
using Nest;

namespace ESLearn.Repository.Mappings
{
    public class UserIndexMappingsConfiguration: IIndexMappingsConfiguration<User>
    {
        public string IndexName => "users";

        public PropertiesDescriptor<User> ConfigureMapping(PropertiesDescriptor<User> descriptor)
        {
            return descriptor
                .Text(s => s.Name(l => l.UserName))
                .Text(s => s.Name(l => l.Password))
                .Text(s => s.Name(l => l.BioDescription))
                .Number(s => s.Name(l => l.Roles))
                .Number(s => s.Name(u => u.JoinDate).Type(NumberType.Double));
        }
        
    }
}
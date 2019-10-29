using ESLearn.Domain.AggregatesModel.UsersAggregate;
using Nest;

namespace ESLearn.Repository.Mappings
{
    public class UserTypeMappingDescriptor: ITypeMappingDescriptor<User>
    {
        public string IndexName => "users";

        public PropertiesDescriptor<User> ConfigureMappings(PropertiesDescriptor<User> descriptor)
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
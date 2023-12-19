using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfigurations
{
    internal sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(userId => userId.id, id => new UserId(id));
        }
    }
    internal sealed class UserEventEntityConfiguration : IEntityTypeConfiguration<UserDomainEvent>
    {
        public void Configure(EntityTypeBuilder<UserDomainEvent> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(userDomainId => userDomainId.id, id => new UserDomainId(id));
            builder.Property(x => x.StreamId).HasConversion(streamId => streamId.id, streamId => new UserId(streamId));
        }
    }
}

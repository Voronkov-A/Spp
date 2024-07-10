using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Spp.IdentityProvider.Persistence.Authorization.Configurations;

public class ApplicationConfiguration : IEntityTypeConfiguration<Domain.Applications.Application>
{
    public void Configure(EntityTypeBuilder<Domain.Applications.Application> builder)
    {
        var valueComparer = new ValueComparer<IReadOnlyCollection<Uri>>(
            (left, right) => left == null || right == null ? left == right : left.SequenceEqual(right),
            x => x.GetHashCode(),
            x => x);
        var valueConverter = new ValueConverter<IReadOnlyCollection<Uri>, Uri[]>(
            domain => domain.ToArray(),
            persistence => persistence.ToList());

        builder.HasKey(x => x.Id);
        builder.Property(x => x.ClientId).IsRequired();
        builder.Property(x => x.ClientSecretHash).IsRequired();
        builder.Property(x => x.RedirectUris)
            .IsRequired()
            .HasColumnType("text[]")
            .HasConversion(valueConverter)
            .Metadata
            .SetValueComparer(valueComparer);

        builder.HasIndex(x => x.ClientId).IsUnique();
    }
}

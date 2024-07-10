using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Spp.Common.Miscellaneous;

namespace Spp.Common.Postgres.EntityFramework.Converters;

public class StringValueConverter<T>() :
    ValueConverter<T, string>(domain => domain.ToString()!, persistence => StringValueFactory<T>.Create(persistence))
    where T : struct;

using System;

namespace Spp.Common.OpenApiGenerator.Core.Exceptions;

public class ReferencedObjectNotFoundException(string message) : Exception(message);

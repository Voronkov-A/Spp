using Parlot.Fluent;

namespace Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Tags;

public class ForeachTag
{
    public void Foo()
    {
        /*var foreachTag = Parsers. .OneOf(
            )*/

                    /*var ForTag = OneOf(
                            Identifier
                            .AndSkip(Terms.Text("in"))
                            .And(Member)
                            .And(ZeroOrMany(OneOf( // Use * since each can appear in any order. Validation is done once it's parsed
                                Terms.Text("reversed").Then(x => new ForModifier { IsReversed = true }),
                                Terms.Text("limit").SkipAnd(Colon).SkipAnd(Primary).Then(x => new ForModifier { IsLimit = true, Value = x }),
                                Terms.Text("offset").SkipAnd(Colon).SkipAnd(Primary).Then(x => new ForModifier { IsOffset = true, Value = x })
                                )))
                            .AndSkip(TagEnd)
                            .And(AnyTagsList)
                            .And(ZeroOrOne(
                                CreateTag("else").SkipAnd(AnyTagsList))
                                .Then(x => x != null ? new ElseStatement(x) : null))
                            .AndSkip(CreateTag("endfor").ElseError($"'{{% endfor %}}' was expected"))
                            .Then<Statement>(x =>
                            {
                                var identifier = x.Item1;
                                var member = x.Item2;
                                var statements = x.Item4;
                                var elseStatement = x.Item5;
                                var (limitResult, offsetResult, reversed) = ReadForStatementConfiguration(x.Item3);
                                return new ForStatement(statements, identifier, member, limitResult, offsetResult, reversed, elseStatement);
                            }),

                            Identifier
                            .AndSkip(Terms.Text("in"))
                            .And(Range)
                            .And(ZeroOrMany(OneOf( // Use * since each can appear in any order. Validation is done once it's parsed
                                Terms.Text("reversed").Then(x => new ForModifier { IsReversed = true }),
                                Terms.Text("limit").SkipAnd(Colon).SkipAnd(Primary).Then(x => new ForModifier { IsLimit = true, Value = x }),
                                Terms.Text("offset").SkipAnd(Colon).SkipAnd(Primary).Then(x => new ForModifier { IsOffset = true, Value = x })
                                )))
                            .AndSkip(TagEnd)
                            .And(AnyTagsList)
                            .AndSkip(CreateTag("endfor").ElseError($"'{{% endfor %}}' was expected"))
                            .Then<Statement>(x =>
                            {
                                var identifier = x.Item1;
                                var range = x.Item2;
                                var statements = x.Item4;
                                var (limitResult, offsetResult, reversed) = ReadForStatementConfiguration(x.Item3);
                                return new ForStatement(statements, identifier, range, limitResult, offsetResult, reversed, null);

                            })
                        ).ElseError("Invalid 'for' tag");*/
    }
}

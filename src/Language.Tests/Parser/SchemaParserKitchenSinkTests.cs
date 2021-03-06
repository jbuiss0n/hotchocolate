using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace HotChocolate.Language
{
    public class SchemaParserKitchenSinkTests
    {
        [Fact]
        public void ParseFacebookKitchenSinkSchema()
        {
            // arrange
            string schemaSource = FileResource.Open("schema-kitchen-sink.graphql");

            // act
            Parser parser = new Parser();
            DocumentNode document = parser.Parse(
                schemaSource, new ParserOptions(noLocations: true));

            // assert
            Assert.Equal(Snapshot.Current(), Snapshot.New(document));
        }

        [Fact]
        public void ParseFacebookKitchenSinkQuery()
        {
            // arrange
            string querySource = FileResource.Open("kitchen-sink.graphql");

            // act
            Parser parser = new Parser();
            DocumentNode document = parser.Parse(
                querySource, new ParserOptions(
                    noLocations: true, allowFragmentVariables: true));

            // assert
            Assert.Equal(Snapshot.Current(), Snapshot.New(document));
        }
    }
}

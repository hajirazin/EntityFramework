// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using Microsoft.Data.Entity.Utilities;
using Microsoft.Data.Migrations.Model;
using Moq;
using Xunit;

namespace Microsoft.Data.Migrations.Tests.Model
{
    public class AddForeignKeyOperationTest
    {
        [Fact]
        public void Create_and_initialize_operation()
        {
            var addForeignKeyOperation = new AddForeignKeyOperation(
                "MyFK", "dbo.MyTable", "dbo.MyTable2",
                new[] { "Foo", "Bar" }, new[] { "Foo2", "Bar2" },
                cascadeDelete: true);

            Assert.Equal("MyFK", addForeignKeyOperation.ForeignKeyName);
            Assert.Equal("dbo.MyTable", addForeignKeyOperation.TableName);
            Assert.Equal("dbo.MyTable2", addForeignKeyOperation.ReferencedTableName);
            Assert.Equal(new[] { "Foo", "Bar" }, addForeignKeyOperation.ColumnNames);
            Assert.Equal(new[] { "Foo2", "Bar2" }, addForeignKeyOperation.ReferencedColumnNames);
            Assert.True(addForeignKeyOperation.CascadeDelete);
            Assert.False(addForeignKeyOperation.IsDestructiveChange);
        }

        [Fact]
        public void Dispatches_visitor()
        {
            var addForeignKeyOperation = new AddForeignKeyOperation(
                "MyFK", "dbo.MyTable", "dbo.MyTable2",
                new[] { "Foo", "Bar" }, new[] { "Foo2", "Bar2" },
                cascadeDelete: true);
            var mockVisitor = new Mock<MigrationOperationSqlGenerator>();
            var builder = new Mock<IndentedStringBuilder>();
            addForeignKeyOperation.GenerateSql(mockVisitor.Object, builder.Object, false);

            mockVisitor.Verify(g => g.Generate(addForeignKeyOperation, builder.Object, false), Times.Once());
        }
    }
}
using FluentMigrator;

namespace CaseManagement.DataAccess.Migrations
{
    [Migration(202412110938)]
    public class CreateInitialSchema : Migration
    {
        public override void Up()
        {
            Create.Table("LookupConstant")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Code").AsString(50).NotNullable()
                .WithColumn("Text").AsString(255).NotNullable()
                .WithColumn("Type").AsString(50).NotNullable();

            Create.Table("Person")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable().Unique()
                .WithColumn("Contact").AsInt64().NotNullable()
                .WithColumn("DateOfBirth").AsDateTime().NotNullable()
                .WithColumn("Gender").AsString(50).NotNullable();

            Create.Table("User")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("UserName").AsString(255).NotNullable().Unique()
                .WithColumn("PasswordHash").AsString(255).NotNullable()
                .WithColumn("PasswordSalt").AsString(255).NotNullable()
                .WithColumn("RoleId").AsInt32().NotNullable().ForeignKey("LookupConstant", "Id")
                .WithColumn("PersonId").AsGuid().NotNullable().ForeignKey("Person", "Id");
        }

        public override void Down()
        {
            Delete.Table("User");
            Delete.Table("Person");
            Delete.Table("LookupConstant");
        }
    }
}
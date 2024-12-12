using FluentMigrator;

namespace CaseManagement.DataAccess.Migrations
{
    [Migration(202412120946)]
    public class CreateOtpMigration : Migration
    {
        public override void Up()
        {
            Create.Table("OTP")
                .WithColumn("Id").AsString(26).NotNullable().PrimaryKey()
                .WithColumn("OtpHash").AsString(255).NotNullable()
                .WithColumn("IsVerified").AsBoolean().NotNullable()
                .WithColumn("RequestedBy").AsString(26).NotNullable().ForeignKey("Person", "Id")
                .WithColumn("GeneratedAt").AsDateTime().NotNullable()
                .WithColumn("ExpiresAt").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("OTP");
        }
    }
}

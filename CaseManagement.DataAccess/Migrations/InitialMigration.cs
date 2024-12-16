using FluentMigrator;

namespace CaseManagement.DataAccess.Migrations
{
    [Migration(202412120845)]
    public class CreateInitialSchemaAndSeedLookupConstants : Migration
    {
        public override void Up()
        {
            // Create LookupConstant table
            Create.Table("LookupConstant")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Code").AsString(50).NotNullable()
                .WithColumn("Text").AsString(255).NotNullable()
                .WithColumn("Type").AsString(50).NotNullable();

            // Create Person table
            Create.Table("Person")
                .WithColumn("Id").AsString(26).PrimaryKey() // ULID as string (26 characters)
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable().Unique()
                .WithColumn("Contact").AsInt64().NotNullable()
                .WithColumn("DateOfBirth").AsDateTime().NotNullable()
                .WithColumn("Gender").AsString(50).NotNullable();

            // Create User table
            Create.Table("User")
                .WithColumn("Id").AsString(26).PrimaryKey() // ULID as string (26 characters)
                .WithColumn("UserName").AsString(255).NotNullable().Unique()
                .WithColumn("PasswordHash").AsString(255).NotNullable()
                .WithColumn("PasswordSalt").AsString(255).NotNullable()
                .WithColumn("RoleId").AsInt32().NotNullable().ForeignKey("LookupConstant", "Id") //general default
                .WithColumn("PersonId").AsString(26).NotNullable().ForeignKey("Person", "Id"); // ForeignKey as ULID of person table

            // Seed data into LookupConstant table
            Insert.IntoTable("LookupConstant")
                .Row(new { Code = "GEN", Text = "General", Type = "User Role" })
                .Row(new { Code = "ADM", Text = "Admin", Type = "User Role" })
                .Row(new { Code = "JUG", Text = "Judge", Type = "User Role" })
                .Row(new { Code = "ADV", Text = "Advocate", Type = "User Role" })
                .Row(new { Code = "JDO", Text = "Judicial Officer", Type = "User Role" })
                .Row(new { Code = "OPN", Text = "Open", Type = "Case Status" })
                .Row(new { Code = "IPR", Text = "In Progress", Type = "Case Status" })
                .Row(new { Code = "IPE", Text = "In Pending", Type = "Case Status" })
                .Row(new { Code = "CLS", Text = "Closed", Type = "Case Status" })
                .Row(new { Code = "REJ", Text = "Rejected", Type = "Case Status" })
                .Row(new { Code = "CVL", Text = "Civil", Type = "Case Type" })
                .Row(new { Code = "CRL", Text = "Criminal", Type = "Case Type" })
                .Row(new { Code = "FML", Text = "Family", Type = "Case Type" })
                .Row(new { Code = "CPR", Text = "Copyright", Type = "Case Type" })
                .Row(new { Code = "TRS", Text = "Trade Secret", Type = "Case Type" })
                .Row(new { Code = "TRC", Text = "Traffic", Type = "Case Type" })
                .Row(new { Code = "PHT", Text = "Photo", Type = "File Type" })
                .Row(new { Code = "IDP", Text = "ID Proof", Type = "File Type" })
                .Row(new { Code = "CSD", Text = "Case Document", Type = "File Type" })
                .Row(new { Code = "ACP", Text = "Accepted", Type = "Request Status" })
                .Row(new { Code = "RSR", Text = "Rejected", Type = "Request Status" })
                .Row(new { Code = "HLD", Text = "Hold", Type = "Request Status" })
                .Row(new { Code = "OSU", Text = "Signup", Type = "OTP Used For" })
                .Row(new { Code = "OLN", Text = "Login", Type = "OTP Used For" })
                .Row(new { Code = "OEV", Text = "Email Verification", Type = "OTP Used For" })
                .Row(new { Code = "ORP", Text = "Reset Password", Type = "OTP Used For" })
                .Row(new { Code = "ODV", Text = "Document View Verification", Type = "OTP Used For" })
                .Row(new { Code = "OTH", Text = "Other", Type="OTP Used For"});

        }

        public override void Down()
        {
            // Delete all tables
            Delete.Table("User");
            Delete.Table("Person");
            Delete.Table("LookupConstant");
        }
    }
}

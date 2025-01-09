using FluentMigrator;

namespace CaseManagement.DataAccess.Migrations
{
    [Migration(202501020950)]
    public class UpdateFileMigration : Migration
    {
        public override void Up()
        {
            Delete.ForeignKey("FK_File_UploadedBy_Person_Id").OnTable("File");

            Rename.Table("File").To("Files");

            Alter.Table("Files")
                .AlterColumn("UploadedBy").AsString(26).NotNullable()
                .ForeignKey("User", "Id");
        }
        public override void Down()
        {
            Delete.ForeignKey("FK_File_UploadedBy_User_Id").OnTable("Files");

            Rename.Table("Files").To("File");

            Alter.Table("File")
                .AlterColumn("UploadedBy").AsString(26).NotNullable()
                .ForeignKey("Person", "Id");
        }
    }
}

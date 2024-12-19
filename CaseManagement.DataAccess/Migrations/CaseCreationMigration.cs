using FluentMigrator;

namespace CaseManagement.DataAccess.Migrations
{
    [Migration(202412191120)]
    public class CaseCreationMigration : Migration
    {
        public override void Up()
        {
            Create.Table("Case")
                .WithColumn("Id").AsString(26).PrimaryKey()
                .WithColumn("CaseTypeId").AsInt32().NotNullable().ForeignKey("LookupConstant", "Id")
                .WithColumn("Description").AsString(1000).NotNullable()
                .WithColumn("CaseNumber").AsString(50).NotNullable().Unique()
                .WithColumn("AccusedId").AsString(26).NotNullable().ForeignKey("Person", "Id")
                .WithColumn("VictimId").AsString(26).NotNullable().ForeignKey("Person", "Id")
                .WithColumn("AdvocateId").AsString(26).Nullable().ForeignKey("Person", "Id")
                .WithColumn("CaseStatusId").AsInt32().NotNullable().ForeignKey("LookupConstant", "Id");

            Create.Table("File")
                .WithColumn("Id").AsString(26).PrimaryKey()
                .WithColumn("FileTypeId").AsInt32().NotNullable().ForeignKey("LookupConstant", "Id")
                .WithColumn("FileName").AsString(128).NotNullable()
                .WithColumn("FilePath").AsString(255).NotNullable()
                .WithColumn("UploadedBy").AsString(26).NotNullable().ForeignKey("Person", "Id");

            Create.Table("CaseFiles")
                .WithColumn("Id").AsString(26).PrimaryKey() 
                .WithColumn("CaseId").AsString(26).NotNullable().ForeignKey("Case", "Id") 
                .WithColumn("FileId").AsString(26).NotNullable().ForeignKey("File", "Id");



        }
        public override void Down() 
        {
            Delete.Table("Case");
            Delete.Table("File");
            Delete.Table("CaseFiles");
        }
    }

}

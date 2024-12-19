using FluentMigrator;

namespace CaseManagement.DataAccess.Migrations
{
    [Migration(202412191135)]
    public class ReqHearingsMigration : Migration
    {
        public override void Up()
        {
            //Create Request table
            Create.Table("Request")
                .WithColumn("Id").AsString(26).PrimaryKey() // ULID as string (26 characters)
                .WithColumn("RequestStatusId").AsInt32().NotNullable().ForeignKey("LookupConstant", "Id")
                .WithColumn("RequestTypeId").AsInt32().NotNullable().ForeignKey("LookupConstant", "Id")
                .WithColumn("Description").AsString().NotNullable()
                .WithColumn("FileId").AsString(26).NotNullable()
                //.ForeignKey("Files", "Id")
                .WithColumn("RaisedBy").AsString(26).NotNullable().ForeignKey("Person", "Id")
                .WithColumn("CaseId").AsString(26).NotNullable();
                //.ForeignKey("Case", "Id");

            //Create Schedule hearings table
            Create.Table("ScheduleHearings")
                .WithColumn("Id").AsString(26).PrimaryKey() // ULID as string (26 characters)
                .WithColumn("CaseId").AsString(26).NotNullable()
                //.ForeignKey("Case", "Id")
                .WithColumn("JudgeId").AsString(26).NotNullable().ForeignKey("User", "Id")
                .WithColumn("ScheduledAt").AsDateTime().NotNullable()
                .WithColumn("Judgement").AsString();       
        }

        public override void Down()
        {
            Delete.Table("Request");
            Delete.Table("ScheduleHearings");
        }

    }
}

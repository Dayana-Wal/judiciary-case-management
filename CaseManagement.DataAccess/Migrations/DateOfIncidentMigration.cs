using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.DataAccess.Migrations
{
    [Migration(202501231133)]
    public class DateOfIncidentMigration : Migration
    {
        public override void Down()
        {
            Delete.Column("DateOfIncident").FromTable("case");
        }

        public override void Up()
        {
            Alter.Table("case")
                 .AddColumn("DateOfIncident")
                 .AsDateTime()
                 .Nullable();

        }
    }
}

using FluentMigrator;

namespace noteapi.Migrations
{
    [Migration(202401130001)]
    public class InitialTables_202401130001: Migration
    {
        public override void Down()
        {
            Delete.Table("user");
            Delete.Table("note");
        }
        public override void Up()
        {
            Create.Table("user")
                .WithColumn("username").AsString(200).NotNullable().PrimaryKey()
                .WithColumn("password").AsString(100).NotNullable()
                .WithColumn("date_created").AsDateTime().NotNullable()
                .WithColumn("date_updated").AsDateTime().NotNullable();

            Create.Table("note")
                .WithColumn("title").AsString(200).NotNullable().PrimaryKey()
                .WithColumn("content").AsString(100).NotNullable()
                .WithColumn("date_created").AsDateTime().NotNullable()
                .WithColumn("date_updated").AsDateTime().NotNullable();
        }
    }
}
